using System;
using System.Collections.Generic;
using System.Web;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Host;

using DotNetNuke.Entities.Tabs;
using System.IO;
using DotNetNuke.Services.Localization;
using System.Collections;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Security;
using System.Web.UI.WebControls;
using System.Globalization;
using DotNetNuke.Entities.Controllers;

namespace Playngo.Modules.ClientZone
{
    public class BasePage : DotNetNuke.Framework.PageBase
    {

        #region "扩展的属性和方法"


        /// <summary>
        /// 模块编号
        /// </summary>
        public Int32 ModuleId = WebHelper.GetIntParam(HttpContext.Current.Request, "ModuleId", 0);

        public Int32 PortalId = WebHelper.GetIntParam(HttpContext.Current.Request, "PortalId", 0);
        public Int32 TabId = WebHelper.GetIntParam(HttpContext.Current.Request, "TabId", 0);



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
            get
            {
                if (!(_Settings_TabInfo != null && _Settings_TabInfo.TabID > 0))
                {
                    _Settings_TabInfo = new TabController().GetTab(Settings_TabID, PortalId, true);
                }
                return _Settings_TabInfo;
            }
        }


        private PortalSettings _PortalSettings = new PortalSettings();
        /// <summary>
        /// 重写获取站点配置
        /// </summary>
        public PortalSettings PortalSettings
        {
            get
            {
                if (!(_PortalSettings != null && _PortalSettings.PortalId > 0))
                {
                    if (Settings_PortalID != PortalId)
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




        private Hashtable _settings = new Hashtable();
        /// <summary>
        /// 模块设置
        /// </summary>
        public Hashtable Settings
        {
            get
            {
                ModuleController controller = new ModuleController();
                if (!(_settings != null && _settings.Count > 0))
                {
                    _settings = new Hashtable(controller.GetModuleSettings(ModuleId));
                }
                return _settings;
            }
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
                    _ClientZone_Settings = new ModuleController().GetModule(Settings_ModuleID).ModuleSettings;
                }
                return _ClientZone_Settings;
            }
        }
        /// <summary>
        /// 是否开启SSL
        /// </summary>
        public Boolean IsSSL
        {
            get { return TabInfo.IsSecure || WebHelper.GetSSL; }
        }




        /// <summary>
        /// 模块地址
        /// </summary>
        public string ModulePath
        {
            get { return this.TemplateSourceDirectory + "/"; }
        }


        private ModuleInfo _ModuleConfiguration = new ModuleInfo();
        /// <summary>
        /// 模块信息
        /// </summary>
        public ModuleInfo ModuleConfiguration
        {
            get
            {
                if (!(_ModuleConfiguration != null && _ModuleConfiguration.ModuleID > 0) && ModuleId > 0)
                {
                    ModuleController mc = new ModuleController();
                    _ModuleConfiguration = mc.GetModule(ModuleId, TabId);

                }
                return _ModuleConfiguration;
            }
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
        /// 配色方案
        /// </summary>
        public String ColorSchemes
        {
            get { return GetSetting<String>("General", "ColorSchemes", "light"); }
        }

        #endregion


        #region "获取DNN对象"




        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            get { return UserController.GetCurrentUserInfo(); }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId
        {
            get
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    return UserInfo.UserID;
                }
                else
                {
                    return Null.NullInteger;
                }
            }
        }





