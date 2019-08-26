using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class ViewDisplay_MyAccount : BaseModule
    {

        #region "事件"


        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                //加载主题相关的脚本
                //LoadThemeScript();

                var XMLDB = GetTemplateDB("MyAccount");
                if (!String.IsNullOrEmpty(XMLDB.Name))
                {

                    if (!IsPostBack)
                    {
                        //绑定数据项到前台
                        DataListBind(XMLDB);

                    }



                    BindXmlDBToPage(XMLDB, "Templates");


                }
                else
                {
                    //未绑定效果
                    liContentHTML.Text = ViewNoTemplate();
                }

            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }



        }

        #endregion





        #region "方法"

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataListBind(TemplateDB XmlDB)
        {

            TemplateFormat xf = new TemplateFormat(this);
            xf.TemplateName = XmlDB.Name;
            Hashtable Puts = new Hashtable();




            String ContentHTML = ViewTemplate(XmlDB, "View_Template.html", Puts, xf);

 

            liContentHTML.Text = ContentHTML;


        }
         





        #endregion
    }
}