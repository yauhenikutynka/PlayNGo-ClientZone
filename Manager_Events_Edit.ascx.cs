using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_Events_Edit : BaseModule
    {





        #region "属性"
        /// <summary>
        /// 文章编号
        /// </summary>
        public Int32 EventID = WebHelper.GetIntParam(HttpContext.Current.Request, "ID", 0);






        private Playngo_ClientZone_Event _EventItem;
        /// <summary>
        /// 文章项
        /// </summary>
        public Playngo_ClientZone_Event EventItem
        {
            get
            {
                if (!(_EventItem != null && _EventItem.ID > 0))
                {
                    if (EventID > 0)
                        _EventItem = Playngo_ClientZone_Event.FindByKeyForEdit(EventID);
                    else
                        _EventItem = new Playngo_ClientZone_Event();
                }
                return _EventItem;
            }
            set { _EventItem = value; }
        }

        private List<KeyValueEntity> _ItemSettings;
        /// <summary>
        /// 封装的参数集合
        /// </summary>
        public List<KeyValueEntity> ItemSettings
        {
            get
            {
                if (!(_ItemSettings != null && _ItemSettings.Count > 0))
                {
                    if (EventItem != null && EventItem.ID > 0 && !String.IsNullOrEmpty(EventItem.Options))
                    {
                        try
                        {
                            _ItemSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(EventItem.Options);
                        }
                        catch
                        {
                            _ItemSettings = new List<KeyValueEntity>();
                        }
                    }
                    else
                        _ItemSettings = new List<KeyValueEntity>();
                }
                return _ItemSettings;
            }
        }





        private List<SettingEntity> _Event_ItemSettingDB = new List<SettingEntity>();
        /// <summary>
        /// 事件的动态选项
        /// </summary>
        public List<SettingEntity> Event_ItemSettingDB
        {
            get
            {
                if (!(_Event_ItemSettingDB != null && _Event_ItemSettingDB.Count > 0))
                {
                    //全局的数据项
                    String ItemSettingGlobalPath = Server.MapPath(String.Format("{0}Resource/xml/Config.Setting.Event.xml", ModulePath));
                    if (File.Exists(ItemSettingGlobalPath))
                    {
                        XmlFormat xf = new XmlFormat(ItemSettingGlobalPath);
                        _Event_ItemSettingDB.AddRange(xf.ToList<SettingEntity>());
                    }
                }
                return _Event_ItemSettingDB;
            }
        }





        /// <summary>
        /// 提示操作类
        /// </summary>
        MessageTips mTips = new MessageTips();
        #endregion


        #region "事件"

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    //绑定页面项
                    BindPageItem();
                    // 绑定方案项
                    BindDataItem();
                }
                //绑定设置参数到页面
                BindGroupsToPage();
                BindDownFilesToPage();
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }

        protected void Page_Init(System.Object sender, System.EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 设置方案项
                Playngo_ClientZone_Event Event = new Playngo_ClientZone_Event();
                Boolean SaveResult = SaveDataItem(-1, out Event);

                if (SaveResult)
                {
                    mTips.LoadMessage("SaveSuccess", EnumTips.Success, this, new String[] { Event.Title });
                    //Response.Redirect(xUrl("EventID", Event.ID.ToString(), "EditLoading", String.Format("ReturnUrl={0}", HttpUtility.UrlEncode( xUrl("Events")))) ,false);
                    Response.Redirect(xUrl("ID", Event.ID.ToString(), "Events-Edit"), false);
                }
                else
                {
                    mTips.IsPostBack = false;
                    mTips.LoadMessage("SaveError", EnumTips.Warning, this, new String[] { Event.Title });
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {

                Response.Redirect(xUrl("Events"), false);
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        /// <summary>
        /// 保存草稿
        /// </summary>
        protected void cmdSaveDraft_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 EventStatus = (Int32)EnumStatus.Draft;
                Playngo_ClientZone_Event Event = new Playngo_ClientZone_Event();
                Boolean SaveResult = SaveDataItem(EventStatus, out Event);



                if (SaveResult)
                {

                    mTips.LoadMessage("SaveSuccess", EnumTips.Success, this, new String[] { Event.Title });
                    Response.Redirect(xUrl("ID", Event.ID.ToString(), "Events-Edit"), false);
                }
                else
                {
                    mTips.IsPostBack = false;
                    mTips.LoadMessage("SaveError", EnumTips.Success, this, new String[] { Event.Title });
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        /// <summary>
        /// 文章软删除(删除到回收站)
        /// </summary>
        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {

                Playngo_ClientZone_Event Event = Playngo_ClientZone_Event.FindByKeyForEdit(EventID);
                Event.Status = (Int32)EnumStatus.Recycle;

                if (Event != null && Event.ID > 0 && Event.Update() > 0)
                {
          

                    //操作成功
                    mTips.LoadMessage("DeleteEventSuccess", EnumTips.Success, this, new String[] { Event.Title });
                    Response.Redirect(xUrl("Events"), false);
                }
                else
                {
                    mTips.IsPostBack = false;
                    //操作失败
                    mTips.LoadMessage("DeleteEventError", EnumTips.Success, this, new String[] { Event.Title });
                }



            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        protected void RepeaterOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SettingEntity ThemeSetting = e.Item.DataItem as SettingEntity;

                KeyValueEntity KeyValue = ItemSettings.Find(r1 => r1.Key == ThemeSetting.Name);
                if (KeyValue != null && !String.IsNullOrEmpty(KeyValue.Key))
                {
                    ThemeSetting.DefaultValue = KeyValue.Value.ToString();
                }

                //构造输入控件
                PlaceHolder ThemePH = e.Item.FindControl("ThemePH") as PlaceHolder;

                #region "创建控件"
                ControlHelper ctl = new ControlHelper(this);

                ThemePH.Controls.Add((Control)ctl.ViewControl(ThemeSetting));
                #endregion

                Literal liTitle = e.Item.FindControl("liTitle") as Literal;
                liTitle.Text = String.Format("<label class=\"col-sm-3 control-label\" for=\"{1}\">{0}:</label>", !String.IsNullOrEmpty(ThemeSetting.Alias) ? ThemeSetting.Alias : ThemeSetting.Name, ctl.ViewControlID(ThemeSetting));

                if (!String.IsNullOrEmpty(ThemeSetting.Description))
                {
                    Literal liHelp = e.Item.FindControl("liHelp") as Literal;
                    liHelp.Text = String.Format("<span class=\"help-block\"><i class=\"fa fa-info-circle\"></i> {0}</span>", ThemeSetting.Description);
                }
            }
        }


        /// <summary>
        /// 分组绑定事件
        /// </summary>
        protected void RepeaterGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater RepeaterOptions = e.Item.FindControl("RepeaterOptions") as Repeater;
                KeyValueEntity GroupItem = e.Item.DataItem as KeyValueEntity;
                int OptionCount = 0;
                BindOptionsToPage(RepeaterOptions, GroupItem.Key, out OptionCount);

                if (OptionCount == 0)
                {
                    e.Item.Visible = false;
                }

            }
        }

        #endregion

        #region "方法"
        /// <summary>
        /// 绑定页面项
        /// </summary>
        private void BindPageItem()
        {
            if (EventID > 0)
            {
                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");
                cmdDelete.Visible = true;

      
                hlPreview.NavigateUrl = CommonFriendlyUrls.FriendlyUrl(EventItem, ViewSettingT<Int32>("ClientZone_DisplayTab_Events", TabId), true, this);
                hlPreview.Visible = true;



            }
            else
            {
                hlExportCSV.Attributes.Add("disabled", "disabled");
            }


            hlAddJurisdiction.NavigateUrl = xUrl("Jurisdictions");
            hlAddJurisdiction.Attributes.Add("onclick", String.Format("return confirm('{0}');", Localization.GetString("hlAddJurisdiction.Confirm", this.LocalResourceFile)));


            //插入图片按钮的连接

            hlAddMedia.Attributes.Add("data-href", String.Format("{0}Resource_FeaturedImage.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}&Insert=true", ModulePath, PortalId, TabId, ModuleId, language));



            ////插入用户按钮的连接
            hlEventAuthor.Attributes.Add("data-href", String.Format("{0}Resource_EventAuthors.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}", ModulePath, PortalId, TabId, ModuleId, language));

            //绑定状态代码
            WebHelper.BindList(ddlEventStatus, typeof(EnumStatus));


            WebHelper.BindList(ddlNotificationStatus, typeof(EnumNotificationStatus));


            //非管理员时需要屏蔽掉的按钮
            if (!IsAdministrator && !IsAdmin)
            {
            
                ddlEventStatus.Visible = false;

                divEventAuthor.Visible = false;
                divFeature.Visible = false;
                divTopStatus.Visible = false;

            }
      


 

 


        }


        /// <summary>
        /// 绑定数据项
        /// </summary>
        private void BindDataItem()
        {
            Playngo_ClientZone_Event Event = EventItem;
            TemplateFormat xf = new TemplateFormat(this);

            //验证文章是否存在
            if (EventID > 0 && (Event == null || EventID != Event.ID))
            {
                //需要给出提示,载入文章错误
                mTips.LoadMessage("LoadingEventError", EnumTips.Error, this, new String[] { "" });
                Response.Redirect(xUrl("Events"), false);
            }

            if (Event == null) Event = new Playngo_ClientZone_Event();



            //这里需要验证一下权限,当作者进入到别人的文章时，需要跳到列表页面
            if (Event != null && Event.ID > 0 && (!IsAdministrator && !IsAdmin) && Event.CreateUser != UserId)
            {
                //需要给出提示,你无权编辑其他作者的文章
                mTips.LoadMessage("NoPermissionEventAlert", EnumTips.Warning, this, new String[] { Event.Title });
                Response.Redirect(xUrl("Events"), false);
            }


            if (Event != null && Event.ID > 0)
            {
                liLastUpdated.Text = Event.LastTime.ToString("MM/dd/yyyy hh:mm tt", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }

            //导出CSV链接
            hlExportCSV.NavigateUrl = xf.GoDownloadCSVToEventUrl(Event);


            txtTitle.Text = Common.RestoreEscape(Event.Title);
            txtContentText.Text = HttpUtility.HtmlDecode(Event.ContentText);


            txtAddress.Text = Event.Address  ;
            txtLocationX.Text = Event.LocationX ;
            txtLocationY.Text = Event.LocationY  ;



            cbIncludeNotification.Checked = Event.NotifyInclude == 1;
            WebHelper.SelectedListByValue(ddlNotificationStatus, Event.NotifyStatus);
            cbNotifySubscribers.Checked = Event.NotifySubscribers == 1;


            txtStartDate.Text = Event.StartTime.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            txtStartTime.Text = Event.StartTime.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            txtReleaseDate.Text = Event.ReleaseDate.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            txtReleaseTime.Text = Event.ReleaseDate.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            txtDisableDate.Text = Event.EndTime.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            txtDisableTime.Text = Event.EndTime.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);



            //增加权限用户
            DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();
            WebHelper.BindList(cblPermissionsRoles, rc.GetPortalRoles(PortalId), "RoleName", "RoleName");
            WebHelper.SelectedListMultiByValue(cblPermissionsRoles, Event.Per_Roles);

            cbPermissionsAllUsers.Checked = Event.Per_AllUsers == 0;




            //区域
            cbAllJurisdictions.Checked = Event.Per_AllJurisdictions == 0;
            WebHelper.BindList(cblJurisdictions, Playngo_ClientZone_Jurisdiction.GetAllCheckList(ModuleId), "Name", "ID");
            WebHelper.SelectedListMultiByValue(cblJurisdictions, Event.Per_Jurisdictions);
 





            Event.CreateUser = Event.CreateUser == 0 ? UserId : Event.CreateUser;
            UserInfo CreateUserInfo = UserController.GetUserById(PortalId, Event.CreateUser);
            hfEventAuthor.Value = Event.CreateUser.ToString();
            lbEventAuthor.Text = CreateUserInfo != null && CreateUserInfo.UserID > 0 ? CreateUserInfo.DisplayName : "None";
            imgEventAuthor.ImageUrl = ViewUserPic(CreateUserInfo);

          
             WebHelper.SelectedListByValue(ddlEventStatus, Event.Status);//管理员看到的文章状态
         
 



        }

        /// <summary>
        /// 保存文章
        /// </summary>
        /// <param name="EventStatus">文章状态(为-1的时候取选项的值)</param>
        private Boolean SaveDataItem(Int32 EventStatus, out Playngo_ClientZone_Event Event)
        {



            Boolean SaveResult = false;

            Event = EventItem;

            Event.Title = Common.ReplaceEscape(txtTitle.Text);
            Event.ContentText = HttpUtility.HtmlEncode(txtContentText.Text);

            Event.Address = txtAddress.Text;
            Event.LocationX = txtLocationX.Text;
            Event.LocationY =  txtLocationY.Text;
 

            Event.NotifyInclude = cbIncludeNotification.Checked ? 1 : 0;
            Event.NotifyStatus = WebHelper.GetIntParam(Request, ddlNotificationStatus.UniqueID, 0);
            //Event.NotifySubscribers = cbNotifySubscribers.Checked ? 1 : 0;
 

            //权限
            Event.Per_AllUsers = cbPermissionsAllUsers.Checked ? 0 : 1;

            String textStr, idStr = String.Empty;
            WebHelper.GetSelected(cblPermissionsRoles, out textStr, out idStr);
            Event.Per_Roles = idStr;


            //区域
            Event.Per_AllJurisdictions = cbAllJurisdictions.Checked ? 0 : 1;
            String JurisdictionTexts, JurisdictionIDs = String.Empty;
            WebHelper.GetSelected(cblJurisdictions, out JurisdictionTexts, out JurisdictionIDs);
            Event.Per_Jurisdictions = JurisdictionIDs;

            //文章的发布状态
            if (EventStatus == -1)//如果没有指定状态就取控件的
            {
            
               Event.Status = WebHelper.GetIntParam(Request, ddlEventStatus.UniqueID, (Int32)EnumStatus.Published);
               
            }
            else
            {
                Event.Status = EventStatus;
            }

            #region "关于动态选项的代码"

            List<KeyValueEntity> list = new List<KeyValueEntity>();
            List<SettingEntity> ItemSettingDB = Event_ItemSettingDB;
            if (ItemSettingDB != null && ItemSettingDB.Count > 0)
            {
                ControlHelper ControlItem = new ControlHelper(ModuleId);

                foreach (SettingEntity ri in ItemSettingDB)
                {
                    KeyValueEntity item = new KeyValueEntity();
                    item.Key = ri.Name;
                    item.Value = ControlHelper.GetWebFormValue(ri, this);
                    list.Add(item);
                }
            }
         

            //从动态选项中获取Roadmap启用状态
            if (list.Exists(r => r.Key == "IncludeRoadmap"))
            {
                KeyValueEntity Entity = list.Find(r => r.Key == "IncludeRoadmap");
                Event.IncludeRoadmap = Convert.ToBoolean(Entity.Value) ? 0 : 1;
            }

            //伪静态URL
            String FriendlyUrl = String.Empty;
            if (list.Exists(r => r.Key == "FriendlyUrl"))
            {
                KeyValueEntity TitleEntity = list.Find(r => r.Key == "FriendlyUrl");
                FriendlyUrl = TitleEntity.Value.ToString();

                if (String.IsNullOrEmpty(FriendlyUrl) && !String.IsNullOrEmpty(Event.Title))
                {
                    FriendlyUrl = Common.FriendlySlug(Event.Title);
                }

                Event.UrlSlug = FriendlyUrl;
                TitleEntity.Value = FriendlyUrl;
                list[list.FindIndex(r => r.Key == "FriendlyUrl")] = TitleEntity;
            }
            Event.Options = ConvertTo.Serialize<List<KeyValueEntity>>(list);
            #endregion


            //更新项
            Event.LastIP = WebHelper.UserHost;
            Event.LastTime = xUserTime.LocalTime();
            Event.LastUser = UserId;

            //开始状态和时间
            DateTime oTime = xUserTime.LocalTime();
            string[] expectedFormats = { "G", "g", "f", "F" };
            string StartDate = WebHelper.GetStringParam(Request, txtStartDate.UniqueID, oTime.ToString("MM/dd/yyyy"));
            string StartTime = WebHelper.GetStringParam(Request, txtStartTime.UniqueID, oTime.ToString("HH:mm:ss"));
            if (DateTime.TryParseExact(String.Format("{0} {1}", StartDate, StartTime), "MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out oTime))
            {
                Event.StartTime = oTime;
            }


            //发布状态和时间
            DateTime oReleaseTime = xUserTime.LocalTime();
            string ReleaseDate = WebHelper.GetStringParam(Request, txtReleaseDate.UniqueID, oTime.ToString("MM/dd/yyyy"));
            string ReleaseTime = WebHelper.GetStringParam(Request, txtReleaseTime.UniqueID, oTime.ToString("HH:mm:ss"));
            if (DateTime.TryParseExact(String.Format("{0} {1}", ReleaseDate, ReleaseTime), "MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out oReleaseTime))
            {
                Event.ReleaseDate = oReleaseTime;
            }


            //结束状态和时间
            DateTime EndTime = xUserTime.LocalTime().AddDays(1);
            string DisableDate = WebHelper.GetStringParam(Request, txtDisableDate.UniqueID, EndTime.ToString("MM/dd/yyyy"));
            string DisableTime = WebHelper.GetStringParam(Request, txtDisableTime.UniqueID, EndTime.ToString("HH:mm:ss"));
            if (DateTime.TryParseExact(String.Format("{0} {1}", DisableDate, DisableTime), "MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out EndTime))
            {
                Event.EndTime = EndTime;
            }


            //创建用户改为可以选择
            Event.CreateUser = WebHelper.GetIntParam(Request, hfEventAuthor.UniqueID, UserId);


            if (Event.ID > 0)
            {
                //更新
            }
            else
            {
                //新增
                Event.ModuleId = ModuleId;
                Event.PortalId = PortalId;
                Event.CreateTime = xUserTime.LocalTime();

               
            }


            int ResultEvent = 0;

            if (Event.ID > 0)
                ResultEvent = Event.Update();
            else
                ResultEvent = Event.ID = Event.Insert();

            if (ResultEvent > 0)
            {
                SaveResult = true;


                //邮件发送
                //if (Event.SendMail == 0 && cbNotifySubscribers.Checked)
                if (cbNotifySubscribers.Checked)
                {
                    //利用现成发送邮件
                    var objs = new Dictionary<object, object>();
                    objs.Add("DataItem", Event);
                    objs.Add("PortalUrl", PortalUrl);
                    ManagedThreadPool.QueueUserWorkItem(new WaitCallback(ThreadCreateSendMail), objs);


                    //更新发送状态
                    //Playngo_ClientZone_Event.Update(String.Format("SendMail={0}", 1), String.Format("ID={0}", Event.ID));
                }




            }
            else
            {
                SaveResult = false;
            }
            return SaveResult;


        }

         
         

 
         



        /// <summary>
        /// 绑定选项分组框到页面
        /// </summary>
        private void BindGroupsToPage()
        {
            //获取效果参数
            List<SettingEntity> ItemSettingDB = Event_ItemSettingDB;

            if (ItemSettingDB != null && ItemSettingDB.Count > 0)
            {

                List<KeyValueEntity> Items = new List<KeyValueEntity>();
                foreach (SettingEntity ItemSetting in ItemSettingDB)
                {
                    if (!Items.Exists(r1 => r1.Key == ItemSetting.Group))
                    {
                        Items.Add(new KeyValueEntity(ItemSetting.Group, ""));
                    }
                }

                if (Items != null && Items.Count > 0)
                {

                    //绑定参数项
                    RepeaterGroup.DataSource = Items;
                    RepeaterGroup.DataBind();
                }
                divOptions.Visible = true;
            }
        }




        /// <summary>
        /// 绑定选项集合到页面
        /// </summary>
        private void BindOptionsToPage(Repeater RepeaterOptions, String Group, out int OptionCount)
        {
            OptionCount = 0;
            //获取效果参数
            List<SettingEntity> ItemSettingDB = Event_ItemSettingDB;

            if (ItemSettingDB != null && ItemSettingDB.Count > 0)
            {
                ItemSettingDB = ItemSettingDB.FindAll(r1 => r1.Group == Group);
                OptionCount = ItemSettingDB.Count;
                //绑定参数项
                RepeaterOptions.DataSource = ItemSettingDB;
                RepeaterOptions.DataBind();
            }
        }

        /// <summary>
        /// 拼接数据项的设置参数
        /// </summary>
        /// <returns></returns>
        public String SetItemSettings(ref List<KeyValueEntity> list)
        {
            //获取效果参数
            List<SettingEntity> ItemSettingDB = Event_ItemSettingDB;
            if (ItemSettingDB != null && ItemSettingDB.Count > 0)
            {
                ControlHelper ControlItem = new ControlHelper(ModuleId);

                foreach (SettingEntity ri in ItemSettingDB)
                {
                    KeyValueEntity item = new KeyValueEntity();
                    item.Key = ri.Name;
                    item.Value = ControlHelper.GetWebFormValue(ri, this);
                    list.Add(item);
                }
            }
            return ConvertTo.Serialize<List<KeyValueEntity>>(list);
        }


        /// <summary>
        /// 利用线程发送订阅邮件
        /// </summary>
        /// <param name="ObjectItem"></param>8
        public void ThreadCreateSendMail(Object ObjectItem)
        {

            var objs = (Dictionary<object, object>)ObjectItem;

            Playngo_ClientZone_Event DataItem = (Playngo_ClientZone_Event)objs["DataItem"];
            if (DataItem != null && DataItem.ID > 0)
            {
                //这里需要调用所有邮件的信息
                NotificationEmail NotEmail = new NotificationEmail();
                //所有区域
                NotEmail.AllJurisdictions = Playngo_ClientZone_Jurisdiction.FindAllByModuleID(DataItem.ModuleId);
                //邮件设置
                NotEmail.MailSetting = Playngo_ClientZone_MailSetting.FindByModuleId(ModuleId, "Notification.Events");
                if (NotEmail.MailSetting != null && NotEmail.MailSetting.Status == 1)
                {
                    NotEmail.Settings = Settings;
                    NotEmail.Settings.Add("PortalUrl", objs["PortalUrl"]);
                    NotEmail.SendMail(DataItem);
                }
            }
        }





        #endregion



        #region "==下载文件列表函数及事件集合=="

        /// <summary>
        /// 绑定动态模块到页面
        /// </summary>
        private void BindDownFilesToPage()
        {
           

            if (EventID > 0)
            {
                //绑定动态模块的模态窗口 Resource_Attachments|Manager_Modal_SelectDownloadFiles
                hlSelectFiles.Attributes.Add("data-href", String.Format("{0}Resource_Masters.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}&Master=Manager_Modal_Downloads", ModulePath, PortalId, TabId, ModuleId, language));
            }
            else
            {
                hlSelectFiles.Enabled = false;
                hlSelectFiles.Attributes.Add("disabled", "disabled");
            }


            List<Object> Items = new List<Object>();
            if (EventItem != null && EventItem.ID > 0)
            {
                var Relations = Playngo_ClientZone_DownloadRelation.FindListByItem(EventItem.ID, (Int32)EnumDisplayModuleType.Events);


                if (Relations != null && Relations.Count > 0)
                {
                    foreach (var Relation in Relations)
                    {

                        var Item = Playngo_ClientZone_DownloadFile.FindByKeyForEdit(Relation.DownloadID);
                        Items.Add(new { ID = Relation.ID, ItemID = Item.ID, DownloadID = Relation.DownloadID, Title = Item.Title, PageType = (Int32)EnumDisplayModuleType.Events, PageTypeText = "Events" });

                    }
                }
            }



            //绑定参数项
            RepeaterFiles.DataSource = Items;
            RepeaterFiles.DataBind();


        }

        /// <summary>
        /// 分组绑定事件
        /// </summary>
        protected void RepeaterFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


            }
        }

        #endregion

    }
}