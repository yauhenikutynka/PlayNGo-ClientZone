using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Profile;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Playngo.Modules.ClientZone
{


    /// <summary>
    /// 更新下载文件关联页面
    /// </summary>
    public class ServiceRelationPageUpdate : iService
    {
        public ServiceRelationPageUpdate()
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
            Common.UpdateDictionary(jsonDicts, "Message", "");
            Common.UpdateDictionary(jsonDicts, "Result", "false");

            Int32 DownloadID = WebHelper.GetIntParam(Context.Request, "DownloadID", 0);
            Int32 ItemID = WebHelper.GetIntParam(Context.Request, "ItemID", 0);
            if (DownloadID > 0 && ItemID > 0)
            {
                var DownloadRelation = new Playngo_ClientZone_DownloadRelation();


                Int32 PageType = WebHelper.GetIntParam(Context.Request, "PageType", 0);

                Int32 RecordCount = 0;
                QueryParam qp = new QueryParam();
                qp.Where.Add(new SearchParam(Playngo_ClientZone_DownloadRelation._.DownloadID, DownloadID, SearchType.Equal));
                qp.Where.Add(new SearchParam(Playngo_ClientZone_DownloadRelation._.PageType, PageType, SearchType.Equal));
                qp.Where.Add(new SearchParam(Playngo_ClientZone_DownloadRelation._.ItemID, ItemID, SearchType.Equal));

                if (Playngo_ClientZone_DownloadRelation.FindCount(qp) == 0)
                {
                    DownloadRelation.PageType = PageType;
                    DownloadRelation.DownloadID = DownloadID;
                    DownloadRelation.ItemID = ItemID;

                    DownloadRelation.Sort = Playngo_ClientZone_DownloadRelation.FindMaxSrot(DownloadID, PageType)+1;

                    DownloadRelation.ModuleId = Context.ModuleId;
                    DownloadRelation.PortalId = Context.PortalId;
                    DownloadRelation.CreateTime = xUserTime.LocalTime();
                    DownloadRelation.CreateUser = Context.UserId;
                    DownloadRelation.ID = DownloadRelation.Insert();

                    if (DownloadRelation.ID > 0)
                    {
                        foreach (var Field in Playngo_ClientZone_DownloadRelation.Meta.Fields)
                        {
                            jsonDicts.Add(Field.ColumnName, DownloadRelation[Field.ColumnName]);
                        }

                        Common.UpdateDictionary(jsonDicts, "PageTypeText", EnumHelper.GetEnumTextVal(DownloadRelation.PageType,typeof(EnumDisplayModuleType)));
                        Common.UpdateDictionary(jsonDicts, "Title",WebHelper.GetStringParam(Context.Request, "Title", ""));

                        Common.UpdateDictionary(jsonDicts, "Result", "true");
                    }
                    else
                    {
                        Common.UpdateDictionary(jsonDicts, "Message", "添加数据不成功");
                    }

                    



                } else
                {
                    //DownloadRelation = Playngo_ClientZone_DownloadRelation.FindItem(qp, out RecordCount);

                    Common.UpdateDictionary(jsonDicts, "Message", "数据已经存在忽略");
                }
 
            }
            else
            {
                //出入的ID数据不对
                Common.UpdateDictionary(jsonDicts, "Message", "出入的ID数据不对");
            }
            //转换数据为json
            ResponseString = jsSerializer.Serialize(jsonDicts);

        }



        public String ConvertBool(String ss)
        {
            if (!string.IsNullOrEmpty(ss))
            {
                return ss == "true" ? "True" : ss;
            }
            return "false";

        }




    }
}