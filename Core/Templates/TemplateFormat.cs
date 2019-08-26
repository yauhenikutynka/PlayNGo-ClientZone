using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common;
using DotNetNuke.Services.Localization;

using System.Web.UI.WebControls;


using System.Collections;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Common.Utilities;
using System.Globalization;

namespace Playngo.Modules.ClientZone
{
    public class TemplateFormat
    {


        #region "属性"
        /// <summary>
        /// 模块基类
        /// </summary>
        private BaseModule baseModule = new BaseModule();

        private BasePage basePage = new BasePage();

        private Hashtable _ClientZone_Settings = new Hashtable();
        /// <summary>
        /// 博客的设置
        /// </summary>
        public Hashtable ClientZone_Settings
        {
            get {
                if (!(_ClientZone_Settings != null && _ClientZone_Settings.Count > 0))
                {
                    _ClientZone_Settings = baseModule.ModuleId > 0 ? baseModule.ClientZone_Settings : basePage.ClientZone_Settings;
                }
                return _ClientZone_Settings; }

        }

        private Hashtable _Settings = new Hashtable();
        /// <summary>
        /// 博客的设置
        /// </summary>
        public Hashtable Settings
        {
            get
            {
                if (!(_Settings != null && _Settings.Count > 0))
                {
                    _Settings = baseModule.ModuleId > 0 ? baseModule.Settings : basePage.Settings;
                }
                return _Settings;
            }

        }


        private String _TemplateName = String.Empty;

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName
        {
            get
            {
                return _TemplateName;
            }

            set
            {
                _TemplateName = value;
            }
        }

        private PortalSettings _ClientZone_PortalSettings;
        /// <summary>
        /// 站点的设置
        /// </summary>
        public PortalSettings ClientZone_PortalSettings
        {
            get
            {
                if (!(_ClientZone_PortalSettings != null))
                {

                    _ClientZone_PortalSettings = baseModule != null && baseModule.ModuleId > 0 ? baseModule.PortalSettings : basePage.PortalSettings;
                }
                return _ClientZone_PortalSettings;
            }

        }

        /// <summary>
        /// 模块路径
        /// </summary>
        public String ModulePath
        {
            get { return baseModule != null && baseModule.ModuleId > 0 ? baseModule.ModulePath : basePage.ModulePath; }
        }

        /// <summary>
        /// 模块编号(绑定)
        /// </summary>
        public Int32 ModuleId
        {
            get { return baseModule.ModuleId > 0 ? baseModule.Settings_ModuleID : basePage.Settings_ModuleID; }
        }

        /// <summary>
        /// 站点(绑定)
        /// </summary>
        public Int32 PortalID
        {
            get { return baseModule.ModuleId > 0 ? baseModule.Settings_PortalID : basePage.Settings_PortalID; }
        }


        /// <summary>
        /// 页面编号(绑定)
        /// </summary>
        public Int32 TabId
        {
            get { return baseModule.ModuleId > 0 ? baseModule.Settings_TabID : basePage.Settings_TabID; }
        }


        /// <summary>
        /// 是否开启SSL
        /// </summary>
        public bool IsSSL
        {
            get { return baseModule != null && baseModule.ModuleId > 0 ? baseModule.IsSSL : basePage.IsSSL; }
        }


        private Button _ViewButton;
        /// <summary>
        /// 触发按钮
        /// </summary>
        public Button ViewButton
        {
            get { return _ViewButton; }
            set { _ViewButton = value; }
        }




        private String _PortalUrl = String.Empty;
        /// <summary>
        /// 站点URL (可以在绑定的时候用到)
        /// </summary>
        public String PortalUrl
        {
            get
            {
                if (String.IsNullOrEmpty(_PortalUrl))
                {
                    if (PortalID == ClientZone_PortalSettings.PortalId)
                    {
                        _PortalUrl = String.Format("{0}://{1}", IsSSL ? "https" : "http", WebHelper.GetHomeUrl());
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(ClientZone_PortalSettings.PortalAlias.HTTPAlias))
                        {
                            _PortalUrl = String.Format("{0}://{1}", IsSSL ? "https" : "http", ClientZone_PortalSettings.PortalAlias.HTTPAlias);
                        }
                    }
                }
                return _PortalUrl;
            }
        }

        /// <summary>
        /// 填充目标的URL
        /// </summary>
        /// <param name="_Url"></param>
        /// <returns></returns>
        public String FullPortalUrl(String _Url)
        {
            if (!String.IsNullOrEmpty(_Url))
            {
                if (_Url.ToLower().IndexOf("http://") < 0 && _Url.ToLower().IndexOf("https://") < 0)
                {
                    _Url = string.Format("{0}{1}", PortalUrl, _Url);
                }
            }
            return _Url;
        }



        /// <summary>
        /// SEO URL参数,FriendlyUrl 开关
        /// </summary>
        public Boolean Settings_Seo_FriendlyUrl
        {
            get { return ViewSettingT<Boolean>("SEO.FriendlyUrl",true); }
        }


        #endregion



        #region "方法"

        #region "--关于内容与标题--"

        /// <summary>
        /// 显示标题(通过资源文件)
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="DefaultValue">资源文件未定义时默认值</param>
        /// <returns>返回值</returns>
        public String ViewTitle(String Title, String DefaultValue)
        {
            return ViewResourceText(Title, DefaultValue);
        }


        /// <summary>
        /// 显示内容
        /// </summary>
        /// <returns></returns>
        public String ViewContent(String FieldName, Playngo_ClientZone_Event DataItem)
        {
            String sContent = String.Empty;
            if (DataItem != null && DataItem.ID > 0)
            {
                if (!String.IsNullOrEmpty(FieldName))
                {
                    if (FieldName.ToLower() == "contenttext")
                    {
                        sContent = HttpUtility.HtmlDecode(Convert.ToString(DataItem[FieldName]));
                    }
                    else
                    {
                        sContent = Convert.ToString(DataItem[FieldName]);
                    }
                }
            }
            return sContent;
        }

        /// <summary>
        /// 显示内容并截取数据
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="DataItem">数据项</param>
        /// <param name="Lenght">显示长度</param>
        /// <returns></returns>
        public String ViewContent(String FieldName, Playngo_ClientZone_Event DataItem, Int32 Lenght)
        {
            return ViewContent(FieldName, DataItem, Lenght, "...");
        }


        /// <summary>
        /// 显示内容并截取数据
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="DataItem">数据项</param>
        /// <param name="Lenght">显示长度</param>
        /// <param name="Suffix">终止符号</param>
        /// <returns></returns>
        public String ViewContent(String FieldName, Playngo_ClientZone_Event DataItem, Int32 Lenght, String Suffix)
        {
            String Content = ViewContent(FieldName, DataItem);//先取内容
            return WebHelper.leftx(Content, Lenght, Suffix);
        }

        /// <summary>
        /// 显示内容
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String ViewContentText(Playngo_ClientZone_Event DataItem)
        {
            return HttpUtility.HtmlDecode(ViewContent("ContentText", DataItem));
        }

        /// <summary>
        /// HTML 解码
        /// </summary>
        /// <param name="ContentText"></param>
        /// <returns></returns>
        public String HtmlDecode(String ContentText)
        {
            return HttpUtility.HtmlDecode(ContentText);
        }

        /// <summary>
        /// HTML 编码
        /// </summary>
        /// <param name="ContentText"></param>
        /// <returns></returns>
        public String HtmlEncode(String ContentText)
        {
            return HttpUtility.HtmlEncode(ContentText);
        }


        /// <summary>
        /// 显示JSON格式的标题
        /// </summary>
        /// <param name="DataList"></param>
        /// <returns></returns>
        public String ViewTitleToJson(List<Playngo_ClientZone_Event> DataList)
        {
            StringBuilder Titles = new StringBuilder();

            foreach (Playngo_ClientZone_Event Content in DataList)
            {
                if (Titles.Length > 0)
                {
                    Titles.Append(",");
                }
                Titles.AppendFormat("'{0}'", Content.Title);
            }
            return Titles.ToString();
        }

        /// <summary>
        /// 显示友好的标题
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String ViewFriendlyTitle(Playngo_ClientZone_Event DataItem)
        {
            return Common.CreateFriendlySlugTitle(DataItem.Title);
        }

        /// <summary>
        /// 按字符位数补齐
        /// </summary>
        /// <param name="CharTxt">字符</param>
        /// <param name="CharLen">位数</param>
        /// <param name="Vacancy"></param>
        /// <returns></returns>
        public String ViewFillVacancy(string CharTxt, int CharLen, String Vacancy)
        {
            return Common.FillVacancy(CharTxt, CharLen, Vacancy);
        }


        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="DefaultValue"></param>
        /// <param name="oldString"></param>
        /// <param name="newString"></param>
        /// <returns></returns>
        public String Replace(String DefaultValue, String oldString, String newString)
        {
            return DefaultValue.Replace(oldString, newString);
        }

        /// <summary>
        /// 除法运算
        /// </summary>
        /// <param name="FirstNumber">第一个数</param>
        /// <param name="LastNumber">第二个数</param>
        /// <returns></returns>
        public String Division(String FirstNumber, String LastNumber)
        {
            String d = "0";
            if (!String.IsNullOrEmpty(FirstNumber) && !String.IsNullOrEmpty(LastNumber))
            {
                if (FirstNumber.IndexOf(",") >= 0) FirstNumber = FirstNumber.Replace(",", ".");
                if (LastNumber.IndexOf(",") >= 0) LastNumber = LastNumber.Replace(",", ".");

                float f = float.Parse(FirstNumber) / float.Parse(LastNumber);

                if (f > 0f)
                {
                    d = f.ToString();
                    if (!String.IsNullOrEmpty(d) && d.IndexOf(",") >= 0)
                    {
                        d = d.Replace(",", ".");
                    }

                }


            }
            return d;
        }



