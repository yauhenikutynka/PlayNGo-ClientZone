﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Resource_Attachments1 : BasePage
    {

        public String Display = WebHelper.GetStringParam(HttpContext.Current.Request, "Display", "list");


        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                


                hlNavigation_add.NavigateUrl = String.Format("{0}Resource_Attachments.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}&Display=add", ModulePath, PortalId, TabId, ModuleId, language);
                hlNavigation_list.NavigateUrl = String.Format("{0}Resource_Attachments.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}&Display=list", ModulePath, PortalId, TabId, ModuleId, language);

            
            }

            if (Display == "list")
            {
                panel_tab3_example2.CssClass = "tab-pane in active";
                liNavigation_list.Attributes.Add("class", "active");
                //Resource_DropzoneUpload1.Visible = false;
                //载入模块
                LoadModule("Resource_Attachments.ascx", ref phAttachments);
            }
            else
            {
                panel_tab3_example1.CssClass = "tab-pane in active";
                liNavigation_add.Attributes.Add("class", "active");
                //Resource_Attachments2.Visible = false;
                //载入模块
                LoadModule("Resource_DropzoneUpload.ascx", ref phDropzoneUpload);

            }
        }


        protected override void Page_Init(System.Object sender, System.EventArgs e)
        {
            //调用基类Page_Init，主要用于权限验证
            base.Page_Init(sender, e);
        }
    }
}