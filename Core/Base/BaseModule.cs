using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Modules;

using DotNetNuke.Entities.Tabs;
using System.IO;


using DotNetNuke.Security;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common;
using DotNetNuke.UI.Skins;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Web.Client.ClientResourceManagement;
using System.Globalization;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Controllers;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 模块基类
    /// </summary>
    public class BaseModule : PortalModuleBase
    {


        #region "动态模块URL函数"



        String _UIToken = String.Empty;
        /// <summary>
        /// 前台界面的跳转命令
        /// </summary>
        public String UIToken
        {
            get
            {

                if (String.IsNullOrEmpty(_UIToken))
                {
                    switch (ViewSettingT<Int32>("ClientZone_ModulePageType", 0))
                    {
                        case 1: _UIToken = EnumDisplayModuleType.GameSheets.ToString(); break;
                        case 2: _UIToken = EnumDisplayModuleType.Downloads.ToString(); break;
                        case 3: _UIToken = EnumDisplayModuleType.Campaigns.ToString(); break;
                        case 4: _UIToken = EnumDisplayModuleType.Events.ToString(); break;
                        case 5: _UIToken = EnumDisplayModuleType.MyAccount.ToString(); break;
                        case 6: _UIToken = EnumDisplayModuleType.Help.ToString(); break;
                        default: _UIToken = EnumDisplayModuleType.ProductRoadmap.ToString(); break;
                    }
                }
                return _UIToken;
            }
        }



        /// <summary>
        /// 动态Item的URL
        /// </summary>
        public String DynamicItem_IframeUrl(Int32 ID,Int32 DynamicID, String ConfigName = "")
        {
            ConfigName = !String.IsNullOrEmpty(ConfigName) ? String.Format("&ConfigName={0}", ConfigName) : "";
            return String.Format("{0}Resource_Masters.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}&Master=Manager_Modal_DynamicItem&ID={5}&DynamicID={6}{7}", ModulePath, PortalId, TabId, ModuleId, language, ID, DynamicID, ConfigName);
        }

        public String DynamicModule_IframeUrl(object ID)
        {
            return String.Format("{0}Resource_Masters.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}&Master=Manager_Modal_DynamicModule&ID={5}", ModulePath, PortalId, TabId, ModuleId, language, ID);
        }


        /// <summary>
        /// 删除动态项
        /// </summary>
        /// <param name="DynamicItemId">动态项编号</param>
        /// <returns></returns>
        public String GoDynamicItemDelete(String DynamicItemId)
        {
            String DynamicItemDeleteUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}&DynamicItemId={6}",
                ModulePath,
                Settings_ModuleID,
                "DynamicItemDelete",
                Settings_TabID,
                PortalId,
                language,
                DynamicItemId);

            return FullPortalUrl(DynamicItemDeleteUrl);
        }

        public String GoDynamicItemDelete()
        {
            return GoDynamicItemDelete("");
        }


        /// <summary>
        /// 删除动态模块
        /// </summary>
        /// <param name="DynamicItemId">动态项编号</param>
        /// <returns></returns>
        public String GoDynamicModuleDelete(String DynamicModuleId)
        {
            String DynamicItemDeleteUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}&DynamicModuleId={6}",
                ModulePath,
                Settings_ModuleID,
                "DynamicModuleDelete",
                Settings_TabID,
                PortalId,
                language,
                DynamicModuleId);

            return FullPortalUrl(DynamicItemDeleteUrl);
        }

        public String GoDynamicModuleDelete()
        {
            return GoDynamicModuleDelete("");
        }


        /// <summary>
        /// 动态项排序
        /// </summary>
        /// <param name="DynamicItemId">动态项编号</param>
        /// <returns></returns>
        public String GoDynamicItemsSort(String DynamicModuleId)
        {
            String DynamicItemSortUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}&DynamicModuleId={6}",
                ModulePath,
                Settings_ModuleID,
                "DynamicItemsSort",
                Settings_TabID,
                PortalId,
                language,
                DynamicModuleId);

            return FullPortalUrl(DynamicItemSortUrl);
        }

        public String GoDynamicItemsSort()
        {
            return GoDynamicItemsSort("");
        }


        public String GoDynamicModulesSort()
        {
            String DynamicModuleSortUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                ModulePath,
                Settings_ModuleID,
                "DynamicModulesSort",
                Settings_TabID,
                PortalId,
                language);

            return FullPortalUrl(DynamicModuleSortUrl);
        }


        /// <summary>
        /// 更新下载关系页面数据
        /// </summary>
        /// <returns></returns>
        public String GoServiceUrlByUpdateRelationPages()
        {

            Int32 DownloadID = WebHelper.GetIntParam(HttpContext.Current.Request, "ID", 0);
            String ServiceUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                     ModulePath,
                                                    Settings_ModuleID,
                                                     "AjaxUpdateRelationPages",
                                                     Settings_TabID,
                                                     PortalId,
                                                     language);


            return FullPortalUrl(ServiceUrl);
        }
        /// <summary>
        /// 删除下载关系页面数据
        /// </summary>
        /// <returns></returns>
        public String GoServiceUrlByDeleteRelationPages()
        {
            String ServiceUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}&ID=",
                                                 ModulePath,
                                                Settings_ModuleID,
                                                 "AjaxDeleteRelationPages",
                                                 Settings_TabID,
                                                 PortalId,
                                                 language
                                                );


            return FullPortalUrl(ServiceUrl);
        }


        /// <summary>
        /// 排序下载关系页面数据
        /// </summary>
        /// <returns></returns>
        public String GoServiceUrlBySortRelationPages()
        {
            String ServiceUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 ModulePath,
                                                Settings_ModuleID,
                                                 "AjaxSortRelationPages",
                                                 Settings_TabID,
                                                 PortalId,
                                                 language
                                                );


            return FullPortalUrl(ServiceUrl);
        }


        /// <summary>
        /// AJAX 搜索框下拉数据（Users）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxSearchToUsers()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 ModulePath,
                                                 Settings_ModuleID,
                                                 "AjaxSearchUsers",
                                                 Settings_TabID,
                                                 PortalId,
                                                 language);


            return FullPortalUrl(DownloadUrl);
        }


        /// <summary>
        /// AJAX 搜索框下拉数据（Groups）
        /// </summary>
        /// <returns></returns>
        public String GoAjaxSearchToGroups()
        {
            String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}",
                                                 ModulePath,
                                                 Settings_ModuleID,
                                                 "AjaxSearchGroups",
                                                 Settings_TabID,
                                                 PortalId,
                                                 language);


            return FullPortalUrl(DownloadUrl);
        }

        

        #endregion



        #region "基本属性"

        /// <summary>
        /// 当前标签
        /// </summary>
        public String Token
        {
            get { return WebHelper.GetStringParam(HttpContext.Current.Request, "Token", MainModule ? "EventList" : "Effect_Options").ToLower(); }
        }


        /// <summary>
        /// 预览状态
        /// </summary>
        public bool Preview = WebHelper.GetBooleanParam(HttpContext.Current.Request, "Preview", false);

        /// <summary>
        /// 详情页面编号
        /// </summary>
        public Int32 DetailId = WebHelper.GetIntParam(HttpContext.Current.Request, "ID", 0);


        public String FriendlyUrl
        {
            get { return WebHelper.GetStringParam(Request, null, ""); }
        }

        /// <summary>
        /// 是否详情页面
        /// </summary>
        public Boolean IsDetail
        {
            get
            {
                return DetailId > 0 ? true : (!String.IsNullOrEmpty(FriendlyUrl));
            }
        }


        /// <summary>
        /// 是否为主模块
        /// </summary>
        public Boolean MainModule
        {
            get { return !String.IsNullOrEmpty(BaseModuleName) && BaseModuleName == "Playngo_ClientZone"; }
        }


        private String _BaseModuleName = String.Empty;
        /// <summary>
        /// 基础模块名
        /// </summary>
        public String BaseModuleName
        {
            get
            {
                if (String.IsNullOrEmpty(_BaseModuleName))
                {
                    _BaseModuleName = ModuleProperty("ModuleName");
                }
                return _BaseModuleName;
            }
            set { _BaseModuleName = value; }
        }

        /// <summary>
        /// 为当前 Web 请求获取与服务器控件关联的 System.Web.HttpContext 对象
        /// </summary>
        public HttpContext HttpContext
        {
            get { return Context; }
        }
 

        private Hashtable _ClientZone_Settings = new Hashtable();
        /// <summary>
        /// 博客主模块设置
        /// </summary>
        public Hashtable ClientZone_Settings
        {
            get
            {
                if (!(_ClientZone_Settings != null && _ClientZone_Settings.Count > 0))
                {

                    if (!String.IsNullOrEmpty(BaseModuleName) && BaseModuleName == "Playngo_ClientZone")
                    {
                        _ClientZone_Settings = Settings;
                    }
                    else
                    {
                        _ClientZone_Settings = new ModuleController().GetModule(Settings_ModuleID).ModuleSettings;
                    }
                }
                return _ClientZone_Settings;
            }
        }


        private PortalSettings _PortalSettings = null;
        /// <summary>
        /// 重写获取站点配置
        /// </summary>
        public PortalSettings PortalSettings
        {
            get
            {
                if (!(_PortalSettings != null && _PortalSettings.PortalId > 0))
                {
                    if ( Settings_PortalID != PortalId)
                    {
                        _PortalSettings = new PortalSettings(Settings_PortalID);

                        DotNetNuke.Entities.Portals.PortalAliasController pac = new PortalAliasController();
                        ArrayList PortalAlias = pac.GetPortalAliasArrayByPortalID(Settings_PortalID);
                        if (PortalAlias != null && PortalAlias.Count > 0)
                        {
                            _PortalSettings.PortalAlias = (PortalAliasInfo)PortalAlias[0];
                        }
                        else
                        {
                            _PortalSettings.PortalAlias = new PortalAliasInfo();
                            _PortalSettings.PortalAlias.PortalID = Settings_PortalID;
                        }
                    }
                    else
                    {
                        _PortalSettings = PortalController.GetCurrentPortalSettings();
                    }
                }
                return _PortalSettings;
            }
        }




        /// <summary>
        /// 设置绑定的模块编号
        /// </summary>
        public Int32 Settings_ModuleID
        {
            get { return Settings["ClientZone_ModuleID"] != null && BaseModuleName != "Playngo_ClientZone" ? Convert.ToInt32(Settings["ClientZone_ModuleID"]) : ModuleId; }
        }


        /// <summary>
        /// 设置绑定的页面编号
        /// </summary>
        public Int32 Settings_TabID
        {
            get { return Settings["ClientZone_TabID"] != null && BaseModuleName != "Playngo_ClientZone" ? Convert.ToInt32(Settings["ClientZone_TabID"]) : TabId; }
        }

        /// <summary>
        /// 设置绑定的站点编号
        /// </summary>
        public Int32 Settings_PortalID
        {
            get { return Settings["ClientZone_PortalID"] != null && BaseModuleName != "Playngo_ClientZone" ? Convert.ToInt32(Settings["ClientZone_PortalID"]) : PortalId; }
        }

        private TabInfo _Settings_TabInfo = new TabInfo();
        /// <summary>
        /// 设置绑定的页面
        /// </summary>
        public TabInfo Settings_TabInfo
        {
            get {
                if (!(_Settings_TabInfo != null && _Settings_TabInfo.TabID > 0))
                {
                      _Settings_TabInfo = new TabController().GetTab(Settings_TabID,PortalId,true);
                }
                return _Settings_TabInfo;
            }
        }


        /// <summary>
        /// 是否开启SSL
        /// </summary>
        public Boolean IsSSL
        {
            get { return Settings_TabInfo.IsSecure || WebHelper.GetSSL; }
        }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public Boolean IsAdministrator
        {
            get { return UserId >0 &&( UserInfo.IsSuperUser || UserInfo.IsInRole("Administrators")); }
        }

        /// <summary>
        /// 管理员锁
        /// (检索目录下有无admindisplay.lock)
        /// </summary>
        public Boolean AdministratorLock
        {
            get { return File.Exists(MapPath(String.Format("{0}admindisplay.lock", ModulePath))); }
        }
        /// <summary>
        /// 显示管理员选项
        /// </summary>
        public Boolean DisplayAdminOption
        {
            get
            {
                Boolean display = true;
                if (AdministratorLock && !IsAdministrator)
                {
                    display = false;
                }
                return display;
            }
        }

  


        /// <summary>
        /// 是否模块管理员(管理作者的文章)
        /// </summary>
        public Boolean IsAdmin
        {
            get {
                Boolean _IsAdmin = false;
                String ManagerModuleAdmin = ViewSettingT<String>("General.ManagerRoles", "");
                if (!String.IsNullOrEmpty(ManagerModuleAdmin))
                {
                    List<String> Roles = Common.GetList(ManagerModuleAdmin.Replace("\r\n", "\r"), "\r");
                    if (Roles != null && Roles.Count > 0)
                    {
                        foreach (String Userrole in UserInfo.Roles)
                        {
                            if (Roles.Exists(r => r.ToLower() == Userrole.ToLower()))
                            {
                                _IsAdmin = true;
                                break;
                            }
                        }

                    }
                }
                return _IsAdmin;
            }
        }

        /// <summary>
        /// 是否具有该模块的编辑权限
        /// </summary>
        public Boolean IsEdit
        {
            get { return UserId > 0 && (IsAdministrator || ModulePermissionController.HasModuleAccess(SecurityAccessLevel.Edit, "CONTENT", ModuleConfiguration)); }
             //get { return UserId > 0 && (IsAdministrator ||  PortalSecurity.HasEditPermissions(ModuleId,TabId)); }
        }

        /// <summary>
        /// 语言
        /// </summary>
        public String language
        {
            get { return WebHelper.GetStringParam(Request, "language", ((DotNetNuke.Framework.PageBase)Page).PageCulture.Name); }
        }


        private String _PortalUrl = String.Empty;
        /// <summary>
        /// 站点URL (可以在绑定的时候用到)
        /// </summary>
        public String PortalUrl
        {
            get {
                if (String.IsNullOrEmpty(_PortalUrl))
                {
                    if (Settings_PortalID == PortalId)
                    {
                        _PortalUrl = String.Format("{0}://{1}", IsSSL ? "https" : "http", WebHelper.GetHomeUrl());
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(PortalSettings.PortalAlias.HTTPAlias))
                        {
                            _PortalUrl = String.Format("{0}://{1}", IsSSL ? "https" : "http", PortalSettings.PortalAlias.HTTPAlias);
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
        public Boolean designMode
        {
            get { return DesignMode; }
        }


        private string _CrmVersion = String.Empty;
        /// <summary>
        /// 引用文件版本
        /// </summary>
        public string CrmVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_CrmVersion))
                {
                    var ModuleVersion = ModuleProperty("Version");
                    string setting = GetHostSetting("CrmVersion");
                    if (!string.IsNullOrEmpty(setting))
                    {
                        _CrmVersion = String.Format("{0}.{1}", ModuleVersion, setting);
                    }
                }
                return _CrmVersion;
            }
        }

        private string GetHostSetting(string key, string defaultValue = "")
        {
            return HostController.Instance.GetString(key, defaultValue); 
        }


        #endregion

        #region "有关皮肤效果的设置"

        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public TemplateDB GetTemplateDB(String DBName)
        {
            String XmlDBPath = Server.MapPath(String.Format("{0}Templates/{1}/TemplateDB.xml", ModulePath, DBName));
            if (File.Exists(XmlDBPath))
            {
                XmlFormat xf = new XmlFormat(XmlDBPath);
                return xf.ToItem<TemplateDB>();
            }
            return new TemplateDB();
        }

         

        #endregion



        #region "获取基本配置信息的方法"

        /// <summary>
        /// 获取全局配置参数的基础类
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="Prefix">前缀(设置的XML名称)</param>
        /// <param name="SettingName">配置项名称</param>
        /// <param name="SettingValue">默认值</param>
        /// <returns></returns>
        public T GetSetting<T>(String Prefix, String SettingName, T DefaultValue)
        {
            return ViewSettingT<T>(String.Format("{0}.{1}", Prefix, SettingName), DefaultValue);
        }

        /// <summary>
        /// 读取参数
        /// </summary>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewSetting(String Name, object DefaultValue)
        {
            return Settings[Name] != null ? Common.FormatValue(Settings[Name].ToString(), DefaultValue.GetType()) : DefaultValue;
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


        #endregion

        #region "--读取管理界面配置的值--"


        #region "Config.General.xml 配置参数"


        /// <summary>
        /// 管理界面分页的数量
        /// </summary>
        public Int32 Settings_General_ManagerPerPage
        {
            get { return GetSetting<Int32>("General", "ManagerPerPage", 10); }
        }



        /// <summary>
        /// ping服务开关
        /// </summary>
        public Boolean Settings_General_EnablePingService
        {
            get { return GetSetting<Boolean>("General", "EnablePingService", false); }
        }

        /// <summary>
        /// ping服务配置项
        /// </summary>
        public List<String> Settings_General_PingService
        {
            get { return WebHelper.GetList(GetSetting<String>("General", "PingService", "").Replace("\r\n", ",").Replace("\n", ",")); }
        }

        /// <summary>
        /// 索引服务开关
        /// </summary>
        public Boolean Settings_General_EnableSearchable
        {
            get { return GetSetting<Boolean>("General", "Searchable", false); }
        }

        /// <summary>
        /// 索引服务（个数）
        /// </summary>
        public Int32 Settings_General_SearchableItems
        {
            get { return GetSetting<Int32>("General", "SearchableItems", 10000); }
        }


        /// <summary>
        /// ping是否开启发布时间的限制
        /// </summary>
        public Boolean Settings_General_EnableStartTime
        {
            get { return GetSetting<Boolean>("General", "EnablePingService", true); }
        }


 



        #endregion





        #region "Config.SEO.xml 配置参数"

        /// <summary>
        /// SEO URL参数,可以设置EventID
        /// </summary>
        public String Settings_Seo_UrlEventID
        {
            get { return GetSetting<String>("SEO", "SEOUrlParameter", "EventID"); }
        }




        #endregion


        #endregion


      



        #region "jQuery配置属性"

        /// <summary>
        /// 开始模块jQuery
        /// </summary>
        public Boolean Settings_jQuery_Enable
        {
            get { return ClientZone_Settings["ClientZone_jQuery_Enable"] != null && !string.IsNullOrEmpty(ClientZone_Settings["ClientZone_jQuery_Enable"].ToString()) ? Convert.ToBoolean(ClientZone_Settings["ClientZone_jQuery_Enable"]) : false; }
        }

        /// <summary>
        /// 使用jQuery库
        /// </summary>
        public Boolean Settings_jQuery_UseHosted
        {
            get { return ClientZone_Settings["ClientZone_jQuery_UseHosted"] != null && !string.IsNullOrEmpty(ClientZone_Settings["ClientZone_jQuery_UseHosted"].ToString()) ? Convert.ToBoolean(ClientZone_Settings["ClientZone_jQuery_UseHosted"]) : false; }
        }

        /// <summary>
        /// jQuery库的地址
        /// </summary>
        public String Settings_jQuery_HostedjQuery
        {
            get { return ClientZone_Settings["ClientZone_jQuery_HostedjQuery"] != null && !string.IsNullOrEmpty(ClientZone_Settings["ClientZone_jQuery_HostedjQuery"].ToString()) ? Convert.ToString(ClientZone_Settings["ClientZone_jQuery_HostedjQuery"]) : "https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"; }
        }

        /// <summary>
        /// jQueryUI库的地址
        /// </summary>
        public String Settings_jQuery_HostedjQueryUI
        {
            get { return ClientZone_Settings["ClientZone_jQuery_HostedjQueryUI"] != null && !string.IsNullOrEmpty(ClientZone_Settings["ClientZone_jQuery_HostedjQueryUI"].ToString()) ? Convert.ToString(ClientZone_Settings["ClientZone_jQuery_HostedjQueryUI"]) : "https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.4/jquery-ui.min.js"; }
        }


        #endregion

        #region "加载样式表"

        /// <summary>
        /// 绑定样式表文件
        /// </summary>
        /// <param name="Name"></param>
        public void BindStyleFile(String Name, String FileName)
        {
            BindStyleFile(Name, FileName, 50);
        }

        /// <summary>
        /// 绑定样式表文件
        /// </summary>
        /// <param name="Name"></param>
        public void BindStyleFile(String Name, String FileName, int priority)
        {
            if (HttpContext.Current.Items[Name] == null)
            {
                HttpContext.Current.Items.Add(Name, "true");
                ClientResourceManager.RegisterStyleSheet(Page, FileName);
            }
        }

        /// <summary>
        /// 绑定脚本文件
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="FileName"></param>
        public void BindJavaScriptFile(String Name, String FileName)
        {
            BindJavaScriptFile(Name, FileName, 50);
        }

        /// <summary>
        /// 绑定脚本文件
        /// </summary>
        /// <param name="ThemeName"></param>
        public void BindJavaScriptFile(String Name, String FileName, int priority)
        {
            if (HttpContext.Current.Items[Name] == null)
            {
                HttpContext.Current.Items.Add(Name, "true");
                ClientResourceManager.RegisterScript(Page, FileName, priority);
                //Page.ClientScript.RegisterClientScriptInclude(Name, ResolveUrl(String.Format("{0}?cdv={1}", FileName, CrmVersion)));
            }
        }


        #endregion


        #region "加载界面脚本样式表"







        /// <summary>
        /// 加载系统的jquery
        /// </summary>
        public void LoadSystemJQuery(System.Web.UI.Control objCSS)
        {
           

            string ContentSrc = ResolveUrl("~/admin/Skins/jQuery.ascx");
            if (File.Exists(Server.MapPath(ContentSrc)))
            {
                SkinObjectBase ManageContent = new SkinObjectBase();
                ManageContent = (SkinObjectBase)LoadControl(ContentSrc);
                ManageContent.ModuleControl = this;
                objCSS.Controls.Add(ManageContent);//具有编辑权限才能看到模块

            }
        }



        /// <summary>
        /// 加载显示界面脚本样式表
        /// </summary>
        public void LoadViewScript()
        {
            System.Web.UI.Control objCSS = this.Page.FindControl("CSS");
            if ((objCSS != null))
            {
                //LoadScriptForJqueryAndUI(ModulePath);


                LoadSystemJQuery(objCSS);

                //if (HttpContext.Current.Items["jquery-ui-CSS"] == null)
                //{
                //    Literal litLink = new Literal();
                //    litLink.Text = "<link  rel=\"stylesheet\" type=\"text/css\" href=\"" + ModulePath + "Resource/css/jquery-ui-1.7.custom.css\" />";

                //    HttpContext.Current.Items.Add("jquery-ui-CSS", "true");
                //    objCSS.Controls.Add(litLink);
                //}



                //if (HttpContext.Current.Items["Playngo_ClientZone_Modules_css"] == null)
                //{
                //    Literal litLink = new Literal();
                //    litLink.Text = "<link  rel=\"stylesheet\" type=\"text/css\" href=\"" + ModulePath + "Resource/css/Modules.css\" />";

                //    HttpContext.Current.Items.Add("Playngo_ClientZone_Modules_css", "true");
                //    objCSS.Controls.Add(litLink);
                //}


                BindJavaScriptFile("jquery.validationEngine-en.js", ViewValidationEngineLanguage());
                BindJavaScriptFile("jquery.validationEngine.js", String.Format("{0}Resource/js/jquery.validationEngine.js", ModulePath));
        

                //if (HttpContext.Current.Items["Vanadium_js"] == null)
                //{

                //    HttpContext.Current.Items.Add("Vanadium_js", "true");
                //    DotNetNuke.Framework.AJAX.AddScriptManager(this.Page);
                //    Page.ClientScript.RegisterClientScriptInclude("Vanadium_js", String.Format("{0}Resource/js/vanadium.js", ModulePath));

                //}




                //if (HttpContext.Current.Items["jquery.category.js"] == null)
                //{
                //    Literal litLink = new Literal();
                //    litLink.Text =
                //         Microsoft.VisualBasic.Constants.vbCrLf + "<script type=\"text/javascript\" src=\"" + ModulePath + "Resource/js/jquery.category.js\"></script>" +
                //        Microsoft.VisualBasic.Constants.vbCrLf;
                //    HttpContext.Current.Items.Add("jquery.category.js", "true");
                //    objCSS.Controls.Add(litLink);
                //}


                //if (HttpContext.Current.Items["Playngo.Common.js"] == null)
                //{

                //    HttpContext.Current.Items.Add("Playngo.Common.js", "true");
                //    DotNetNuke.Framework.AJAX.AddScriptManager(this.Page);
                //    Page.ClientScript.RegisterClientScriptInclude("Playngo.Common.js", String.Format("{0}Resource/js/Playngo.Common.js", ModulePath));
                //}
            }
        }

         

        /// <summary>
        /// 加载脚本
        /// </summary>
        public void LoadScriptForJqueryAndUI(string modulePath)
        {
            System.Web.UI.Control objCSS = this.Page.FindControl("CSS");
            if ((objCSS != null))
            {
                String jQueryUrl = String.Format("{0}Resource/js/jquery.min.js?cdv={1}", ModulePath, CrmVersion);
                String jQueryUIUrl = String.Format("{0}Resource/js/jquery-ui.min.js?cdv={1}", ModulePath, CrmVersion);
                if (Settings_jQuery_UseHosted)//使用指定的jQuery库的地址
                {
                    jQueryUrl = Settings_jQuery_HostedjQuery;
                    jQueryUIUrl = Settings_jQuery_HostedjQueryUI;
                }





                if ((Settings_jQuery_Enable && !HttpContext.Current.Items.Contains("jQueryUIRequested")) || (Settings_jQuery_Enable && !HttpContext.Current.Items.Contains("Playngo_jQueryUI")))
                {
                    Literal litLink = new Literal();
                    litLink.Text = String.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", jQueryUIUrl);
                    //if (!Settings_jQuery_Enable)
                    //{

                    if (!HttpContext.Current.Items.Contains("jQueryUIRequested")) HttpContext.Current.Items.Add("jQueryUIRequested", "true");
                    //}
                    if (!HttpContext.Current.Items.Contains("Playngo_jQueryUI")) HttpContext.Current.Items.Add("Playngo_jQueryUI", "true");
                    objCSS.Controls.AddAt(0, litLink);
                }

                if ((Settings_jQuery_Enable && !HttpContext.Current.Items.Contains("jquery_registered") && !HttpContext.Current.Items.Contains("jQueryRequested")) || (Settings_jQuery_Enable && !HttpContext.Current.Items.Contains("Playngo_jQuery")))
                {
                    Literal litLink = new Literal();
                    litLink.Text = String.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", jQueryUrl);
                    //if (!Settings_jQuery_Enable)
                    //{
                    if (!HttpContext.Current.Items.Contains("jquery_registered")) HttpContext.Current.Items.Add("jquery_registered", "true");
                    if (!HttpContext.Current.Items.Contains("jQueryRequested")) HttpContext.Current.Items.Add("jQueryRequested", "true");
                    //}
                    if (!HttpContext.Current.Items.Contains("Playngo_jQuery")) HttpContext.Current.Items.Add("Playngo_jQuery", "true");

                    objCSS.Controls.AddAt(0, litLink);
                }
            }
        }



        #endregion

        #region "验证脚本多语言"
        /// <summary>
        /// 获取当前验证引擎语言文件的URL
        /// </summary>
        /// <returns></returns>
        public String ViewValidationEngineLanguage()
        {
            //String VEL = String.Format("{0}Resource/js/jquery.validationEngine-en.js?cdv={1}", ModulePath, CrmVersion);
            String VEL = String.Format("{0}Resource/js/jquery.validationEngine-en.js", ModulePath);
            String language = WebHelper.GetStringParam(Request, "language", PortalSettings.DefaultLanguage).ToLower(); ;
            if (!String.IsNullOrEmpty(language) && language != "en-us")
            {
                //先判断这个语言文件是否存在
                String webJS = String.Format("{0}Resource/plugins/validation/jquery.validationEngine-{1}.js", ModulePath, language);
                String serverJS = MapPath(webJS);
                if (File.Exists(serverJS))
                {
                    //VEL = String.Format("{0}?cdv={1}", webJS, CrmVersion);
                    VEL = webJS;
                }
                else if (language.IndexOf("-") >= 0)
                {
                    String lTemp = language.Remove(language.IndexOf("-"));
                    webJS = String.Format("{0}Resource/plugins/validation/jquery.validationEngine-{1}.js", ModulePath, lTemp);
                    serverJS = MapPath(webJS);
                    if (File.Exists(serverJS))
                    {
                        // VEL = String.Format("{0}?cdv={1}", webJS, CrmVersion);
                        VEL = webJS;
                    }
                }
            }
            return VEL;
        }

        #endregion

        #region "加载XML配置文件中的脚本与样式表"
        /// <summary>
        /// XmlDB
        /// </summary>
        /// <param name="XmlDB">配置文件</param>
        /// <param name="XmlName">效果/皮肤</param>
        public void BindXmlDBToPage(TemplateDB XmlDB, String XmlName)
        {
            

            //是否支持谷歌地图
            if (XmlDB.GoogleMap)
            {
                BindJavaScriptFile("GoogleMap", "https://maps.google.com/maps/api/js?sensor=false");
            }


            int priority = 50;

            //绑定全局附带的脚本
            if (!String.IsNullOrEmpty(XmlDB.GlobalScript))
            {
                List<String> GlobalScripts = WebHelper.GetList(XmlDB.GlobalScript);

                foreach (String Script in GlobalScripts)
                {
                    if (!String.IsNullOrEmpty(Script))
                    {
                        if (Script.IndexOf(".css", StringComparison.CurrentCultureIgnoreCase) > 0)
                        {
                            String FullFileName = String.Format("{0}Resource/css/{1}", ModulePath, Script);
                            BindStyleFile(Script, FullFileName, priority);
                        }
                        else //if (Script.IndexOf(".js", StringComparison.CurrentCultureIgnoreCase) > 0)
                        {
                            String FullFileName = String.Format("{0}Resource/js/{1}", ModulePath, Script);
                            BindJavaScriptFile(Script, FullFileName, priority);
                        }
                        priority++;
                    }
                }
            }
            //绑定效果附带的脚本
            if (!String.IsNullOrEmpty(XmlDB.TemplateScript))
            {
                List<String> TemplateScripts = WebHelper.GetList(XmlDB.TemplateScript);

                foreach (String Script in TemplateScripts)
                {
                    if (!String.IsNullOrEmpty(Script))
                    {
                        if (Script.IndexOf(".css", StringComparison.CurrentCultureIgnoreCase) > 0)
                        {
                            String FullFileName = String.Format("{0}{1}/{2}/css/{3}", ModulePath, XmlName, XmlDB.Name, Script);
                            BindStyleFile(Script, FullFileName, priority);
                        }
                        else
                        {
                            String FullFileName = String.Format("{0}{1}/{2}/js/{3}", ModulePath, XmlName, XmlDB.Name, Script);
                            BindJavaScriptFile(Script, FullFileName, priority);
                        }
                        priority++;
                    }
                }
            }

      

        }




        #endregion

        #region "加载提示语句"

        /// <summary>
        /// 显示未绑定模版的语句
        /// </summary>
        /// <returns></returns>
        public String ViewNoTemplate()
        {
            String NoTemplate = Localization.GetString("NoTemplate.Message", Localization.GetResourceFile(this, "Message.ascx.resx"));


            return NoTemplate + ViewThemeGoUrl();
        }


        public String ViewMessageToTemplate(String MessageToken)
        {
            String NoTemplate = Localization.GetString(String.Format("{0}.Template", MessageToken), Localization.GetResourceFile(this, "Message.ascx.resx"));
            TemplateFormat xf = new TemplateFormat(this);
            return NoTemplate.Replace("[ReturnUrl]",xf.GoUiUrl(UIToken));
        }



        /// <summary>
        /// 显示未绑定主题时的跳转链接
        /// </summary>
        /// <returns></returns>
        public String ViewThemeGoUrl()
        {
            String ThemeGoUrl = String.Empty;
            //有编辑权限的时候，显示跳转到模版加载页
            if (IsAdministrator)
            {
                ThemeGoUrl = Localization.GetString("ThemeGoUrl.Message", Localization.GetResourceFile(this, "Message.ascx.resx"));
                ThemeGoUrl = ThemeGoUrl.Replace("[ThemeUrl]", xUrl("Event_List_Skin"));
            }
            return ThemeGoUrl;
        }

        /// <summary>
        /// 未设置模块的绑定
        /// </summary>
        /// <returns></returns>
        public String ViewNoSettingBind(String OnlyMenu = "")
        {
            String NoSettingString = Localization.GetString("NoModuleSetting.Message", Localization.GetResourceFile(this, "Message.ascx.resx"));
            if(!String.IsNullOrEmpty(NoSettingString) )
            {
                String NoSettingStringUrl = Localization.GetString("NoModuleSettingLink.Message", Localization.GetResourceFile(this, "Message.ascx.resx"));
                if (!String.IsNullOrEmpty(NoSettingStringUrl) && IsEdit)
                {
                    NoSettingString = NoSettingString.Replace("[Settings]", String.Format(NoSettingStringUrl, xUrl("OnlyMenu", OnlyMenu, "DashBoard_Settings")));
                }
            }
            
            return NoSettingString;
        }



        /// <summary>
        /// 显示列表无数据的提示
        /// </summary>
        /// <returns></returns>
        public String ViewGridViewEmpty()
        {
            return Localization.GetString("GridViewEmpty.Message", Localization.GetResourceFile(this, "Message.ascx.resx"));
        }


        /// <summary>
        /// 绑定GridView的空信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gvList"></param>
        public void BindGridViewEmpty<T>(GridView gvList)
             where T : new()
        {
            BindGridViewEmpty<T>(gvList, new T());
        }

        /// <summary>
        /// 绑定GridView的空信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gvList"></param>
        /// <param name="t"></param>
        public void BindGridViewEmpty<T>(GridView gvList, T t)
        {
            String EmptyDataText = ViewGridViewEmpty();
            if (gvList.Rows.Count == 0 || gvList.Rows[0].Cells[0].Text == EmptyDataText)
            {
                List<T> ss = new List<T>();
                ss.Add(t);
                gvList.DataSource = ss;
                gvList.DataBind();

                gvList.Rows[0].Cells.Clear();
                gvList.Rows[0].Cells.Add(new TableCell());
                gvList.Rows[0].Cells[0].ColumnSpan = gvList.HeaderRow.Cells.Count;
                gvList.Rows[0].Cells[0].Text = EmptyDataText;
                gvList.Rows[0].Cells[0].Style.Add("text-align", "center");
            }
        }

        #endregion


        #region "新的后台URL"

        /// <summary>
        /// URL转换默认名
        /// </summary>
        /// <returns></returns>
        public String xUrlToken()
        {
            return !String.IsNullOrEmpty(BaseModuleName) && BaseModuleName == "Playngo_ClientZone" ? "ProductRoadmap" : "Settings";
        }


        public string xUrl()
        {
            return xUrl("", "", xUrlToken());
        }
        public string xUrl(string ControlKey)
        {
            return xUrl("", "", ControlKey);
        }
        public string xUrl(string KeyName, string KeyValue)
        {
            return xUrl(KeyName, KeyValue, xUrlToken());
        }
        public string xUrl(string KeyName, string KeyValue, string ControlKey)
        {
            string[] parameters = new string[] { };
            return xUrl(KeyName, KeyValue, ControlKey, parameters);
        }

        public string xUrl( string KeyName, string KeyValue, string ControlKey, params string[] AddParameters)
        {
            return xUrl(TabId,ModuleId,  KeyName,  KeyValue,  ControlKey,  AddParameters);
        }

        public string xUrl(Int32 _TabId,Int32 _ModuleId, string KeyName, string KeyValue, string ControlKey, params string[] AddParameters)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            String language = WebHelper.GetStringParam(Request,"language",PortalSettings.DefaultLanguage);

            sb.AppendFormat("{0}Index_Manager.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}", ModulePath, PortalId, _TabId, _ModuleId, language);

            string key = ControlKey;
            if (string.IsNullOrEmpty(key))
            {
                sb.AppendFormat("&Token={0}", xUrlToken());
            }
            else
            {
                sb.AppendFormat("&Token={0}", key);
            }



            if (!string.IsNullOrEmpty(KeyName) && !string.IsNullOrEmpty(KeyValue))
            {
                sb.AppendFormat("&{0}={1}", KeyName, KeyValue);
            }

            if (AddParameters != null && AddParameters.Length > 0)
            {
                foreach (String parameter in AddParameters)
                {
                    sb.AppendFormat("&{0}", parameter);
                }
            }
            return sb.ToString();

        }


      


        #endregion

        #region "错误捕获"

        /// <summary>
        /// 错误捕获
        /// </summary>
        /// <param name="exc">错误</param>
        public void ProcessModuleLoadException(Exception exc)
        {
            if (HttpContext.Current.Session["Exception"] != null)
            {
                HttpContext.Current.Session.Remove("Exception");
            }
            //增加当前序列化的内容到Session
            HttpContext.Current.Session.Add("Exception", exc);

            if (WebHelper.GetStringParam(Request, "Token", "").ToLower() != "error")
            {
                Response.Redirect(xUrl("ReturnUrl", HttpUtility.UrlEncode(WebHelper.GetScriptUrl), "Error"), false);
            }

        }
        #endregion


        #region "更新模块设置"


        /// <summary>
        /// 更新当前模块的设置
        /// </summary>
        /// <param name="SettingName"></param>
        /// <param name="SettingValue"></param>
        public void UpdateModuleSetting(string SettingName, string SettingValue)
        {
            UpdateModuleSetting(ModuleId, SettingName, SettingValue);
        }


        /// <summary>
        /// 更新模块设置
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <param name="SettingName"></param>
        /// <param name="SettingValue"></param>
        public void UpdateModuleSetting(int ModuleId, string SettingName, string SettingValue)
        {
            ModuleController controller = new ModuleController();

            controller.UpdateModuleSetting(ModuleId, SettingName, SettingValue);

            //refresh cache
            SynchronizeModule();
        }

 


        /// <summary>
        /// 效果参数保存名称格式化
        /// </summary>
        /// <param name="EffectName">效果名</param>
        /// <param name="ThemeName">主题名</param>
        /// <returns></returns>
        public String SettingsFormat(String EffectName, String ThemeName)
        {
            return String.Format("{0}.{1}", EffectName, ThemeName);
        }

        #endregion

        #region "获取时区相关"

        /// <summary>
        /// 显示时区
        /// </summary>
        /// <returns></returns>
        public String ViewTimeZone()
        {
            ////时区j计算
            //String TimeZone = String.Empty;
            //System.Collections.Specialized.NameValueCollection timeZones = Localization.GetTimeZones(language.ToLower());
            //if (timeZones.Count == 0)
            //{
            //    timeZones = Localization.GetTimeZones(Localization.SystemLocale);
            //}
            //if (timeZones != null && timeZones.Count > 0 )
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
            int oldOffset =0;
            var PortalSettings = PortalController.Instance.GetCurrentPortalSettings();
            if (PortalSettings != null && PortalSettings.UserId > Null.NullInteger)
            {
                int.TryParse(PortalSettings.UserInfo.Profile.PreferredTimeZone.BaseUtcOffset.TotalMinutes.ToString(CultureInfo.InvariantCulture), out oldOffset);
            } else
            {
                int.TryParse(PortalSettings.TimeZone.BaseUtcOffset.TotalMinutes.ToString(CultureInfo.InvariantCulture), out oldOffset);
            }
            return Localization.ConvertLegacyTimeZoneOffsetToTimeZoneInfo(oldOffset).ToString();
        }

        #endregion

        #region "关于用户处理"

        /// <summary>
        /// 显示用户头像
        /// </summary>
        /// <param name="uInfo"></param>
        /// <returns></returns>
        public String ViewUserPic(UserInfo uInfo)
        {
          String  Photo = String.Format("{0}Resource/images/no_user.png", ModulePath);
          if (uInfo != null && !String.IsNullOrEmpty(uInfo.Username) && uInfo.Profile != null)
          {
              String UserPic = uInfo.Profile.GetPropertyValue("Photo");
              Int32 result = 0;
              if (!String.IsNullOrEmpty(UserPic) && int.TryParse(UserPic.Trim(), out result) && result > 0)
              {
                  try
                  {
                      Photo = Globals.LinkClick(String.Format("fileid={0}", result), TabId, ModuleId);
                  }
                  catch
                  {
                      Photo = String.Format("{0}Resource/images/no_user.png", ModulePath);
                  }
              }
          }
          return Photo;
        }



        #endregion


        #region "角色和区域的权限控制"

        /// <summary>
        /// 判断该数据是否有角色权限浏览
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public Boolean IsPreRoleView(int Per_AllUsers, String Per_Roles)
        {

            Boolean IsPre = false;
            if (Per_AllUsers == 0 || (UserId > 0 && UserInfo.IsSuperUser))
            {
                IsPre = true;
            }
            else
            {
                if (UserId > 0 && !String.IsNullOrEmpty(Per_Roles))
                {
                    foreach (var r in UserInfo.Roles)
                    {
                        var Roles = Common.GetList(Per_Roles);

                        if (Roles.IndexOf(r) >= 0)
                        {
                            IsPre = true;
                            break;
                        }
                    }
                }
            }
            return IsPre;
        }


        /// <summary>
        /// 判断该数据是否有区域权限浏览
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public Boolean IsPreJurisdictionView(int Per_AllJurisdictions, String Per_Jurisdictions)
        {

            Boolean IsPre = false;
            if (Per_AllJurisdictions == 0)
            {
                IsPre = true;
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
                            if (Int32.TryParse(Jurisdiction, out JurisdictionId))
                            {
                                //需要根据当前的区域去查找区域的关联角色
                                if (AllJurisdictions.Exists(r => r.ID == JurisdictionId))
                                {
                                    var JurisdictionItem = AllJurisdictions.Find(r => r.ID == JurisdictionId);
 
                                    //根据关联的角色判断当前用户是否需要符合
                                    if (IsPreRoleView(JurisdictionItem.Per_AllUsers, JurisdictionItem.Per_Roles))
                                    {
                                        IsPre = true;
                                        break;
                                    }

                                }
                            }


                        }

                    }
                }
            }
            return IsPre;
        }

        private List<Playngo_ClientZone_Jurisdiction> _AllJurisdictions = new List<Playngo_ClientZone_Jurisdiction>();
        /// <summary>
        /// 所有区域限制
        /// </summary>
        public List<Playngo_ClientZone_Jurisdiction> AllJurisdictions
        {
            get
            {
                if (!(_AllJurisdictions != null && _AllJurisdictions.Count > 0))
                {
                    _AllJurisdictions = Playngo_ClientZone_Jurisdiction.FindAllByModuleID(Settings_ModuleID);
                }

                return _AllJurisdictions;
            }


        }


        /// <summary>
        /// 创建查询语句(权限)
        /// </summary>
        /// <param name="qp"></param>
        /// <returns></returns>
        public QueryParam CreateQueryByRoles(QueryParam qp)
        {
            if (UserId > 0)
            {
                if (!UserInfo.IsSuperUser)//超级管理员不限制
                {
                    if (qp.WhereSql.Length > 0) qp.WhereSql.Append(" AND ");

                    qp.WhereSql.Append(" ( ");
                    //公开的
                    qp.WhereSql.Append(new SearchParam("Per_AllUsers", 0, SearchType.Equal).ToSql());

                    //有角色的
                    if (UserInfo.Roles != null && UserInfo.Roles.Length > 0)
                    {
                        qp.WhereSql.Append(" OR ");
                        qp.WhereSql.Append(" ( ");

                        Int32 RoleIndex = 0;
                        foreach (var r in UserInfo.Roles)
                        {
                            if (RoleIndex > 0)
                            {
                                qp.WhereSql.Append(" OR ");
                            }

                            qp.WhereSql.Append(new SearchParam("Per_Roles", String.Format(",{0},", r), SearchType.Like).ToSql());

                            qp.WhereSql.Append(" OR ");

                            qp.WhereSql.Append(new SearchParam("Per_Roles", r, SearchType.Like).ToSql());


                            RoleIndex++;
                        }
                        qp.WhereSql.Append(" ) ");
                    }


                    qp.WhereSql.Append(" ) ");
                }
            }
            else
            {
                qp.Where.Add(new SearchParam("Per_AllUsers", 0, SearchType.Equal));
            }
            return qp;
        }

        #endregion

        #region "绑定模版文件"
        /// <summary>
        /// 模版文件夹名称
        /// </summary>
        public String TemplateFolder
        {
            get { return Token.IndexOf("Event_Detail_Skin", StringComparison.CurrentCultureIgnoreCase) >= 0 ? "Template_Detail" : "Template_Effect"; }
        }

        /// <summary>
        /// 获取当前配置文件的文件夹位置
        /// </summary>
        /// <param name="DBs"></param>
        /// <returns></returns>
        public String GetTemplateFolder(TemplateDB DBs)
        {
            return DBs.TemplateAttribute == (Int32)EnumTemplateAttribute.Detail ? "Template_Detail" : "Template_Effect";
        }



        /// <summary>
        /// 显示模版
        /// </summary>
        /// <param name="Theme"></param>
        /// <param name="ThemeFile"></param>
        /// <param name="Puts"></param>
        /// <returns></returns>
        public String ViewTemplate(TemplateDB Theme, String ThemeFile, Hashtable Puts)
        {
            TemplateFormat xf = new TemplateFormat(this);
            return ViewTemplate(Theme, ThemeFile, Puts, xf);
        }

        /// <summary>
        /// 显示模版
        /// </summary>
        /// <param name="Theme"></param>
        /// <param name="xf"></param>
        /// <param name="Puts"></param>
        /// <returns></returns>
        public String ViewTemplate(TemplateDB Theme, String ThemeFile, Hashtable Puts, TemplateFormat xf)
        {
            VelocityHelper vltContext = new VelocityHelper(this, Theme);


            vltContext.Put("xf", xf);//模版格式化共用方法
            vltContext.Put("ModuleID", ModuleId);//绑定的主模块编号
            vltContext.Put("TabID", TabId);//绑定的主模块页面编号


            //（为了兼容老的皮肤，吧这两个反过来了）
            vltContext.Put("IsAdministrator", IsAdmin);//是否超级管理员
            vltContext.Put("IsAdmin", IsAdministrator);//是否管理员

        
           

            //模板中使用的时间相关数据
            vltContext.Put("LocalTime", xUserTime.LocalTime());
            vltContext.Put("UtcTime", xUserTime.UtcTime());
            vltContext.Put("ServerTime", xUserTime.ServerTime());
            vltContext.Put("DateTimeFormat", CultureInfo.CurrentCulture.DateTimeFormat);

            if (!Puts.Contains("TimeZone")) Puts.Add("TimeZone", ViewTimeZone());



            if (Puts != null && Puts.Count > 0)
            {
                foreach (String key in Puts.Keys)
                {
                    vltContext.Put(key, Puts[key]);
                }
            }
            return vltContext.Display(ThemeFile);
        }



        #endregion

        #region "绑定页面标题和帮助"

        /// <summary>
        /// 显示控件标题
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="ControlName"></param>
        /// <param name="Suffix"></param>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public String ViewControlTitle(String Title, String DefaultValue, String ControlName,String Suffix, String ClassName)
        {
            String Content = ViewResourceText(Title, DefaultValue);
            if (!String.IsNullOrEmpty(ControlName))
            {
                System.Web.UI.Control c = FindControl(ControlName);
                if (c != null && !String.IsNullOrEmpty(c.ClientID))
                {
                    ControlName = c.ClientID;
                }
                else
                {
                    ControlName = String.Empty;
                }
            }

            return String.Format("<label  {2} {1}>{0}{3}</label>",
                Content,
                !String.IsNullOrEmpty(ClassName) ? String.Format("class=\"{0}\"", ClassName) : "",
              !String.IsNullOrEmpty(ControlName) ? String.Format("for=\"{0}\"", ControlName) : "",
              Suffix
                );
        }




        /// <summary>
        /// 显示标题
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewTitle(String Title, String DefaultValue)
        {
            return ViewTitle(Title, DefaultValue,"");
        }

        /// <summary>
        /// 显示标题
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewTitle(String Title, String DefaultValue, String ControlName)
        {
            return ViewTitle(Title, DefaultValue, ControlName, "");
        }

        /// <summary>
        /// 显示标题
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewTitle(String Title, String DefaultValue, String ControlName,String ClassName)
        {
            String Content = ViewResourceText(Title, DefaultValue);
            return ViewSpan(Content, ControlName, ClassName);
        }

        /// <summary>
        /// 显示帮助
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewHelp(String Title, String DefaultValue)
        {
            return ViewHelp(Title, DefaultValue,"");
        }

        /// <summary>
        /// 显示帮助
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewHelp(String Title, String DefaultValue, String ControlName)
        {
            String Content = ViewResourceText(Title, DefaultValue, "Help");
            //return ViewSpan( Content, ControlName, "span_help");
            return !String.IsNullOrEmpty(Content) ? String.Format("<span class=\"help-block\" for=\"{1}\"><i class=\"fa fa-info-circle\"></i> {0}</span>", Content, ControlName):"";
        }

        /// <summary>
        /// 显示内容框
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="ControlName"></param>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public String ViewSpan(String Content, String ControlName, String ClassName)
        {
            if (!String.IsNullOrEmpty(ControlName))
            {
                System.Web.UI.Control c = FindControl(ControlName);
               if (c != null && !String.IsNullOrEmpty(c.ClientID))
               {
                   ControlName = c.ClientID;
               }
               else
               {
                   ControlName = String.Empty;
               }
            }

            return String.Format("<label  {2} {1}><span {1} >{0}</span></label>",
                Content,
                !String.IsNullOrEmpty(ClassName) ? String.Format("class=\"{0}\"", ClassName) : "",
              !String.IsNullOrEmpty(ControlName) ? String.Format("for=\"{0}\"", ControlName) : ""
                );
        }




        /// <summary>
        /// 显示资源文件内容
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        public String ViewResourceText(String Title)
        {
            return ViewResourceText(Title, "");
        }

        /// <summary>
        /// 显示资源文件内容
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewResourceText(String Title, String DefaultValue)
        {
            return ViewResourceText(Title, DefaultValue, "Text");
        }

        /// <summary>
        /// 显示资源文件内容
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="TextType"></param>
        /// <returns></returns>
        public String ViewResourceText(String Title, String DefaultValue, String TextType)
        {
            return ViewResourceText(Title, DefaultValue, TextType, this.LocalResourceFile);
        }

        /// <summary>
        /// 显示资源文件内容
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="TextType"></param>
        /// <param name="_LocalResourceFile"></param>
        /// <returns></returns>
        public String ViewResourceText(String Title, String DefaultValue, String TextType,String _LocalResourceFile)
        {
            
            String _Title = Localization.GetString(String.Format("{0}.{1}", Title, TextType), _LocalResourceFile);
            if (String.IsNullOrEmpty(_Title))
            {
                _Title = DefaultValue;
            }
            return _Title;
        }

        /// <summary>
        /// 显示菜单的文本
        /// </summary>
        /// <param name="MenuItem">菜单项</param>
        /// <returns></returns>
        public String ShowMenuText(TokenItem MenuItem)
        {
            return ViewResourceText(MenuItem.Token, MenuItem.Title, "MenuText");
        }


        /// <summary>
        /// 计算页面执行的时间
        /// </summary>
        /// <param name="TimeStart">开始时间</param>
        public String InitTimeSpan(DateTime TimeStart)
        {
            //查询数据库所花的时间
            System.DateTime endTime =DateTime.Now;
            System.TimeSpan ts = endTime - TimeStart;
            String RunTime = string.Format("{0}秒{1}毫秒", ts.Seconds, ts.Milliseconds);
            TimeStart = endTime = DateTime.Now;
            return RunTime;
        }

        /// <summary>
        /// 显示字段标题
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="ClassName"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewTitleSpan(String Content, String ClassName, String ControlName)
        {

            return String.Format("<label  {2}><span {1} >{0}</span></label>",
                        Content,
                        !String.IsNullOrEmpty(ClassName) ? String.Format("class=\"{0}\"", ClassName) : "",
                      !String.IsNullOrEmpty(ControlName) ? String.Format("for=\"{0}\"", ControlName) : ""
                        );
        }
        #endregion

        #region "名称格式化"
        /// <summary>
        /// 搜索条件格式化
        /// </summary>
        /// <param name="Search">搜索条件</param>
        /// <returns></returns>
        public String SearchFormat(String Search)
        {
            return String.Format("{0}-{1}-{2}-{3}", Search, ModuleId, ClientID, TabId);
        }
        #endregion


        #region "获取文件后缀名和路径"

        /// <summary>
        /// 根据后缀名显示图标
        /// </summary>
        /// <param name="FileExtension">文件后缀</param>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public String GetPhotoExtension(String FileExtension, String FilePath)
        {
            FileExtension = FileExtension.ToLower();

            //先判断是否是图片格式的
            if (FileExtension == "jpg")
                return GetPhotoPath(FilePath);
            else if (FileExtension == "png")
                return GetPhotoPath(FilePath);
            else if (FileExtension == "jpeg")
                return GetPhotoPath(FilePath);
            else if (FileExtension == "gif")
                return GetPhotoPath(FilePath);
            else if (FileExtension == "bmp")
                return GetPhotoPath(FilePath);
            else if (FileExtension == "mp4")
                return GetFileIcon("video.jpg");
            else if (FileExtension == "ogv")
                return GetFileIcon("video.jpg");
            else if (FileExtension == "webm")
                return GetFileIcon("video.jpg");
            else if (FileExtension == "mp3")
                return GetFileIcon("audio.jpg");
            else if (FileExtension == "wma")
                return GetFileIcon("audio.jpg");
            else if (FileExtension == "zip")
                return GetFileIcon("zip.jpg");
            else if (FileExtension == "rar")
                return GetFileIcon("zip.jpg");
            else if (FileExtension == "7z")
                return GetFileIcon("zip.jpg");
            else if (FileExtension == "xls")
                return GetFileIcon("Document.jpg");
            else if (FileExtension == "txt")
                return GetFileIcon("text.jpg");
            else if (FileExtension == "cs")
                return GetFileIcon("code.jpg");
            else if (FileExtension == "html")
                return GetFileIcon("code.jpg");
            else if (FileExtension == "pdf")
                return GetFileIcon("pdf.jpg");
            else if (FileExtension == "doc")
                return GetFileIcon("Document.jpg");
            else if (FileExtension == "docx")
                return GetFileIcon("Document.jpg");
            else
                return GetFileIcon("Unknown type.jpg");
        }

        /// <summary>
        /// 获取图片的路径
        /// </summary>
        /// <param name="FilePath">图片路径</param>
        /// <returns></returns>
        public String GetPhotoPath(String FilePath)
        {
            return String.Format("{0}{1}", PortalSettings.HomeDirectory, FilePath);
        }

        /// <summary>
        /// 获取文件图标
        /// </summary>
        /// <param name="IconName">图标文件</param>
        /// <returns></returns>
        public String GetFileIcon(String IconName)
        {
            return String.Format("{0}Resource/images/crystal/{1}", ModulePath, IconName);
        }

        #endregion


        #region "DNN 920 的支持"

        #region "获取模块信息属性DNN920"

        /// <summary>
        /// 获取模块信息属性DNN920
        /// </summary>
        /// <param name="m">模块信息</param>
        /// <param name="Name">属性名</param>
        /// <returns></returns>
        public String ModuleProperty(ModuleInfo m, String Name)
        {
            bool propertyNotFound = false;
            return m.GetProperty(Name, "", System.Globalization.CultureInfo.CurrentCulture, UserInfo, DotNetNuke.Services.Tokens.Scope.DefaultSettings, ref propertyNotFound);
        }

        /// <summary>
        /// 获取模块信息属性DNN920
        /// </summary>
        /// <param name="Name">属性名</param>
        /// <returns></returns>
        public String ModuleProperty(String Name)
        {
            return ModuleProperty(ModuleConfiguration, Name);
        }

        #endregion

        #region "模块路径"
        /// <summary>
        /// 模块路径
        /// </summary>
        public String ModulePath
        {
            get { return ControlPath; }
        }

        #endregion

        #endregion


    }
}