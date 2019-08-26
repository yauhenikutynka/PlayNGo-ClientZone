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
    public class ServiceDynamicModuleDelete : iService
    {
        public ServiceDynamicModuleDelete()
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

        public void Execute(BasePage Context)
        {
            Dictionary<String, Object> jsonDicts = new Dictionary<String, Object>();


            Int32 DynamicModuleId = WebHelper.GetIntParam(Context.Request, "DynamicModuleId", 0);

            QueryParam qp = new QueryParam();
            qp.Where.Add(new SearchParam(Playngo_ClientZone_DynamicModule._.ID, DynamicModuleId, SearchType.Equal));

            Int32 DeleteCount = Playngo_ClientZone_DynamicModule.Delete(qp);
            if (DeleteCount > 0)
            {

                QueryParam qpItem = new QueryParam();
                qpItem.Where.Add(new SearchParam(Playngo_ClientZone_DynamicItem._.DynamicID, DynamicModuleId, SearchType.Equal));

                Int32 DeleteItemCount = Playngo_ClientZone_DynamicItem.Delete(qpItem);
                jsonDicts.Add("DeleteItemCount", DeleteItemCount);
            }


            jsonDicts.Add("DeleteCount", DeleteCount);



            //转换数据为json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            ResponseString = jsSerializer.Serialize(jsonDicts);


        }




    }
}