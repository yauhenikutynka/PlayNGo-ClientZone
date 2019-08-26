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
    public class ServiceJSONEvents : BaseService, iService
    {
        public ServiceJSONEvents()
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
                return "Events JSON";
            }
        }

        public void Execute(BasePage Context)
        {
            TemplateFormat xf = new TemplateFormat(Context);

            QueryParam qp = new QueryParam();
            
            qp.PageIndex = WebHelper.GetIntParam(Context.Request, "PageIndex", 1);
            qp.PageSize = WebHelper.GetIntParam(Context.Request, "PageSize", xf.ViewSettingT<Int32>("General.Events.Pagings", 12));

            //排序的规则
            Int32 Sort = WebHelper.GetIntParam(Context.Request, "Sort", 0);
            if (Sort == (Int32)EnumSortQueryByEvent.EventName_DESC)
            {
                qp.Orderfld = Playngo_ClientZone_Event._.Title;
                qp.OrderType = 1;
            }
            else if (Sort == (Int32)EnumSortQueryByEvent.EventName_ASC)
            {
                qp.Orderfld = Playngo_ClientZone_Event._.Title;
                qp.OrderType = 0;
            }
            else if (Sort == (Int32)EnumSortQueryByEvent.EventDate_DESC)
            {
                qp.Orderfld = Playngo_ClientZone_Event._.ReleaseDate;
                qp.OrderType = 1;
            }
            else if (Sort == (Int32)EnumSortQueryByEvent.EventDate_ASC)
            {
                qp.Orderfld = Playngo_ClientZone_Event._.ReleaseDate;
                qp.OrderType = 0;
            }
            else
            {
                qp.Orderfld = Playngo_ClientZone_Event._.ID;
                qp.OrderType = 1;
            }

            //查询语句
            qp = CreateQueryParam(qp, Context);
 
            //权限筛选
            qp = CreateQueryByRoles(qp, Context);

            //区域筛选
            qp = CreateQueryByJurisdictions(qp, Context);
            


            int RecordCount = 0;
            List<Playngo_ClientZone_Event> EventList = Playngo_ClientZone_Event.FindAll(qp, out RecordCount);

            Dictionary<String, Object> jsonPictures = new Dictionary<string, Object>();


            //配置值
            XmlFormat xmlFormat = new XmlFormat(Context.Server.MapPath(String.Format("{0}Resource/xml/Config.Setting.Event.xml", Context.ModulePath)));
            var XmlItemSetting = xmlFormat.ToList<SettingEntity>();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            List<Dictionary<String, Object>> DictFiles = new List<Dictionary<string, object>>();
            foreach (var EventItem in EventList)
            {
                int index = EventList.IndexOf(EventItem); //index 为索引值

                Dictionary<String, Object> jsonDict = new Dictionary<String, Object>();
              


                //循环输出所有的固定项
                foreach (var Field in Playngo_ClientZone_Event.Meta.Fields)
                {
                    jsonDict.Add(Field.ColumnName, EventItem[Field.ColumnName]);
                }


              
                if (XmlItemSetting != null && XmlItemSetting.Count > 0)
                {
                    var ItemSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(EventItem.Options);
                    foreach (var ItemSetting in XmlItemSetting)
                    {
                        jsonDict = Common.UpdateDictionary(jsonDict, ItemSetting.Name, xf.ViewItemSetting(EventItem, ItemSetting.Name, ItemSetting.DefaultValue));
                    }
                }


                jsonDict = Common.UpdateDictionary(jsonDict, "ContentTextDecode", HttpUtility.HtmlDecode( EventItem.ContentText));
                jsonDict = Common.UpdateDictionary(jsonDict, "ReleaseDateString", EventItem.ReleaseDate.ToShortDateString());
                jsonDict = Common.UpdateDictionary(jsonDict, "Image",xf.ViewLinkUrl(xf.ViewItemSettingT<string>(EventItem, "Image", ""),"", Context));

                //未来日期出现Coming Soon
                jsonDict = Common.UpdateDictionary(jsonDict, "ComingSoonDisplay", EventItem.ReleaseDate > xUserTime.LocalTime());
 

                jsonDict = Common.UpdateDictionary(jsonDict, "Url",xf.GoUrl(EventItem));


                Int32 NotifyStatus = (Int32)EnumNotificationStatus.None;
                if (EventItem.NotifyInclude == 1 )//&& EventItem.StartTime >= xUserTime.LocalTime().AddDays(-xf.ViewSettingT<Int32>("General.ExpiryTimeNotification", 7)))
                {
                    NotifyStatus = EventItem.NotifyStatus;
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
            qp.Where.Add(new SearchParam(Playngo_ClientZone_Event._.Status, (Int32)EnumStatus.Published, SearchType.Equal));
            qp.Where.Add(new SearchParam(Playngo_ClientZone_Event._.PortalId, Context.PortalId, SearchType.Equal));

            //未到开始时间的不显示数据
            qp.Where.Add(new SearchParam(Playngo_ClientZone_Event._.StartTime, xUserTime.UtcTime(), SearchType.LtEqual));

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