using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Mail;
using DotNetNuke.Entities.Tabs;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 提醒邮件
    /// </summary>
    public class NotificationEmail
    {




        /// <summary>
        /// 邮件设置
        /// </summary>
        public Playngo_ClientZone_MailSetting MailSetting { get; set; }




        private Hashtable _Settings = new Hashtable();
        /// <summary>
        /// 模块设置
        /// </summary>
        public Hashtable Settings
        {
            get {
                if (!(_Settings != null && _Settings.Count > 0))
                {
                    ModuleController controller = new ModuleController();
                    _Settings = new Hashtable(controller.GetModuleSettings(MailSetting.ModuleId));
                }
                return _Settings;
            }

            set { _Settings = value; }
        }





        private List<Playngo_ClientZone_Jurisdiction> _AllJurisdictions = new List<Playngo_ClientZone_Jurisdiction>();
        /// <summary>
        /// 所有区域限制
        /// </summary>
        public List<Playngo_ClientZone_Jurisdiction> AllJurisdictions
        {
            get
            {
                return _AllJurisdictions;
            }

            set { _AllJurisdictions = value; }
        }





        /// <summary>
        /// 验证用户是否有权限接受邮件
        /// </summary>
        /// <param name="Per_AllUsers"></param>
        /// <param name="Per_Roles"></param>
        /// <param name="UserItem"></param>
        /// <returns></returns>
        public Boolean ValidUserRole(Int32 Per_AllUsers, String Per_Roles, UserInfo UserItem)
        {
            Boolean Valid = false;
            if (Per_AllUsers == 0)
            {
                Valid = true;
            }
            else
            {
                if (!String.IsNullOrEmpty(Per_Roles))
                {
                    var Roles = Common.GetList(Per_Roles);
                    if (Roles != null && Roles.Count > 0)
                    {
                        var UserRoles = Common.Split<String>(UserItem.Roles, 1, Int32.MaxValue);
                        if (UserRoles != null && UserRoles.Count > 0)
                        {
                            foreach (var UserRole in UserRoles)
                            {
                                Valid = Roles.Exists(r => r.ToLower() == UserRole.ToLower());
                                if (Valid) break;

                            }
                        }

                    }
                }
            }


            return Valid;
        }

        /// <summary>
        /// 验证用户区域是否符合
        /// </summary>
        /// <param name="Per_AllJurisdictions"></param>
        /// <param name="Per_Jurisdictions"></param>
        /// <param name="UserItem"></param>
        /// <returns></returns>
        public Boolean ValidUserJurisdictions(Int32 Per_AllJurisdictions, String Per_Jurisdictions, UserInfo UserItem)
        {
            Boolean Valid = false;
            if (Per_AllJurisdictions == 0)
            {
                Valid = true;
            }
            else
            {
                if (!String.IsNullOrEmpty(Per_Jurisdictions))
                {
                    var Jurisdictions = Common.GetList(Per_Jurisdictions);
                    if (Jurisdictions != null && Jurisdictions.Count > 0)
                    {
                        foreach (var Jurisdiction in Jurisdictions)
                        {
                            Int32 JurisdictionId = 0;
                            if (Int32.TryParse(Jurisdiction,out JurisdictionId))
                            {
                                //需要根据当前的区域去查找区域的关联角色
                                if(AllJurisdictions.Exists(r=>r.ID == JurisdictionId))
                                {
                                    var JurisdictionItem = AllJurisdictions.Find(r => r.ID == JurisdictionId);

                                    //根据关联的角色判断当前用户是否需要符合
                                    if (ValidUserRole(JurisdictionItem.Per_AllUsers, JurisdictionItem.Per_Roles, UserItem))
                                    {
                                        Valid = true;
                                        break;
                                    }

                                }
                            }


                        }

                    }
                }
            }
            return Valid;
        }





        #region "==订阅邮件:Playngo_ClientZone_Event"

        /// <summary>
        /// 发送邮件(多篇文章)-订阅列表中的角色+邮箱
        /// </summary>
        /// <param name="Articles"></param>
        /// <returns></returns>
        public Int32 SendMail(Playngo_ClientZone_Event DataEvent)
        {
            Int32 RecordCount = 0;

            Int32 UserRecordCount = 0;
            var UserList = Common.Split<UserInfo>(UserController.GetUsersByProfileProperty(DataEvent.PortalId, "Newsletter_Events", "True", 0, Int32.MaxValue, ref UserRecordCount), 1, Int32.MaxValue);
 
            if (UserList != null && UserList.Count > 0)
            {
                foreach (var UserItem in UserList)
                {
                    //验证邮件地址符合规则
                    if (Mail.IsValidEmailAddress(UserItem.Email, Null.NullInteger))
                    {
                        //验证用户是否有权限接受邮件
                        if (ValidUserRole(DataEvent.Per_AllUsers, DataEvent.Per_Roles, UserItem))
                        {
                            //验证用户区域
                            if (ValidUserJurisdictions(DataEvent.Per_AllJurisdictions, DataEvent.Per_Jurisdictions, UserItem))
                            {
                                //创建邮件
                                EmailInfo email = CreateMailHtml(DataEvent, UserItem);
                                //推送邮件
                                MailScheduler.AssignMessage(email);

                                RecordCount++;
                            }
                        }
                    }
                }
            }


            return RecordCount;
        }






        /// <summary>
        /// 创建邮件内容
        /// </summary>
        /// <param name="EmailTo"></param>
        /// <returns></returns>
        public EmailInfo CreateMailHtml(Playngo_ClientZone_Event DataItem, UserInfo UserItem)
        {
 
            //构造邮件信息
            EmailInfo emailItem = new EmailInfo();
            emailItem.Settings = Settings;
            emailItem.PushSettings();


            //事件作者
            UserInfo EventUser = UserController.GetUserById(DataItem.PortalId, DataItem.CreateUser);

            string Template_Subject = NetHelper.ReplaceNoCase(MailSetting.MailSubject, "[TITLE]", DataItem.Title);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[MAILSETTING_TIME]", MailSetting.MailTime.ToString());




            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_MAIL]", EventUser.Email);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_USERNAME]", EventUser.Username);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_DISPLAYNAME]", EventUser.DisplayName);





            emailItem.Subject = Template_Subject;

            #region "构造邮件信息"

            String Template_Content = NetHelper.ReplaceNoCase(MailSetting.MailBody, "[TITLE]", DataItem.Title);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CONTENTTEXT]", HttpUtility.HtmlDecode(DataItem.ContentText));
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CREATETIME]", DataItem.CreateTime.ToString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CREATEDATE]", DataItem.CreateTime.ToShortDateString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[MAILSETTING_TIME]", MailSetting.MailTime.ToString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[DATETIME_NOW]", xUserTime.LocalTime().ToString());


            //注册的信息丢进来
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_MAIL]", EventUser.Email);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_USERNAME]", EventUser.Username);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_DISPLAYNAME]", EventUser.DisplayName);

            //订阅用户的信息丢进来
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_MAIL]", UserItem.Email);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_USERNAME]", UserItem.Username);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_DISPLAYNAME]", UserItem.DisplayName);


            //图片替换
            if (Common.Contains(Template_Content, "[PICTURE]"))
            {
                Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[PICTURE]", FormatThumbnailUrl(DataItem));
            }

            //文章链接
            if (Common.Contains(Template_Content, "[LINK]"))
            {
                Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[LINK]", FormatItemUrl(DataItem));
            }

            emailItem.Content = Template_Content;
            #endregion




            emailItem.MailTo = MailSetting.MailTo;
            if (MailSetting.MailTo.ToUpper() == "[AUTHOR_MAIL]")//用户邮件需要发送给用户
            {
                emailItem.MailTo = EventUser.Email;
            }
            else if (MailSetting.MailTo.ToUpper() == "[USER_MAIL]")//用户邮件需要发送给用户
            {
                emailItem.MailTo = UserItem.Email;
            }
            emailItem.MailCC = MailSetting.MailCC;

            return emailItem;
        }
        #endregion

        #region "==订阅邮件:Playngo_ClientZone_DownloadFile"

        /// <summary>
        /// 发送邮件(多篇文章)-订阅列表中的角色+邮箱
        /// </summary>
        /// <param name="Articles"></param>
        /// <returns></returns>
        public Int32 SendMail(Playngo_ClientZone_DownloadFile DataItem)
        {
            Int32 RecordCount = 0;

            Int32 UserRecordCount = 0;
            var UserList = Common.Split<UserInfo>(UserController.GetUsersByProfileProperty(DataItem.PortalId, "Newsletter_Downloads", "True", 0, Int32.MaxValue, ref UserRecordCount), 1, Int32.MaxValue);
        
            if (UserList != null && UserList.Count > 0)
            {
                foreach (var UserItem in UserList)
                {
                    //验证邮件地址符合规则
                    if (Mail.IsValidEmailAddress(UserItem.Email, Null.NullInteger))
                    {
                        //验证用户是否有权限接受邮件
                        if (ValidUserRole(DataItem.Per_AllUsers, DataItem.Per_Roles, UserItem))
                        {
                            //验证用户区域
                            if (ValidUserJurisdictions(DataItem.Per_AllJurisdictions, DataItem.Per_Jurisdictions, UserItem))
                            {
                                //创建邮件
                                EmailInfo email = CreateMailHtml(DataItem, UserItem);
                                //推送邮件
                                MailScheduler.AssignMessage(email);

                                RecordCount++;
                            }
                        }
                    }
                }
            }


            return RecordCount;
        }






        /// <summary>
        /// 创建邮件内容
        /// </summary>
        /// <param name="EmailTo"></param>
        /// <returns></returns>
        public EmailInfo CreateMailHtml(Playngo_ClientZone_DownloadFile DataItem, UserInfo UserItem)
        {

            //构造邮件信息
            EmailInfo emailItem = new EmailInfo();
            emailItem.Settings = Settings;
            emailItem.PushSettings();


            //事件作者
            UserInfo EventUser = UserController.GetUserById(DataItem.PortalId, DataItem.CreateUser);

            string Template_Subject = NetHelper.ReplaceNoCase(MailSetting.MailSubject, "[TITLE]", DataItem.Title);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[MAILSETTING_TIME]", MailSetting.MailTime.ToString());




            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_MAIL]", EventUser.Email);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_USERNAME]", EventUser.Username);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_DISPLAYNAME]", EventUser.DisplayName);





            emailItem.Subject = Template_Subject;

            #region "构造邮件信息"

            String Template_Content = NetHelper.ReplaceNoCase(MailSetting.MailBody, "[TITLE]", DataItem.Title);
            //Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CONTENTTEXT]", HttpUtility.HtmlDecode(DataItem.ContentText));
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CREATETIME]", DataItem.CreateTime.ToString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CREATEDATE]", DataItem.CreateTime.ToShortDateString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[MAILSETTING_TIME]", MailSetting.MailTime.ToString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[DATETIME_NOW]", xUserTime.LocalTime().ToString());


            //注册的信息丢进来
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_MAIL]", EventUser.Email);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_USERNAME]", EventUser.Username);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_DISPLAYNAME]", EventUser.DisplayName);

            //订阅用户的信息丢进来
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_MAIL]", UserItem.Email);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_USERNAME]", UserItem.Username);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_DISPLAYNAME]", UserItem.DisplayName);


            //图片替换
            if (Common.Contains(Template_Content, "[PICTURE]"))
            {
                Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[PICTURE]", FormatThumbnailUrl(DataItem));
            }


            //文章链接
            if (Common.Contains(Template_Content, "[LINK]"))
            {
                Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[LINK]", FormatItemUrl(DataItem));
            }

            if (Common.Contains(Template_Content, "[FILETYPES]"))
            {
                
                Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[FILETYPES]", Playngo_ClientZone_FileType.ConvertFileTypes(DataItem.FileTypes));
            }



           

        emailItem.Content = Template_Content;
            #endregion




            emailItem.MailTo = MailSetting.MailTo;
            if (MailSetting.MailTo.ToUpper() == "[AUTHOR_MAIL]")//用户邮件需要发送给用户
            {
                emailItem.MailTo = EventUser.Email;
            }
            else if (MailSetting.MailTo.ToUpper() == "[USER_MAIL]")//用户邮件需要发送给用户
            {
                emailItem.MailTo = UserItem.Email;
            }
            emailItem.MailCC = MailSetting.MailCC;

            return emailItem;
        }
        #endregion

        #region "==订阅邮件:Playngo_ClientZone_Campaign"

        /// <summary>
        /// 发送邮件(多篇文章)-订阅列表中的角色+邮箱
        /// </summary>
        /// <param name="Articles"></param>
        /// <returns></returns>
        public Int32 SendMail(Playngo_ClientZone_Campaign DataItem)
        {
            Int32 RecordCount = 0;

            Int32 UserRecordCount = 0;
            var UserList = Common.Split<UserInfo>(UserController.GetUsersByProfileProperty(DataItem.PortalId, "Newsletter_Campaigns", "True", 0, Int32.MaxValue, ref UserRecordCount), 1, Int32.MaxValue);
   
            if (UserList != null && UserList.Count > 0)
            {
                foreach (var UserItem in UserList)
                {
                    //验证邮件地址符合规则
                    if (Mail.IsValidEmailAddress(UserItem.Email, Null.NullInteger))
                    {
                        //验证用户是否有权限接受邮件
                        if (ValidUserRole(DataItem.Per_AllUsers, DataItem.Per_Roles, UserItem))
                        {
                            //验证用户区域
                            if (ValidUserJurisdictions(DataItem.Per_AllJurisdictions, DataItem.Per_Jurisdictions, UserItem))
                            {
                                //创建邮件
                                EmailInfo email = CreateMailHtml(DataItem, UserItem);
                                //推送邮件
                                MailScheduler.AssignMessage(email);

                                RecordCount++;
                            }
                        }
                    }
                }
            }


            return RecordCount;
        }






        /// <summary>
        /// 创建邮件内容
        /// </summary>
        /// <param name="EmailTo"></param>
        /// <returns></returns>
        public EmailInfo CreateMailHtml(Playngo_ClientZone_Campaign DataItem, UserInfo UserItem)
        {

            //构造邮件信息
            EmailInfo emailItem = new EmailInfo();
            emailItem.Settings = Settings;
            emailItem.PushSettings();


            //事件作者
            UserInfo EventUser = UserController.GetUserById(DataItem.PortalId, DataItem.CreateUser);

            string Template_Subject = NetHelper.ReplaceNoCase(MailSetting.MailSubject, "[TITLE]", DataItem.Title);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[MAILSETTING_TIME]", MailSetting.MailTime.ToString());




            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_MAIL]", EventUser.Email);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_USERNAME]", EventUser.Username);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_DISPLAYNAME]", EventUser.DisplayName);





            emailItem.Subject = Template_Subject;

            #region "构造邮件信息"

            String Template_Content = NetHelper.ReplaceNoCase(MailSetting.MailBody, "[TITLE]", DataItem.Title);
            //Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CONTENTTEXT]", HttpUtility.HtmlDecode(DataItem.ContentText));
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CREATETIME]", DataItem.CreateTime.ToString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CREATEDATE]", DataItem.CreateTime.ToShortDateString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[MAILSETTING_TIME]", MailSetting.MailTime.ToString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[DATETIME_NOW]", xUserTime.LocalTime().ToString());


            //注册的信息丢进来
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_MAIL]", EventUser.Email);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_USERNAME]", EventUser.Username);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_DISPLAYNAME]", EventUser.DisplayName);

            //订阅用户的信息丢进来
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_MAIL]", UserItem.Email);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_USERNAME]", UserItem.Username);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_DISPLAYNAME]", UserItem.DisplayName);


            //图片替换
            if (Common.Contains(Template_Content, "[PICTURE]"))
            {
                Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[PICTURE]", FormatThumbnailUrl(DataItem));
            }

            //文章链接
            if (Common.Contains(Template_Content, "[LINK]"))
            {
                Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[LINK]", FormatItemUrl(DataItem));
            }

            emailItem.Content = Template_Content;
            #endregion




            emailItem.MailTo = MailSetting.MailTo;
            if (MailSetting.MailTo.ToUpper() == "[AUTHOR_MAIL]")//用户邮件需要发送给用户
            {
                emailItem.MailTo = EventUser.Email;
            }
            else if (MailSetting.MailTo.ToUpper() == "[USER_MAIL]")//用户邮件需要发送给用户
            {
                emailItem.MailTo = UserItem.Email;
            }
            emailItem.MailCC = MailSetting.MailCC;

            return emailItem;
        }
        #endregion

        #region "==订阅邮件:Playngo_ClientZone_GameSheet"

        /// <summary>
        /// 发送邮件(多篇文章)-订阅列表中的角色+邮箱
        /// </summary>
        /// <param name="Articles"></param>
        /// <returns></returns>
        public Int32 SendMail(Playngo_ClientZone_GameSheet DataItem)
        {
            Int32 RecordCount = 0;
         
            Int32 UserRecordCount = 0;
            var UserList = Common.Split<UserInfo>(UserController.GetUsersByProfileProperty(DataItem.PortalId, "Newsletter_GameSheets", "True", 0, Int32.MaxValue, ref UserRecordCount), 1, Int32.MaxValue);
         
            if (UserList != null && UserList.Count > 0)
            {
                foreach (var UserItem in UserList)
                {
                    //验证邮件地址符合规则
                    if (Mail.IsValidEmailAddress(UserItem.Email, Null.NullInteger))
                    {
                        //验证用户是否有权限接受邮件
                        if (ValidUserRole(DataItem.Per_AllUsers, DataItem.Per_Roles, UserItem))
                        {
                            //验证用户区域
                            if(ValidUserJurisdictions(DataItem.Per_AllJurisdictions, DataItem.Per_Jurisdictions, UserItem))
                            {
                                //创建邮件
                                EmailInfo email = CreateMailHtml(DataItem, UserItem);
                                //推送邮件
                                MailScheduler.AssignMessage(email);

                                RecordCount++;
                            }

                   
                        }
                    }
                }
            }


            return RecordCount;
        }






        /// <summary>
        /// 创建邮件内容
        /// </summary>
        /// <param name="EmailTo"></param>
        /// <returns></returns>
        public EmailInfo CreateMailHtml(Playngo_ClientZone_GameSheet DataItem, UserInfo UserItem)
        {

            //构造邮件信息
            EmailInfo emailItem = new EmailInfo();
            emailItem.Settings = Settings;
            emailItem.PushSettings();


            //事件作者
            UserInfo EventUser = UserController.GetUserById(DataItem.PortalId, DataItem.CreateUser);

            string Template_Subject = NetHelper.ReplaceNoCase(MailSetting.MailSubject, "[TITLE]", DataItem.Title);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[MAILSETTING_TIME]", MailSetting.MailTime.ToString());




            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_MAIL]", EventUser.Email);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_USERNAME]", EventUser.Username);
            Template_Subject = NetHelper.ReplaceNoCase(Template_Subject, "[AUTHOR_DISPLAYNAME]", EventUser.DisplayName);





            emailItem.Subject = Template_Subject;

            #region "构造邮件信息"

            String Template_Content = NetHelper.ReplaceNoCase(MailSetting.MailBody, "[TITLE]", DataItem.Title);
            //Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CONTENTTEXT]", HttpUtility.HtmlDecode(DataItem.ContentText));
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CREATETIME]", DataItem.CreateTime.ToString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[CREATEDATE]", DataItem.CreateTime.ToShortDateString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[MAILSETTING_TIME]", MailSetting.MailTime.ToString());
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[DATETIME_NOW]", xUserTime.LocalTime().ToString());


            //注册的信息丢进来
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_MAIL]", EventUser.Email);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_USERNAME]", EventUser.Username);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[AUTHOR_DISPLAYNAME]", EventUser.DisplayName);

            //订阅用户的信息丢进来
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_MAIL]", UserItem.Email);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_USERNAME]", UserItem.Username);
            Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[USER_DISPLAYNAME]", UserItem.DisplayName);


            //图片替换
            if (Common.Contains(Template_Content, "[PICTURE]"))
            {
                Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[PICTURE]", FormatThumbnailUrl(DataItem));
            }

            //文章链接
            if (Common.Contains(Template_Content, "[LINK]"))
            {
                Template_Content = NetHelper.ReplaceNoCase(Template_Content, "[LINK]", FormatItemUrl(DataItem));
            }

            emailItem.Content = Template_Content;
            #endregion




            emailItem.MailTo = MailSetting.MailTo;
            if (MailSetting.MailTo.ToUpper() == "[AUTHOR_MAIL]")//用户邮件需要发送给作者
            {
                emailItem.MailTo = EventUser.Email;
            }
            else if (MailSetting.MailTo.ToUpper() == "[USER_MAIL]")//用户邮件需要发送给用户
            {
                emailItem.MailTo = UserItem.Email;
            }
            emailItem.MailCC = MailSetting.MailCC;

            return emailItem;
        }
        #endregion


 



        #region "获取时区相关"

        /// <summary>
        /// 显示时区
        /// </summary>
        /// <returns></returns>
        public String ViewTimeZone(String language)
        {
            //时区j计算
            //String TimeZone = String.Empty;
            //System.Collections.Specialized.NameValueCollection timeZones = Localization.GetTimeZones(language.ToLower());
            //if (timeZones.Count == 0)
            //{
            //    timeZones = Localization.GetTimeZones(DotNetNuke.Services.Localization.Localization.SystemLocale);
            //}
            //if (timeZones != null && timeZones.Count > 0)
            //{
            //    for (int i = 0; i <= timeZones.Keys.Count - 1; i++)
            //    {
            //        if (timeZones.Get(i) == PortalSettings.TimeZoneOffset.ToString())
            //        {
            //            TimeZone = timeZones.GetKey(i).ToString();
            //            break;
            //        }
            //    }
            //}
            //return TimeZone;


            int oldOffset = 0;
            var PortalSettings = PortalController.Instance.GetCurrentPortalSettings();
            if (PortalSettings != null && PortalSettings.UserId > DotNetNuke.Common.Utilities.Null.NullInteger)
            {
                int.TryParse(PortalSettings.UserInfo.Profile.PreferredTimeZone.BaseUtcOffset.TotalMinutes.ToString(System.Globalization.CultureInfo.InvariantCulture), out oldOffset);
            }
            else
            {
                int.TryParse(PortalSettings.TimeZone.BaseUtcOffset.TotalMinutes.ToString(System.Globalization.CultureInfo.InvariantCulture), out oldOffset);
            }
            return Localization.ConvertLegacyTimeZoneOffsetToTimeZoneInfo(oldOffset).ToString();
        }

        #endregion

        #region "处理URL"




        public String FormatThumbnailUrl(Playngo_ClientZone_Event DataItem)
        {
            
            return String.Format("{0}{1}Resource_Service.aspx?Token=Thumbnail&PortalId={2}&TabId={3}&ID={4}&width={5}&height={6}&mode={7}&language={8}", PortalUrl, MailSetting.ModulePath, DataItem.PortalId, MailSetting.TabID, DataItem.ID, 500, 500, "W", "en-US");
        }


        public String FormatThumbnailUrl(Playngo_ClientZone_DownloadFile DataItem)
        {
            return String.Format("{0}{1}Resource_Service.aspx?Token=Thumbnail&PortalId={2}&TabId={3}&ID={4}&width={5}&height={6}&mode={7}&language={8}", PortalUrl, MailSetting.ModulePath, DataItem.PortalId, MailSetting.TabID, DataItem.ID, 500, 500, "W", "en-US");
        }


        public String FormatThumbnailUrl(Playngo_ClientZone_Campaign DataItem)
        {

            return String.Format("{0}{1}Resource_Service.aspx?Token=Thumbnail&PortalId={2}&TabId={3}&ID={4}&width={5}&height={6}&mode={7}&language={8}", PortalUrl, MailSetting.ModulePath, DataItem.PortalId, MailSetting.TabID, DataItem.ID, 500, 500, "W", "en-US");
        }



        public String FormatThumbnailUrl(Playngo_ClientZone_GameSheet DataItem)
        {

            return String.Format("{0}{1}Resource_Service.aspx?Token=Thumbnail&PortalId={2}&TabId={3}&ID={4}&width={5}&height={6}&mode={7}&language={8}", PortalUrl, MailSetting.ModulePath, DataItem.PortalId, MailSetting.TabID, DataItem.ID, 500, 500, "W", "en-US");
        }

        




        public String FormatItemUrl(Playngo_ClientZone_Event DataItem)
        {
            return String.Format("{0}{1}Resource_Service.aspx?Token=GoItemLinkUrl&PortalId={2}&TabId={3}&ModuleId={4}&ID={5}&Type={6}", PortalUrl, MailSetting.ModulePath, DataItem.PortalId, MailSetting.TabID, DataItem.ModuleId, DataItem.ID,(Int32)EnumDisplayModuleType.Events);
        }




        public String FormatItemUrl(Playngo_ClientZone_DownloadFile DataItem)
        {

            return String.Format("{0}{1}Resource_Service.aspx?Token=GoItemLinkUrl&PortalId={2}&TabId={3}&ModuleId={4}&ID={5}&Type={6}", PortalUrl, MailSetting.ModulePath, DataItem.PortalId, MailSetting.TabID, DataItem.ModuleId, DataItem.ID, (Int32)EnumDisplayModuleType.Downloads);
        }


        public String FormatItemUrl(Playngo_ClientZone_Campaign DataItem)
        {
            return String.Format("{0}{1}Resource_Service.aspx?Token=GoItemLinkUrl&PortalId={2}&TabId={3}&ModuleId={4}&ID={5}&Type={6}", PortalUrl, MailSetting.ModulePath, DataItem.PortalId, MailSetting.TabID, DataItem.ModuleId, DataItem.ID, (Int32)EnumDisplayModuleType.Campaigns);
        }



        public String FormatItemUrl(Playngo_ClientZone_GameSheet DataItem)
        {
            return String.Format("{0}{1}Resource_Service.aspx?Token=GoItemLinkUrl&PortalId={2}&TabId={3}&ModuleId={4}&ID={5}&Type={6}", PortalUrl, MailSetting.ModulePath, DataItem.PortalId, MailSetting.TabID, DataItem.ModuleId, DataItem.ID, (Int32)EnumDisplayModuleType.GameSheets);
        }

        /// <summary>
        /// 站点目录
        /// </summary>
        public String PortalUrl
        {
            get { return Settings["PortalUrl"] != null ? Convert.ToString(Settings["PortalUrl"]) :""; }
        }

        #endregion




    }
}