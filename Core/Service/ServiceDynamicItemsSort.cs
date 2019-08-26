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
    public class ServiceDynamicItemsSort : iService
    {
        public ServiceDynamicItemsSort()
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
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            Int32 DynamicModuleId = WebHelper.GetIntParam(Context.Request, "DynamicModuleId", 0);
            int UpdateResult = 0;
            var json = WebHelper.GetStringParam(Context.Request, "json", "");
            if(!String.IsNullOrEmpty(json))
            {
               
                var JsonItems = jsSerializer.Deserialize<List< Dictionary<String, Int32>>>(json);
                if (JsonItems != null && JsonItems.Count > 0)
                {
                    for (int i = 0; i < JsonItems.Count; i++)
                    {
                        UpdateResult += Playngo_ClientZone_DynamicItem.Update(String.Format("Sort={0}", 1 + i), String.Format("ID={0}", JsonItems[i]["id"]));
                    }
                }

            }
            jsonDicts.Add("UpdateCount", UpdateResult);



            //转换数据为json

            ResponseString = jsSerializer.Serialize(jsonDicts);


        }




    }
}