        /// <summary>
        /// 将字符串转化为列表,逗号为分隔符
        /// </summary>
        /// <param name="Items"></param>
        /// <returns></returns>
        public List<String> ToList(String Items)
        {
            List<String> list = new List<String>();
            if (!String.IsNullOrEmpty(Items))
            {
                list = Common.GetList(Items);
            }
            return list;
        }

        /// <summary>
        /// 过滤掉空格
        /// </summary>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public String ToTrim(String _Value)
        {
            if (!String.IsNullOrEmpty(_Value))
            {
                return _Value.Replace(" ", "");
            }
            return String.Empty;
        }


        /// <summary>
        /// 过滤掉空格和其他符号
        /// </summary>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public String ToTrim2(String _Value)
        {
            if (!String.IsNullOrEmpty(_Value))
            {
                _Value = _Value.Replace(" ", "");
                _Value = _Value.Replace(".", "");
                _Value = _Value.Replace(",", "");
                _Value = _Value.Replace("#", "");
                _Value = _Value.Replace("?", "");
                _Value = _Value.Replace("&", "");
                _Value = _Value.Replace("+", "");
                _Value = _Value.Replace("/", "");
                return _Value;
            }
            return String.Empty;
        }

        #endregion

        #region "--关于链接跳转--"


        public String GoUrl(Playngo_ClientZone_Event DataItem)
        {
            return GoUrl(DataItem, false);
        }

        /// <summary>
        /// Event 详情页
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String GoUrl(Playngo_ClientZone_Event DataItem, Boolean Preview = false)
        {
            if (basePage.ModuleId > 0)
            {
                return FullPortalUrl(CommonFriendlyUrls.FriendlyUrl(DataItem, basePage.ViewSettingT<Int32>("ClientZone_DisplayTab_Events", basePage.TabId), Preview, basePage));
            }

            return FullPortalUrl(CommonFriendlyUrls.FriendlyUrl(DataItem, ViewSettingT<Int32>("ClientZone_DisplayTab_Events", baseModule.TabId), Preview, baseModule));
        }


        public String GoUrl(Playngo_ClientZone_GameSheet DataItem)
        {
            return GoUrl(DataItem, false);
        }

        /// <summary>
        /// Event 详情页
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String GoUrl(Playngo_ClientZone_GameSheet DataItem, Boolean Preview = false)
        {
            if (basePage.ModuleId > 0)
            {
                return FullPortalUrl(CommonFriendlyUrls.FriendlyUrl(DataItem, basePage.ViewSettingT<Int32>("ClientZone_DisplayTab_GameSheets", basePage.TabId), Preview, basePage));
            }

            return FullPortalUrl(CommonFriendlyUrls.FriendlyUrl(DataItem, ViewSettingT<Int32>("ClientZone_DisplayTab_GameSheets", baseModule.TabId), Preview, baseModule));
        }


        public String GoUrl(Playngo_ClientZone_Campaign DataItem)
        {
            return GoUrl(DataItem, false);
        }

        /// <summary>
        /// Event 详情页
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String GoUrl(Playngo_ClientZone_Campaign DataItem, Boolean Preview = false)
        {
            if (basePage.ModuleId > 0)
            {
                return FullPortalUrl(CommonFriendlyUrls.FriendlyUrl(DataItem, basePage.ViewSettingT<Int32>("ClientZone_DisplayTab_Campaigns", basePage.TabId), Preview, basePage));
            }

            return FullPortalUrl(CommonFriendlyUrls.FriendlyUrl(DataItem, ViewSettingT<Int32>("ClientZone_DisplayTab_Campaigns", baseModule.TabId), Preview, baseModule));
        }





        public String GoUiUrl(String UiToken)
        {
            Int32 DisplayTab =  ViewSettingT<Int32>(String.Format("ClientZone_DisplayTab_{0}", UiToken),0);

            //String StrUiToken = String.IsNullOrEmpty(UiToken) ? "" : String.Format("ui={0}", UiToken);
            //return FullPortalUrl(Globals.NavigateURL(baseModule.TabId, "", StrUiToken));

            return FullPortalUrl(Globals.NavigateURL(DisplayTab));
        }


        public String GoUrl()
        {
            return FullPortalUrl(Globals.NavigateURL( baseModule.TabId, ""));
        }

        /// <summary>
        /// 返回到列表
        /// </summary>
        /// <returns></returns>
        public String GoUrl(String IsMain = "true")
        {
            return FullPortalUrl(Globals.NavigateURL(IsMain == "true" ? baseModule.Settings_TabID : baseModule.TabId, ""));
        }

        



 
         


 

         


        /// <summary>
        /// 归档查询
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String GoUrl(Playngo_ClientZone_Archive DataItem)
        {
            return baseModule.FullPortalUrl(Globals.NavigateURL(baseModule.Settings_TabID, baseModule.PortalSettings, "", String.Format("Archive={0}-{1}", DataItem.Year, DataItem.Month)));
        }



