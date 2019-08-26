using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Common.Utilities;

namespace Playngo.Modules.ClientZone
{
    public partial class Resource_MultiImages : BaseModule
    {

        #region "属性"

        private SettingEntity _FieldItem;
        /// <summary>
        /// 字段设置
        /// </summary>
        public SettingEntity FieldItem
        {
            get { return _FieldItem; }
            set { _FieldItem = value; }
        }

        public String QueryString
        {
            get { return String.Format("{0}&ModulePath={1}", WebHelper.GetScriptNameQueryString, HttpUtility.UrlEncode(ModulePath)); }
        }


        private String _ClientName = "MultiImages";
        /// <summary>
        /// 控件的名称
        /// </summary>
        public String ClientName
        {
            get {
                if (_ClientName == "MultiImages" && FieldItem != null && !String.IsNullOrEmpty(FieldItem.Name))
                {
                    _ClientName = ControlHelper.GetMultiImagesName(FieldItem);
                }
                return _ClientName; }
        }


 


        #endregion

        #region "事件"

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindPageItem();
                }
                else
                {
                    String urllink = WebHelper.GetStringParam(Request, hfImages.UniqueID, "");
                    if (Context.Items.Contains(ClientName))
                    {
                        Context.Items[ClientName] = String.IsNullOrEmpty(urllink) ? "" : string.Format(",{0},", urllink.Trim(','));
                    }
                    else
                    {
                        Context.Items.Add(ClientName, String.IsNullOrEmpty(urllink) ? "" : string.Format(",{0},", urllink.Trim(',')));
                    }
                }
            }
            catch (Exception exc)
            {
                ProcessModuleLoadException(exc);
            }
        }
        #endregion

        #region "方法"
        /// <Description>
        /// 绑定页面项
        /// </Description>
        private void BindPageItem()
        {
            hfImages.Value = String.IsNullOrEmpty(FieldItem.DefaultValue) ? "": string.Format(",{0},", FieldItem.DefaultValue.Trim(','));


        }
         

        #endregion

    }
}