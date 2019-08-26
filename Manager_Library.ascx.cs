﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Users;
using System.IO;
 
namespace Playngo.Modules.ClientZone
{
    public partial class Manager_Library : BaseModule
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
        /// 文件类型
        /// </summary>
        public Int32 FileType = WebHelper.GetIntParam(HttpContext.Current.Request, "type", -1);



        /// <summary>
        /// 文章搜索_标题
        /// </summary>
        public String Search_Title = WebHelper.GetStringParam(HttpContext.Current.Request, "SearchText", "");

        /// <summary>
        /// 可见性
        /// </summary>
        public Int32 Visibility = WebHelper.GetIntParam(HttpContext.Current.Request, "Visibility", -1);

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

                if (FileType >= 0)
                {
                    urls.Add(String.Format("type={0}", FileType));
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

                if (Visibility >= 0)
                {
                    urls.Add(String.Format("Visibility={0}", Visibility));
                }

                return xUrl("", "", Token, urls.ToArray());
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
                qp.Orderfld = Playngo_ClientZone_Files._.ID;
            }

            #region "分页的一系列代码"


            int RecordCount = 0;
            int pagesize = qp.PageSize = Settings_General_ManagerPerPage;
            qp.PageIndex = PageIndex;


            #endregion

            //查询的方法
            qp.Where = BindSearch();

            List<Playngo_ClientZone_Files> Comments = Playngo_ClientZone_Files.FindAll(qp, out RecordCount);
            qp.RecordCount = RecordCount;
            RecordPages = qp.Pages;
            lblRecordCount.Text = String.Format("{0} {2} / {1} {3}", RecordCount, RecordPages, ViewResourceText("Title_Items", "Items"), ViewResourceText("Title_Pages", "Pages"));

            Boolean is_admin = !IsAdministrator && !IsAdmin;

            hlFileMateAll.Text = String.Format("{1} ({0})", Playngo_ClientZone_Files.FindCountByType(PortalId, Visibility, - 1, is_admin, UserId), ViewResourceText("hlFileMateAll", "All"));
            hlFileMateImage.Text = String.Format("{1} ({0})", Playngo_ClientZone_Files.FindCountByType(PortalId, Visibility, (Int32)EnumFileMate.Image, is_admin, UserId), ViewResourceText("hlFileMateImage", "Image"));
            hlFileMateZip.Text = String.Format("{1} ({0})", Playngo_ClientZone_Files.FindCountByType(PortalId, Visibility, (Int32)EnumFileMate.Zip, is_admin, UserId), ViewResourceText("hlFileMateZip", "Zip"));
            hlFileMateVideo.Text = String.Format("{1} ({0})", Playngo_ClientZone_Files.FindCountByType(PortalId, Visibility, (Int32)EnumFileMate.Video, is_admin, UserId), ViewResourceText("hlFileMateVideo", "Video"));
            hlFileMateDoc.Text = String.Format("{1} ({0})", Playngo_ClientZone_Files.FindCountByType(PortalId, Visibility, (Int32)EnumFileMate.Doc, is_admin, UserId), ViewResourceText("hlFileMateDoc", "Doc"));


            //ctlPagingControl.TotalRecords = RecordCount;

            //if (RecordCount <= pagesize)
            //{
            //    ctlPagingControl.Visible = false;

            //}

