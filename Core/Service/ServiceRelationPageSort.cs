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
    public class ServiceRelationPageSort : iService
    {
        public ServiceRelationPageSort()
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

            String StrIDs = WebHelper.GetStringParam(Context.Request, "IDs", "");
            Int32 UpdateResult = 0;
            if (!String.IsNullOrEmpty(StrIDs))
            {
                var Ids = Common.GetList(StrIDs);
                if (Ids != null && Ids.Count > 0)
                {
                    foreach (var Id in Ids)
                    {
                        var index = Ids.IndexOf(Id);

                        UpdateResult += Playngo_ClientZone_DownloadRelation.Update(String.Format("Sort={0}", 1 + index), String.Format("ID={0}", Id));

                    }
                }
 

            }
            jsonDicts.Add("UpdateCount", UpdateResult);



            //转换数据为json

            ResponseString = jsSerializer.Serialize(jsonDicts);


        }




    }
}