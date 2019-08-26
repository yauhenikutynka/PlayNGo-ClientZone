using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_Modal_RelationPages : BaseModule
    {


        #region "属性"

        /// <summary>
        /// 提示操作类
        /// </summary>
        MessageTips mTips = new MessageTips();

        /// <summary>
        /// 当前页码
        /// </summary>
        public Int32 PageIndex = WebHelper.GetIntParam(HttpContext.Current.Request, "PageIndex", 1);

        /// <summary>
        /// 文章状态
        /// </summary>
        public Int32 EventStatus = WebHelper.GetIntParam(HttpContext.Current.Request, "Status", 1);

        /// <summary>
        /// 页面类型
        /// </summary>
        public Int32 PageType = WebHelper.GetIntParam(HttpContext.Current.Request, "PageType",(Int32)EnumDisplayModuleType.GameSheets);



        /// <summary>
        /// 文章搜索_标题
        /// </summary>
        public String Search_Title = WebHelper.GetStringParam(HttpContext.Current.Request, "SearchText", "");

        /// <summary>
        /// 总页码数
        /// </summary>
        public Int32 RecordPages
        {
            get;
            set;
        }

        /// <summary>
        /// 当前页面URL(不包含分页)
        /// </summary>
        public String CurrentUrl
        {
            get
            {

                List<String> urls = new List<String>();


                if (PageType >= 0)
                {
                    urls.Add(String.Format("PageType={0}", PageType));
                }

                if (EventStatus >= 0)
                {
                    urls.Add(String.Format("Status={0}", EventStatus));
                }

                if (!String.IsNullOrEmpty(Orderfld))
                {
                    urls.Add(String.Format("sort_f={0}", Orderfld));
                }

                if (OrderType > 0)
                {
                    urls.Add(String.Format("sort_t={0}", OrderType));
                }

                if (!String.IsNullOrEmpty(Search_Title))
                {
                    urls.Add(String.Format("SearchText={0}", Search_Title));
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.AppendFormat("{0}?Master=Manager_Modal_RelationPages&PortalId={1}&TabId={2}&ModuleId={3}&language={4}", "Resource_Masters.aspx", PortalId, TabId, ModuleId, language);

                foreach (String parameter in urls)
                {
                    sb.AppendFormat("&{0}", parameter);
                }

                return sb.ToString(); ;
            }
        }


        /// <summary>
        /// 排序字段
        /// </summary>
        public string Orderfld = WebHelper.GetStringParam(HttpContext.Current.Request, "sort_f", "");


        /// <summary>
        /// 排序类型 1:降序 0:升序
        /// </summary>
        public int OrderType = WebHelper.GetIntParam(HttpContext.Current.Request, "sort_t", 1);



        #endregion



        #region "方法"



        /// <summary>
        /// 绑定列表
        /// </summary>
        private void BindDataList()
        {
            if (PageType == (Int32)EnumDisplayModuleType.Campaigns)
            {
                BindDataListByCampaigns();
            }
            else if (PageType == (Int32)EnumDisplayModuleType.Events)
            {
                BindDataListByEvents();
            }
            else
            {
                BindDataListByGameSheets();
            }
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void BindDataListByGameSheets()
        {
            QueryParam qp = new QueryParam();
            qp.OrderType = OrderType;
            if (!String.IsNullOrEmpty(Orderfld))
            {
                qp.Orderfld = Orderfld;
            }
            else
            {
                qp.Orderfld = Playngo_ClientZone_GameSheet._.ID;
            }

            #region "分页的一系列代码"


            int RecordCount = 0;
            int pagesize = qp.PageSize = 10;
            qp.PageIndex = PageIndex;


            #endregion

            //查询的方法
            qp.Where = BindSearch();

            List<Playngo_ClientZone_GameSheet> Events = Playngo_ClientZone_GameSheet.FindAll(qp, out RecordCount);
            qp.RecordCount = RecordCount;
            RecordPages = qp.Pages;
            lblRecordCount.Text = String.Format("{0} {2} / {1} {3}", RecordCount, RecordPages, ViewResourceText("Title_Items", "Items"), ViewResourceText("Title_Pages", "Pages"));

            gvList.DataSource = Events;
            gvList.DataBind();
            BindGridViewEmpty<Playngo_ClientZone_GameSheet>(gvList, new Playngo_ClientZone_GameSheet());
        }


        private void BindDataListByCampaigns()
        {
            QueryParam qp = new QueryParam();
            qp.OrderType = OrderType;
            if (!String.IsNullOrEmpty(Orderfld))
            {
                qp.Orderfld = Orderfld;
            }
            else
            {
                qp.Orderfld = Playngo_ClientZone_Campaign._.ID;
            }

            #region "分页的一系列代码"


            int RecordCount = 0;
            int pagesize = qp.PageSize = 10;
            qp.PageIndex = PageIndex;


            #endregion

            //查询的方法
            qp.Where = BindSearch();

            List<Playngo_ClientZone_Campaign> Events = Playngo_ClientZone_Campaign.FindAll(qp, out RecordCount);
            qp.RecordCount = RecordCount;
            RecordPages = qp.Pages;
            lblRecordCount.Text = String.Format("{0} {2} / {1} {3}", RecordCount, RecordPages, ViewResourceText("Title_Items", "Items"), ViewResourceText("Title_Pages", "Pages"));

            gvList.DataSource = Events;
            gvList.DataBind();
            BindGridViewEmpty<Playngo_ClientZone_Campaign>(gvList, new Playngo_ClientZone_Campaign());
        }



        private void BindDataListByEvents()
        {
            QueryParam qp = new QueryParam();
            qp.OrderType = OrderType;
            if (!String.IsNullOrEmpty(Orderfld))
            {
                qp.Orderfld = Orderfld;
            }
            else
            {
                qp.Orderfld = Playngo_ClientZone_Event._.ID;
            }

            #region "分页的一系列代码"


            int RecordCount = 0;
            int pagesize = qp.PageSize = 10;
            qp.PageIndex = PageIndex;


            #endregion

            //查询的方法
            qp.Where = BindSearch();

            List<Playngo_ClientZone_Event> Events = Playngo_ClientZone_Event.FindAll(qp, out RecordCount);
            qp.RecordCount = RecordCount;
            RecordPages = qp.Pages;
            lblRecordCount.Text = String.Format("{0} {2} / {1} {3}", RecordCount, RecordPages, ViewResourceText("Title_Items", "Items"), ViewResourceText("Title_Pages", "Pages"));

            gvList.DataSource = Events;
            gvList.DataBind();
            BindGridViewEmpty<Playngo_ClientZone_Event>(gvList, new Playngo_ClientZone_Event());
        }



        /// <summary>
        /// 绑定页面项
        /// </summary>
        private void BindPageItem()
        {



        }


        /// <summary>
        /// 绑定查询的方法
        /// </summary>
        private List<SearchParam> BindSearch()
        {
            List<SearchParam> Where = new List<SearchParam>();
            Where.Add(new SearchParam("PortalId", PortalId, SearchType.Equal));


            //筛选文章的状态
            if (EventStatus >= 0)
            {
                Where.Add(new SearchParam("Status", EventStatus, SearchType.Equal));
            }


            if (!String.IsNullOrEmpty(Search_Title))
            {
                txtSearch.Text = HttpUtility.UrlDecode(Search_Title);
                Where.Add(new SearchParam("Title", HttpUtility.UrlDecode(Search_Title), SearchType.Like));
            }


            if (ViewSettingT<string>("General.MediaLibrary", "Public") == "Private")
            {
                Where.Add(new SearchParam("LastUser", UserId, SearchType.Equal));
            }



            return Where;
        }


        public String GetDataTypeUrl(Int32 _PageType)
        {
            PageType = _PageType;
            var url = CurrentUrl;
            PageType = WebHelper.GetIntParam(HttpContext.Current.Request, "PageType", (Int32)EnumDisplayModuleType.GameSheets);
            return url;
        }

        public String GetDataTypeActive(Int32 _PageType)
        {
            return PageType == _PageType ? " active " : "";
        }

        #endregion


        #region "事件"

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDataList();
                    BindPageItem();
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        /// <summary>
        /// 列表行创建
        /// </summary>
        protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Int32 DataIDX = 0;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //增加check列头全选
                TableCell cell = new TableCell();
                cell.Width = Unit.Pixel(5);
                cell.Text = " <input id='CheckboxAll' value='0' type='checkbox' onclick='SelectAll()'/>";
                e.Row.Cells.AddAt(0, cell);

                foreach (TableCell var in e.Row.Cells)
                {
                    if (var.Controls.Count > 0 && var.Controls[0] is LinkButton)
                    {
                        string Colume = ((LinkButton)var.Controls[0]).CommandArgument;
                        if (Colume == Orderfld)
                        {
                            LinkButton l = (LinkButton)var.Controls[0];
                            l.Text += string.Format("<i class=\"fa {0}{1}\"></i>", Orderfld == "Title" ? "fa-sort-alpha-" : "fa-sort-amount-", (OrderType == 0) ? "asc" : "desc");
                        }
                    }
                }

            }
            else
            {
                //增加行选项
                DataIDX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID"));
                String Title = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Title"));
                TableCell cell = new TableCell();
                cell.Width = Unit.Pixel(5);
                
                cell.Text = string.Format(" <input name='Checkbox' id='Checkbox' value='{0}' type='checkbox' title='{1}' data-json='{2}'  />", DataIDX, Title,new JavaScriptSerializer().Serialize(e.Row.DataItem));
                e.Row.Cells.AddAt(0, cell);

            }
        }

        /// <summary>
        /// 列表行绑定
        /// </summary>
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (PageType == (Int32)EnumDisplayModuleType.Campaigns)
                {
                    RowDataBoundByCampaigns(e);
                }
                else if (PageType == (Int32)EnumDisplayModuleType.Events)
                {
                    RowDataBoundByEvents(e);
                }
                else
                {
                    RowDataBoundByGameSheets(e);
                }



                
            }
        }

        public void RowDataBoundByGameSheets(GridViewRowEventArgs e)
        {
            //还原出数据
            Playngo_ClientZone_GameSheet DataItem = e.Row.DataItem as Playngo_ClientZone_GameSheet;

            if (DataItem != null && DataItem.ID > 0)
            {
                TemplateFormat xf = new TemplateFormat(this);


                HyperLink hlFileName = e.Row.FindControl("hlFileName") as HyperLink;
                hlFileName.Text = DataItem.Title;

                Image imgFileName = e.Row.FindControl("imgFileName") as Image;

                //发布者信息
                e.Row.Cells[2].Text = "--";
                if (DataItem.LastUser > 0)
                {
                    UserInfo uInfo = UserController.GetUserById(PortalId, DataItem.LastUser);
                    if (uInfo != null && uInfo.UserID > 0) e.Row.Cells[2].Text = String.Format("{0}<br />{1}", uInfo.Username, uInfo.DisplayName);
                }


                //发布时间
                e.Row.Cells[3].Text = DataItem.CreateTime.ToShortDateString();

                //状态
                e.Row.Cells[4].Text = EnumHelper.GetEnumTextVal(DataItem.Status, typeof(EnumStatus));

            }
        }


        public void RowDataBoundByCampaigns(GridViewRowEventArgs e)
        {
            //还原出数据
            Playngo_ClientZone_Campaign DataItem = e.Row.DataItem as Playngo_ClientZone_Campaign;

            if (DataItem != null && DataItem.ID > 0)
            {
                TemplateFormat xf = new TemplateFormat(this);


                HyperLink hlFileName = e.Row.FindControl("hlFileName") as HyperLink;
                hlFileName.Text = DataItem.Title;

                Image imgFileName = e.Row.FindControl("imgFileName") as Image;

                //发布者信息
                e.Row.Cells[2].Text = "--";
                if (DataItem.LastUser > 0)
                {
                    UserInfo uInfo = UserController.GetUserById(PortalId, DataItem.LastUser);
                    if (uInfo != null && uInfo.UserID > 0) e.Row.Cells[2].Text = String.Format("{0}<br />{1}", uInfo.Username, uInfo.DisplayName);
                }


                //发布时间
                e.Row.Cells[3].Text = DataItem.CreateTime.ToShortDateString();

                //状态
                e.Row.Cells[4].Text = EnumHelper.GetEnumTextVal(DataItem.Status, typeof(EnumStatus));

            }
        }


        public void RowDataBoundByEvents(GridViewRowEventArgs e)
        {
            //还原出数据
            Playngo_ClientZone_Event DataItem = e.Row.DataItem as Playngo_ClientZone_Event;

            if (DataItem != null && DataItem.ID > 0)
            {
                TemplateFormat xf = new TemplateFormat(this);


                HyperLink hlFileName = e.Row.FindControl("hlFileName") as HyperLink;
                hlFileName.Text = DataItem.Title;
 
                Image imgFileName = e.Row.FindControl("imgFileName") as Image;
 
                //发布者信息
                e.Row.Cells[2].Text = "--";
                if (DataItem.LastUser > 0)
                {
                    UserInfo uInfo = UserController.GetUserById(PortalId, DataItem.LastUser);
                    if (uInfo != null && uInfo.UserID > 0) e.Row.Cells[2].Text = String.Format("{0}<br />{1}", uInfo.Username, uInfo.DisplayName);
                }


                //发布时间
                e.Row.Cells[3].Text = DataItem.CreateTime.ToShortDateString();

                //状态
                e.Row.Cells[4].Text = EnumHelper.GetEnumTextVal(DataItem.Status, typeof(EnumStatus));

            }
        }


        /// <summary>
        /// 列表排序
        /// </summary>
        protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (Orderfld == e.SortExpression)
            {
                if (OrderType == 0)
                {
                    OrderType = 1;
                }
                else
                {
                    OrderType = 0;
                }
            }
            Orderfld = e.SortExpression;
            //BindDataList();
            Response.Redirect(CurrentUrl);
        }



        /// <summary>
        /// 搜索按钮事件
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search_Title = HttpUtility.UrlEncode(txtSearch.Text.Trim());
                Response.Redirect(CurrentUrl);
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        /// <summary>
        /// 提交按钮
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }


        #endregion


    }
}