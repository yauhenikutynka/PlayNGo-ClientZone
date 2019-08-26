using DotNetNuke.Entities.Users;
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
    public class ServiceSearchUsers : BaseService, iService
    {
        public ServiceSearchUsers()
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

            //排序的规则
            qp.OrderType = WebHelper.GetIntParam(Context.Request, "OrderType", 1);
            qp.Orderfld = "ID"; //WebHelper.GetStringParam(Context.Request, "Orderfld", "ID");


            //查询语句
            qp = CreateQueryParam(qp, Context);

            //权限筛选
            qp = CreateQueryByRoles(qp, Context);

            //区域筛选
            qp = CreateQueryByJurisdictions(qp, Context);



            int RecordCount = 0;

            var UserList = new List<UserInfo>();
           String SearchText = WebHelper.GetStringParam(Context.Request, "search", "");
            if (!String.IsNullOrEmpty(SearchText))
            {
                //UserList = UserController.Instance.GetUsersAdvancedSearch(Context.PortalId, 0, -1, -1, -1, false, 0, 10, "USERID", false, "Username,DisplayName", "%" + SearchText  ).ToList<UserInfo>();
                UserList = Common.Split<UserInfo>(UserController.GetUsersByUserName(Context.PortalId, "%" + SearchText + "%", 0, 10, ref RecordCount, false, false), 1, 100); //  "USERID", false, "Username", ).ToList<UserInfo>();
            }
            else
            {
                UserList = Common.Split<UserInfo>( UserController.GetUsers(Context.PortalId, 0, 10, ref RecordCount),1,100);   //.GetUsersBasicSearch(Context.PortalId, 1, 10, "USERID", false, "", "").ToList<UserInfo>();
            }


            

            Dictionary<String, Object> jsonPictures = new Dictionary<string, Object>();


            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            List<Dictionary<String, Object>> DictItems = new List<Dictionary<string, object>>();
            foreach (var UserItem in UserList)
            {
                int index = UserList.IndexOf(UserItem); //index 为索引值

                Dictionary<String, Object> jsonDict = new Dictionary<String, Object>();

                jsonDict.Add("id", UserItem.UserID);
                jsonDict.Add("text", String.Format("{0} - {1}", UserItem.Username, UserItem.DisplayName));
                jsonDict.Add("roles",Common.GetStringByList(  UserItem.Roles) );
                DictItems.Add(jsonDict);
            }

            jsonPictures.Add("Items", DictItems);
            jsonPictures.Add("Pages", qp.Pages);
            jsonPictures.Add("RecordCount", RecordCount);

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