            gvCommentList.DataSource = Comments;
            gvCommentList.DataBind();
            BindGridViewEmpty<Playngo_ClientZone_Files>(gvCommentList, new Playngo_ClientZone_Files());
        }



        /// <summary>
        /// 绑定页面项
        /// </summary>
        private void BindPageItem()
        {

            hlFileMateAll.NavigateUrl = xUrl("Visibility", Visibility.ToString(), Token);
            hlFileMateImage.NavigateUrl = xUrl("type", ((Int32)EnumFileMate.Image).ToString(),Token, String.Format("Visibility={0}", Visibility));
            hlFileMateZip.NavigateUrl = xUrl("type", ((Int32)EnumFileMate.Zip).ToString(), Token, String.Format("Visibility={0}", Visibility));
            hlFileMateVideo.NavigateUrl = xUrl("type", ((Int32)EnumFileMate.Video).ToString(), Token, String.Format("Visibility={0}", Visibility));
            hlFileMateDoc.NavigateUrl = xUrl("type", ((Int32)EnumFileMate.Doc).ToString(), Token, String.Format("Visibility={0}", Visibility));

            switch (FileType)
            {
                case -1: hlFileMateAll.CssClass = "btn btn-default active"; break;
                case (Int32)EnumFileMate.Image: hlFileMateImage.CssClass = "btn btn-default active"; break;
                case (Int32)EnumFileMate.Zip: hlFileMateZip.CssClass = "btn btn-default active"; break;
                case (Int32)EnumFileMate.Video: hlFileMateVideo.CssClass = "btn btn-default active"; break;
                case (Int32)EnumFileMate.Doc: hlFileMateDoc.CssClass = "btn btn-default active"; break;
                default: hlFileMateAll.CssClass = "btn btn-default active"; break;
            }

            //hlAddNewLink.NavigateUrl = xUrl("AddMedia");

            //插入图片按钮的连接
            hlAddNewLink.Attributes.Add("data-href", String.Format("{0}Resource_jQueryFileUpload.aspx?ModuleId={1}&PortalId={2}&TabId={3}&Visibility={4}", ModulePath, ModuleId, PortalId, TabId, Visibility));

            //同步图片等设置的菜单
            hlSynchronizeFiles.NavigateUrl = xUrl("Visibility", Visibility.ToString(), "Synchronize");


            DotNetNuke.UI.Utilities.ClientAPI.RegisterKeyCapture(txtSearch, btnSearch, 13);
        }


        /// <summary>
        /// 绑定查询的方法
        /// </summary>
        private List<SearchParam> BindSearch()
        {
            List<SearchParam> Where = new List<SearchParam>();
            Where.Add(new SearchParam("PortalId", PortalId, SearchType.Equal));
            //Where.Add(new SearchParam("ModuleId", ModuleId, SearchType.Equal));

            //不是管理员，只能看到自己发布文章的评论
            if (!IsAdministrator && !IsAdmin)
            {
                Where.Add(new SearchParam(Playngo_ClientZone_Files._.LastUser, UserId, SearchType.Equal));
            }
            //else if (ViewSettingT<string>("General.MediaLibrary", "public") == "private")
            //{
            //    Where.Add(new SearchParam(Playngo_ClientZone_Files._.LastUser, UserId, SearchType.Equal));
            //}
             

            //筛选文章的状态
            if (FileType >= 0)
            {
                Where = Playngo_ClientZone_Files.ByType(Where, FileType);
            }


            if (!String.IsNullOrEmpty(Search_Title))
            {
                txtSearch.Text = HttpUtility.UrlDecode(Search_Title);
                Where.Add(new SearchParam(Playngo_ClientZone_Files._.Name, HttpUtility.UrlDecode(Search_Title), SearchType.Like));
            }


    
            if (Visibility >= 0)
            {
                Where.Add(new SearchParam(Playngo_ClientZone_Files._.Extension1, Visibility, SearchType.Equal));
            }


            return Where;
        }

        /// <summary>
        /// 批量改名
        /// 由于之前导入文件会把多加一个.zip的名称
        /// 暂时不删除
        /// </summary>
        public void BatchReNames()
        {
            Int32 ReName = WebHelper.GetIntParam(Request, "rename", 0);
            if (ReName > 0)
            {
                Int32 RecordCount = 0;
                QueryParam qp = new QueryParam();
                if (Visibility >= 0)
                {
                    qp.Where.Add(new SearchParam(Playngo_ClientZone_Files._.Extension1, Visibility, SearchType.Equal));
                }

                qp.Where.Add(new SearchParam(Playngo_ClientZone_Files._.FileName, ".zip.zip", SearchType.Like));


                List<Playngo_ClientZone_Files> Files = Playngo_ClientZone_Files.FindAll(qp, out RecordCount);
                if (Files != null && Files.Count > 0)
                {
                    foreach (var file in Files)
                    {
                        //得到新的文件名称和路径
                        String NewFileName = Common.ReplaceNoCase(file.FileName, ".zip.zip", ".zip");
                        String NewFilePath = Common.ReplaceNoCase(file.FilePath, ".zip.zip", ".zip");
                        var NewFileInfo = new FileInfo(MapPath(String.Format("{0}{1}", PortalSettings.HomeDirectory, NewFilePath)));

                        //得到老的文件路径
                        var OldFileInfo = new FileInfo(MapPath(String.Format("{0}{1}", PortalSettings.HomeDirectory, file.FilePath)));

                        //需要将存储的文件更名
                        OldFileInfo.MoveTo(NewFileInfo.FullName);

                        //需要移除掉重复的文件名
                        file.FileName = NewFileName;

                        //需要移除掉路径中重复的文件名
                        file.FilePath = NewFilePath;

                        //保存当前的修改
                        file.Update();


                    }
                }


            }
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
                    //BatchReNames();


                    BindDataList();
                    BindPageItem();
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException( ex);
            }

        }

        /// <summary>
        /// 列表行创建
        /// </summary>
        protected void gvCommentList_RowCreated(object sender, GridViewRowEventArgs e)
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
        protected void gvCommentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //还原出数据
                Playngo_ClientZone_Files Media = e.Row.DataItem as Playngo_ClientZone_Files;

                if (Media != null && Media.ID > 0)
                {
                    #region "编辑&删除按钮"
                    HyperLink hlEdit = e.Row.FindControl("hlEdit") as HyperLink;
                    HyperLink hlMobileEdit = e.Row.FindControl("hlMobileEdit") as HyperLink;
                    LinkButton btnRemove = e.Row.FindControl("btnRemove") as LinkButton;
                    LinkButton btnMobileRemove = e.Row.FindControl("btnMobileRemove") as LinkButton;
                    //设置按钮的CommandArgument
                    btnRemove.CommandArgument = btnMobileRemove.CommandArgument = Media.ID.ToString();
                    //设置删除按钮的提示
                    //if (Media.Status == (Int32)EnumFileStatus.Recycle)
                    //{
                        btnRemove.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");
                        btnMobileRemove.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");
                    //}
                    //else
                    //{
                    //    btnRemove.Attributes.Add("onClick", "javascript:return confirm('" + ViewResourceText("DeleteRecycleItem", "Are you sure to move it to recycle bin?") + "');");
                    //    btnMobileRemove.Attributes.Add("onClick", "javascript:return confirm('" + ViewResourceText("DeleteRecycleItem", "Are you sure to move it to recycle bin?") + "');");
                    //}

                    hlEdit.NavigateUrl = hlMobileEdit.NavigateUrl = xUrl("ID", Media.ID.ToString(), "AddMedia");
                    #endregion

                    //发布者信息
                    e.Row.Cells[3].Text = "--";
                    if (Media.LastUser > 0)
                    {
                        UserInfo uInfo = UserController.GetUserById(PortalId,  Media.LastUser);
                        if (uInfo != null && uInfo.UserID > 0) e.Row.Cells[3].Text = String.Format("{0}<br />{1}", uInfo.Username, uInfo.DisplayName);
                    } 

                    
                    //发布时间
                    e.Row.Cells[4].Text = Media.LastTime.ToShortDateString();

                    //状态
                    e.Row.Cells[5].Text = EnumHelper.GetEnumTextVal( Media.Status ,typeof(EnumFileStatus));

                    Label lblFileExtension = e.Row.FindControl("lblFileExtension") as Label;
                    lblFileExtension.Text = Media.FileExtension;

                    Image imgFileName = e.Row.FindControl("imgFileName") as Image;
                 
                    HyperLink hlFileName = e.Row.FindControl("hlFileName") as HyperLink;
                    hlFileName.Text = Media.FileName.Replace("." + Media.FileExtension, "");
                   imgFileName.ImageUrl =  GetPhotoExtension(Media.FileExtension, Media.FilePath);
                   hlFileName.NavigateUrl = GetPhotoPath(Media.FilePath);

                   Label lblSize = e.Row.FindControl("lblSize") as Label;
                   if (Media.FileSize > 1024)
                   {
                       lblSize.Text = String.Format("{0} MB", Media.FileSize/1024);
                   }
                   else
                   {
                       lblSize.Text = String.Format("{0} KB", Media.FileSize);
                   }
                    

                }
            }
        }

        /// <summary>
        /// 列表排序
        /// </summary>
        protected void gvCommentList_Sorting(object sender, GridViewSortEventArgs e)
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
            Response.Redirect(CurrentUrl, false);
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

                    Playngo_ClientZone_Files Multimedia = Playngo_ClientZone_Files.FindByKeyForEdit(btnRemove.CommandArgument);

                    if (Multimedia != null && Multimedia.ID > 0)
                    {
                        //if (Multimedia.Status == (Int32)EnumFileStatus.Recycle)
                        //{
                            //要删除实际的文件
                            String DeletePath = Server.MapPath(GetPhotoPath(Multimedia.FilePath));
                            if (Multimedia.Delete() > 0)
                            {
                               

                                //删除文件
                                if (File.Exists(DeletePath))
                                {
                                    File.Delete(DeletePath);
                                }

                                //操作成功
                                mTips.LoadMessage("DeleteMediaLibrarySuccess", EnumTips.Success, this, new String[] { Multimedia.FileName });
                            }
                            else
                            {
                                //操作失败
                                mTips.LoadMessage("DeleteMediaLibraryError", EnumTips.Success, this, new String[] { Multimedia.FileName });
                            }
                        //}
                        //else
                        //{
                        //    Multimedia.Status = (Int32)EnumFileStatus.Recycle;
                        //    if (Multimedia.Update() > 0)
                        //    {
                        //        //移动到回收站操作成功
                        //        mTips.LoadMessage("DeleteCommentSuccess", EnumTips.Success, this, new String[] { Multimedia.FileName });
                        //    }
                        //    else
                        //    {
                        //        //移动到回收站操作失败
                        //        mTips.LoadMessage("DeleteCommentError", EnumTips.Success, this, new String[] { Multimedia.FileName });
                        //    }
                        //}
                        BindDataList();
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException( ex);
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
                ProcessModuleLoadException( ex);
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
                    mTips.IsPostBack = true;
                    for (int i = 0; i < Checkbox_Value_Array.Length; i++)
                    {
                        if (Int32.TryParse(Checkbox_Value_Array[i], out IDX))
                        {
                            Playngo_ClientZone_Files Multimedia = Playngo_ClientZone_Files.FindByKeyForEdit(IDX);
                            if (Multimedia != null && Multimedia.ID > 0)
                            {

                                //if (Multimedia.Status == (Int32)EnumFileStatus.Recycle && Status == (Int32)EnumFileStatus.Recycle)
                                //{
                                    //要删除实际的文件
                                    String DeletePath = Server.MapPath(GetPhotoPath(Multimedia.FilePath));
                                    if (Multimedia.Delete() > 0)
                                    {
                                       

                                        //删除文件
                                        if (File.Exists(DeletePath))
                                        {
                                            File.Delete(DeletePath);
                                        }

                                        //操作成功
                                        mTips.LoadMessage("DeleteMediaLibrarySuccess", EnumTips.Success, this, new String[] { Multimedia.FileName });
                                    }
                                    else
                                    {
                                        //操作失败
                                        mTips.LoadMessage("DeleteMediaLibraryError", EnumTips.Success, this, new String[] { Multimedia.FileName });
                                    }

                                //}
                                //else
                                //{
                                //    Multimedia.Status = Status;
                                //    if (Multimedia.Update() > 0)
                                //    {
                                //    }
                                //}
                            }
                        }
                    }
                    BindDataList();
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException( ex);
            }
        }



        #endregion



















    }
}