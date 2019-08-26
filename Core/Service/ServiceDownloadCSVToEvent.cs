
using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 事件详情编辑页面导出相关用户
    /// </summary>
    public class ServiceDownloadCSVToEvent : iService
    {
        public ServiceDownloadCSVToEvent()
        {
            IsResponseWrite = false;
        }


        public string ResponseString
        {
            get;
            set;
        }

        /// <summary>
        /// 是否写入输出
        /// </summary>
        public bool IsResponseWrite
        {
            get;
            set;
        }

        public void Execute(BasePage Context)
        {
            Int32 EventId = WebHelper.GetIntParam(Context.Request, "EventId", 0);
            if (EventId > 0)
            {
                var EventItem = Playngo_ClientZone_Event.FindByKeyForEdit(EventId);
                if (EventItem != null && EventItem.ID > 0)
                {
                    //拼凑文件名
                    String FileName = String.Format("Users_{0}_{1}.{2}", Context.ModuleId, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "csv");
                    //文件路径
                    String FilePath = Context.Server.MapPath(String.Format("{0}ClientZone/Export/{1}", Context.PortalSettings.HomeDirectory, FileName));
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
                    excel.Title =String.Format("User list of events {0}", EventItem.Title);
                    excel.ExportFileName = FilePath;


                    DataTable dt = new DataTable(excel.Title);
                    //获取表格数据
                    dt = GetDataTable(EventItem, dt, Context);
                    //导出表格数据到CSV
                    Boolean  flag = excel.ExportToCSV(dt);

                    if (flag)
                    {
                        //下载文件
                        FileSystemUtils.DownloadFile(excel.ExportFileName, FileName);
                    }
                }
                else
                {
                    ResponseString = "没找到数据怎么搞";
                }
            }
            else
            {
                ResponseString = "传过来的文件编号都不对";
            }
             
        }


        /// <summary>
        /// 获取表格
        /// </summary>
        /// <param name="EventItem"></param>
        /// <param name="dt"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public DataTable GetDataTable(Playngo_ClientZone_Event EventItem, DataTable dt, BasePage Context)
        {
            //添加表字段
            dt.Columns.Add("UserID");
            dt.Columns.Add("Username");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");

            var UserList = ConvertUserList(EventItem);

            if (UserList != null && UserList.Count > 0)
            {
                foreach (var UserItem in UserList)
                {
                    DataRow dr = dt.NewRow();

                    dr["UserID"] = UserItem.UserID;
                    dr["Username"] = UserItem.Username;
                    dr["FirstName"] = UserItem.FirstName;
                    dr["LastName"] = UserItem.LastName;
                  
                    dt.Rows.Add(dr);
                }
            }
 
            return dt;
        }

        /// <summary>
        /// 获取转换事件相关的用户列表
        /// </summary>
        /// <param name="EventItem"></param>
        /// <returns></returns>
        public List<UserInfo> ConvertUserList(Playngo_ClientZone_Event EventItem)
        {

            var UserDataList = new List<UserInfo>();
            var UserList = new List<UserInfo>();


            if (EventItem.Per_AllUsers == 0)
            {
                UserDataList = Common.Split<UserInfo>(UserController.GetUsers(EventItem.PortalId), 1, Int32.MaxValue);
            }
            else if (!String.IsNullOrEmpty(EventItem.Per_Roles))
            {
                var RoleNames =  Common.GetList(EventItem.Per_Roles);
                if (RoleNames != null && RoleNames.Count > 0)
                {
                   
                    foreach (var RoleName in RoleNames)
                    {
                        if (!String.IsNullOrEmpty(RoleName))
                        {
                            UserDataList =  DotNetNuke.Security.Roles.RoleController.Instance.GetUsersByRole(EventItem.PortalId, RoleName).ToList();
                        }
                    }
              
                }

            }



            if (UserDataList != null && UserDataList.Count > 0)
            {
                foreach (var tempUser in UserDataList)
                {
                    if (!UserList.Exists(r => r.UserID == tempUser.UserID))
                    {
                        if (IsPreJurisdictionView(tempUser, EventItem.Per_AllJurisdictions, EventItem.Per_Jurisdictions))
                        {
                            UserList.Add(tempUser);
                        }
                    }
                }
            }


            return UserList;
        }



        /// <summary>
        /// 判断该数据是否有区域权限浏览
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public Boolean IsPreJurisdictionView(UserInfo User,  int Per_AllJurisdictions, String Per_Jurisdictions)
        {

            Boolean IsPre = false;
            if (Per_AllJurisdictions == 0)
            {
                IsPre = true;
            }
            else
            {
                if (!String.IsNullOrEmpty(Per_Jurisdictions))
                {
                    var Jurisdictions = Common.GetList(Per_Jurisdictions);
                    if (Jurisdictions != null && Jurisdictions.Count > 0)
                    {
                        foreach (var Jurisdiction in Jurisdictions)
                        {
                            Int32 JurisdictionId = 0;
                            if (Int32.TryParse(Jurisdiction, out JurisdictionId))
                            {
                                //需要根据当前的区域去查找区域的关联角色
                                if (AllJurisdictions.Exists(r => r.ID == JurisdictionId))
                                {
                                    var JurisdictionItem = AllJurisdictions.Find(r => r.ID == JurisdictionId);

                                    //根据关联的角色判断当前用户是否需要符合
                                    if (IsPreRoleView(User,JurisdictionItem.Per_AllUsers, JurisdictionItem.Per_Roles))
                                    {
                                        IsPre = true;
                                        break;
                                    }

                                }
                            }


                        }

                    }
                }
            }
            return IsPre;
        }


        private List<Playngo_ClientZone_Jurisdiction> _AllJurisdictions = new List<Playngo_ClientZone_Jurisdiction>();
        /// <summary>
        /// 所有区域限制
        /// </summary>
        public List<Playngo_ClientZone_Jurisdiction> AllJurisdictions
        {
            get
            {
                if (!(_AllJurisdictions != null && _AllJurisdictions.Count > 0))
                {
                    _AllJurisdictions = Playngo_ClientZone_Jurisdiction.FindAllByModuleID(WebHelper.GetIntParam(HttpContext.Current.Request, "ModuleId", 0));
                }

                return _AllJurisdictions;
            }


        }

        /// <summary>
        /// 判断该数据是否有角色权限浏览
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public Boolean IsPreRoleView(UserInfo User, int Per_AllUsers, String Per_Roles)
        {

            Boolean IsPre = false;
            if (Per_AllUsers == 0 || (User.UserID > 0 && User.IsSuperUser))
            {
                IsPre = true;
            }
            else
            {
                if (User.UserID > 0 && !String.IsNullOrEmpty(Per_Roles))
                {
                    foreach (var r in User.Roles)
                    {
                        var Roles = Common.GetList(Per_Roles);

                        if (Roles.IndexOf(r) >= 0)
                        {
                            IsPre = true;
                            break;
                        }
                    }
                }
            }
            return IsPre;
        }
    }
}