        /// <summary>
        /// 跳转到登录页面
        /// </summary>
        /// <returns></returns>
        public String GoLogin()
        {
            return Globals.LoginURL(HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl), false);
            //return  Globals.NavigateURL(pmb.PortalSettings.LoginTabId, "Login", "returnurl=" +  HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl));
        }
        /// <summary>
        /// 跳转到文章编辑界面
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String GoEditUrl(Playngo_ClientZone_Event DataItem)
        {
            String EditUrl = String.Empty;
            if (baseModule.ModuleId > 0)
            {
                EditUrl = baseModule.xUrl(baseModule.Settings_TabID, baseModule.Settings_ModuleID, "ID", Convert.ToString(DataItem.ID), "Events-Edit");
            } else
            {
                EditUrl = basePage.xUrl(basePage.Settings_TabID, basePage.Settings_ModuleID, "ID", Convert.ToString(DataItem.ID), "Events-Edit");
            }

            return EditUrl;//pmb.EditUrl("Token", "Event", "Manager", "EventID=" + Convert.ToString(DataItem.ID));
        }

        /// <summary>
        /// 填充为完整的URL
        /// </summary>
        public String GoFullUrl(String goUrl)
        {
            return  baseModule.FullPortalUrl(goUrl);
        }
        /// <summary>
        /// 填充为完整的URL
        /// </summary>
        public String GoFullUrl()
        {
            return baseModule.PortalUrl;
        }
       



        /// <summary>
        /// 跳转到消息页面1
        /// </summary>
        /// <param name="MessagesCode"></param>
        /// <param name="MessagesText"></param>
        /// <param name="MessagesType"></param>
        /// <returns></returns>
        public String GoMessagesUrl(String MessagesCode, String MessagesText, EnumMessagesType MessagesType)
        {
            return GoMessagesUrl(MessagesCode, MessagesText,(Int32) MessagesType);
        }
        /// <summary>
        /// 跳转到消息页面2
        /// </summary>
        /// <param name="MessagesCode"></param>
        /// <param name="MessagesText"></param>
        /// <param name="MessagesType"></param>
        /// <returns></returns>
        public String GoMessagesUrl(String MessagesCode, String MessagesText, Int32 MessagesType)
        {
            List<String> Urls = new List<String>();
            if (!String.IsNullOrEmpty(MessagesCode))
            {
                Urls.Add(String.Format("MessagesCode={0}", MessagesCode));
            }

            if (!String.IsNullOrEmpty(MessagesText))
            {
                Urls.Add(String.Format("MessagesText={0}",HttpUtility.UrlEncode( MessagesText)));
            }

            if (MessagesType > -1 )
            {
                Urls.Add(String.Format("MT={0}", MessagesType));
            }

            Urls.Add("Token=Messages");
            

            return baseModule.FullPortalUrl(Globals.NavigateURL(baseModule.Settings_TabID, baseModule.PortalSettings, "", Urls.ToArray()));
        }





        #endregion

        #region "--关于服务的URL--"



        /// <summary>
        /// 下载单个文件
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String GoDownloadUrl(Playngo_ClientZone_DownloadFile DataItem)
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}&FileId={6}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "DownloadFile",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language,
                                                 DataItem.ID);


            return baseModule.FullPortalUrl(DownloadUrl);
        }

        /// <summary>
        /// 下载所选择的文件集合
        /// </summary>
        /// <returns></returns>
        public String GoDownloadSelectsUrl()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "DownloadFiles",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(DownloadUrl);
        }

        /// <summary>
        /// 下载GameSheet中所有关联的文件
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String GoDownloadsUrl(Playngo_ClientZone_GameSheet DataItem)
        {

            //获取当前列表中所有的文件
            var FileIds =  Playngo_ClientZone_DownloadRelation.FindFileIds(DataItem.ID, (Int32)EnumDisplayModuleType.GameSheets);


            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}&FileIds={6}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "DownloadFiles",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language,
                                                 Common.GetStringByList(FileIds));


            return baseModule.FullPortalUrl(DownloadUrl);
        }



        /// <summary>
        /// 下载Campaign中所有关联的文件
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String GoDownloadsUrl(Playngo_ClientZone_Campaign DataItem)
        {
            //获取当前列表中所有的文件
            var FileIds = Playngo_ClientZone_DownloadRelation.FindFileIds(DataItem.ID, (Int32)EnumDisplayModuleType.Campaigns);

            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}&FileIds={6}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "DownloadFiles",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language,
                                                Common.GetStringByList(FileIds));


            return baseModule.FullPortalUrl(DownloadUrl);
        }


        /// <summary>
        /// 下载Event中所有关联的文件
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String GoDownloadsUrl(Playngo_ClientZone_Event DataItem)
        {
            //获取当前列表中所有的文件
            var FileIds = Playngo_ClientZone_DownloadRelation.FindFileIds(DataItem.ID, (Int32)EnumDisplayModuleType.Events);

            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}&FileIds={6}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "DownloadFiles",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language,
                                                Common.GetStringByList(FileIds));


            return baseModule.FullPortalUrl(DownloadUrl);
        }


        /// <summary>
        /// 事件编辑页面下载CSV文件地址
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String GoDownloadCSVToEventUrl(Playngo_ClientZone_Event DataItem)
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}&EventId={6}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "DownloadCSVToEvent",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language,
                                                 DataItem.ID);


            return baseModule.FullPortalUrl(DownloadUrl);
        }


        /// <summary>
        /// AJAX 数据（DownloadFiles）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxJSONToDownloadFiles()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxJSONDownloadFiles",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(DownloadUrl);
        }

        /// <summary>
        /// AJAX 数据（Events）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxJSONToEvents()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxJSONEvents",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(DownloadUrl);
        }

        /// <summary>
        /// AJAX 数据（ProductRoadmap 集合数据）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxJSONToProductRoadmap()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxJSONProductRoadmap",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(DownloadUrl);
        }

        /// <summary>
        /// AJAX 数据（GameSheets）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxJSONToGameSheets()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxJSONGameSheets",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(DownloadUrl);
        }


        /// <summary>
        /// AJAX 数据（GameSheets）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxJSONToCampaigns()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxJSONCampaigns",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(DownloadUrl);
        }


        /// <summary>
        /// AJAX更新用户账户信息
        /// </summary>
        /// <returns></returns>
        public String GoAjaxUpdateMyAccount()
        {
            String UpdateMyAccountUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxUpdateMyAccount",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(UpdateMyAccountUrl);
        }

        /// <summary>
        /// AJAX更新区域选择的状态
        /// </summary>
        /// <returns></returns>
        public String GoAjaxUpdateSelectJurisdictions()
        {
            String UpdateMyAccountUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxJSONUserJurisdictions",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(UpdateMyAccountUrl);
        }

        /// <summary>
        /// AJAX更新GameCategories选择的状态
        /// </summary>
        /// <returns></returns>
        public String GoAjaxUpdateSelectGameCategories()
        {
            String UpdateMyAccountUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxJSONUserGameCategories",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(UpdateMyAccountUrl);
        }



        /// <summary>
        /// AJAX更新FileTypes选择的状态
        /// </summary>
        /// <returns></returns>
        public String GoAjaxUpdateSelectFileTypes()
        {
            String UpdateMyAccountUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxJSONUserFileTypes",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(UpdateMyAccountUrl);
        }


        /// <summary>
        /// AJAX 数据（角标）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxJSONToNotifyBadges()
        {
            String UpdateMyAccountUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxJSONNotifyBadges",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(UpdateMyAccountUrl);
        }




        /// <summary>
        /// AJAX 搜索框下拉数据（DownloadFiles）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxSearchToDownloadFiles()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxSearchDownloadFiles",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(DownloadUrl);
        }


        /// <summary>
        /// AJAX 搜索框下拉数据（Campaigns）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxSearchToCampaigns()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxSearchCampaigns",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(DownloadUrl);
        }

        /// <summary>
        /// AJAX 搜索框下拉数据（Events）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxSearchToEvents()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxSearchEvents",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(DownloadUrl);
        }

 

        /// <summary>
        /// AJAX 搜索框下拉数据（GameSheets）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxSearchToGameSheets()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 baseModule.ModulePath,
                                                 baseModule.Settings_ModuleID,
                                                 "AjaxSearchGameSheets",
                                                 baseModule.Settings_TabID,
                                                 baseModule.PortalId,
                                                 baseModule.language);


            return baseModule.FullPortalUrl(DownloadUrl);
        }



        #endregion


        #region "--关于分类的一些数据打印--"

        /// <summary>
        /// 获取模板打印的文件分类
        /// </summary>
        /// <returns></returns>
        public List<Playngo_ClientZone_FileType> GetViewFileTypes()
        {
            var FileTypes = new List<Playngo_ClientZone_FileType>();
            FileTypes.Add(new Playngo_ClientZone_FileType() { ID = 0, Name = "All" });

            var items = Playngo_ClientZone_FileType.GetAllCheckList(baseModule.Settings_ModuleID);
            if (items != null && items.Count > 0)
            {
                FileTypes.AddRange(items);
            }

            return FileTypes;
        }



        /// <summary>
        /// 获取模板打印的文件分类
        /// </summary>
        /// <returns></returns>
        public List<Playngo_ClientZone_GameCategory> GetViewGameCategorys()
        {
            var GameCategorys = new List<Playngo_ClientZone_GameCategory>();
            GameCategorys.Add(new Playngo_ClientZone_GameCategory() { ID = 0, Name = "All" });

            var items = Playngo_ClientZone_GameCategory.GetAllCheckList(baseModule.Settings_ModuleID);
            if (items != null && items.Count > 0)
            {
                GameCategorys.AddRange(items);
            }

            return GameCategorys;
        }



        /// <summary>
        /// 获取模板打印的文件分类
        /// </summary>
        /// <returns></returns>
        public List<Playngo_ClientZone_CampaignCategory> GetViewCampaignCategorys()
        {
            var CampaignCategorys = new List<Playngo_ClientZone_CampaignCategory>();
            CampaignCategorys.Add(new Playngo_ClientZone_CampaignCategory() { ID = 0, Name = "All" });


            var items =  Playngo_ClientZone_CampaignCategory.GetAllCheckList(baseModule.Settings_ModuleID);
            if (items != null && items.Count > 0)
            {
                CampaignCategorys.AddRange(items);
            }


            return CampaignCategorys;
        }



        public List<Playngo_ClientZone_Jurisdiction> GetViewJurisdictions()
        {
            var Jurisdictions = new List<Playngo_ClientZone_Jurisdiction>();
            Jurisdictions.Add(new Playngo_ClientZone_Jurisdiction() { ID = 0, Name = "All" });

            var items = Playngo_ClientZone_Jurisdiction.GetAllCheckList(baseModule.Settings_ModuleID);
            if (items != null && items.Count > 0)
            {
                Jurisdictions.AddRange(items);
            }

            return Jurisdictions;
        }


        /// <summary>
        /// 选择中的区域列表（编号集合）
        /// </summary>
        /// <returns></returns>
        public List<String> GetViewSelectJurisdictions(List<Playngo_ClientZone_Jurisdiction> AllJurisdiction)
        {
            if(baseModule.UserId > 0)
            {
                var SelectJurisdictionsStr = ShowUserProfile(baseModule.UserInfo, "SelectJurisdictions");
                if (!String.IsNullOrEmpty(SelectJurisdictionsStr))
                {
                    if (SelectJurisdictionsStr != "-")
                    {
                        return Common.GetList(SelectJurisdictionsStr);
                    }

                }
                else
                {
                    if (AllJurisdiction != null && AllJurisdiction.Count > 0)
                    {
                        var JurisdictionStrs = new List<String>();
                        foreach (var Jurisdiction in AllJurisdiction)
                        {
                            JurisdictionStrs.Add(Jurisdiction.ID.ToString());
                        }
                        return JurisdictionStrs;
                    }

                }
            }
            return new List<string>();
        }

        /// <summary>
        /// 判断区域是否需要被选中
        /// </summary>
        /// <param name="SelectIds"></param>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public Boolean JurisdictionChecked(List<String> SelectIds, Playngo_ClientZone_Jurisdiction DataItem)
        {
            return SelectIds.Exists(r => r == DataItem.ID.ToString());
        }

        /// <summary>
        /// 选择中的文件类型列表（编号集合）
        /// </summary>
        /// <returns></returns>
        public List<String> GetViewSelectFileTypes(List<Playngo_ClientZone_FileType> AllFileTypes)
        {
            if (baseModule.UserId > 0)
            {
                var SelectFileTypesStr = ShowUserProfile(baseModule.UserInfo, "SelectFileTypes");
                if (!String.IsNullOrEmpty(SelectFileTypesStr))
                {
                    if (SelectFileTypesStr != "-")
                    {
                        return Common.GetList(SelectFileTypesStr);
                    }

                }
                else
                {
                    if (AllFileTypes != null && AllFileTypes.Count > 0)
                    {
                        var FileTypeStrs = new List<String>();
                        foreach (var FileType in AllFileTypes)
                        {
                            FileTypeStrs.Add(FileType.ID.ToString());
                        }
                        return FileTypeStrs;
                    }

                }
            }
            return new List<string>();
        }
        /// <summary>
        /// 判断文件类型是否需要被选中
        /// </summary>
        /// <param name="SelectIds"></param>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public Boolean FileTypeChecked(List<String> SelectIds, Playngo_ClientZone_FileType DataItem)
        {
            return SelectIds.Exists(r => r == DataItem.ID.ToString());
        }


        /// <summary>
        /// 选择中的区域列表（编号集合）
        /// </summary>
        /// <returns></returns>
        public List<String> GetViewSelectGameCategorys(List<Playngo_ClientZone_GameCategory> AllGameCategory)
        {
            if (baseModule.UserId > 0)
            {
                var SelectGameCategorysStr = ShowUserProfile(baseModule.UserInfo, "SelectGameCategories");
                if (!String.IsNullOrEmpty(SelectGameCategorysStr))
                {
                    if (SelectGameCategorysStr != "-")
                    {
                        return Common.GetList(SelectGameCategorysStr);
                    }

                }
                else
                {
                    if (AllGameCategory != null && AllGameCategory.Count > 0)
                    {
                        var GameCategoryStrs = new List<String>();
                        foreach (var GameCategory in AllGameCategory)
                        {
                            GameCategoryStrs.Add(GameCategory.ID.ToString());
                        }
                        return GameCategoryStrs;
                    }

                }
            }
            return new List<string>();
        }
        /// <summary>
        /// 判断游戏分类是否需要被选中
        /// </summary>
        /// <param name="SelectIds"></param>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public Boolean GameCategoryChecked(List<String> SelectIds, Playngo_ClientZone_GameCategory DataItem)
        {
            return SelectIds.Exists(r => r == DataItem.ID.ToString());
        }


        #endregion


        #region "--关于排序项的数据打印--"

        public List<EnumEntity> GetViewSortQueryByGame()
        {
            return EnumHelper.GetEnumList(typeof(EnumSortQueryByGame));
        }

        public List<EnumEntity> GetViewSortQueryByGame2()
        {
            return EnumHelper.GetEnumList(typeof(EnumSortQueryByGame2));
        }


        public List<EnumEntity> GetViewSortQueryByEvent()
        {
            return EnumHelper.GetEnumList(typeof(EnumSortQueryByEvent));
        }


        #endregion




        #region "--关于 RSS Feeds--"



        /// <summary>
        /// 生成访问RSS的地址
        /// </summary>
        /// <param name="Search_Text"></param>
        /// <param name="Search_Tag"></param>
        /// <param name="Search_CategoryID"></param>
        /// <param name="Search_Calendar"></param>
        /// <param name="Search_Archive"></param>
        /// <returns></returns>
        public String GoRssUrl(String Search_Text, String Search_Tag, Int32 Search_CategoryID, Int32 Search_Author, String Search_Calendar, String Search_Archive)
        {
            return String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}{6}{7}{8}{9}{10}{11}",
                          baseModule.ModulePath,
                          baseModule.Settings_ModuleID,
                          "RssFeeds",
                          baseModule.TabId,
                           baseModule.PortalId,
                         baseModule.language,
                          String.IsNullOrEmpty(Search_Tag) ? "" : String.Format("&Tag={0}", HttpUtility.UrlEncode(Search_Tag)),
                          Search_CategoryID == 0 ? "" : String.Format("&Category={0}", Search_CategoryID),
                          Search_Author == 0 ? "" : String.Format("&Author={0}", Search_Author),
                         String.IsNullOrEmpty(Search_Calendar) ? "" : String.Format("&Calendar={0}", HttpUtility.UrlEncode(Search_Calendar)),
                         String.IsNullOrEmpty(Search_Archive) ? "" : String.Format("&Archive={0}", HttpUtility.UrlEncode(Search_Archive)),
                         String.IsNullOrEmpty(Search_Text) ? "" : String.Format("&Search={0}", HttpUtility.UrlEncode(Search_Text))
                         );
        }



        /// <summary>
        /// 生成访问RSS的地址
        /// </summary>
        /// <returns></returns>
        public String GoRssUrl()
        {
            //接受参数进行拼接
            String Search_Text = WebHelper.GetStringParam(HttpContext.Current.Request, "Search", "");
            String Search_Tag = WebHelper.GetStringParam(HttpContext.Current.Request, "Tag", "");
            Int32 Search_CategoryID = WebHelper.GetIntParam(HttpContext.Current.Request, "CategoryID", 0);
            Int32 Search_Author = WebHelper.GetIntParam(HttpContext.Current.Request, "Author", 0);
            String Search_Calendar = WebHelper.GetStringParam(HttpContext.Current.Request, "Calendar", "");
            String Search_Archive = WebHelper.GetStringParam(HttpContext.Current.Request, "Archive", "");

            return GoRssUrl(Search_Text, Search_Tag, Search_CategoryID, Search_Author, Search_Calendar, Search_Archive);
           
        }
         

              /// <summary>
        /// 生成访问RSS的地址
        /// </summary>
        /// <param name="DataItem">归档项</param>
        /// <returns></returns>
        public String GoRssUrl(Playngo_ClientZone_Archive DataItem)
        {
            return GoRssUrl("", "", 0,0, "", String.Format("{0}-{1}", DataItem.Year, DataItem.Month));
        }

        

        #endregion

   

    

        #region "--关于查询搜索--"

        /// <summary>
        /// 在内存中分页
        /// </summary>
        /// <param name="list"></param>

        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<List<Playngo_ClientZone_Event>> Split(List<Playngo_ClientZone_Event> list, Int32 PageSize)
        {
            List<List<Playngo_ClientZone_Event>> ttList = new List<List<Playngo_ClientZone_Event>>();
            int size = list.Count / PageSize + (list.Count % PageSize > 0 ? 1 : 0);
            for (int i = 1; i <= size; i++)
            {
               ttList.Add(    Common.Split<Playngo_ClientZone_Event>(list, i, PageSize));
            }


            return ttList;
        }

        /// <summary>
        /// 在内存中分页
        /// </summary>
        /// <param name="list"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Playngo_ClientZone_Event> Split(List<Playngo_ClientZone_Event> list, Int32 PageIndex, Int32 PageSize)
        {
            return Common.Split<Playngo_ClientZone_Event>(list, PageIndex, PageSize);
        }


        #endregion

    

        #region "--关于分类--"
         


        #endregion


        #region "--关于图片--"





        /// <summary>
        /// 显示图片文件的大小 kb/mb
        /// </summary>
        /// <param name="DataItem"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String PictureSize(Playngo_ClientZone_Event DataItem, String FieldName)
        {
            String _PictureSize = "0kb";
            String _PictureUrl = Convert.ToString(ViewItemSetting(DataItem, FieldName, ""));
            if (!String.IsNullOrEmpty(_PictureUrl) && _PictureUrl.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                int MediaID = 0;
                if (int.TryParse(_PictureUrl.Replace("MediaID=", ""), out MediaID) && MediaID > 0)
                {
                    Playngo_ClientZone_Files Multimedia = Playngo_ClientZone_Files.FindByID(MediaID);
                    if (Multimedia != null && Multimedia.ID > 0 && Multimedia.FileSize > 0)
                    {
                        if (Multimedia.FileSize > (1024 * 1024))
                        {
                            _PictureSize = String.Format("{0:N}gb", Convert.ToSingle(Multimedia.FileSize) / (1024 * 1024));
                        }
                        else if (Multimedia.FileSize > 1024)
                        {
                            _PictureSize = String.Format("{0:N}mb", Convert.ToSingle(Multimedia.FileSize) / Convert.ToSingle(1024));
                        }
                        else
                        {
                            _PictureSize = String.Format("{0}kb", Multimedia.FileSize);
                        }


                    }
                }
            }
            return _PictureSize;
        }

        /// <summary>
        /// 显示图片地址
        /// </summary>
        /// <param name="DataItem"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String PictureUrl(Playngo_ClientZone_Event DataItem, String FieldName, String DefaultValue)
        {
            String _PictureUrl = Convert.ToString(ViewItemSetting(DataItem, FieldName, ""));

            return ViewLinkUrl(_PictureUrl, DefaultValue);
        }



        ///// <summary>
        ///// 显示文章附加图片
        ///// </summary>
        ///// <param name="DataItem"></param>
        ///// <returns></returns>
        //public String ViewPicture(Playngo_ClientZone_Event DataItem)
        //{
        //    if (DataItem.Picture > 0)
        //    {
        //        //查找附加的图片
        //        Playngo_ClientZone_Files Multimedia = Playngo_ClientZone_Files.FindByID(DataItem.Picture);

        //        if (Multimedia != null && Multimedia.ID > 0)
        //        {
        //            return String.Format("{0}{1}", pmb.PortalSettings.HomeDirectory, Multimedia.FilePath);
        //        }
        //    }
        //    return String.Format("{0}Resource/images/no_image.png", pmb.ModulePath); ;
        //}


        /// <summary>
        /// 显示封面图片的缩略图片
        /// </summary>
        /// <param name="DataItem"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public String ThumbnailUrl(Int32 Picture, object width, object height, object mode)
        {
            return FullPortalUrl( String.Format("{0}Resource_Service.aspx?Token=Thumbnail&PortalId={1}&TabId={2}&ID={3}&width={4}&height={5}&mode={6}&language={7}", 
                baseModule.ModulePath, baseModule.PortalId, baseModule.Settings_TabID, Picture, width, height, mode,baseModule.language));
        }
        /// <summary>
        /// 显示封面图片的缩略图片
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String ThumbnailUrl(Playngo_ClientZone_Event DataItem)
        {
            //return ThumbnailUrl(DataItem.Picture, 200, 200, "AUTO");
            return "";
        }


        /// <summary>
        /// 显示封面图片的原始图片
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String ViewPicture(Playngo_ClientZone_Event DataItem)
        {
            String Picture = String.Format("{0}Resource/images/no_image.png", baseModule.ModulePath);
            //if (DataItem != null && DataItem.ID > 0 && DataItem.Picture > 0)
            //{
            //    Playngo_ClientZone_Files PhotoItem = Playngo_ClientZone_Files.FindByID(DataItem.Picture);
            //    if (PhotoItem != null && PhotoItem.ID > 0)
            //    {
            //        Picture = ViewPicture(PhotoItem);
            //    }
            //}
            return Picture;
        }

        /// <summary>
        /// 显示相片的原始图片
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String ViewPicture(Playngo_ClientZone_Files DataItem)
        {
            String Picture = String.Format("{0}Resource/images/no_image.png", baseModule.ModulePath);
            if (DataItem != null && DataItem.ID > 0)
            {
                Picture = String.Format("{0}{1}", baseModule.PortalSettings.HomeDirectory, DataItem.FilePath);
            }
            return Picture;

        }

        /// <summary>
        /// 显示附件Url
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String ViewAttachment(Playngo_ClientZone_Files DataItem)
        {
            String Attachment = String.Empty;
            if (DataItem != null && DataItem.ID > 0)
            {
                Attachment = String.Format("{0}{1}", baseModule.PortalSettings.HomeDirectory, DataItem.FilePath);
            }
            return Attachment;

        }


        /// <summary>
        /// 获取图片集
        /// </summary>
        /// <param name="ImageIds"></param>
        /// <returns></returns>
        public List<Playngo_ClientZone_Files> GetImages(String ImageIds)
        {
            var Files = new  List<Playngo_ClientZone_Files>();
            if (!String.IsNullOrEmpty(ImageIds))
            {
                var FileIds = Common.GetList(ImageIds);
                if (FileIds != null && FileIds.Count > 0)
                {
                    foreach (var FileId in FileIds)
                    {
                        Int32 result = 0;
                        if (!String.IsNullOrEmpty(FileId) && Int32.TryParse(FileId, out result))
                        {
                            var FileItem = Playngo_ClientZone_Files.FindByKeyForEdit(result);
                            if (FileItem != null && FileItem.ID > 0)
                            {
                                Files.Add(FileItem);
                            }
                        }
                    }
                }
            }

            return Files;
        }



        public String GetFilePath(String FileID, BasePage Context)
        {
            String FilePath = String.Empty;
            if (FileID.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                int MediaID = 0;
                if (int.TryParse(FileID.Replace("MediaID=", ""), out MediaID) && MediaID > 0)
                {
                    Playngo_ClientZone_Files Multimedia = Playngo_ClientZone_Files.FindByID(MediaID);
                    if (Multimedia != null && Multimedia.ID > 0)
                    {
                        //FilePath = Context.Server.UrlPathEncode(Context.GetPhotoPath(Multimedia.FilePath));
                        FilePath = Context.GetPhotoPath(Multimedia.FilePath);
                    }
                }
            }
            return FilePath;
        }


        public String GetFilePath(String FileID, BasePage Context,out Playngo_ClientZone_Files Multimedia)
        {
            Multimedia = new Playngo_ClientZone_Files();
            String FilePath = String.Empty;
            if (FileID.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                int MediaID = 0;
                if (int.TryParse(FileID.Replace("MediaID=", ""), out MediaID) && MediaID > 0)
                {
                    Multimedia = Playngo_ClientZone_Files.FindByID(MediaID);
                    if (Multimedia != null && Multimedia.ID > 0)
                    {
                        //FilePath = Context.Server.UrlPathEncode(Context.GetPhotoPath(Multimedia.FilePath));
                        FilePath = Context.GetPhotoPath(Multimedia.FilePath);
                    }
                }
            }
            return FilePath;
        }

        #endregion




        #region "--关于归档格式--"
        /// <summary>
        /// 数字转换为英文月份
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public String ViewMonth(Int32 Month)
        {
            if (Month > 0 && Month <= 12)
            {
                String strMonth = EnumHelper.GetEnumTextVal(Month, typeof(EnumMonth));
                return ViewResourceText(strMonth,strMonth);
            }
            else
                return String.Empty;
        }
        /// <summary>
        /// 数字转换为英文月份
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String ViewMonth(Playngo_ClientZone_Archive DataItem)
        {
            return ViewMonth(DataItem.Month);
        }

        /// <summary>
        /// 归档样式
        /// </summary>
        /// <param name="DataItem"></param>
        /// <param name="CssClass"></param>
        /// <returns></returns>
        public String ViewArchiveClass(Playngo_ClientZone_Archive DataItem, String _CssClass)
        {
            String CssClass = String.Empty;
            String Search_Archive = WebHelper.GetStringParam(HttpContext.Current.Request, "Archive", "");
            if (!String.IsNullOrEmpty(Search_Archive))
            {
                if (Search_Archive.IndexOf(String.Format("{0}-{1}", DataItem.Year, DataItem.Month)) >= 0)
                {
                    CssClass = _CssClass;
                }
            }
            return CssClass;
        }


        #endregion


        #region "关于日期的ST ND  TH等输出"
        /// <summary>
        /// 关于日期的ST ND  TH等输出
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string GetDaySuffix(int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }
        #endregion




        #region "--关于控件--"

        /// <summary>
        /// 生成HTML按钮
        /// </summary>
        /// <param name="Name">按钮名称</param>
        /// <param name="ClassName">样式名称</param>
        /// <returns></returns>
        public String ViewHtmlButton(String Name, String ClassName)
        {
            return ViewHtmlButton(Name, ClassName, false);
        }

        /// <summary>
        /// 生成HTML按钮
        /// </summary>
        /// <param name="Name">按钮名称</param>
        /// <param name="Verification">是否验证</param>
        /// <returns></returns>
        public String ViewHtmlButton(String Name, Boolean Verification)
        {
            return ViewHtmlButton(Name, "CommandButton", Verification);
        }



        /// <summary>
        /// 生成HTML按钮
        /// </summary>
        /// <param name="Name">按钮名称</param>
        /// <param name="ClassName">样式名称</param>
        /// <param name="Verification">是否验证</param>
        /// <returns></returns>
        public String ViewHtmlButton(String Name, String ClassName, Boolean Verification)
        {
            Name = ViewLanguage(Name, Name);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("<input type=\"submit\" class=\"{0}\"", ClassName);
            sb.AppendFormat("name=\"{0}\" id=\"{1}\"  value=\"{2}\" OnClick=\"{4}\"  {3} />", ViewButton.UniqueID, ViewButton.ClientID, Name, Verification ? "lang=\"Submit\"" : "", ViewButton.OnClientClick);
            return sb.ToString();
        }




        /// <summary>
        /// 搜索按钮
        /// </summary>
        /// <param name="Name">按钮名称</param>
        /// <returns></returns>
        public String ViewHtmlButton(String Name)
        {
            return ViewHtmlButton(Name, "CommandButton", false);
        }

        /// <summary>
        /// 搜索按钮
        /// </summary>
        /// <returns></returns>
        public String ViewHtmlButton()
        {
            return ViewHtmlButton("Search", "CommandButton");
        }


        /// <summary>
        /// 显示搜索框控件
        /// </summary>
        /// <param name="Name">控件名</param>
        /// <returns></returns>
        public String ViewTextBox()
        {
            return ViewTextBox(String.Format("Text{0}", baseModule.Settings_ModuleID), "", 150, "NormalTextBox");
        }




        /// <summary>
        /// 显示搜索框控件
        /// </summary>
        /// <param name="Name">控件名</param>
        /// <returns></returns>
        public String ViewTextBox(String Name)
        {
            return ViewTextBox(Name, "", 150, "NormalTextBox");
        }



        /// <summary>
        /// 显示输入框控件
        /// </summary>
        /// <param name="Name">控件名</param>
        /// <param name="Width">宽度</param>
        /// <param name="ClassCss">样式名</param>
        /// <returns></returns>
        public String ViewTextBox(String Name, String DefaultValue, Int32 Width, String ClassCss, String Attribute = "")
        {
            //构造查询控件
            return String.Format("<input type=\"text\" id=\"{0}\" name=\"{0}\" Value=\"{1}\" style=\"width:{2}px;\" class=\"{3}\" {4}/>", Name, DefaultValue, Width > 50 ? Width : 150, ClassCss, Attribute);
        }

        /// <summary>
        /// 显示搜索框控件
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public String ViewSearchTextBox()
        {
            String CtlName = String.Format("SctlText{0}", baseModule.Settings_ModuleID);
            //页面查询值
            String CtlValue = WebHelper.GetStringParam(HttpContext.Current.Request, "Search", String.Empty).Trim();
            return ViewTextBox(CtlName, CtlValue, 180, "NormalTextBox", "x-webkit-speech " + Common.EnterClick(ViewButton.UniqueID));
        }

        /// <summary>
        /// 显示多行文本框
        /// </summary>
        /// <param name="Name">控件名</param>
        /// <param name="Width">宽度</param>
        /// <param name="Rows">行数</param>
        /// <returns></returns>
        public String ViewTextArea(String Name, String DefaultValue, Int32 Width, Int32 Rows)
        {
            return ViewTextArea(Name, DefaultValue, Width, Rows, "NormalTextBox");
        }

        /// <summary>
        /// 显示多行文本框
        /// </summary>
        /// <param name="Name">控件名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="Width">宽度</param>
        /// <param name="Rows">行数</param>
        /// <param name="ClassCss">样式名</param>
        /// <returns></returns>
        public String ViewTextArea(String Name, String DefaultValue, Int32 Width, Int32 Rows, String ClassCss)
        {
            return String.Format("<textarea id=\"{0}\" name=\"{0}\" style=\"width:{2}px; height:{3}px;\" rows=\"{4}\" class=\"{5}\">{1}</textarea>", Name, DefaultValue, Width, Rows * 15, Rows, ClassCss);
        }

        /// <summary>
        /// 显示评论提交框
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Width"></param>
        /// <param name="Rows"></param>
        /// <param name="Required"></param>
        /// <param name="Verification"></param>
        /// <returns></returns>
        public String ViewCommentTextArea(String Name, Int32 Width, Int32 Rows, Boolean Required, String Verification)
        {
            String ClassCss = String.Empty;

            if (!String.IsNullOrEmpty(Verification))
            {
                ClassCss = String.Format("custom[{0}]", Verification);
                if (!Required)
                    ClassCss = String.Format("validate[{0}]", ClassCss);
                else
                    ClassCss = String.Format("validate[required,{0}]", ClassCss);
            }
            else if (Required)
            {
                ClassCss = "validate[required]";
            }

            return ViewTextArea(String.Format("CtlTextArea{0}", baseModule.Settings_ModuleID), "", Width, Rows, ClassCss);
        }

        /// <summary>
        /// 显示评论提交框
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Width"></param>
        /// <param name="Rows"></param>
        /// <param name="Required"></param>
        /// <returns></returns>
        public String ViewCommentTextArea(String Name, Int32 Width, Int32 Rows, Boolean Required)
        {
            return ViewCommentTextArea(Name, Width, Rows, Required, "");
        }
        /// <summary>
        /// 显示评论提交框
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Rows"></param>
        /// <param name="Required"></param>
        /// <returns></returns>
        public String ViewCommentTextArea( Int32 Width, Int32 Rows, Boolean Required)
        {
            return ViewCommentTextArea(String.Format("CtlTextArea{0}", baseModule.Settings_ModuleID), Width, Rows, Required, "");
        }


        /// <summary>
        /// 显示评论提交框
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Rows"></param>
        /// <returns></returns>
        public String ViewCommentTextArea(Int32 Width, Int32 Rows, String ClassCss)
        {
            return ViewTextArea(String.Format("CtlTextArea{0}", ModuleId), "", Width, Rows, ClassCss + " :required");
        }


        /// <summary>
        /// 显示评论提交框
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Rows"></param>
        /// <returns></returns>
        public String ViewCommentTextArea(Int32 Width, Int32 Rows)
        {
            return ViewCommentTextArea(Width, Rows, "NormalTextBox");
        }

        /// <summary>
        /// 显示评论提交框
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Width"></param>
        /// <returns></returns>
        public String ViewCommentTextBox(String Name, Int32 Width)
        {
            return ViewCommentTextBox(Name, Width, "NormalTextBox");
        }


        /// <summary>
        /// 显示评论提交框
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Rows"></param>
        /// <returns></returns>
        public String ViewCommentTextBox(String Name, Int32 Width, String ClassCss)
        {
            return ViewTextBox(String.Format("CtlText{0}{1}", Name, baseModule.Settings_ModuleID), "", Width, ClassCss + " :required");
        }

        /// <summary>
        /// 显示评论提交框
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Width">宽度</param>
        /// <param name="Required">是否必填</param>
        /// <param name="Verification">验证类型</param>
        /// <returns></returns>
        public String ViewCommentTextBox(String Name, Int32 Width, Boolean Required, String Verification)
        {
            String ClassCss = String.Empty;
 
            if (!String.IsNullOrEmpty(Verification))
            {
                ClassCss = String.Format("custom[{0}]", Verification);
                if (!Required)
                    ClassCss = String.Format("validate[{0}]", ClassCss);
                else
                    ClassCss = String.Format("validate[required,{0}]", ClassCss);
            }
            else if(Required)
            {
                ClassCss = "validate[required]";
            }

            return ViewTextBox(String.Format("CtlText{0}{1}", Name, baseModule.Settings_ModuleID), "", Width, ClassCss);
        }
        /// <summary>
        /// 显示评论提交框
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Width">宽度</param>
        /// <param name="Required">是否必填</param>
        /// <returns></returns>
        public String ViewCommentTextBox(String Name, Int32 Width, Boolean Required)
        {
            return ViewCommentTextBox(Name, Width, Required, "");
        }


        
        #endregion

        #region "--关于用户--"


        public String ViewUser(Int32 UserID, String FieldName)
        {
            return ViewUser(UserID, FieldName, String.Empty);
        }

        public String ViewUser(Int32 UserID, String FieldName, String DefaultValue)
        {
            DotNetNuke.Entities.Users.UserInfo uInfo = new UserController().GetUser(baseModule.PortalId, UserID);
            return ViewUser(uInfo, FieldName, DefaultValue);
        }

        public String ViewUser(DotNetNuke.Entities.Users.UserInfo uInfo, String FieldName)
        {
            return ViewUser(uInfo, FieldName, String.Empty);
        }

        public String ViewUser(DotNetNuke.Entities.Users.UserInfo uInfo, String FieldName, String DefaultValue)
        {
            String FieldValue = DefaultValue;
            if (uInfo != null && uInfo.UserID > 0 && !String.IsNullOrEmpty(FieldName))
            {

                switch (FieldName.ToLower())
                {
                    case "username": FieldValue = uInfo.Username; break;
                    case "email": FieldValue = uInfo.Email; break;
                    case "firstName": FieldValue = uInfo.FirstName; break;
                    case "lastname": FieldValue = uInfo.LastName; break;
                    case "displayname": FieldValue = uInfo.DisplayName; break;
                    default:  FieldValue = Common.GetPropertyValue<String>(uInfo, FieldName,DefaultValue); break;
                }
            }
            return FieldValue;
        }

        public String ViewUserProfile(Int32 UserID, String FieldName)
        {
            return ViewUserProfile(UserID, FieldName, String.Empty);
        }

        public String ViewUserProfile(Int32 UserID, String FieldName, String DefaultValue)
        {
            DotNetNuke.Entities.Users.UserInfo uInfo = new UserController().GetUser(baseModule.PortalId, UserID);
            return ViewUserProfile(uInfo, FieldName, DefaultValue);
        }

        public String ViewUserProfile(DotNetNuke.Entities.Users.UserInfo uInfo, String FieldName)
        {
            return ViewUserProfile(uInfo, FieldName, String.Empty);
        }

        public String ViewUserProfile(DotNetNuke.Entities.Users.UserInfo uInfo, String FieldName, String DefaultValue)
        {
            String FieldValue = DefaultValue;
            if (uInfo != null && uInfo.UserID > 0 && !String.IsNullOrEmpty(FieldName))
            {
                String __FieldValue = uInfo.Profile.GetPropertyValue(FieldName);
                if (!String.IsNullOrEmpty(__FieldValue))
                {
                    FieldValue = __FieldValue;
                }
            }
            return FieldValue;
        }

        public String ShowUserProfile(UserInfo uInfo, String name)
        {
            String u = String.Empty;
            if (uInfo != null && uInfo.UserID > 0)
            {
                if (uInfo.Profile != null)
                {

                    u = uInfo.Profile.GetPropertyValue(name);
                    if (!String.IsNullOrEmpty(u) && uInfo.Profile.ProfileProperties[name] != null)
                    {
                        u = uInfo.Profile.ProfileProperties[name].PropertyValue;
                    }
                }
            }
            return !String.IsNullOrEmpty(u) ? HttpUtility.HtmlDecode(u) : "";
        }


 


        /// <summary>
        /// 显示用户头像
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns></returns>
        public String ViewUserPic(Int32 UserID)
        {
            if (UserID > 0)
            {
                return ViewUserPic(new UserController().GetUser(baseModule.PortalId, UserID));
            }
            else
            {
                return ViewUserPic(new DotNetNuke.Entities.Users.UserInfo());
            }
        }


        /// <summary>
        /// 显示用户头像
        /// </summary>
        /// <param name="uInfo">用户信息</param>
        /// <returns></returns>
        public String ViewUserPic(DotNetNuke.Entities.Users.UserInfo uInfo)
        {
            String UserPic = baseModule.FullPortalUrl(String.Format("{0}Resource/images/no_user.png", baseModule.ModulePath));

            if (uInfo != null && uInfo.UserID > 0)
            {
                UserPic = ViewUserPic(uInfo.Email);
            }
            return UserPic;
        }

         

        /// <summary>
        /// 显示用户头像
        /// </summary>
        /// <param name="EmailAddress"></param>
        /// <returns></returns>
        public String ViewUserPic(String EmailAddress)
        {
            String UserPic = String.Format("{0}Resource/images/no_user.png", baseModule.ModulePath);
            if (UserPic.ToLower().IndexOf("http://") < 0 && UserPic.ToLower().IndexOf("https://") < 0)
            {
                UserPic = baseModule.FullPortalUrl(UserPic);
            }
            if (!String.IsNullOrEmpty(EmailAddress))
            {

               EnumAvatarRating AvatarRating =(EnumAvatarRating)( ClientZone_Settings["ClientZone_AvatarRating"] != null && !string.IsNullOrEmpty(ClientZone_Settings["ClientZone_AvatarRating"].ToString()) ? Convert.ToInt32(ClientZone_Settings["ClientZone_AvatarRating"]) : (Int32)EnumAvatarRating.R);
               EnumDefaultAvatar DefaultAvatar = (EnumDefaultAvatar)(ClientZone_Settings["ClientZone_DefaultAvatar"] != null && !string.IsNullOrEmpty(ClientZone_Settings["ClientZone_DefaultAvatar"].ToString()) ? Convert.ToInt32(ClientZone_Settings["ClientZone_DefaultAvatar"]) : (Int32)EnumDefaultAvatar.mm);
               Int32  ClientZone_AvatarSize =  ClientZone_Settings["ClientZone_AvatarSize"] != null && !string.IsNullOrEmpty(ClientZone_Settings["ClientZone_AvatarSize"].ToString()) ? Convert.ToInt32(ClientZone_Settings["ClientZone_AvatarSize"]) : 62;

                //加密邮件地址
                EmailAddress = Common.md5(EmailAddress.ToLower().Trim(), 32).ToLower();
                UserPic = String.Format("{4}://www.gravatar.com/avatar/{0}?s={1}&d={2}&r={3}", EmailAddress, ClientZone_AvatarSize, DefaultAvatar, AvatarRating, baseModule.IsSSL ? "https" : "http");
            }
            return UserPic;
        }

        /// <summary>
        /// 显示作者的头像
        /// 5.2以后的版本通过Profile取
        /// </summary>
        /// <param name="Author"></param>
        /// <returns></returns>
        public String ViewAuthorPic(AuthorEntity Author)
        {
            return ViewAuthorPic(Author.CreateUser);
        }

        /// <summary>
        /// 显示作者的头像
        /// 5.2以后的版本通过Profile取
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String ViewAuthorPic(Playngo_ClientZone_Event DataItem)
        {
            return ViewAuthorPic(DataItem.CreateUser);
        }

         

        /// <summary>
        /// 显示作者的头像
        /// 5.2以后的版本通过Profile取
        /// </summary>
        /// <param name="AuthorID"></param>
        /// <returns></returns>
        public String ViewAuthorPic(Int32 AuthorID)
        {
            //获取用户的信息
            UserInfo AuthorInfo = new UserController().GetUser(baseModule.PortalId, AuthorID);
            return ViewAuthorPic(AuthorInfo);
        }


        /// <summary>
        /// 显示作者的头像通过Profile取
        /// 5.2以后的版本通过Profile取
        /// </summary>
        /// <param name="AuthorInfo"></param>
        /// <returns></returns>
        public String ViewAuthorPic(UserInfo AuthorInfo)
        {
            String UserPic = baseModule.FullPortalUrl(String.Format("{0}Resource/images/no_user.png", baseModule.ModulePath));

            if (AuthorInfo != null && AuthorInfo.UserID > 0)
            {

                Int32 ClientZone_AvatarType = baseModule.ClientZone_Settings["ClientZone_AvatarType"] != null ? Convert.ToInt32(baseModule.ClientZone_Settings["ClientZone_AvatarType"]) : (Int32)EnumAvatarType.GravatarUserProfile;
                if (ClientZone_AvatarType == (Int32)EnumAvatarType.GravatarUserProfile)
                {
                    UserPic = ViewAuthorPic2(AuthorInfo);
                }
                else if (ClientZone_AvatarType == (Int32)EnumAvatarType.UserProfile)
                {
                    String Photo = ViewUserProfile(AuthorInfo, "Photo");
                    Int32 result = 0;
                    if (!String.IsNullOrEmpty(Photo) && int.TryParse(Photo.Trim(), out result) && result > 0)
                    {
                        try
                        {
                            UserPic = Globals.LinkClick(String.Format("fileid={0}", result), baseModule.TabId, baseModule.ModuleId);
                        }
                        catch { }
                    }
                }
                else if (ClientZone_AvatarType == (Int32)EnumAvatarType.Gravatar && !String.IsNullOrEmpty(AuthorInfo.Email))
                {
                    UserPic = ViewUserPic(AuthorInfo.Email);
                }
            }
            return UserPic;
        }

        /// <summary>
        /// 显示作者图片,UserProfile & Gravatar
        /// </summary>
        /// <param name="AuthorInfo"></param>
        /// <returns></returns>
        public String ViewAuthorPic2(UserInfo AuthorInfo)
        {
            String Photo = String.Empty;
            if (AuthorInfo != null && AuthorInfo.UserID > 0)
            {
                //通过反射获取Photo
                Photo = AuthorInfo.Profile.GetPropertyValue("Photo"); //
                Int32 result = 0;
                if (!String.IsNullOrEmpty(Photo) && int.TryParse(Photo.Trim(), out result) && result > 0)
                {
                    try
                    {
                        Photo = Globals.LinkClick(String.Format("fileid={0}", result), baseModule.TabId, baseModule.ModuleId);
                    }
                    catch
                    {
                        Photo = ViewUserPic(AuthorInfo.Email);
                    }
                }
                else
                {
                    Photo = ViewUserPic(AuthorInfo.Email);
                }
            }
            return Photo;
        }



        /// <summary>
        /// 显示作者的简介
        /// 5.2以后的版本通过Profile取
        /// </summary>
        /// <param name="Author"></param>
        /// <returns></returns>
        public String ViewAuthorBiography(AuthorEntity Author)
        {
            return ViewAuthorBiography(Author.CreateUser);
        }

        /// <summary>
        /// 显示作者的简介
        /// 5.2以后的版本通过Profile取
        /// </summary>
        /// <param name="AuthorInfo"></param>
        /// <returns></returns>
        public String ViewAuthorBiography(UserInfo AuthorInfo)
        {
            String Biography = String.Empty;
            if (AuthorInfo != null && AuthorInfo.UserID > 0)
            {
                //通过反射获取Biography
                Biography = AuthorInfo.Profile.GetPropertyValue("Biography"); //Common.GetValueFromAnonymousType<String>(AuthorInfo.Profile,"Biography");
                if (String.IsNullOrEmpty(Biography))
                {
                    Biography = String.Empty;
                }
            }
            return HttpUtility.HtmlDecode(Biography);

        }

        /// <summary>
        /// 显示作者的简介
        /// 5.2以后的版本通过Profile取
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String ViewAuthorBiography(Playngo_ClientZone_Event DataItem)
        {

            return ViewAuthorBiography(DataItem.CreateUser);

        }
        /// <summary>
        /// 显示作者的简介
        /// 5.2以后的版本通过Profile取
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String ViewAuthorBiography(Int32 AuthorID)
        {
            //获取用户的信息
            UserInfo AuthorInfo = new UserController().GetUser(baseModule.PortalId, AuthorID);
            return ViewAuthorBiography(AuthorInfo);
        }


        #endregion

        #region "--显示控件存放的值--"

         



        /// <summary>
        /// 显示URL控件存放的值
        /// </summary>
        /// <param name="UrlValue"></param>
        /// <returns></returns>
        public String ViewLinkUrl(String UrlValue, String DefaultValue, int PortalId)
        {
            if (!String.IsNullOrEmpty(UrlValue) && UrlValue != "0")
            {
                if (UrlValue.IndexOf("FileID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    int FileID = 0;
                    if (int.TryParse(UrlValue.Replace("FileID=", ""), out FileID) && FileID > 0)
                    {
                        var fi = DotNetNuke.Services.FileSystem.FileManager.Instance.GetFile(FileID);
                        if (fi != null && fi.FileId > 0)
                        {
                            DefaultValue = string.Format("{0}{1}{2}", ClientZone_PortalSettings.HomeDirectory, fi.Folder, baseModule.Server.UrlPathEncode(fi.FileName));
                        }
                    }
                }
                else if (UrlValue.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {


                    int MediaID = 0;
                    if (int.TryParse(UrlValue.Replace("MediaID=", ""), out MediaID) && MediaID > 0)
                    {
                        Playngo_ClientZone_Files Multimedia = Playngo_ClientZone_Files.FindByID(MediaID);
                        if (Multimedia != null && Multimedia.ID > 0)
                        {
                            DefaultValue =  baseModule.Server.UrlPathEncode(baseModule.GetPhotoPath(Multimedia.FilePath));// String.Format("{0}{1}", bpm.MemberGroup_PortalSettings.HomeDirectory, Multimedia.FilePath);
                        }
                    }
                }
                else if (UrlValue.IndexOf("TabID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {

                    DefaultValue = Globals.NavigateURL(Convert.ToInt32(UrlValue.Replace("TabID=", "")), false, ClientZone_PortalSettings, Null.NullString, "", "");

                }
                else
                {
                    DefaultValue = UrlValue;
                }
            }
            return DefaultValue;

        }




        public String ViewLinkUrl(String UrlValue, String DefaultValue,BasePage basePage)
        {
            if (!String.IsNullOrEmpty(UrlValue) && UrlValue != "0")
            {
                if (UrlValue.IndexOf("FileID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    int FileID = 0;
                    if (int.TryParse(UrlValue.Replace("FileID=", ""), out FileID) && FileID > 0)
                    {
                        var fi = DotNetNuke.Services.FileSystem.FileManager.Instance.GetFile(FileID);
                        if (fi != null && fi.FileId > 0)
                        {
                            DefaultValue = string.Format("{0}{1}{2}", ClientZone_PortalSettings.HomeDirectory, fi.Folder, basePage.Server.UrlPathEncode(fi.FileName));
                        }
                    }
                }
                else if (UrlValue.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {


                    int MediaID = 0;
                    if (int.TryParse(UrlValue.Replace("MediaID=", ""), out MediaID) && MediaID > 0)
                    {
                        Playngo_ClientZone_Files Multimedia = Playngo_ClientZone_Files.FindByID(MediaID);
                        if (Multimedia != null && Multimedia.ID > 0)
                        {
                            DefaultValue = basePage.Server.UrlPathEncode(basePage.GetPhotoPath(Multimedia.FilePath));// String.Format("{0}{1}", bpm.MemberGroup_PortalSettings.HomeDirectory, Multimedia.FilePath);
                        }
                    }
                }
                else if (UrlValue.IndexOf("TabID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {

                    DefaultValue = Globals.NavigateURL(Convert.ToInt32(UrlValue.Replace("TabID=", "")), false, ClientZone_PortalSettings, Null.NullString, "", "");

                }
                else
                {
                    DefaultValue = UrlValue;
                }
            }
            return DefaultValue;

        }


        public String ViewLinkUrl(String UrlValue, String DefaultValue)
        {
            return ViewLinkUrl(UrlValue, DefaultValue, PortalID);
        }


        public String ViewLinkUrl(String UrlValue)
        {
            String DefaultValue = String.Empty;
            if (UrlValue.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                DefaultValue = String.Format("{0}Resource/images/no_image.png", ModulePath);
            }
            return ViewLinkUrl(UrlValue, DefaultValue, PortalID);
        }

        /// <summary>
        /// 显示媒体库文件的地址
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public String ViewMediaUrl(Playngo_ClientZone_Files FileItem)
        {
            return FullPortalUrl( baseModule.Server.UrlPathEncode(baseModule.GetPhotoPath(FileItem.FilePath)));
        }



        #endregion

        #region "关于ajax参数的定义"

        /// <summary>
        /// ajax的基本参数
        /// </summary>
        public String AjaxParameters
        {
            get { return String.Format(" data-moduleid=\"{0}\" data-tabid=\"{1}\" data-portalid=\"{2}\" data-modulepath=\"{3}\" ", baseModule.ModuleId, baseModule.TabId, baseModule.PortalId, baseModule.ModulePath); }
        }




        #endregion

        #region "--XML参数读取--"


        /// <summary>
        /// 读取参数
        /// </summary>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewSetting(String Name, object DefaultValue)
        {
            return ClientZone_Settings[Name] != null ? Common.FormatValue(ClientZone_Settings[Name].ToString(), DefaultValue.GetType()) : DefaultValue;
        }

        /// <summary>
        /// 读取参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewSettingT<T>(String Name, object DefaultValue)
        {
            var o = ViewSetting(Name, DefaultValue);
            return (T)Convert.ChangeType(o, typeof(T));
        }




        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="Options">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewItemSetting(String Options, String Name, object DefaultValue)
        {
            return TemplateFormat.ViewItemSettingByStatic(Options, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewItemSettingT<T>(String Options, String Name, object DefaultValue)
        {
            return (T)Convert.ChangeType(ViewItemSetting(Options, Name, DefaultValue), typeof(T));
        }

        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="Options">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public static object ViewItemSettingByStatic(String Options, String Name, object DefaultValue)
        {
            object o = DefaultValue;
            if (!String.IsNullOrEmpty(Options))
            {
                try
                {
                    List<KeyValueEntity> ItemSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(Options);
                    KeyValueEntity KeyValue = ItemSettings.Find(r1 => r1.Key.ToLower() == Name.ToLower());
                    if (KeyValue != null && !String.IsNullOrEmpty(KeyValue.Key))
                    {
                        o = KeyValue.Value;
                    }

                }
                catch
                {

                }
            }
            return o;
        }






        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewItemSetting(Playngo_ClientZone_Event DataItem, String Name, object DefaultValue)
        {
            return ViewItemSetting(DataItem.Options, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewItemSettingT<T>(Playngo_ClientZone_Event DataItem, String Name, object DefaultValue)
        {
            return ViewItemSettingT<T>(DataItem.Options, Name, DefaultValue);
        }


        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewItemSetting(Playngo_ClientZone_DownloadFile DataItem, String Name, object DefaultValue)
        {
            return ViewItemSetting(DataItem.Options, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewItemSettingT<T>(Playngo_ClientZone_DownloadFile DataItem, String Name, object DefaultValue)
        {
            return ViewItemSettingT<T>(DataItem.Options, Name, DefaultValue);
        }



        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewItemSetting(Playngo_ClientZone_GameSheet DataItem, String Name, object DefaultValue)
        {
            return ViewItemSetting(DataItem.Options, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewItemSettingT<T>(Playngo_ClientZone_GameSheet DataItem, String Name, object DefaultValue)
        {
            return ViewItemSettingT<T>(DataItem.Options, Name, DefaultValue);
        }



        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewItemSetting(Playngo_ClientZone_Campaign DataItem, String Name, object DefaultValue)
        {
            return ViewItemSetting(DataItem.Options, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewItemSettingT<T>(Playngo_ClientZone_Campaign DataItem, String Name, object DefaultValue)
        {
            return ViewItemSettingT<T>(DataItem.Options, Name, DefaultValue);
        }




        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewItemSetting(Playngo_ClientZone_FileType DataItem, String Name, object DefaultValue)
        {
            return ViewItemSetting(DataItem.Options, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewItemSettingT<T>(Playngo_ClientZone_FileType DataItem, String Name, object DefaultValue)
        {
            return ViewItemSettingT<T>(DataItem.Options, Name, DefaultValue);
        }



        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewItemSetting(Playngo_ClientZone_Jurisdiction DataItem, String Name, object DefaultValue)
        {
            return ViewItemSetting(DataItem.Options, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewItemSettingT<T>(Playngo_ClientZone_Jurisdiction DataItem, String Name, object DefaultValue)
        {
            return ViewItemSettingT<T>(DataItem.Options, Name, DefaultValue);
        }



        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewItemSetting(Playngo_ClientZone_GameCategory DataItem, String Name, object DefaultValue)
        {
            return ViewItemSetting(DataItem.Options, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewItemSettingT<T>(Playngo_ClientZone_GameCategory DataItem, String Name, object DefaultValue)
        {
            return ViewItemSettingT<T>(DataItem.Options, Name, DefaultValue);
        }



        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewItemSetting(Playngo_ClientZone_CampaignCategory DataItem, String Name, object DefaultValue)
        {
            return ViewItemSetting(DataItem.Options, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewItemSettingT<T>(Playngo_ClientZone_CampaignCategory DataItem, String Name, object DefaultValue)
        {
            return ViewItemSettingT<T>(DataItem.Options, Name, DefaultValue);
        }



        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewItemSetting(Playngo_ClientZone_DynamicModule DataItem, String Name, object DefaultValue)
        {
            return ViewItemSetting(DataItem.Options, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewItemSettingT<T>(Playngo_ClientZone_DynamicModule DataItem, String Name, object DefaultValue)
        {
            return ViewItemSettingT<T>(DataItem.Options, Name, DefaultValue);
        }



        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewItemSetting(Playngo_ClientZone_DynamicItem DataItem, String Name, object DefaultValue)
        {
            return ViewItemSetting(DataItem.Options, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewItemSettingT<T>(Playngo_ClientZone_DynamicItem DataItem, String Name, object DefaultValue)
        {
            return ViewItemSettingT<T>(DataItem.Options, Name, DefaultValue);
        }

        #endregion

        #region "--Url参数读取--"
        /// <summary>
        /// 请求参数读取
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="errorReturn"></param>
        /// <returns></returns>
        public Int32 GetIntParam(string paramName, int errorReturn)
        {
            return WebHelper.GetIntParam(baseModule.Request, paramName, errorReturn);
        }
        /// <summary>
        /// 请求参数读取
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="errorReturn"></param>
        /// <returns></returns>
        public  string GetStringParam( string paramName, string errorReturn)
        {
            return WebHelper.GetStringParam(baseModule.Request, paramName, errorReturn);
        }


        #endregion



        #region "--动态模块处理--"

        public List<Playngo_ClientZone_DynamicItem> FindItemsByDynamicID(List<Playngo_ClientZone_DynamicItem> DataItems,Int32 DynamicID)
        {
            var DynamicItems = new List<Playngo_ClientZone_DynamicItem>();

            if (DataItems != null)
            {
                var TempDynamicItems = DataItems.FindAll(r => r.DynamicID == DynamicID);
                if (TempDynamicItems != null && TempDynamicItems.Count > 0)
                {
                    DynamicItems.AddRange(TempDynamicItems);
                }
               
            }
            return DynamicItems; ;
        }


        #endregion


        #region "语言翻译"

        /// <summary>
        /// 显示多语言
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewLanguage(String Title, String DefaultValue)
        {
         

            String LocalResourceFile = String.Format("{0}App_LocalResources/{1}.ascx.resx", ModulePath, TemplateName);

            return ViewResourceText(Title, DefaultValue, "Text", LocalResourceFile);
        }



        /// <summary>
        /// 显示资源文件内容
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewResourceText(String Title, String DefaultValue)
        {
            return ViewResourceText(Title, DefaultValue, "Text", baseModule.LocalResourceFile);
        }


        /// <summary>
        /// 显示资源文件内容
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="Extension"></param>
        /// <param name="LocalResourceFile"></param>
        /// <returns></returns>
        public String ViewResourceText(String Title, String DefaultValue, String Extension, String LocalResourceFile)
        {
            String _Title = Localization.GetString(String.Format("{0}.{1}", Title, Extension), LocalResourceFile);
            if (String.IsNullOrEmpty(_Title))
            {
                _Title = DefaultValue;
            }
            return _Title;
        }

        #endregion

        #region "特殊处理和转换"
        /// <summary>
        /// 转换时间
        /// </summary>
        /// <param name="StringDateTime"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public String ConvertDateTitme(String StringDateTime, String format)
        {
            String ReturnTime = StringDateTime;
            //发布状态和时间
            DateTime oTime = xUserTime.UtcTime();
            string[] expectedFormats = { "G", "g", "f", "F" };
            if (DateTime.TryParseExact(StringDateTime, "MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out oTime))
            {
                ReturnTime = xUserTime.ServerTime(oTime).ToString(format, CultureInfo.InvariantCulture);
            }
            return ReturnTime;
        }


        /// <summary>
        /// 转换时间
        /// </summary>
        /// <param name="StringDateTime"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public String ConvertDate(String StringDate, String format)
        {
            String ReturnTime = StringDate;
            //发布状态和时间
            DateTime oTime = xUserTime.UtcTime();
            string[] expectedFormats = { "G", "g", "f", "F" };
            if (DateTime.TryParseExact(StringDate, "MM/dd/yyyy", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out oTime))
            {
                ReturnTime = xUserTime.ServerTime(oTime).ToString(format, CultureInfo.InvariantCulture);
            }
            return ReturnTime;
        }
        #endregion


        #endregion



        #region "构造"


        public TemplateFormat()
        { }

        public TemplateFormat(BaseModule _base)
        {
            baseModule = _base;
        }

        public TemplateFormat(BasePage _base)
        {
            basePage = _base;
        }

        #endregion

    }
}