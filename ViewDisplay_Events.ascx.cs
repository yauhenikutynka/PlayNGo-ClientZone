using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class ViewDisplay_Events : BaseModule
    {

        #region "属性"


        private Playngo_ClientZone_Event _DataItem = new Playngo_ClientZone_Event();
        /// <summary>
        /// 数据实体
        /// </summary>
        public Playngo_ClientZone_Event DataItem
        {
            get
            {
                if (!(_DataItem != null && _DataItem.ID > 0))
                {
                    _DataItem = Playngo_ClientZone_Event.FindItemByFriendlyUrl(DetailId, FriendlyUrl, Settings_ModuleID);


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

                var XMLDB = GetTemplateDB("Events");
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
        /// 获取文档关联下载文件
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public List<Playngo_ClientZone_DownloadFile> GetDownloadFiles(Playngo_ClientZone_Event DataItem)
        {
            var DownLoadFiles = new List<Playngo_ClientZone_DownloadFile>();
            if (DataItem != null && DataItem.ID > 0)
            {
 

                var Relations = Playngo_ClientZone_DownloadRelation.FindListByItem(DataItem.ID, (Int32)EnumDisplayModuleType.Events);


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

                        //获取动态模块和动态项
                        //Puts = GetDynamics(Puts);
                        //数据项
                        Puts.Add("DataItem", DataItem);


                        //当前文档关联的文件集
                        Puts.Add("DownloadFiles", GetDownloadFiles(DataItem));



                        //详情模板调用
                        ContentHTML = ViewTemplate(XmlDB, "View_Template_Detail.html", Puts, xf);


                        ////动态模块的配置
                        //if (!String.IsNullOrEmpty(ContentHTML) && ContentHTML.IndexOf("[DynamicModules]", StringComparison.CurrentCultureIgnoreCase) >= 0)
                        //{
                        //    //读取关于动态模块的数据
                        //    //------

                        //    ContentHTML = Common.ReplaceNoCase(ContentHTML, "[DynamicModules]", ViewTemplate(GetTemplateDB("DynamicModules"), "View_Template.html", Puts, xf));
                        //}
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

            }
            else
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
        public Boolean IsPublishTime(Playngo_ClientZone_Event DataItem)
        {
            return DataItem.StartTime <= xUserTime.LocalTime();
        }



        /// <summary>
        /// 绑定查询的方法
        /// </summary>
        private QueryParam BindSearch(QueryParam qp)
        {



            List<SearchParam> Where = qp.Where;
            //文章所属模块
            Where.Add(new SearchParam(Playngo_ClientZone_Event._.ModuleId, Settings_ModuleID, SearchType.Equal));
            //文章状态
            Where.Add(new SearchParam(Playngo_ClientZone_Event._.Status, (Int32)EnumStatus.Published, SearchType.Equal));


            qp.Where = Where;


            //根据权限来筛选数据
            #region "根据权限来筛选数据"
            if (UserId > 0)
            {
                if (!UserInfo.IsSuperUser)//超级管理员不限制
                {

                    if (!String.IsNullOrEmpty(qp.WhereSql.ToString()))
                    {
                        qp.WhereSql.Append(" AND ");
                    }

                    qp.WhereSql.Append(" ( ");
                    //公开的
                    qp.WhereSql.Append(new SearchParam(Playngo_ClientZone_Event._.Per_AllUsers, 0, SearchType.Equal).ToSql());

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

                            qp.WhereSql.Append(new SearchParam(Playngo_ClientZone_Event._.Per_Roles, String.Format(",{0},", r), SearchType.Like).ToSql());

                            qp.WhereSql.Append(" OR ");

                            qp.WhereSql.Append(new SearchParam(Playngo_ClientZone_Event._.Per_Roles, r, SearchType.Like).ToSql());

                            RoleIndex++;
                        }
                        qp.WhereSql.Append(" ) ");
                    }


                    qp.WhereSql.Append(" ) ");
                }
            }
            else
            {
                qp.Where.Add(new SearchParam(Playngo_ClientZone_Event._.Per_AllUsers, 0, SearchType.Equal));
            }
            #endregion


            return qp;
        }


        #endregion
    }
}