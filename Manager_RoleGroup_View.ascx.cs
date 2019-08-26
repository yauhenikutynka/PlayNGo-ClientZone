using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_RoleGroup_View : BaseModule
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

                return xUrl("", "", "Downloads", urls.ToArray());
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

        private List<RoleGroupInfo> _RoleGroups = new List<RoleGroupInfo>();
        /// <summary>
        /// 角色分组列表
        /// </summary>
        public List<RoleGroupInfo> RoleGroups
        {
            get
            {
                if (!(_RoleGroups != null && _RoleGroups.Count > 0))
                {
                    _RoleGroups = Common.Split<RoleGroupInfo>(RoleController.GetRoleGroups(PortalId), 1, 999);
                }
                return _RoleGroups;
            }
        }

        #endregion



        #region "方法"

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void BindDataList()
        {
            
            gvEventList.DataSource = GetUserList();
            gvEventList.DataBind();
            BindGridViewEmpty<UserInfo>(gvEventList, new UserInfo());
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetUserList()
        {
            int RecordCount = 0;

            var UserList = new List<UserInfo>();
            Int32 RoleGroupId = WebHelper.GetIntParam(Request, ddlRoleGroups.UniqueID, -1);

            if (RoleGroupId >= 0)
            {
                var Roles = Playngo_ClientZone_RoleGroup.FindRolesByGroup(PortalId, RoleGroupId);
                if (Roles != null && Roles.Count > 0)
                {
                    foreach (var role in Roles)
                    {
                        var RoleUsers = RoleController.Instance.GetUsersByRole(PortalId, role.RoleName);
                        if (RoleUsers != null && RoleUsers.Count > 0)
                        {
                            foreach (var RoleUser in RoleUsers)
                            {
                                if (!UserList.Exists(r => r.UserID == RoleUser.UserID))
                                {
                                    UserList.Add(RoleUser);
                                }
                            }

                        }


                    }
                }
                else
                {
                    //该分组下没有任何角色
                }



            }
            else
            {
                //没有选择任何角色
                //UserList = Common.Split<UserInfo>(UserController.GetUsers(PortalId, 0, 999, ref RecordCount), 1, 9999);
            }

            return UserList;
        }

        /// <summary>
        /// 转换用户列表
        /// </summary>
        /// <param name="UserList"></param>
        /// <returns></returns>
        public DataTable ConvertUserList(List<UserInfo> UserList)
        {
            DataTable dt = new DataTable();
            if (!dt.Columns.Contains("UserID")) dt.Columns.Add("UserID");
            if (!dt.Columns.Contains("Username")) dt.Columns.Add("Username");
            if (!dt.Columns.Contains("DisplayName")) dt.Columns.Add("DisplayName");
            if (!dt.Columns.Contains("Roles")) dt.Columns.Add("Roles");



            foreach (var UserItem in UserList)
            {
                DataRow dr = dt.NewRow();
                dr["UserID"] = UserItem.UserID;
                dr["Username"] = UserItem.Username;
                dr["DisplayName"] = UserItem.DisplayName;
                dr["Roles"] = Common.GetStringByList( UserItem.Roles,", ");
                dt.Rows.Add(dr);
            }

           


                return dt;
        }


        /// <summary>
        /// 绑定页面项
        /// </summary>
        private void BindPageItem()
        {
            WebHelper.BindList<RoleGroupInfo>(ddlRoleGroups, RoleGroups, "RoleGroupName", "RoleGroupID");
            WebHelper.BindItem(ddlRoleGroups, "Select Role Group", "-1");




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
                Where.Add(new SearchParam(Playngo_ClientZone_DownloadFile._.CreateUser, UserId, SearchType.Equal));
            }

            //筛选文章的状态
            if (EventStatus >= 0)
            {
                Where.Add(new SearchParam(Playngo_ClientZone_DownloadFile._.Status, EventStatus, SearchType.Equal));
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

            //Int32 DataIDX = 0;
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    //增加check列头全选
            //    TableCell cell = new TableCell();
            //    cell.Width = Unit.Pixel(5);
            //    cell.Text = "<label> <input id='CheckboxAll' value='0' type='checkbox' class='input_text' onclick='SelectAll()'/></label>";
            //    e.Row.Cells.AddAt(0, cell);


            //    foreach (TableCell var in e.Row.Cells)
            //    {
            //        if (var.Controls.Count > 0 && var.Controls[0] is LinkButton)
            //        {
            //            string Colume = ((LinkButton)var.Controls[0]).CommandArgument;
            //            if (Colume == Orderfld)
            //            {
            //                LinkButton l = (LinkButton)var.Controls[0];
            //                l.Text += string.Format("<i class=\"fa {0}{1}\"></i>", Orderfld == "Title" ? "fa-sort-alpha-" : "fa-sort-amount-", (OrderType == 0) ? "asc" : "desc");
            //            }
            //        }
            //    }

            //}
            //else
            //{
            //    //增加行选项
            //    DataIDX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UserID"));
            //    TableCell cell = new TableCell();
            //    cell.Width = Unit.Pixel(5);
            //    cell.Text = string.Format("<label> <input name='Checkbox' id='Checkbox' value='{0}' type='checkbox' type-item=\"true\" class=\"input_text\" /></label>", DataIDX);
            //    e.Row.Cells.AddAt(0, cell);

            //}


        }

        /// <summary>
        /// 列表行绑定
        /// </summary>
        protected void gvEventList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //还原出数据
                UserInfo UserItem = e.Row.DataItem as UserInfo;

                if (UserItem != null && UserItem.UserID > 0)
                {

                    if (UserItem.Roles != null && UserItem.Roles.Count() > 0)
                    {
                        var Roles = Common.Split<String>(UserItem.Roles,1,9999);
                        Roles.Remove("Registered Users");
                        Roles.Remove("Subscribers");
                        e.Row.Cells[3].Text = Common.GetStringByList(Roles, ", ");

                    }
 
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
        /// 导出表格信息
        /// </summary>
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var UserList = GetUserList();
                if (UserList != null && UserList.Count > 0)
                {
                    DataTable UserTable = ConvertUserList(UserList);




                    //拼凑文件名
                    String FileName = String.Format("Users_{0}_{1}.{2}", ModuleId, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "xls");
                    //文件路径
                    String FilePath = Context.Server.MapPath(String.Format("{0}ClientZone/Export/{1}", PortalSettings.HomeDirectory, FileName));
                    //文件实体
                    var fileInfo = new FileInfo(FilePath);

                    //文件路径是否需要创建
                    if (!fileInfo.Directory.Exists)
                    {
                        fileInfo.Directory.Create();
                    }


                    //创建导出类实体
                    ExportDotNet excel = new ExportDotNet();
                    //导出标题(Excel之类的才有)
                    excel.Title = "Users";
                    excel.ExportFileName = FilePath;
              
                    Boolean flag = excel.ExportToExcel(UserTable);

                    if (flag)
                    {
                        //下载文件
                        FileSystemUtils.DownloadFile(excel.ExportFileName, FileName);
                    }

                }
                else
                {
                    //没有用户
                }





            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindDataList();
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }























        #endregion


    }
}