using DotNetNuke.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Playngo.Modules.ClientZone
{


    /// <summary>
    /// 删除下载文件和页面的关系
    /// </summary>
    public class ServiceRelationPageDelete : iService
    {
        public ServiceRelationPageDelete()
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
                return "Relation Delete";
            }
        }

        public void Execute(BasePage Context)
        {
            Dictionary<String, Object> jsonDicts = new Dictionary<String, Object>();


            Int32 RelationId = WebHelper.GetIntParam(Context.Request, "ID", 0);

            QueryParam qp = new QueryParam();
            qp.Where.Add(new SearchParam(Playngo_ClientZone_DownloadRelation._.ID, RelationId, SearchType.Equal));

            Int32 DeleteCount = Playngo_ClientZone_DownloadRelation.Delete(qp);

            jsonDicts.Add("DeleteCount", DeleteCount);



            //转换数据为json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            ResponseString = jsSerializer.Serialize(jsonDicts);


        }




    }
}