        private TabInfo _tabInfo;
        /// <summary>
        /// 页面信息
        /// </summary>
        public TabInfo TabInfo
        {
            get
            {
                if (!(_tabInfo != null && _tabInfo.TabID > 0) && TabId > 0)
                {
                    TabController tc = new TabController();
                    _tabInfo = tc.GetTab(TabId);

                }

                return _tabInfo;


            }
        }
 
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public Boolean IsAdministrator
        {
            get { return UserInfo.IsSuperUser || UserInfo.IsInRole("Administrators"); }
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
            get
            {
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
            get { return WebHelper.GetStringParam(Request, "language", PageCulture.Name); }
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
                    if (Settings_PortalID == PortalId)
                    {
                        _PortalUrl = String.Format("{0}://{1}",IsSSL? "https" : "http", WebHelper.GetHomeUrl());
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


        /// <summary>
        /// 验证登陆状态(没有登陆跳转到登陆页面)
        /// </summary>
        public void VerificationLogin()
        {
            //没有登陆的用户
            if (!(UserId > 0))
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.LoginTabId, "Login", "returnurl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl)));

            }
        }

        /// <summary>
        /// 验证作者状态(不是作者跳转到登陆页面)
        /// </summary>
        public void VerificationAuthor()
        {
            //没有登陆的用户
            if (!(UserId > 0))
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.LoginTabId, "Login", "returnurl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl)));
            }
            else if (!ModulePermissionController.HasModuleAccess(SecurityAccessLevel.Edit, "CONTENT", ModuleConfiguration))
            {
                Response.Redirect(Globals.NavigateURL(TabId));
            }



          
        }
        /// <summary>
        /// 当前页面地址
        /// </summary>
        public String CurrentUrl
        {
            get { return String.Format("{2}://{0}{1}", WebHelper.GetScriptName, WebHelper.GetScriptUrl, IsSSL ? "https" : "http"); }
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
            return HostController.Instance.GetString(key, defaultValue); ;
        }

        #endregion

        #region "新的后台URL"

        /// <summary>
        /// URL转换默认名
        /// </summary>
        /// <returns></returns>
        public String xUrlToken()
        {
            return !String.IsNullOrEmpty(BaseModuleName) && BaseModuleName == "Playngo_ClientZone" ? "EventList" : "Effect_Options";
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

        public string xUrl(string KeyName, string KeyValue, string ControlKey, params string[] AddParameters)
        {
            return xUrl(TabId, ModuleId, KeyName, KeyValue, ControlKey, AddParameters);
        }

        public string xUrl(Int32 _TabId, Int32 _ModuleId, string KeyName, string KeyValue, string ControlKey, params string[] AddParameters)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            String language = WebHelper.GetStringParam(Request, "language", PortalSettings.DefaultLanguage);

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

        #region "SEO配置属性"

        /// <summary>
        /// SEO URL参数,可以设置EventID
        /// </summary>
        public String Settings_Seo_UrlEventID
        {
            get { return GetSetting<String>("SEO", "SEOUrlParameter", "EventID"); }
        }


        /// <summary>
        /// SEO URL参数,FriendlyUrl 开关
        /// </summary>
        public Boolean Settings_Seo_FriendlyUrl
        {
            get { return GetSetting<Boolean>("SEO", "FriendlyUrl", true); }
        }

        #endregion

        #region "有关皮肤效果的设置"

        private String _Settings_SkinName = String.Empty;
        /// <summary>
        /// 获取绑定的皮肤名
        /// </summary>
        public String Settings_SkinName
        {
            get
            {
                if (String.IsNullOrEmpty(_Settings_SkinName))
                {
                    _Settings_SkinName = Settings["ClientZone_SkinName"] != null ? Convert.ToString(Settings["ClientZone_SkinName"]) : "Skin_01_Calendar";
                }
                return _Settings_SkinName;

            }
        }


        private String _Settings_SkinThemeName = String.Empty;
        /// <summary>
        /// 获取绑定的皮肤名(小皮肤)
        /// </summary>
        public String Settings_SkinThemeName
        {
            get
            {
                if (String.IsNullOrEmpty(_Settings_SkinThemeName))
                {
                    _Settings_SkinThemeName = Settings["ClientZone_SkinThemeName"] != null ? Convert.ToString(Settings["ClientZone_SkinThemeName"]) : "Skin_01_Default";
                }
                return _Settings_SkinThemeName;

            }
        }







        private TemplateDB _Settings_SkinDB = new TemplateDB();
        /// <summary>
        /// 获取绑定皮肤配置
        /// </summary>
        public TemplateDB Settings_SkinDB
        {
            get
            {
                if (!(_Settings_SkinDB != null && !String.IsNullOrEmpty(_Settings_SkinDB.Name)))
                {
                    String XmlDBPath = Server.MapPath(String.Format("{0}Template_Effect/{1}/TemplateDB.xml", ModulePath, Settings_SkinName));
                    if (File.Exists(XmlDBPath))
                    {
                        XmlFormat xf = new XmlFormat(XmlDBPath);
                        _Settings_SkinDB = xf.ToItem<TemplateDB>();
                    }
                }
                return _Settings_SkinDB;
            }
        }




        private List<SettingEntity> _Settings_SkinSettingDB = new List<SettingEntity>();
        /// <summary>
        /// 获取绑定皮肤设置项
        /// </summary>
        public List<SettingEntity> Settings_SkinSettingDB
        {
            get
            {
                if (!(_Settings_SkinSettingDB != null && _Settings_SkinSettingDB.Count > 0))
                {
                    String EffectSettingDBPath = Server.MapPath(String.Format("{0}Template_Effect/{1}/TemplateSetting.xml", ModulePath, Settings_SkinName));
                    if (File.Exists(EffectSettingDBPath))
                    {
                        XmlFormat xf = new XmlFormat(EffectSettingDBPath);
                        _Settings_SkinSettingDB = xf.ToList<SettingEntity>();
                    }
                }
                return _Settings_SkinSettingDB;
            }
        }




        private List<SettingEntity> _Settings_ItemSettingDB = new List<SettingEntity>();
        /// <summary>
        /// 获取绑定数据设置项(非效果)
        /// </summary>
        public List<SettingEntity> Settings_ItemSettingDB
        {
            get
            {
                if (!(_Settings_ItemSettingDB != null && _Settings_ItemSettingDB.Count > 0))
                {
                    if (!String.IsNullOrEmpty(Settings_SkinName))
                    {
                        //效果的数据项
                        String ItemSettingSkinPath = Server.MapPath(String.Format("{0}Template_Effect/{1}/ItemSetting.xml", ModulePath, Settings_SkinName));
                        if (File.Exists(ItemSettingSkinPath))
                        {
                            XmlFormat xf = new XmlFormat(ItemSettingSkinPath);
                            _Settings_ItemSettingDB.AddRange(xf.ToList<SettingEntity>());
                        }
                    }

                    if (!String.IsNullOrEmpty(Settings_SkinDetailName))
                    {
                        //皮肤详情的数据项
                        String ItemSettingSkinPath = Server.MapPath(String.Format("{0}Template_Detail/{1}/ItemSetting.xml", ModulePath, Settings_SkinDetailName));
                        if (File.Exists(ItemSettingSkinPath))
                        {
                            XmlFormat xf = new XmlFormat(ItemSettingSkinPath);
                            _Settings_ItemSettingDB.AddRange(xf.ToList<SettingEntity>());
                        }
                    }



                    //全局的数据项
                    String ItemSettingGlobalPath = Server.MapPath(String.Format("{0}Resource/xml/ItemSetting.xml", ModulePath));
                    if (File.Exists(ItemSettingGlobalPath))
                    {
                        XmlFormat xf = new XmlFormat(ItemSettingGlobalPath);
                        _Settings_ItemSettingDB.AddRange(xf.ToList<SettingEntity>());
                    }
                }
                return _Settings_ItemSettingDB;
            }
        }

        #endregion

        #region "有关皮肤详情的设置"

        /// <summary>
        /// 获取绑定的详情皮肤名称
        /// </summary>
        public String Settings_SkinDetailName
        {
            get { return Settings["ClientZone_SkinDetailName"] != null ? Convert.ToString(Settings["ClientZone_SkinDetailName"]) : "EventDetail_01"; }
        }




        /// <summary>
        /// 获取绑定的详情皮肤名(小皮肤)
        /// </summary>
        public String Settings_SkinDetailThemeName
        {
            get { return Settings["ClientZone_SkinDetailThemeName"] != null ? Convert.ToString(Settings["ClientZone_SkinDetailThemeName"]) : "EventDetail_01_Default"; }
        }






        private TemplateDB _Settings_SkinDetailDB = new TemplateDB();
        /// <summary>
        /// 获取绑定详情皮肤配置
        /// </summary>
        public TemplateDB Settings_SkinDetailDB
        {
            get
            {
                if (!(_Settings_SkinDetailDB != null && !String.IsNullOrEmpty(_Settings_SkinDetailDB.Name) && !String.IsNullOrEmpty(Settings_SkinDetailName)))
                {
                    String XmlDBPath = Server.MapPath(String.Format("{0}Template_Detail/{1}/TemplateDB.xml", ModulePath, Settings_SkinDetailName));
                    if (File.Exists(XmlDBPath))
                    {
                        XmlFormat xf = new XmlFormat(XmlDBPath);
                        _Settings_SkinDetailDB = xf.ToItem<TemplateDB>();
                    }
                }
                return _Settings_SkinDetailDB;
            }
        }


        private List<SettingEntity> _Settings_SkinDetailSettingDB = new List<SettingEntity>();
        /// <summary>
        /// 获取绑定详情皮肤设置项
        /// </summary>
        public List<SettingEntity> Settings_SkinDetailSettingDB
        {
            get
            {
                if (!(_Settings_SkinDetailSettingDB != null && _Settings_SkinDetailSettingDB.Count > 0) && !String.IsNullOrEmpty(Settings_SkinDetailName))
                {
                    String SkinSettingDBPath = Server.MapPath(String.Format("{0}Template_Detail/{1}/TemplateSetting.xml", ModulePath, Settings_SkinDetailName));
                    if (File.Exists(SkinSettingDBPath))
                    {
                        XmlFormat xf = new XmlFormat(SkinSettingDBPath);
                        _Settings_SkinDetailSettingDB = xf.ToList<SettingEntity>();
                    }
                }
                return _Settings_SkinDetailSettingDB;
            }
        }
        #endregion

        #region "绑定页面标题和帮助"

        /// <summary>
        /// 显示标题
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewTitle(String Title, String DefaultValue)
        {
            return ViewTitle(Title, DefaultValue, "");
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
        public String ViewTitle(String Title, String DefaultValue, String ControlName, String ClassName)
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
            String Content = ViewResourceText(Title, DefaultValue, "Help");
            return ViewSpan(Content, "", "span_help");
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

            return String.Format("<label  {2}><span {1} >{0}</span></label>",
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
            String _Title = Localization.GetString(String.Format("{0}.{1}", Title, TextType), this.LocalResourceFile);
            if (String.IsNullOrEmpty(_Title))
            {
                _Title = DefaultValue;
            }
            return _Title;
        }




        /// <summary>
        /// 计算页面执行的时间
        /// </summary>
        /// <param name="TimeStart">开始时间</param>
        public String InitTimeSpan(DateTime TimeStart)
        {
            //查询数据库所花的时间
            System.DateTime endTime = DateTime.Now;
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
            int oldOffset = 0;
            var PortalSettings = PortalController.Instance.GetCurrentPortalSettings();
            if (PortalSettings != null && PortalSettings.UserId > Null.NullInteger)
            {
                int.TryParse(PortalSettings.UserInfo.Profile.PreferredTimeZone.BaseUtcOffset.TotalMinutes.ToString(CultureInfo.InvariantCulture), out oldOffset);
            }
            else
            {
                int.TryParse(PortalSettings.TimeZone.BaseUtcOffset.TotalMinutes.ToString(CultureInfo.InvariantCulture), out oldOffset);
            }
            return Localization.ConvertLegacyTimeZoneOffsetToTimeZoneInfo(oldOffset).ToString();
        }

        #endregion

        #region "验证脚本多语言"
        /// <summary>
        /// 获取当前验证引擎语言文件的URL
        /// </summary>
        /// <returns></returns>
        public String ViewValidationEngineLanguage()
        {
            String VEL = String.Format("{0}Resource/js/jquery.validationEngine-en.js?cdv={1}", ModulePath, CrmVersion);
            String language = WebHelper.GetStringParam(Request, "language", PortalSettings.DefaultLanguage).ToLower(); ;
            if (!String.IsNullOrEmpty(language) && language != "en-us")
            {
                //先判断这个语言文件是否存在
                String webJS = String.Format("{0}Resource/plugins/validation/jquery.validationEngine-{1}.js", ModulePath, language);
                String serverJS = MapPath(webJS);
                if (File.Exists(serverJS))
                {
                    VEL = String.Format("{0}?cdv={1}", webJS, CrmVersion);
                }
                else if (language.IndexOf("-") >= 0)
                {
                    String lTemp = language.Remove(language.IndexOf("-"));
                    webJS = String.Format("{0}Resource/plugins/validation/jquery.validationEngine-{1}.js", ModulePath, lTemp);
                    serverJS = MapPath(webJS);
                    if (File.Exists(serverJS))
                    {
                        VEL = String.Format("{0}?cdv={1}", webJS, CrmVersion);
                    }
                }
            }
            return VEL;
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

        #region "载入模块"
        /// <summary>
        /// 载入模块
        /// </summary>
        /// <param name="ModuleSrc"></param>
        /// <param name="phContainer"></param>
        public void LoadModule(String ModuleSrc,ref PlaceHolder phContainer)
        {
            BaseModule ManageContent = new BaseModule();
            ManageContent.ID = ModuleSrc.Replace(".ascx", "");
            String ContentSrc = ResolveClientUrl(string.Format("{0}/{1}", this.TemplateSourceDirectory, ModuleSrc));
            ManageContent = (BaseModule)LoadControl(ContentSrc);
            ManageContent.ModuleConfiguration = this.ModuleConfiguration;
            ManageContent.LocalResourceFile = Localization.GetResourceFile(this, string.Format("{0}.resx", ModuleSrc));
            phContainer.Controls.Add(ManageContent);
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

        }

        /// <summary>
        /// 主题参数保存名称格式化
        /// </summary>
        /// <param name="ThemeFile"></param>
        /// <param name="ThemeName"></param>
        /// <param name="ThemeID"></param>
        /// <returns></returns>
        public String ThemeXmlSettingsFormat(String ThemeFile, String ThemeName, Int32 ThemeID)
        {
            return String.Format("ClientZoneViews_{0}_{1}_{2}", ThemeID, ThemeName, ThemeFile);
        }


        /// <summary>
        /// 效果参数保存名称格式化
        /// </summary>
        /// <param name="EffectName">效果名</param>
        /// <param name="ThemeName">主题名</param>
        /// <returns></returns>
        public String SettingsFormat(String EffectName, String ThemeName)
        {
            return String.Format("ClientZone{0}_{1}", EffectName, ThemeName);
        }

        #endregion

        #region "读取模块设置"
        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewSkinSetting(String Name, object DefaultValue)
        {
            String SettingKey = SettingsFormat(Settings_SkinName, Name);
            return Settings[SettingKey] != null ? Settings[SettingKey] : DefaultValue;
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewSkinSettingT<T>(String Name, object DefaultValue)
        {
            object o = ViewSkinSetting(Name, DefaultValue);
            return (T)Convert.ChangeType(o, typeof(T));
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
            return Settings[Name] != null ? Settings[Name] : DefaultValue;
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
            return (T)Convert.ChangeType(ViewSetting(Name, DefaultValue), typeof(T));
        }


        #endregion

        #region "显示URL控件存放的值"


        /// <summary>
        /// 显示URL控件存放的值
        /// </summary>
        /// <param name="UrlValue"></param>
        /// <returns></returns>
        public String ViewLinkUrl(String UrlValue)
        {
            return ViewLinkUrl(UrlValue, true);
        }

        /// <summary>
        /// 显示URL控件存放的值
        /// </summary>
        /// <param name="UrlValue"></param>
        /// <param name="IsPhotoExtension">是否显示扩展名图片</param>
        /// <returns></returns>
        public String ViewLinkUrl(String UrlValue, Boolean IsPhotoExtension)
        {
            String DefaultValue = String.Empty;
            if (!String.IsNullOrEmpty(UrlValue) && UrlValue != "0")
            {
                if (UrlValue.IndexOf("FileID=", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    int FileID = 0;
                    if (int.TryParse(UrlValue.Replace("FileID=", ""), out FileID) && FileID > 0)
                    {
                        var fi = DotNetNuke.Services.FileSystem.FileManager.Instance.GetFile(FileID);
                        if (fi != null && fi.FileId > 0)
                        {
                            DefaultValue = string.Format("{0}{1}{2}", PortalSettings.HomeDirectory, fi.Folder, Server.UrlPathEncode(fi.FileName));
                        }
                    }
                }
                else if (UrlValue.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    DefaultValue = String.Format("{0}Resource/images/no_image.png", ModulePath);

                    int MediaID = 0;
                    if (int.TryParse(UrlValue.Replace("MediaID=", ""), out MediaID) && MediaID > 0)
                    {
                        Playngo_ClientZone_Files Multimedia = Playngo_ClientZone_Files.FindByID(MediaID);
                        if (Multimedia != null && Multimedia.ID > 0)
                        {
                            if (IsPhotoExtension)
                            {
                                DefaultValue = Server.UrlPathEncode(GetPhotoExtension(Multimedia.FileExtension, Multimedia.FilePath));// String.Format("{0}{1}", bpm.ClientZone_PortalSettings.HomeDirectory, Multimedia.FilePath);
                            }
                            else
                            {
                                DefaultValue = Server.UrlPathEncode(GetPhotoPath(Multimedia.FilePath));
                            }
                        }
                    }
                }
                else if (UrlValue.IndexOf("TabID=", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {

                    DefaultValue = Globals.NavigateURL(Convert.ToInt32(UrlValue.Replace("TabID=", "")), false, PortalSettings, Null.NullString, "", "");

                }
                else
                {
                    DefaultValue = UrlValue;
                }
            }
            return DefaultValue;

        }
        #endregion


        #region "Page_Init 权限验证"
        /// <summary>
        /// 关于权限验证
        /// </summary>
        protected virtual void Page_Init(System.Object sender, System.EventArgs e)
        {

            //如果不是此模块,则会抛出异常,提示非法入侵
            if (!(("Playngo_ClientZone_Display,Playngo_ClientZone").IndexOf(BaseModuleName, StringComparison.CurrentCultureIgnoreCase) >= 0))
            {
                Response.Redirect(Globals.NavigateURL(TabId), true);
            }

            //没有登陆的用户
            if (!(UserId > 0))
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.LoginTabId, "Login", "returnurl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl)), true);
            }
            else if (!ModulePermissionController.HasModuleAccess(SecurityAccessLevel.Edit, "CONTENT", ModuleConfiguration))
            {
                Response.Redirect(Globals.NavigateURL(TabId), true);
            }
        }
        #endregion


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
            return m.GetProperty(Name, "", null, UserInfo, DotNetNuke.Services.Tokens.Scope.DefaultSettings, ref propertyNotFound);
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

        #endregion

    }
}