using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 下载文件列表的数据
    /// </summary>
    public class ServiceSearchGroups : BaseService, iService
    {
        public ServiceSearchGroups()
        {
            IsResponseWrite = true;
        }


        /// <summary>
        /// 是否写入输出
        /// </summary>
        public bool IsResponseWrite
        {
            get;
            set;
        }


        private String _ResponseString;
        /// <summary>
        /// 输出字符串
        /// </summary>
        public string ResponseString
        {
            get
            {
                return _ResponseString;
            }
            set
            {
                _ResponseString = value;
            }
        }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName
        {
            get
            {
                return "User Search JSON";
            }
        }

        public void Execute(BasePage Context)
        {
            TemplateFormat xf = new TemplateFormat(Context);

            QueryParam qp = new QueryParam();
            qp.ReturnFields = "ID,Title";


            qp.PageIndex = WebHelper.GetIntParam(Context.Request, "PageIndex", 1);
            qp.PageSize = WebHelper.GetIntParam(Context.Request, "PageSize", xf.ViewSettingT<Int32>("General.Search.Size", 10));

       
            var RoleGroupList = RoleController.GetRoleGroups(Context.PortalId);
            var RoleGroups = Common.Split<RoleGroupInfo>(RoleGroupList, 1, 999);

            String SearchText = WebHelper.GetStringParam(Context.Request, "search", "");
            if (!String.IsNullOrEmpty(SearchText))
            {
                RoleGroups = RoleGroups.FindAll(r=>r.RoleGroupName.IndexOf(SearchText,StringComparison.CurrentCultureIgnoreCase) >= 0);
            }

            RoleGroups = Common.Split<RoleGroupInfo>(RoleGroups, qp.PageIndex, qp.PageSize);




            Dictionary <String, Object> jsonPictures = new Dictionary<string, Object>();


            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            List<Dictionary<String, Object>> DictItems = new List<Dictionary<string, object>>();
            foreach (var RoleGroupItem in RoleGroups)
            {
                int index = RoleGroups.IndexOf(RoleGroupItem); //index 为索引值

                Dictionary<String, Object> jsonDict = new Dictionary<String, Object>();

                jsonDict.Add("id", RoleGroupItem.RoleGroupID);
                jsonDict.Add("text", RoleGroupItem.RoleGroupName);

                jsonDict.Add("roles",Playngo_ClientZone_RoleGroup.FindRolesByGroup(Context.PortalId,RoleGroupItem.RoleGroupID));
                DictItems.Add(jsonDict);
            }

            jsonPictures.Add("Items", DictItems);
            jsonPictures.Add("Pages", qp.Pages);
            jsonPictures.Add("RecordCount", RoleGroupList.Count);

            //转换数据为json
            ResponseString = jsSerializer.Serialize(jsonPictures);
        }






        /// <summary>
        /// 创建查询语句
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public QueryParam CreateQueryParam(QueryParam qp, BasePage Context)
        {
            qp.Where.Add(new SearchParam("Status", (Int32)EnumStatus.Published, SearchType.Equal));
            qp.Where.Add(new SearchParam("PortalId", Context.PortalId, SearchType.Equal));

            //搜索标题
            String Search = WebHelper.GetStringParam(Context.Request, "Search", "");
            if (!String.IsNullOrEmpty(Search))
            {
                qp.Where.Add(new SearchParam("Title", Common.ReplaceEscape(Search), SearchType.Like));

            }
            return qp;
        }





    }
}