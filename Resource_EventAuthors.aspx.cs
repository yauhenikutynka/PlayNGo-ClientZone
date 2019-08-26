using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;

namespace Playngo.Modules.ClientZone
{
    public partial class Resource_EventAuthors : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                

                if (!(Request.UrlReferrer != null && !String.IsNullOrEmpty(Request.UrlReferrer.ToString()) && Request.UrlReferrer.ToString() != CurrentUrl))
                {
                    Response.Redirect(Globals.NavigateURL(TabId));
                }

              
            }
            //载入模块
            LoadModule("Resource_EventAuthors.ascx", ref phEventAuthors);
        }


        protected override void Page_Init(System.Object sender, System.EventArgs e)
        {
            //调用基类Page_Init，主要用于权限验证
            base.Page_Init(sender, e);
        }
    }


}