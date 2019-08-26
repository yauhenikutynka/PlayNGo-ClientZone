using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// ProductRoadmap的数据
    /// 需要根据4个列表中的数据混合完成
    /// </summary>
    public class ServiceJSONProductRoadmap : BaseService, iService
    {
        public ServiceJSONProductRoadmap()
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
                return "ProductRoadmap JSON";
            }
        }



        public void Execute(BasePage Context)
        {
            Int32 RecordCount = 0;
            Dictionary<String, Object> jsonData= new Dictionary<string, Object>();
            List<Dictionary<String, Object>> DictDataList = new List<Dictionary<string, object>>();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            TemplateFormat xf = new TemplateFormat(Context);


            //创建数据查询类
            var dal = DAL.Create(Playngo_ClientZone_Event.Meta.ConnName);

            System.Text.StringBuilder SQLQuery = new System.Text.StringBuilder();


            var StrIncludedSections = xf.ViewSettingT<String>("Roadmap_IncludedSections", "0,2,3");
            if (!String.IsNullOrEmpty(StrIncludedSections))
            {
                var IncludedSections = Common.GetList(StrIncludedSections);

                if (IncludedSections != null && IncludedSections.Count > 0)
                {
                    //开始拼接联合查询的SQL

                    if (IncludedSections.Exists(r => r == ((Int32)EnumIncludedSections.Events).ToString()))
                    {
                        SQLQuery.Append(SqlQueryTable("Events", Playngo_ClientZone_Event.Meta.TableName, false, Context));
                    }


                    if (IncludedSections.Exists(r => r == ((Int32)EnumIncludedSections.Downloads).ToString()))
                    {
                        if (SQLQuery.Length > 0)
                        {
                            SQLQuery.Append(" UNION ");
                        }

                        SQLQuery.Append(SqlQueryTable("Downloads", Playngo_ClientZone_DownloadFile.Meta.TableName, true, Context));
                    }
 

                    if (IncludedSections.Exists(r => r == ((Int32)EnumIncludedSections.Campaigns).ToString()))
                    {
                        if (SQLQuery.Length > 0)
                        {
                            SQLQuery.Append(" UNION ");
                        }

                        SQLQuery.Append(SqlQueryTable("Campaigns", Playngo_ClientZone_Campaign.Meta.TableName, true, Context));
                    }


                    if (IncludedSections.Exists(r => r == ((Int32)EnumIncludedSections.GameSheets).ToString()))
                    {
                        if (SQLQuery.Length > 0)
                        {
                            SQLQuery.Append(" UNION ");
                        }

                        SQLQuery.Append(SqlQueryTable("GameSheets", Playngo_ClientZone_GameSheet.Meta.TableName, true, Context));
                    }

                    SQLQuery.Append(" ORDER BY ReleaseDate ASC ");


                    //将拼接的SQL获取数据集
                    String SQLString = SQLQuery.ToString();
                    var DataSets = dal.Select(dal.PageSplit(SQLString, 0, xf.ViewSettingT<Int32>("General.ProductRoadmap.Pagings", 10), "ID"), "ProductRoadmap");
                    RecordCount = dal.SelectCount(SQLString, "ProductRoadmap");
                    //开始解析数据集
                    if (DataSets != null && DataSets.Tables != null && DataSets.Tables.Count > 0 && DataSets.Tables[0].Rows != null)
                    {
                        var Rows = DataSets.Tables[0].Rows;
                        var Columns = DataSets.Tables[0].Columns;
                        if (Rows.Count > 0)
                        {
                            foreach (DataRow Row in Rows)
                            {
                                Dictionary<String, Object> DictDataItem = new Dictionary<string, object>();


                                Int32 ItemId = 0;
                                if (Int32.TryParse(Row["ID"].ToString(), out ItemId))
                                {
                                    var TableName = Convert.ToString(Row["TableName"]);
                                    DictDataItem.Add("TableName", TableName);
                                    //调用不同的数据读取程序
                                    if (TableName == "Events")
                                    {
                                        DictDataItem = GetDictDataItemsByEvents(DictDataItem, ItemId, Context);
                                    }
                                    else if (TableName == "Downloads")
                                    {
                                        DictDataItem = GetDictDataItemsByDownloadFiles(DictDataItem, ItemId, Context);
                                    }
                                    else if (TableName == "Campaigns")
                                    {
                                        DictDataItem = GetDictDataItemsByCampaigns(DictDataItem, ItemId, Context);
                                    }
                                    else if (TableName == "GameSheets")
                                    {
                                        DictDataItem = GetDictDataItemsByGameSheets(DictDataItem, ItemId, Context);
                                    }


                                }

                                DictDataList.Add(DictDataItem);
                            }

                        }
                    }
                }
            }




            jsonData.Add("RecordCount", RecordCount);
            jsonData.Add("DataCount", DictDataList.Count);
            jsonData.Add("data", DictDataList);
           
     

            //转换数据为json
            ResponseString = jsSerializer.Serialize(jsonData);
        }




        /// <summary>
        /// Events
        /// 将事件实体查询出来并填充到数据中
        /// </summary>
        /// <param name="DictDataItem"></param>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public Dictionary<String, Object> GetDictDataItemsByEvents(Dictionary<String, Object> DictDataItem, Int32 ItemId, BasePage Context)
        {
            var DataItem = Playngo_ClientZone_Event.FindByKeyForEdit(ItemId);
            if (DataItem != null && DataItem.ID > 0)
            {
                //循环输出所有的固定项
                foreach (var Field in Playngo_ClientZone_Event.Meta.Fields)
                {
                    DictDataItem.Add(Field.ColumnName, DataItem[Field.ColumnName]);
                }

                //移除累赘的字典项
                DictDataItem = Common.RemoveDictionary(DictDataItem, "Options");
      

                TemplateFormat xf = new TemplateFormat(Context);

                DictDataItem = Common.UpdateDictionary(DictDataItem, "Image", xf.ViewLinkUrl(xf.ViewItemSettingT<string>(DataItem, "Image", ""), "", Context));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "Summary", xf.ViewItemSettingT<string>(DataItem, "Summary", ""));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "Url", xf.GoUrl(DataItem));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "ReleaseDateStr", String.Format("{0} {1}{2},{3}", DataItem.ReleaseDate.ToString("MMM"), DataItem.ReleaseDate.Day, xf.GetDaySuffix(DataItem.ReleaseDate.Day), DataItem.ReleaseDate.ToString("yyyy")));

            }
            return DictDataItem;
        }


        /// <summary>
        /// DownloadFiles
        /// 将事件实体查询出来并填充到数据中
        /// </summary>
        /// <param name="DictDataItem"></param>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public Dictionary<String, Object> GetDictDataItemsByDownloadFiles(Dictionary<String, Object> DictDataItem, Int32 ItemId, BasePage Context)
        {
            var DataItem = Playngo_ClientZone_DownloadFile.FindByKeyForEdit(ItemId);
            if (DataItem != null && DataItem.ID > 0)
            {
                //循环输出所有的固定项
                foreach (var Field in Playngo_ClientZone_DownloadFile.Meta.Fields)
                {
                    DictDataItem.Add(Field.ColumnName, DataItem[Field.ColumnName]);
                }

                //移除累赘的字典项
                DictDataItem = Common.RemoveDictionary(DictDataItem, "Options");

                TemplateFormat xf = new TemplateFormat(Context);


                String DownloadUrl = String.Format("{0}Resource_Service.aspx?ModuleId={1}&Token={2}&TabId={3}&PortalId={4}&language={5}&FileId={6}",
                                               Context.ModulePath,
                                               Context.Settings_ModuleID,
                                               "DownloadFile",
                                               Context.Settings_TabID,
                                               Context.PortalId,
                                               Context.language,
                                               DataItem.ID);

                DictDataItem = Common.UpdateDictionary(DictDataItem, "Image", xf.ViewLinkUrl(xf.ViewItemSettingT<string>(DataItem, "Image", ""), "", Context));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "Summary", xf.ViewItemSettingT<string>(DataItem, "Summary", ""));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "Url", Context.FullPortalUrl( DownloadUrl));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "ReleaseDateStr", String.Format("{0} {1}{2},{3}", DataItem.ReleaseDate.ToString("MMM"), DataItem.ReleaseDate.Day, xf.GetDaySuffix(DataItem.ReleaseDate.Day), DataItem.ReleaseDate.ToString("yyyy")));

            }
            return DictDataItem;
        }

       

        /// <summary>
        /// Campaigns
        /// 将事件实体查询出来并填充到数据中
        /// </summary>
        /// <param name="DictDataItem"></param>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public Dictionary<String, Object> GetDictDataItemsByCampaigns(Dictionary<String, Object> DictDataItem, Int32 ItemId, BasePage Context)
        {
            var DataItem = Playngo_ClientZone_Campaign.FindByKeyForEdit(ItemId);
            if (DataItem != null && DataItem.ID > 0)
            {
                //循环输出所有的固定项
                foreach (var Field in Playngo_ClientZone_Campaign.Meta.Fields)
                {
                    DictDataItem.Add(Field.ColumnName, DataItem[Field.ColumnName]);
                }

                //移除累赘的字典项
                DictDataItem = Common.RemoveDictionary(DictDataItem, "Options");
                DictDataItem = Common.RemoveDictionary(DictDataItem, "Files");

                TemplateFormat xf = new TemplateFormat(Context);

                DictDataItem = Common.UpdateDictionary(DictDataItem, "Image", xf.ViewLinkUrl(xf.ViewItemSettingT<string>(DataItem, "Image", ""), "", Context));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "Summary", xf.ViewItemSettingT<string>(DataItem, "Summary", ""));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "Url", xf.GoUrl(DataItem));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "ReleaseDateStr", String.Format("{0} {1}{2},{3}", DataItem.ReleaseDate.ToString("MMM"), DataItem.ReleaseDate.Day, xf.GetDaySuffix(DataItem.ReleaseDate.Day), DataItem.ReleaseDate.ToString("yyyy")));

            }
            return DictDataItem;
        }



        /// <summary>
        /// GameSheets
        /// 将事件实体查询出来并填充到数据中
        /// </summary>
        /// <param name="DictDataItem"></param>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public Dictionary<String, Object> GetDictDataItemsByGameSheets(Dictionary<String, Object> DictDataItem, Int32 ItemId, BasePage Context)
        {
            var DataItem = Playngo_ClientZone_GameSheet.FindByKeyForEdit(ItemId);
            if (DataItem != null && DataItem.ID > 0)
            {
                //循环输出所有的固定项
                foreach (var Field in Playngo_ClientZone_GameSheet.Meta.Fields)
                {
                    DictDataItem.Add(Field.ColumnName, DataItem[Field.ColumnName]);
                }

                //移除累赘的字典项
                DictDataItem = Common.RemoveDictionary(DictDataItem, "Options");
                DictDataItem = Common.RemoveDictionary(DictDataItem, "Files");

                TemplateFormat xf = new TemplateFormat(Context);

                DictDataItem = Common.UpdateDictionary(DictDataItem, "Image", xf.ViewLinkUrl(xf.ViewItemSettingT<string>(DataItem, "Image", ""), "", Context));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "Summary", xf.ViewItemSettingT<string>(DataItem, "Summary", ""));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "Url", xf.GoUrl(DataItem));
                DictDataItem = Common.UpdateDictionary(DictDataItem, "ReleaseDateStr", String.Format("{0} {1}{2},{3}", DataItem.ReleaseDate.ToString("MMM"), DataItem.ReleaseDate.Day,xf. GetDaySuffix( DataItem.ReleaseDate.Day), DataItem.ReleaseDate.ToString("yyyy")));

            }
            return DictDataItem;
        }






        /// <summary>
        /// 单表联表查询
        /// </summary>
        /// <param name="Name">表名的别名</param>
        /// <param name="TableName">表名</param>
        /// <param name="IsGameGategorys">是否开启分类筛选</param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public String SqlQueryTable(String Name,  String TableName, Boolean IsGameGategorys, BasePage Context)
        {
            QueryParam qp = new QueryParam();

            //普通查询语句
            qp = CreateQueryParam(qp, Context);


            //权限筛选
            qp = CreateQueryByRoles(qp, Context);

            //区域筛选
            qp = CreateQueryByJurisdictions(qp, Context);

            if (IsGameGategorys)
            {
                //游戏分类筛选
                qp = CreateQueryByGameGategorys(qp, Context);
            }

            return String.Format(" ( Select ID,ReleaseDate,StartTime,('{0}') as [TableName] From {1} Where {2} )", Name, TableName, qp.ToSql());
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
            qp.Where.Add(new SearchParam("IncludeRoadmap", 0, SearchType.Equal));

            //开始时间
            qp.Where.Add(new SearchParam("ReleaseDate", Context.ViewSettingT<DateTime>("Roadmap_StartTime", xUserTime.UtcTime().AddDays(-30)), SearchType.GtEqual));
            //结束时间
            qp.Where.Add(new SearchParam("ReleaseDate", Context.ViewSettingT<DateTime>("Roadmap_EndTime", xUserTime.UtcTime()).AddDays(30), SearchType.LtEqual));

            //未到开始时间的不显示数据
            qp.Where.Add(new SearchParam("StartTime", xUserTime.UtcTime(), SearchType.LtEqual));

            return qp;
        }


    }
}