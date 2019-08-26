using DotNetNuke.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Playngo.Modules.ClientZone
{


    /// <summary>
    /// 事件网址的跳转
    /// </summary>
    public class ServiceDynamicItemDelete : iService
    {
        public ServiceDynamicItemDelete()
        {
            IsResponseWrite = true;
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


        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName
        {
            get
            {
                return "DynamicItem Delete";
            }
        }

        public void Execute(BasePage Context)
        {
            Dictionary<String, Object> jsonDicts = new Dictionary<String, Object>();


            Int32 DynamicItemId = WebHelper.GetIntParam(Context.Request, "DynamicItemId", 0);

            QueryParam qp = new QueryParam();
            qp.Where.Add(new SearchParam(Playngo_ClientZone_DynamicItem._.ID, DynamicItemId, SearchType.Equal));

            Int32 DeleteCount =  Playngo_ClientZone_DynamicItem.Delete(qp);

            jsonDicts.Add("DeleteCount", DeleteCount);



            //转换数据为json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            ResponseString = jsSerializer.Serialize(jsonDicts);


        }




    }
}