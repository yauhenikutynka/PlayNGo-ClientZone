using DotNetNuke.Services.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_CampaignsCategory : BaseModule
    {


        #region "属性"
        /// <summary>
        /// 分类编号
        /// </summary>
        private Int32 CategoryID = WebHelper.GetIntParam(HttpContext.Current.Request, "CategoryID", 0);


        /// <summary>
        /// 当前页码
        /// </summary>
        public Int32 PageIndex = WebHelper.GetIntParam(HttpContext.Current.Request, "PageIndex", 1);

        /// <summary>
        /// 文章状态
        /// </summary>
        public Int32 EventStatus = WebHelper.GetIntParam(HttpContext.Current.Request, "Status", -1);



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

                if (EventStatus >= 0)
                {
                    urls.Add(String.Format("Status={0}", EventStatus));
                }

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

                return xUrl("", "", "CampaignCategory", urls.ToArray());
            }
        }


        /// <summary>
        /// 排序字段
        /// </summary>
        public string Orderfld = WebHelper.GetStringParam(HttpContext.Current.Request, "sort_f", "Sort");


        /// <summary>
        /// 排序类型 1:降序 0:升序
        /// </summary>
        public int OrderType = WebHelper.GetIntParam(HttpContext.Current.Request, "sort_t", 1);

        /// <summary>
        /// 提示操作类
        /// </summary>
        MessageTips mTips = new MessageTips();
        #endregion


        #region "方法"


        /// <summary>
        /// 绑定查询的方法
        /// </summary>
        private List<SearchParam> BindSearch()
        {
            List<SearchParam> Where = new List<SearchParam>();
            Where.Add(new SearchParam(Playngo_ClientZone_CampaignCategory._.ModuleId, ModuleId, SearchType.Equal));

            if (!String.IsNullOrEmpty(Search_Title))
            {
                txtSearch.Text = HttpUtility.UrlDecode(Search_Title);
                Where.Add(new SearchParam(Playngo_ClientZone_CampaignCategory._.Name, HttpUtility.UrlDecode(Search_Title), SearchType.Like));
            }






            return Where;
        }

        /// <summary>
        /// 绑定右边列表
        /// </summary>
        private void BindDataList()
        {
            QueryParam qp = new QueryParam();
            qp.Orderfld = Orderfld;
            qp.OrderType = 0;
            qp.PageSize = 9999;
            int RecordCount = 0;


            //查询的方法
            qp.Where = BindSearch();

            List<Playngo_ClientZone_CampaignCategory> lst = Playngo_ClientZone_CampaignCategory.FindAll(qp, out RecordCount);

            qp.RecordCount = RecordCount;
            RecordPages = qp.Pages;
            lblRecordCount.Text = String.Format("{0} {2} / {1} {3}", RecordCount, RecordPages, ViewResourceText("Title_Items", "Items"), ViewResourceText("Title_Pages", "Pages"));

            if (lst != null && lst.Count > 0)
            {



                gvEventList.DataSource = SortList(lst, 0, 0);

            
            }
            //tlview.DataBind();
            gvEventList.DataBind();


        }

        /// <summary>
        /// 排序列表,按照级别关系
        /// </summary>
        /// <param name="list"></param>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public List<Playngo_ClientZone_CampaignCategory> SortList(List<Playngo_ClientZone_CampaignCategory> list, Int32 ParentID, Int32 Level)
        {
            List<Playngo_ClientZone_CampaignCategory> returnList = new List<Playngo_ClientZone_CampaignCategory>();
            List<Playngo_ClientZone_CampaignCategory> tempList = list.FindAll(r => r.ParentID == ParentID);
            //tempList.Sort(new Comparison<Playngo_ClientZone_CampaignCategory>(Compare));
            //tempList = (List<Playngo_ClientZone_CampaignCategory>)tempList.OrderBy(r => r.Sort);
            foreach (Playngo_ClientZone_CampaignCategory item in tempList)
            {
                returnList.Add(item);
                returnList.AddRange(SortList(list, item.ID, Level + 1));
            }
            return returnList;
        }

        private int Compare(Playngo_ClientZone_CampaignCategory info1, Playngo_ClientZone_CampaignCategory info2)
        {
            int result;
            CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();
            result = ObjectCompare.Compare(info1.Sort, info2.Sort);
            return result;
        }




        /// <summary>
        /// 绑定左边项
        /// </summary>
        private void BindDataItem()
        {


            if (CategoryID > 0)
            {
                cmdDelete.Visible = true;
                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");

                Playngo_ClientZone_CampaignCategory item = Playngo_ClientZone_CampaignCategory.FindByKeyForEdit(CategoryID);
                txtName.Text = item.Name;
              


                txtContentText.Text = item.ContentText;
            }
            else
            {

                //新增界面
                cmdDelete.Visible = false;
                txtName.Text = "";
                txtContentText.Text = "";

            }



        }


        #endregion



        #region "事件"
        protected void Page_PreRender(object sender, EventArgs e)
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



        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    BindDataItem();
                    BindDataList();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, exc);
            }
        }







        /// <summary>
        /// 列表上的项删除事件
        /// </summary>
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton btnRemove = (LinkButton)sender;

                Playngo_ClientZone_CampaignCategory item = Playngo_ClientZone_CampaignCategory.FindByKeyForEdit(btnRemove.CommandArgument);
                mTips.IsPostBack = true;
                if (item.ID > 0 && item.Delete() > 0)
                {
                    mTips.LoadMessage("DeleteCategorySuccess", EnumTips.Success, this, new String[] { item.Name });
                }
                else
                {
                    mTips.LoadMessage("DeleteCategoryError", EnumTips.Success, this, new String[] { item.Name });
                }
                BindDataList();

            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }

        }


        protected void cmdDeleteCategory_Click(object sender, EventArgs e)
        {
            try
            {
                Playngo_ClientZone_CampaignCategory item = Playngo_ClientZone_CampaignCategory.FindByKeyForEdit(CategoryID);
                if (item.ID > 0 && item.Delete() > 0)
                {
                

                    mTips.LoadMessage("DeleteCategorySuccess", EnumTips.Success, this, new String[] { item.Name });
                }
                else
                {
                    mTips.LoadMessage("DeleteCategoryError", EnumTips.Success, this, new String[] { item.Name });
                }
                Response.Redirect(xUrl("CampaignCategory"), false);

            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }

        }


        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                Playngo_ClientZone_CampaignCategory item = Playngo_ClientZone_CampaignCategory.FindByKeyForEdit(CategoryID);
                item.ContentText = txtContentText.Text;
                item.Name = txtName.Text;


                item.LastIP = WebHelper.UserHost;
                item.LastTime = xUserTime.UtcTime();
                item.LastUser = UserId;

    

                if (item.ID > 0)
                {
                  
                }
                else
                {
                    QueryParam qp = new QueryParam();

                    item.Sort = Playngo_ClientZone_CampaignCategory.FindMaxSort(ModuleId) + 1;
                    item.ModuleId = ModuleId;
                    item.PortalId = PortalId;
                }

                int Resultitem = 0;

                if (item.ID > 0)
                    Resultitem = item.Update();
                else
                    Resultitem = item.Insert();



                if (Resultitem > 0)
                {
                    mTips.LoadMessage("SaveCategorySuccess", EnumTips.Success, this, new String[] { item.Name });
                }
                else
                {
                    //保存失败
                    mTips.LoadMessage("SaveCategoryError", EnumTips.Success, this, new String[] { item.Name });
                }
                Response.Redirect(xUrl("CampaignCategory"), false);
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }

        }



        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(xUrl("CampaignCategory"), false);

            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }

        }

        protected void lbSort_Click(object sender, EventArgs e)
        {
            LinkButton ImgbutSort = (LinkButton)sender;
            if (ImgbutSort != null)
            {
                //查出当前要排序的字段
                Playngo_ClientZone_CampaignCategory objC = Playngo_ClientZone_CampaignCategory.FindByKeyForEdit(ImgbutSort.CommandArgument);

                mTips.IsPostBack = true;//回发时就要触发
                if (ImgbutSort.ToolTip == "up")
                {
                    Playngo_ClientZone_CampaignCategory.MoveField(objC, EnumMoveType.Up, ModuleId);
                    //字段上移成功
                    mTips.LoadMessage("UpMoveGroupSuccess", EnumTips.Success, this, new String[] { "" });

                }
                else
                {
                    Playngo_ClientZone_CampaignCategory.MoveField(objC, EnumMoveType.Down, ModuleId);
                    //字段下移成功

                    mTips.LoadMessage("DownMoveGroupSuccess", EnumTips.Success, this, new String[] { "" });
                }
                //绑定一下
                BindDataList();
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
                Playngo_ClientZone_CampaignCategory Categorie = e.Row.DataItem as Playngo_ClientZone_CampaignCategory;



                //移动分类按钮
                LinkButton lbSortUp = e.Row.FindControl("lbSortUp") as LinkButton;
                LinkButton lbSortDown = e.Row.FindControl("lbSortDown") as LinkButton;
                LinkButton lbMobileSortUp = e.Row.FindControl("lbMobileSortUp") as LinkButton;
                LinkButton lbMobileSortDown = e.Row.FindControl("lbMobileSortDown") as LinkButton;
                lbSortUp.CommandArgument =
                     lbSortDown.CommandArgument =
                      lbMobileSortUp.CommandArgument =
                       lbMobileSortDown.CommandArgument = Categorie.ID.ToString();
                //编辑按钮
                HyperLink hlMobileEdit = e.Row.FindControl("hlMobileEdit") as HyperLink;
                HyperLink hlEdit = e.Row.FindControl("hlEdit") as HyperLink;
                hlMobileEdit.NavigateUrl = hlEdit.NavigateUrl = xUrl("CategoryID", Categorie.ID.ToString(), "CampaignCategory");

                //删除按钮
                LinkButton btnRemove = e.Row.FindControl("btnRemove") as LinkButton;
                LinkButton btnMobileRemove = e.Row.FindControl("btnMobileRemove") as LinkButton;
                btnRemove.CommandArgument = btnMobileRemove.CommandArgument = Categorie.ID.ToString();
                btnRemove.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");
                btnMobileRemove.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");

                //分类名称
                HyperLink hlCategorie = e.Row.FindControl("hlCategorie") as HyperLink;
                hlCategorie.Text =  Categorie.Name;
                //hlCategorie.NavigateUrl = new TemplateFormat(this).GoUrl(Categorie);


               


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
                            Playngo_ClientZone_CampaignCategory item = Playngo_ClientZone_CampaignCategory.FindByKeyForEdit(IDX);
                            if (item != null && item.ID > 0)
                            {
                                mTips.IsPostBack = true;
                                if (item.Delete() > 0)
                                {
                                  
                                    //mTips.LoadMessage("DeleteCategorySuccess", EnumTips.Success, this, new String[] { item.Name });
                                }
                                else
                                {
                                    //mTips.LoadMessage("DeleteCategoryError", EnumTips.Success, this, new String[] { item.Name });
                                }


                            }
                        }
                    }
                    BindDataList();
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

        #endregion


    }
}