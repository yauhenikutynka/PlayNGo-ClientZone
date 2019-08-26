using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_UserGroup_Add : BaseModule
    {
        #region "==属性=="

        /// <summary>
        /// 模块操作类
        /// </summary>
        private static ModuleController controller = new ModuleController();

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
                    _RoleGroups = Common.Split< RoleGroupInfo >( RoleController.GetRoleGroups(PortalId),1,999);
                }
                return _RoleGroups;
            }
        }


        private List<KeyValueEntity> _RoleGroupItems = new List<KeyValueEntity>();
        /// <summary>
        /// 角色分组列表(绑定到类表上用的)
        /// </summary>
        public List<KeyValueEntity> RoleGroupItems
        {
            get
            {
                if (!(_RoleGroupItems != null && _RoleGroupItems.Count > 0))
                {
                    _RoleGroupItems = new List<KeyValueEntity>();

                    foreach (var RoleGroup in RoleGroups)
                    {
                        String StrRoleNames = String.Empty;
                        var RoleNames = Playngo_ClientZone_RoleGroup. GetRolesByGroup(RoleGroup.RoleGroupID, PortalId);

                        if(!String.IsNullOrEmpty(RoleNames))
                        {
                            StrRoleNames = String.Format("( {0} )", RoleNames);
                        }

                        var RoleGroupItem = new KeyValueEntity();

                        RoleGroupItem.Key = String.Format("{0} {1}", RoleGroup.RoleGroupName, StrRoleNames);
                        RoleGroupItem.Value= RoleGroup.RoleGroupID;

                        _RoleGroupItems.Add(RoleGroupItem);
                    }


                }
                return _RoleGroupItems;
            }
        }


        /// <summary>提示操作类</summary>
        MessageTips mTips = new MessageTips();

        #endregion


        #region "==方法=="

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindDataToPage()
        {
             
            WebHelper.BindList<KeyValueEntity>(cblRoleGroups, RoleGroupItems, "Key", "Value");


        }

    

        /// <summary>
        /// 设置角色和用户的关系
        /// </summary>
        /// <returns>返回已经添加的角色</returns>
        private List<String> SetDataItem(ref UserInfo UserItem)
        {
            Int32 SelectUserID = WebHelper.GetIntParam(Request, "ddlSelectUser", 0);

            String SelectRoleGroups = String.Empty;
            String SelectRoleGroupTexts = String.Empty;
            WebHelper.GetSelected(cblRoleGroups, out SelectRoleGroupTexts, out SelectRoleGroups);


            List<String> AddRoleStatus = new List<string>();

            if (SelectUserID > 0)
            {

                if (!String.IsNullOrEmpty(SelectRoleGroups))
                {

                    List<String> RoleGroupIDs = Common.Split<String>(Common.GetList(SelectRoleGroups), 1, 9999);
                    if (RoleGroupIDs != null && RoleGroupIDs.Count > 0)
                    {

                        //取出用户信息
                        UserItem = UserController.GetUserById(PortalId, SelectUserID);
                        if (UserItem != null && UserItem.UserID > 0)
                        {

                            //整理出来需要添加的分组
                            var Roles = new List<RoleInfo>();
                            foreach (var strRoleGroupID in RoleGroupIDs)
                            {
                                Int32 RoleGroupID = 0;
                                if (Int32.TryParse(strRoleGroupID, out RoleGroupID) && RoleGroupID >= 0)
                                {
                                    //取出角色分组信息
                                    Roles.AddRange(Playngo_ClientZone_RoleGroup.FindRolesByGroup(PortalId,  RoleGroupID));
                                }
                            }

                            //对比用户的角色，将需要添加的角色关系添加好
                            if (Roles != null && Roles.Count > 0)
                            {

                                foreach (var role in Roles)
                                {
                                    //不存在的角色才需要添加
                                    if (!UserItem.IsInRole(role.RoleName))
                                    {
                                        RoleController.Instance.AddUserRole(PortalId, UserItem.UserID, role.RoleID, RoleStatus.Approved, false, xUserTime.LocalTime(), xUserTime.LocalTime().AddYears(100));
                                        AddRoleStatus.Add(role.RoleName);
                                    }
                                }

                            }
                            else
                            {
                                //没有需要添加的角色
                                XTrace.WriteLine("没有需要添加的角色");
                            }



                        }
                        else
                        {
                            //选择的用户无法查找到信息
                            XTrace.WriteLine("选择的用户无法查找到信息");
                        }
                    }
                    else
                    {
                        //角色分组信息解析有问题
                        XTrace.WriteLine("角色分组信息解析有问题");
                    }
                }
                else
                {
                    //未选择角色分组
                    XTrace.WriteLine("未选择角色分组");
                }
            }
            else
            {
                //未选择用户
                XTrace.WriteLine("未选择用户");
            }

            return AddRoleStatus;

        }
 








        #endregion


        #region "==事件=="


        /// <summary>
        /// 页面加载事件
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //绑定数据
                    BindDataToPage();
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        /// <summary>
        /// 更新绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 设置需要绑定的方案项
                var UserItem = new UserInfo();
          
                var UserRoles = SetDataItem(ref UserItem);
         
                mTips.LoadMessage("AddUserRolesSuccess", EnumTips.Success, this, new String[] { UserItem.Username, Common.GetStringByList(UserRoles) });

                //refresh cache
                SynchronizeModule();

                Response.Redirect(xUrl("AddUserGroups"), true);
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
       

         

        #endregion
    }
}