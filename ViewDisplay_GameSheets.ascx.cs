using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class ViewDisplay_GameSheets : BaseModule
    {


        #region "属性"
         

        private Playngo_ClientZone_GameSheet _DataItem = new Playngo_ClientZone_GameSheet();
        /// <summary>
        /// 数据实体
        /// </summary>
        public Playngo_ClientZone_GameSheet DataItem
        {
            get
            {
                if (!(_DataItem != null && _DataItem.ID > 0))
                {
                    _DataItem = Playngo_ClientZone_GameSheet.FindItemByFriendlyUrl(DetailId, FriendlyUrl, Settings_ModuleID);


                }

                return _DataItem;
            }
        }


       

       










        #endregion

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

                var XMLDB = GetTemplateDB("GameSheets");
                if (!String.IsNullOrEmpty(XMLDB.Name))
                {

                    if (!IsPostBack)
                    {
                        if (IsDetail)
                        {
                            //绑定数据项到前台
                            DataItemBind(XMLDB);
                        }
                        else
                        {
                            //绑定数据列表到前台
                            DataListBind(XMLDB);
                        }

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


            
 
            liContentHTML.Text = ViewTemplate(XmlDB, "View_Template.html", Puts, xf);


        }

         



        /// <summary>
        /// 数据项绑定
        /// </summary>
        private void DataItemBind(TemplateDB XmlDB)
        {
            String ContentHTML = String.Empty;
            TemplateFormat xf = new TemplateFormat(this);
            xf.TemplateName = XmlDB.Name;
            Hashtable Puts = new Hashtable();
           
            if (DataItem != null && DataItem.ID > 0)
            {
                if (DataItem.Status == (Int32)EnumStatus.Published && IsPublishTime(DataItem) || (Preview && DataItem.Status == (Int32)EnumStatus.Draft))
                {
                    //判断角色权限及区域权限
                    if (IsPreRoleView(DataItem.Per_AllUsers, DataItem.Per_Roles) && IsPreJurisdictionView(DataItem.Per_AllJurisdictions, DataItem.Per_Jurisdictions))
                    {

                        //数据项
                        Puts.Add("DataItem", DataItem);


                        //当前文档关联的文件集
                        Puts.Add("DownloadFiles", GetDownloadFiles(DataItem));


                        //详情模板调用
                        ContentHTML = ViewTemplate(XmlDB, "View_Template_Detail.html", Puts, xf);


                        //动态模块的配置
                        if (!String.IsNullOrEmpty(ContentHTML) && ContentHTML.IndexOf("[DynamicModules]", StringComparison.CurrentCultureIgnoreCase) >= 0)
                        {
                            //获取动态模块和动态项
                            Puts = GetDynamics(Puts);

                            ContentHTML = Common.ReplaceNoCase(ContentHTML, "[DynamicModules]", ViewTemplate(GetTemplateDB("DynamicModules"), "View_Template.html", Puts, xf));
                        }
                    }
                    else
                    {
                        //无权限访问
                        ContentHTML = "你无当前数据的访问权限";
                        Response.Redirect(new TemplateFormat(this).GoUiUrl(UIToken));
                    }
                }
                else
                {
                    //无法访问
                    ContentHTML = "无法访问内容或未到开始时间";
                    Response.Redirect(new TemplateFormat(this).GoUiUrl(UIToken));
                }

            }else
            {
                ContentHTML = "内容没有找到";
                Response.Redirect(new TemplateFormat(this).GoUiUrl(UIToken));
            }

            liContentHTML.Text = ContentHTML;


        }


        /// <summary>
        /// 是否到了发布日期
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public Boolean IsPublishTime(Playngo_ClientZone_GameSheet DataItem)
        {
            return  DataItem.StartTime <= xUserTime.LocalTime();
        }


        /// <summary>
        /// 获取动态模块和动态项
        /// </summary>
        /// <param name="Puts"></param>
        /// <returns></returns>
        public Hashtable GetDynamics(Hashtable Puts)
        {

            var DynamicModules = Playngo_ClientZone_DynamicModule.FindViewAllByFilter(DataItem.ID, (Int32)EnumDynamicModuleType.GameSheet, Settings_ModuleID, this);

            var DynamicItems = new List<Playngo_ClientZone_DynamicItem>();

            if (DynamicModules != null && DynamicModules.Count > 0)
            {
                foreach (var DynamicModule in DynamicModules)
                {
                    var minDynamicItems = Playngo_ClientZone_DynamicItem.FindListByFilter(DynamicModule.ID, Settings_ModuleID);
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

        /// <summary>
        /// 获取文档关联下载文件
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public List<Playngo_ClientZone_DownloadFile> GetDownloadFiles(Playngo_ClientZone_GameSheet DataItem)
        {
            var DownLoadFiles = new List<Playngo_ClientZone_DownloadFile>();
            if (DataItem != null && DataItem.ID > 0)
            {
 
                var Relations = Playngo_ClientZone_DownloadRelation.FindListByItem(DataItem.ID, (Int32)EnumDisplayModuleType.GameSheets);


                if (Relations != null && Relations.Count > 0)
                {
                    foreach (var Relation in Relations)
                    {
                        Playngo_ClientZone_DownloadFile DownloadFile = Playngo_ClientZone_DownloadFile.FindByKeyForEdit(Relation.DownloadID);
                        if (DownloadFile != null && DownloadFile.ID > 0)
                        {
                            //判断当前角色和区域是否可以看到这些文件
                            if (IsPreRoleView(DownloadFile.Per_AllUsers, DownloadFile.Per_Roles) && IsPreJurisdictionView(DownloadFile.Per_AllJurisdictions, DownloadFile.Per_Jurisdictions))
                            {
                                DownLoadFiles.Add(DownloadFile);
                            }

                        }
                    }
                }
            }

            return DownLoadFiles;
        }





        #endregion
    }
}