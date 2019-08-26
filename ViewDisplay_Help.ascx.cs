using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class ViewDisplay_Help : BaseModule
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

                var XMLDB = GetTemplateDB("Help");
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


            //动态模块的配置
            if (!String.IsNullOrEmpty(ContentHTML) && ContentHTML.IndexOf("[DynamicModules]", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                //获取动态模块和动态项
                Puts = GetDynamics(Puts);

                ContentHTML = Common.ReplaceNoCase(ContentHTML, "[DynamicModules]", ViewTemplate(GetTemplateDB("DynamicModules"), "View_Template.html", Puts, xf));
            }




            liContentHTML.Text = ContentHTML;


        }

        /// <summary>
        /// 获取动态模块和动态项
        /// </summary>
        /// <param name="Puts"></param>
        /// <returns></returns>
        public Hashtable GetDynamics(Hashtable Puts)
        {

            var DynamicModules = Playngo_ClientZone_DynamicModule.FindViewAllByFilter(0, (Int32)EnumDynamicModuleType.Help, Settings_ModuleID,this);
   
            var DynamicItems = new List<Playngo_ClientZone_DynamicItem>();
 
            if (DynamicModules != null && DynamicModules.Count > 0)
            {
                foreach (var DynamicModule in DynamicModules)
                {
                    var minDynamicItems =  Playngo_ClientZone_DynamicItem.FindListByFilter(DynamicModule.ID, Settings_ModuleID);
                    if (minDynamicItems != null && minDynamicItems.Count > 0)
                    {
                        DynamicItems.AddRange(minDynamicItems);
                    }
                }
            }

            Puts.Add("DynamicModules", DynamicModules);
            Puts.Add("DynamicItems", DynamicItems);
 
            return Puts;
        }






        #endregion
    }
}