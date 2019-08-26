using DotNetNuke.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 每个用户选择的Game Categories 获取 / 更新
    /// </summary>
    public class ServiceJSONUserGameCategories : iService
    {
        public ServiceJSONUserGameCategories()
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
                return "User Game Categories JSON";
            }
        }

        public void Execute(BasePage Context)
        {

            Dictionary<String, Object> jsonDatas = new Dictionary<string, Object>();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            TemplateFormat xf = new TemplateFormat(Context);


            if (Context.UserId > 0)
            {
                //jsonDatas.Add("SelectJurisdictions", new List<string>());

                var UserItem = Context.UserInfo;

         
                String SelectJurisdictions = WebHelper.GetStringParam(Context.Request, "SelectGameCategories", "");

                if (!String.IsNullOrEmpty(SelectJurisdictions))
                {
                    UserItem.Profile.SetProfileProperty("SelectGameCategories", SelectJurisdictions);
                }
                else
                {
                    UserItem.Profile.SetProfileProperty("SelectGameCategories", "-");
                }
              


                DataCache.ClearPortalCache(Context.PortalId, true);
                DataCache.ClearCache();


                DotNetNuke.Entities.Profile.ProfileController.UpdateUserProfile(UserItem);
                DotNetNuke.Entities.Users.UserController.UpdateUser(Context.PortalId, UserItem);
                jsonDatas.Add("Result", "Success");
                jsonDatas.Add("SelectGameCategories", SelectJurisdictions);

            }
            else
            {
                //没有登录怎么搞
                jsonDatas.Add("Result", "NoLogin");
            }





            //jsonPictures.Add("data", DictFiles);
            //jsonPictures.Add("Pages", qp.Pages);
            //jsonPictures.Add("RecordCount", RecordCount);

            //转换数据为json

            ResponseString = jsSerializer.Serialize(jsonDatas);
        }
         



    }
}