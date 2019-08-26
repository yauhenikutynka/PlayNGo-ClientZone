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
    public class ServiceJSONGameSheets : BaseService, iService
    {
        public ServiceJSONGameSheets()
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
                return "GameSheets JSON";
            }
        }

        public void Execute(BasePage Context)
        {

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            Dictionary<String, Object> jsonPictures = new Dictionary<string, Object>();


            TemplateFormat xf = new TemplateFormat();
            QueryParam qp = new QueryParam();
 
 
            qp.PageIndex = WebHelper.GetIntParam(Context.Request, "PageIndex", 1);
            qp.PageSize = WebHelper.GetIntParam(Context.Request, "PageSize", xf.ViewSettingT<Int32>("General.GameSheets.Pagings", 12));

            //排序的规则
            Int32 Sort = WebHelper.GetIntParam(Context.Request, "Sort", 0);
            if (Sort == (Int32)EnumSortQueryByGame.GameName_DESC)
            {
                qp.Orderfld = Playngo_ClientZone_GameSheet._.Title;
                qp.OrderType = 1;
            }
            else if (Sort == (Int32)EnumSortQueryByGame.GameName_ASC)
            {
                qp.Orderfld = Playngo_ClientZone_GameSheet._.Title;
                qp.OrderType = 0;
            }
            else if (Sort == (Int32)EnumSortQueryByGame.GameID_DESC)
            {
                qp.Orderfld = Playngo_ClientZone_GameSheet._.GameID;
                qp.OrderType = 1;
            }
            else if (Sort == (Int32)EnumSortQueryByGame.GameID_ASC)
            {
                qp.Orderfld = Playngo_ClientZone_GameSheet._.GameID;
                qp.OrderType = 0;
            }
            else if (Sort == (Int32)EnumSortQueryByGame.ReleaseDate_DESC)
            {
                qp.Orderfld = Playngo_ClientZone_GameSheet._.ReleaseDate;
                qp.OrderType = 1;
            }
            else if (Sort == (Int32)EnumSortQueryByGame.ReleaseDate_ASC)
            {
                qp.Orderfld = Playngo_ClientZone_GameSheet._.ReleaseDate;
                qp.OrderType = 0;
            }
            else
            {
                qp.Orderfld = Playngo_ClientZone_GameSheet._.ID;
                qp.OrderType = 1;
            }


            //查询语句
            qp = CreateQueryParam(qp, Context);

            //权限筛选
            qp = CreateQueryByRoles(qp, Context);

            //区域筛选
            qp = CreateQueryByJurisdictions(qp, Context);

            //游戏分类筛选
            qp = CreateQueryByGameGategorys(qp, Context);



            int RecordCount = 0;
            List<Playngo_ClientZone_GameSheet> GameSheetList = Playngo_ClientZone_GameSheet.FindAll(qp, out RecordCount);


            //配置值
            XmlFormat xmlFormat = new XmlFormat(Context.Server.MapPath(String.Format("{0}Resource/xml/Config.Setting.GameSheets.xml", Context.ModulePath)));
            var XmlItemSetting = xmlFormat.ToList<SettingEntity>();


            List<Dictionary<String, Object>> DictFiles = new List<Dictionary<string, object>>();
            foreach (var GameSheetItem in GameSheetList)
            {
                int index = GameSheetList.IndexOf(GameSheetItem); //index 为索引值

                Dictionary<String, Object> jsonDict = new Dictionary<String, Object>();
              


                //循环输出所有的固定项
                foreach (var Field in Playngo_ClientZone_GameSheet.Meta.Fields)
                {
                    jsonDict.Add(Field.ColumnName, GameSheetItem[Field.ColumnName]);
                }

              
                if (XmlItemSetting != null && XmlItemSetting.Count > 0)
                {
                    var ItemSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(GameSheetItem.Options);
                    foreach (var ItemSetting in XmlItemSetting)
                    {
                        jsonDict = Common.UpdateDictionary(jsonDict, ItemSetting.Name, xf.ViewItemSetting(GameSheetItem, ItemSetting.Name, ItemSetting.DefaultValue));
                    }
                }
 

                jsonDict = Common.UpdateDictionary(jsonDict, "ReleaseDateString", GameSheetItem.ReleaseDate.ToShortDateString());
                jsonDict = Common.UpdateDictionary(jsonDict, "Image", xf.ViewLinkUrl(xf.ViewItemSettingT<string>(GameSheetItem, "Image", ""), "", Context));
 
                jsonDict = Common.UpdateDictionary(jsonDict, "Url", xf.GoUrl(GameSheetItem));
                //未来日期出现Coming Soon
                jsonDict = Common.UpdateDictionary(jsonDict, "ComingSoonDisplay", GameSheetItem.ReleaseDate > xUserTime.LocalTime());


                Int32 NotifyStatus = (Int32)EnumNotificationStatus.None;
                if (GameSheetItem.NotifyInclude == 1 )//&& GameSheetItem.StartTime >= xUserTime.LocalTime().AddDays(-xf.ViewSettingT<Int32>("General.ExpiryTimeNotification", 7)))
                {
                    NotifyStatus = GameSheetItem.NotifyStatus;
                }


                jsonDict = Common.UpdateDictionary(jsonDict, "NotificationStatus", EnumHelper.GetEnumTextVal(NotifyStatus, typeof(EnumNotificationStatus)));
                jsonDict = Common.UpdateDictionary(jsonDict, "NotificationStatusClass", EnumHelper.GetEnumTextVal(NotifyStatus, typeof(EnumNotificationStatus)).ToLower());


                DictFiles.Add(jsonDict);
            }

            jsonPictures.Add("data", DictFiles);
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
            qp.Where.Add(new SearchParam(Playngo_ClientZone_GameSheet._.Status, (Int32)EnumStatus.Published, SearchType.Equal));
            qp.Where.Add(new SearchParam(Playngo_ClientZone_GameSheet._.PortalId, Context.PortalId, SearchType.Equal));
            //未到开始时间的不显示数据
            qp.Where.Add(new SearchParam(Playngo_ClientZone_GameSheet._.StartTime, xUserTime.UtcTime(), SearchType.LtEqual));

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