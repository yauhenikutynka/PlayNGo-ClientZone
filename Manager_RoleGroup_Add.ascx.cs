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
    public partial class Manager_RoleGroup_Add : BaseModule
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
         


        /// <summary>提示操作类</summary>
        MessageTips mTips = new MessageTips();

        #endregion


        #region "==方法=="

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindDataToPage()
        {
             
            WebHelper.BindList<RoleInfo>(cblRoles, RoleController.Instance.GetRoles(PortalId).ToList<RoleInfo>(), "RoleName", "RoleID");


        }



        /// <summary>
        /// 设置角色和用户的关系
        /// </summary>
        /// <returns>返回已经添加的角色</returns>
        private List<String> SetDataItem(ref RoleGroupInfo RoleGroupItem)
        {
            Int32 SelectGroupID = WebHelper.GetIntParam(Request, "ddlSelectGroup",-1);

            String SelectRoles = String.Empty;
            String SelectRoleTexts = String.Empty;
            WebHelper.GetSelected(cblRoles, out SelectRoleTexts, out SelectRoles);
            

            List<String> RoleStatus = new List<string>();
   

            if (SelectGroupID >= 0)
            {
                //请求出待改变的角色分组信息
                RoleGroupItem = RoleController.GetRoleGroup(PortalId, SelectGroupID);



                if (!String.IsNullOrEmpty(SelectRoles))
                {
                 
                    List<String> RoleIDs = Common.Split<String>(Common.GetList(SelectRoles), 1, 9999);

                    var OldRoleGroups = Playngo_ClientZone_RoleGroup.FindListByGroup(SelectGroupID);

                    //角色选择中不存在的需要删除
                    if (OldRoleGroups != null && OldRoleGroups.Count > 0)
                    {
                        foreach (var OldRoleGroup in OldRoleGroups)
                        {
                            //但原RoleId在选择列表中不存在时需要删除
                            if (!(RoleIDs != null && RoleIDs.Count > 0) || !RoleIDs.Exists(r => r == OldRoleGroup.RoleId.ToString()))
                            {
                                if( OldRoleGroup.Delete() > 0)
                                {
                                    //构造删除角色列表状态
                                    var role = RoleController.Instance.GetRoleById(PortalId, OldRoleGroup.RoleId);
                                    if (role != null && role.RoleID >= 0)
                                    {
                                        RoleStatus.Add(role.RoleName);
                                    }
                                }
                            }
                        }
                    }

                    //角色选择中多出的需要添加
                    if (RoleIDs != null && RoleIDs.Count > 0)
                    {
                        foreach (var strRoleID in RoleIDs)
                        {
                            Int32 RoleId = 0;
                            if (int.TryParse(strRoleID, out RoleId) && RoleId >= 0)
                            {
                                if (!(OldRoleGroups != null && OldRoleGroups.Count > 0) || !OldRoleGroups.Exists(r => r.RoleId == RoleId))
                                {
                                
                                    if (new Playngo_ClientZone_RoleGroup() { GroupId = SelectGroupID, RoleId = RoleId }.Insert() > 0)
                                    {
                                        //构造增加角色列表状态
                                        var role = RoleController.Instance.GetRoleById(PortalId, RoleId);
                                        if (role != null && role.RoleID >= 0)
                                        {
                                            RoleStatus.Add(role.RoleName);
                                        }
                                    }
                                  
                                }
                            }
                        }
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

            return RoleStatus;

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
                var RoleGroupItem = new RoleGroupInfo();
          
                var GroupRoles = SetDataItem(ref RoleGroupItem);
         
                mTips.LoadMessage("ChangeGroupRolesSuccess", EnumTips.Success, this, new String[] { RoleGroupItem.RoleGroupName, Common.GetStringByList(GroupRoles) });

                //refresh cache
                SynchronizeModule();

                Response.Redirect(xUrl("AddRoleGroups"), true);
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
       

         

        #endregion
    }
}