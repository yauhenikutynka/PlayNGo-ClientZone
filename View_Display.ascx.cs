using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Modules;

using DotNetNuke.Common;

using DotNetNuke.Security.Permissions;
using System.Collections;

namespace Playngo.Modules.ClientZone
{
    public partial class View_Display : BaseModule, DotNetNuke.Entities.Modules.IActionable
    {
        #region "属性"


        private List<MenuTabItem> _MenuTabCollection = new List<MenuTabItem>();
        /// <summary>
        /// 初始化标签集合
        /// </summary>
        public List<MenuTabItem> MenuTabCollection
        {
            get
            {
                if (_MenuTabCollection == null || _MenuTabCollection.Count < 1)
                {
                    //靠XML地址切换菜单配置文件的地址
                    String MenuPath = MapPath(String.Format("{0}Resource/xml/UITabs.xml", ModulePath));


                    XmlFormat xf = new XmlFormat(MenuPath);

                    _MenuTabCollection = xf.ToList<MenuTabItem>();


                }
                return _MenuTabCollection;
            }
        }




        #endregion


        #region "事件"

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    panNavigation.Visible = IsEdit;
 
                    hlManager.NavigateUrl = xUrl();

                }
            }
            catch (Exception exc) //Module failed to load
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        protected void Page_Init(System.Object sender, System.EventArgs e)
        {
            try
            {

                //加载显示界面脚本
                LoadViewScript();

                BindJavaScriptFile("jquery.tmpl.min.js", String.Format("{0}Resource/js/jquery.tmpl.min.js", ModulePath));
                BindJavaScriptFile("jquery.cookie.js", String.Format("{0}Resource/js/jquery.cookie.js", ModulePath));

                //BindJavaScriptFile("html2canvas.js", String.Format("{0}Resource/js/html2canvas.js", ModulePath));
                BindJavaScriptFile("jspdf.debug.js", String.Format("{0}Resource/js/jspdf.debug.js", ModulePath));

                BindJavaScriptFile("dom-to-image.js", String.Format("{0}Resource/js/dom-to-image.js", ModulePath));

                BindStyleFile("datatables.min.css", String.Format("{0}Resource/plugins/datatables/datatables.min.css", ModulePath));
                BindJavaScriptFile("datatables.min.js", String.Format("{0}Resource/plugins/datatables/datatables.min.js", ModulePath));
                BindStyleFile("responsive.dataTables.min.css", String.Format("{0}Resource/plugins/datatables/responsive.dataTables.min.css", ModulePath));
                BindJavaScriptFile("dataTables.responsive.min.js", String.Format("{0}Resource/plugins/datatables/dataTables.responsive.min.js", ModulePath));


                //BindJavaScriptFile("easyResponsiveTabs.js", String.Format("{0}Resource/plugins/easyResponsiveTabs/easyResponsiveTabs.js", ModulePath));
                BindJavaScriptFile("bootstrap-paginator.min.js", String.Format("{0}Resource/plugins/bootstrap-paginator/bootstrap-paginator.min.js", ModulePath));

                //BindStyleFile("owl.carousel.css", String.Format("{0}Resource/plugins/OwlCarousel2/owl.carousel.css", ModulePath));
                //BindJavaScriptFile("owl.carousel.min.js", String.Format("{0}Resource/plugins/OwlCarousel2/owl.carousel.min.js", ModulePath));

                BindStyleFile("select2.min.css", String.Format("{0}Resource/plugins/select2/select2.min.css", ModulePath));
                BindJavaScriptFile("select2.full.min.js", String.Format("{0}Resource/plugins/select2/select2.full.min.js", ModulePath));


                //加载主要的前台样式
                String CSSName = String.Format("{0}_css", "mian-view-ui");
                String CSSPath = String.Format("{0}Resource/css/mian-view-ui.css", ModulePath);
                BindStyleFile(CSSName, CSSPath);

                String JSName = String.Format("{0}_js", "mian-view-ui");
                String JSPath = String.Format("{0}Resource/js/mian-view-ui.js", ModulePath);
                BindJavaScriptFile(JSName, JSPath);



                //绑定Tabs和容器中的控件

                //绑定菜单
                BindTopMenus();
                //绑定区域
                BindNavJurisdictions();
                //绑定容器内的子控件
                BindContainer();



            }
            catch (Exception exc) //Module failed to load
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// 页面最后加载
        /// </summary>
        protected void Page_PreRender(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                }
            }
            catch (Exception exc) //Module failed to load
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, exc);
            }
        }




        #endregion

        #region "方法"

        /// <summary>
        /// 绑定顶部菜单
        /// </summary>
        private void BindTopMenus()
        {
            var XMLDB = GetTemplateDB("TopTabs");
            TemplateFormat xf = new TemplateFormat(this);
            Hashtable Puts = new Hashtable();
            //当前激活的菜单
            Puts.Add("UIToken", UIToken);
            //所有菜单的列表
            Puts.Add("AllTabList", MenuTabCollection);
            Puts.Add("TopTabList", MenuTabCollection.FindAll(r => String.IsNullOrEmpty(r.Parent)));

            liTopMenus.Text = ViewTemplate(XMLDB, "View_Template.html", Puts, xf);
        }

        /// <summary>
        /// 绑定区域数据
        /// </summary>
        private void BindNavJurisdictions()
        {
            var XMLDB = GetTemplateDB("NavJurisdictions");
            TemplateFormat xf = new TemplateFormat(this);
            Hashtable Puts = new Hashtable();

            Int32 RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Orderfld = Playngo_ClientZone_Jurisdiction._.Sort;
            qp.OrderType = 0;

            qp.Where.Add(new SearchParam(Playngo_ClientZone_Jurisdiction . _.PortalId, PortalId, SearchType.Equal));

            //权限筛选
            qp = CreateQueryByRoles(qp);

            var Jurisdictions = new List<Playngo_ClientZone_Jurisdiction>();
            Jurisdictions.Add(new Playngo_ClientZone_Jurisdiction() { ID = 0, Name = "All" });

            var items = Playngo_ClientZone_Jurisdiction.FindAll(qp, out RecordCount);
            if (items != null && items.Count > 0)
            {
                Jurisdictions.AddRange(items);
            }
            Puts.Add("Jurisdictions", Jurisdictions);

         

            liNavJurisdictions.Text = ViewTemplate(XMLDB, "View_Template.html", Puts, xf);
        }



         



        /// <summary>
        /// 绑定列表数据到容器
        /// </summary>
        private void BindContainer()
        {
            MenuTabItem _MenuTabItem = MenuTabCollection.Exists(r=>r.Token.ToLower() == UIToken.ToLower()) ? MenuTabCollection.Find(r => r.Token.ToLower() == UIToken.ToLower()) : new MenuTabItem();

            if (_MenuTabItem != null && !String.IsNullOrEmpty(_MenuTabItem.Token) && !this.DesignMode)
            {
          
                //加载相应的控件
                BaseModule ManageContent = new BaseModule();
                string ContentSrc = ResolveClientUrl(string.Format("{0}/{1}", this.TemplateSourceDirectory, _MenuTabItem.Src));

                if (System.IO.File.Exists(MapPath(ContentSrc)))
                {
                    ManageContent = (BaseModule)LoadControl(ContentSrc);
                    ManageContent.ModuleConfiguration = ModuleConfiguration;
                    ManageContent.ID = _MenuTabItem.Token;
                    ManageContent.LocalResourceFile = Localization.GetResourceFile(this, string.Format("{0}.resx", _MenuTabItem.Src));
                    phContainer.Controls.Add(ManageContent);
                }
            }
            else if (!String.IsNullOrEmpty(Token) && Token.ToLower() == "error")
            {
                //加载相应的控件
                BaseModule ManageContent = new BaseModule();
                string ContentSrc = ResolveClientUrl(string.Format("{0}/{1}", this.TemplateSourceDirectory, "Manager_ErrorCatch.ascx"));

                if (System.IO.File.Exists(MapPath(ContentSrc)))
                {
                    ManageContent = (BaseModule)LoadControl(ContentSrc);
                    ManageContent.ModuleConfiguration = ModuleConfiguration;
                    ManageContent.ID = "ErrorCatch";
                    ManageContent.LocalResourceFile = Localization.GetResourceFile(this, string.Format("{0}.resx", "Manager_ErrorCatch.ascx"));
                    phContainer.Controls.Add(ManageContent);
                }
                //标题
                Page.Title = String.Format("{0} - {1}", "Error", ModuleConfiguration.ModuleTitle);

            }

             
        }

        #endregion



        #region Optional Interfaces
        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();

  
                Actions.Add(this.GetNextActionID(), Localization.GetString("Manager", this.LocalResourceFile), ModuleActionType.AddContent, "", "settings.gif", xUrl(), false, SecurityAccessLevel.Edit, true, false);

         

                return Actions;
            }
        }
        #endregion
    }
}