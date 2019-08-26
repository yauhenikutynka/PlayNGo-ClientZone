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
    public class ServiceJSONDownloadFiles : BaseService, iService
    {
        public ServiceJSONDownloadFiles()
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
                return "DownloadFile JSON";
            }
        }




        public void Execute(BasePage Context)
        {


            Dictionary<String, Object> jsonDatas = new Dictionary<string, Object>();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            TemplateFormat xf = new TemplateFormat(Context);


            QueryParam qp = new QueryParam();


            qp.Orderfld = WebHelper.GetStringParam(Context.Request, "Orderfld", "ID");
            qp.OrderType = WebHelper.GetIntParam(Context.Request, "OrderType", 1);


            qp.PageIndex = WebHelper.GetIntParam(Context.Request, "PageIndex", 1);
            qp.PageSize = WebHelper.GetIntParam(Context.Request, "PageSize", xf.ViewSettingT<Int32>("General.Downloads.Pagings", 10));


            //查询语句
            qp = CreateQueryParam(qp, Context);

            //权限筛选
            qp = CreateQueryByRoles(qp, Context);

            //区域筛选
            qp = CreateQueryByJurisdictions(qp, Context);

            //文件类型筛选
            qp = CreateQueryByFileTypes(qp, Context);

            //游戏分类筛选
            qp = CreateQueryByGameGategorys(qp, Context);



            int RecordCount = 0;
            List<Playngo_ClientZone_DownloadFile> fileList = Playngo_ClientZone_DownloadFile.FindAll(qp, out RecordCount);


            //配置值
            XmlFormat xmlFormat = new XmlFormat(Context.Server.MapPath(String.Format("{0}Resource/xml/Config.Setting.Downloads.xml", Context.ModulePath)));
            var XmlItemSetting = xmlFormat.ToList<SettingEntity>();


            List<Dictionary<String, Object>> DictFiles = new List<Dictionary<string, object>>();
            foreach (var fileItem in fileList)
            {
                int index = fileList.IndexOf(fileItem); //index 为索引值

                Dictionary<String, Object> jsonDict = new Dictionary<String, Object>();
              


                //循环输出所有的固定项
                foreach (var Field in Playngo_ClientZone_DownloadFile.Meta.Fields)
                {
                    jsonDict.Add(Field.ColumnName, fileItem[Field.ColumnName]);
                }

             
                if (XmlItemSetting != null && XmlItemSetting.Count > 0)
                {
                    var ItemSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(fileItem.Options);
                    foreach (var ItemSetting in XmlItemSetting)
                    {
                        jsonDict = Common.UpdateDictionary(jsonDict, ItemSetting.Name, xf.ViewItemSetting(fileItem, ItemSetting.Name, ItemSetting.DefaultValue));
                    }
                }


                //下载地址
                String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}&FileId={6}",
                                                 Context.ModulePath,
                                                 Context.Settings_ModuleID,
                                                 "DownloadFile",
                                                 Context.Settings_TabID,
                                                 Context.PortalId,
                                                 Context.language,
                                                 fileItem.ID);
                jsonDict = Common.UpdateDictionary(jsonDict, "DownloadUrl", DownloadUrl);

                jsonDict = Common.UpdateDictionary(jsonDict, "ReleaseDateString", fileItem.ReleaseDate.ToShortDateString());

                //文件类型转换
                jsonDict = Common.UpdateDictionary(jsonDict, "FileTypesString", Playngo_ClientZone_FileType.ConvertFileTypes( fileItem.FileTypes));

                //订阅状态等
                Int32 NotifyStatus = (Int32)EnumNotificationStatus.None;
                if (fileItem.NotifyInclude == 1 )//&& fileItem.StartTime >= xUserTime.LocalTime().AddDays(-xf.ViewSettingT<Int32>("General.ExpiryTimeNotification", 7)))
                {
                    NotifyStatus = fileItem.NotifyStatus;
                }


                jsonDict = Common.UpdateDictionary(jsonDict, "NotificationStatus", EnumHelper.GetEnumTextVal(NotifyStatus, typeof(EnumNotificationStatus)));
                jsonDict = Common.UpdateDictionary(jsonDict, "NotificationStatusClass", EnumHelper.GetEnumTextVal(NotifyStatus, typeof(EnumNotificationStatus)).ToLower());
                //未来日期出现Coming Soon
                jsonDict = Common.UpdateDictionary(jsonDict, "ComingSoonDisplay", fileItem.ReleaseDate > xUserTime.LocalTime());

                DictFiles.Add(jsonDict);
            }

            jsonDatas.Add("data", DictFiles);
            jsonDatas.Add("Pages", qp.Pages);
            jsonDatas.Add("RecordCount", RecordCount);

            //转换数据为json

            ResponseString = jsSerializer.Serialize(jsonDatas);
        }



    

       








        /// <summary>
        /// 创建查询语句
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public QueryParam CreateQueryParam(QueryParam qp, BasePage Context)
        {
            qp.Where.Add(new SearchParam(Playngo_ClientZone_DownloadFile._.Status, (Int32)EnumStatus.Published, SearchType.Equal));
            qp.Where.Add(new SearchParam(Playngo_ClientZone_DownloadFile._.PortalId, Context.PortalId, SearchType.Equal));


            //未到开始时间的不显示数据
            qp.Where.Add(new SearchParam(Playngo_ClientZone_DownloadFile._.StartTime, xUserTime.UtcTime(), SearchType.LtEqual));

            //搜索标题
            String Search = WebHelper.GetStringParam(Context.Request, "Search", "");
            if (!String.IsNullOrEmpty(Search))
            {
                if (qp.WhereSql.Length > 0) qp.WhereSql.Append(" AND ");

                qp.WhereSql.Append(" ( ");
    
                qp.WhereSql.Append(new SearchParam("Title", Common.ReplaceEscape(Search), SearchType.Like).ToSql());

                qp.WhereSql.Append(" OR ");

                qp.WhereSql.Append(new SearchParam("Version", Common.ReplaceEscape(Search), SearchType.Like).ToSql());

                qp.WhereSql.Append(" ) ");

               
            }




            return qp;
        }
         


        /// <summary>
        /// 创建查询语句(文件类型)
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public QueryParam CreateQueryByFileTypes(QueryParam qp, BasePage Context)
        {
            String FileTypes = WebHelper.GetStringParam(Context.Request, "FileTypes", "");
            if (!String.IsNullOrEmpty(FileTypes))
            {
                var FileTypeList = Common.GetList(FileTypes);
                if (FileTypeList != null && FileTypeList.Count > 0)
                {
                    if(!FileTypeList.Exists(r=>r=="0"))
                    {
                        System.Text.StringBuilder WhereSql = new System.Text.StringBuilder();
                        foreach (var FileTypeItem in FileTypeList)
                        {
                            if (!String.IsNullOrEmpty(FileTypeItem))
                            {
                                if (WhereSql.Length > 0) WhereSql.Append(" OR ");

                                WhereSql.Append(new SearchParam(Playngo_ClientZone_DownloadFile._.FileTypes, String.Format(",{0},", FileTypeItem), SearchType.Like).ToSql());
                            }
                        }

                        if (WhereSql.Length > 0) qp.WhereSql.AppendFormat(" {0} ( {1} )", qp.WhereSql.Length > 0 ? "AND" : "", WhereSql);
                    }
                }
            }
            else
            {
                //不勾选的时候不出现任何数据
                qp.Where.Add(new SearchParam("FileTypes", "0", SearchType.Equal));

            }

            return qp;
        }








    }
}