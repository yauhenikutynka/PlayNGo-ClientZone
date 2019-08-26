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
    public partial class View_Index : BaseModule, DotNetNuke.Entities.Modules.IActionable
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
                    //hlManager.Visible = UserId > 0 &&  ModulePermissionController.HasModuleAccess(SecurityAccessLevel.Edit, "CONTENT", ModuleConfiguration);

                  
 
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
                //加载主要的前台样式
                String CSSName = String.Format("{0}_css", "mian-view-ui");
                String CSSPath = String.Format("{0}Resource/css/mian-view-ui.css", ModulePath);
                BindStyleFile(CSSName, CSSPath);
         



                //博客初始化数据
                //Initialization init = new Initialization(this);
                //init.Init();



                //绑定Tabs和容器中的控件
                
                    ////绑定菜单
                    //BindTopMenus();
                    ////绑定容器内的子控件
                    //BindContainer();

                

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