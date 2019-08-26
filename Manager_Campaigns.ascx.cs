using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_Campaigns : BaseModule
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
        public Int32 EventStatus = WebHelper.GetIntParam(HttpContext.Current.Request, "Status", (Int32)EnumStatus.Published);



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

                //if (EventStatus >= 0)
                //{
                urls.Add(String.Format("Status={0}", EventStatus));
                //}

                if (!String.IsNullOrEmpty(Orderfld))
                {
                    urls.Add(String.Format("sort_f={0}", Orderfld));
                }

                if (OrderType == 0)
                {
                    urls.Add(String.Format("sort_t={0}", OrderType));
                }

                if (!String.IsNullOrEmpty(Search_Title))
                {
                    urls.Add(String.Format("SearchText={0}", Search_Title));
                }

                return xUrl("", "", "Campaigns", urls.ToArray());
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
            qp.PageSize = Settings_General_ManagerPerPage;
            qp.PageIndex = PageIndex;


            #endregion

            //查询的方法
            qp.Where = BindSearch();

            List<Playngo_ClientZone_Campaign> Events = Playngo_ClientZone_Campaign.FindAll(qp, out RecordCount);
            qp.RecordCount = RecordCount;
            RecordPages = qp.Pages;
            lblRecordCount.Text = String.Format("{0} {2} / {1} {3}", RecordCount, RecordPages, ViewResourceText("Title_Items", "Items"), ViewResourceText("Title_Pages", "Pages"));


            Boolean is_admin = !IsAdministrator && !IsAdmin;


            hlAllEvent.Text = String.Format("{1} ({0})", Playngo_ClientZone_Campaign.FindCountByStatus(ModuleId, -1, is_admin, UserId), ViewResourceText("hlAllEvent", "All"));
            hlPublishedEvent.Text = String.Format("{1} ({0})", Playngo_ClientZone_Campaign.FindCountByStatus(ModuleId, (Int32)EnumStatus.Published, is_admin, UserId), ViewResourceText("hlPublishedEvent", "Published"));
            hlPendingEvent.Text = String.Format("{1} ({0})", Playngo_ClientZone_Campaign.FindCountByStatus(ModuleId, (Int32)EnumStatus.Pending, is_admin, UserId), ViewResourceText("hlPendingEvent", "Pending"));
            hlDraftsEvent.Text = String.Format("{1} ({0})", Playngo_ClientZone_Campaign.FindCountByStatus(ModuleId, (Int32)EnumStatus.Draft, is_admin, UserId), ViewResourceText("hlDraftsEvent", "Drafts"));
            hlRecycleBinEvent.Text = String.Format("{1} ({0})", Playngo_ClientZone_Campaign.FindCountByStatus(ModuleId, (Int32)EnumStatus.Recycle, is_admin, UserId), ViewResourceText("hlRecycleBinEvent", "Recycle Bin"));





            gvEventList.DataSource = Events;
            gvEventList.DataBind();
            BindGridViewEmpty<Playngo_ClientZone_Campaign>(gvEventList, new Playngo_ClientZone_Campaign());
        }



        /// <summary>
        /// 绑定页面项
        /// </summary>
        private void BindPageItem()
        {

            hlAllEvent.NavigateUrl = xUrl("Status", "-1", "Campaigns");
            hlPublishedEvent.NavigateUrl = xUrl("Status", ((Int32)EnumStatus.Published).ToString(), "Campaigns");
            hlPendingEvent.NavigateUrl = xUrl("Status", ((Int32)EnumStatus.Pending).ToString(), "Campaigns");
            hlDraftsEvent.NavigateUrl = xUrl("Status", ((Int32)EnumStatus.Draft).ToString(), "Campaigns");
            hlRecycleBinEvent.NavigateUrl = xUrl("Status", ((Int32)EnumStatus.Recycle).ToString(), "Campaigns");

            switch (EventStatus)
            {
                case -1: hlAllEvent.CssClass = "btn btn-default active"; break;
                case (Int32)EnumStatus.Published: hlPublishedEvent.CssClass = "btn btn-default active"; break;
                case (Int32)EnumStatus.Pending: hlPendingEvent.CssClass = "btn btn-default active"; break;
                case (Int32)EnumStatus.Draft: hlDraftsEvent.CssClass = "btn btn-default active"; break;
                case (Int32)EnumStatus.Recycle: hlRecycleBinEvent.CssClass = "btn btn-default active"; break;
                default: hlPublishedEvent.CssClass = "btn btn-default active"; break;
            }

   
            hlAddNewLink.NavigateUrl = xUrl("Campaigns-Edit");


            //非管理员状态下需要屏蔽批量审核状态
            if (!IsAdministrator && !IsAdmin)
            {
                ddlStatus.Items.RemoveAt(1);
            }

            DotNetNuke.UI.Utilities.ClientAPI.RegisterKeyCapture(txtSearch, btnSearch, 13);

        }


        /// <summary>
        /// 绑定查询的方法
        /// </summary>
        private List<SearchParam> BindSearch()
        {
            List<SearchParam> Where = new List<SearchParam>();
            Where.Add(new SearchParam("ModuleId", ModuleId, SearchType.Equal));

            //不是超级管理员也不是普通管理员时，只能看到自己发布的文章
            if (!IsAdministrator && !IsAdmin)
            {
                Where.Add(new SearchParam(Playngo_ClientZone_Campaign._.CreateUser, UserId, SearchType.Equal));
            }

            //筛选文章的状态
            if (EventStatus >= 0)
            {
                Where.Add(new SearchParam(Playngo_ClientZone_Campaign._.Status, EventStatus, SearchType.Equal));
            }


            if (!String.IsNullOrEmpty(Search_Title))
            {
                txtSearch.Text = HttpUtility.UrlDecode(Search_Title);
                Where.Add(new SearchParam(Playngo_ClientZone_Campaign._.Title, HttpUtility.UrlDecode(Search_Title), SearchType.Like));
            }






            return Where;
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
                ProcessModuleLoadException(ex);
            }

        }

        /// <summary>
        /// 列表行创建
        /// </summary>
        protected void gvEventList_RowCreated(object sender, GridViewRowEventArgs e)
        {

            Int32 DataIDX = 0;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //增加check列头全选
                TableCell cell = new TableCell();
                cell.Width = Unit.Pixel(5);
                cell.Text = "<label> <input id='CheckboxAll' value='0' type='checkbox' class='input_text' onclick='SelectAll()'/></label>";
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
                TableCell cell = new TableCell();
                cell.Width = Unit.Pixel(5);
                cell.Text = string.Format("<label> <input name='Checkbox' id='Checkbox' value='{0}' type='checkbox' type-item=\"true\" class=\"input_text\" /></label>", DataIDX);
                e.Row.Cells.AddAt(0, cell);

            }


        }

        /// <summary>
        /// 列表行绑定
        /// </summary>
        protected void gvEventList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //还原出数据
                Playngo_ClientZone_Campaign Event = e.Row.DataItem as Playngo_ClientZone_Campaign;

                if (Event != null && Event.ID > 0)
                {
        

                    HyperLink hlEdit = e.Row.FindControl("hlEdit") as HyperLink;
                    HyperLink hlMobileEdit = e.Row.FindControl("hlMobileEdit") as HyperLink;
                    HyperLink hlCopy = e.Row.FindControl("hlCopy") as HyperLink;
                    HyperLink hlMobileCopy = e.Row.FindControl("hlMobileCopy") as HyperLink;
                    LinkButton btnRemove = e.Row.FindControl("btnRemove") as LinkButton;
                    LinkButton btnMobileRemove = e.Row.FindControl("btnMobileRemove") as LinkButton;
                    HyperLink hlNewsletter = e.Row.FindControl("hlNewsletter") as HyperLink;
                    Literal liNewsletterClientID = e.Row.FindControl("liNewsletterClientID") as Literal;
                    HyperLink hlRegisters = e.Row.FindControl("hlRegisters") as HyperLink;


                    HyperLink hlRepeats = e.Row.FindControl("hlRepeats") as HyperLink;
                    Literal liRepeatsClientID = e.Row.FindControl("liRepeatsClientID") as Literal;


                    HyperLink HLEventTitle = e.Row.FindControl("HLEventTitle") as HyperLink;
                    //设置按钮的CommandArgument
                    btnRemove.CommandArgument = btnMobileRemove.CommandArgument = Event.ID.ToString();
                    //设置删除按钮的提示
                    if (Event.Status == (Int32)EnumStatus.Recycle)
                    {
                        btnRemove.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");
                        btnMobileRemove.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");
                    }
                    else
                    {
                        btnRemove.Attributes.Add("onClick", "javascript:return confirm('" + ViewResourceText("DeleteRecycleItem", "Are you sure to move it to recycle bin?") + "');");
                        btnMobileRemove.Attributes.Add("onClick", "javascript:return confirm('" + ViewResourceText("DeleteRecycleItem", "Are you sure to move it to recycle bin?") + "');");
                    }

                    hlEdit.NavigateUrl = hlMobileEdit.NavigateUrl = xUrl("ID", Event.ID.ToString(), "Campaigns-Edit");


                    hlCopy.NavigateUrl = hlMobileCopy.NavigateUrl = xUrl("ID", Event.ID.ToString(), "Campaigns-Copy");



                    HLEventTitle.Text = Event.Title;
                    HLEventTitle.NavigateUrl = CommonFriendlyUrls.FriendlyUrl(Event, ViewSettingT<Int32>("ClientZone_DisplayTab_Campaigns", TabId),true, this);

                    //获取用户名称
                    DotNetNuke.Entities.Users.UserInfo createUser = new DotNetNuke.Entities.Users.UserController().GetUser(PortalId, Event.CreateUser);
                    e.Row.Cells[4].Text = createUser != null && createUser.UserID > 0 ? createUser.Username : "";
                    //文章状态
                    e.Row.Cells[7].Text = EnumHelper.GetEnumTextVal(Event.Status, typeof(EnumStatus));

                    //格式化3种时间为短日期格式
                    e.Row.Cells[5].Text = Event.StartTime.ToShortDateString();
                    e.Row.Cells[6].Text = Event.EndTime.ToShortDateString();
                    //e.Row.Cells[7].Text = Event.CreateTime.ToShortDateString();




                }
            }
        }

        /// <summary>
        /// 列表排序
        /// </summary>
        protected void gvEventList_Sorting(object sender, GridViewSortEventArgs e)
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
        /// 列表上的项删除事件
        /// </summary>
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton btnRemove = (LinkButton)sender;

                if (btnRemove != null && !String.IsNullOrEmpty(btnRemove.CommandArgument))
                {

                    mTips.IsPostBack = true;

                    Playngo_ClientZone_Campaign Event = Playngo_ClientZone_Campaign.FindByKeyForEdit(btnRemove.CommandArgument);

                    if (Event != null && Event.ID > 0)
                    {
                        if (Event.Status == (Int32)EnumStatus.Recycle)
                        {
                            if (Event.Delete() > 0)
                            {
                              



                                //操作成功
                                mTips.LoadMessage("DeleteEventSuccess", EnumTips.Success, this, new String[] { Event.Title });
                            }
                            else
                            {
                                //操作失败
                                mTips.LoadMessage("DeleteEventError", EnumTips.Success, this, new String[] { Event.Title });
                            }
                        }
                        else
                        {
                            Event.Status = (Int32)EnumStatus.Recycle;
                            if (Event.Update() > 0)
                            {
                               

                                //移动到回收站操作成功
                                mTips.LoadMessage("DeleteEventSuccess", EnumTips.Success, this, new String[] { Event.Title });
                            }
                            else
                            {
                                //移动到回收站操作失败
                                mTips.LoadMessage("DeleteEventError", EnumTips.Success, this, new String[] { Event.Title });
                            }
                        }
                        BindDataList();
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        /// <summary>
        /// 搜索按钮事件
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search_Title = HttpUtility.UrlEncode(txtSearch.Text.Trim());
                Response.Redirect(CurrentUrl, false);
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }

        /// <summary>
        /// 状态应用按钮事件
        /// </summary>
        protected void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 Status = WebHelper.GetIntParam(Request, ddlStatus.UniqueID, -1);

                if (Status >= 0)
                {
                    string Checkbox_Value = WebHelper.GetStringParam(Request, "Checkbox", "");
                    string[] Checkbox_Value_Array = Checkbox_Value.Split(',');
                    Int32 IDX = 0;
                    for (int i = 0; i < Checkbox_Value_Array.Length; i++)
                    {
                        if (Int32.TryParse(Checkbox_Value_Array[i], out IDX))
                        {
                            Playngo_ClientZone_Campaign Event = Playngo_ClientZone_Campaign.FindByKeyForEdit(IDX);
                            if (Event != null && Event.ID > 0)
                            {

                                if (Event.Status == (Int32)EnumStatus.Recycle && Status == (Int32)EnumStatus.Recycle)
                                {
                                    if (Event.Delete() > 0)
                                    {
                                      

                                    }
                                }
                                else
                                {
                                    Event.Status = Status;
                                    if (Event.Update() > 0)
                                    {
                                       
                                    }
                                }
                            }
                        }
                    }
                    BindDataList();

                    mTips.IsPostBack = true;
                    mTips.LoadMessage("ApplyStatusSuccess", EnumTips.Success, this, new String[] { EnumHelper.GetEnumTextVal(Status, typeof(EnumStatus)) });
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }









        #endregion



















    }
}