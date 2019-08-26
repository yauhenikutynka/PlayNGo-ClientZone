using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 提醒角标
    /// </summary>
    public class ServiceJSONNotifyBadges : BaseService, iService
    {
        public ServiceJSONNotifyBadges()
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
                return "NotifyBadges JSON";
            }
        }

        public void Execute(BasePage Context)
        {
            TemplateFormat xf = new TemplateFormat(Context);

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            Dictionary<String, Object> DictFiles = new Dictionary<string, object>();





            //创建 DownloadFile 查询的浅度拷贝
            var DownloadFileQP = SqlQueryTable(xf, Context);
            //Cookie中保存的最后点击事件
            var ClickTime = GetCookieClickTime(EnumDisplayModuleType.Downloads.ToString());
            if(ClickTime != DateTime.MinValue)
            {
                DownloadFileQP.Where.Add(new SearchParam("StartTime", ClickTime, SearchType.GtEqual));
            }
           
            DictFiles.Add("DownloadFileCount", Playngo_ClientZone_DownloadFile.FindCount(DownloadFileQP));


            //创建 Campaign 查询的浅度拷贝
            var CampaignQP = SqlQueryTable(xf, Context);
            //Cookie中保存的最后点击事件
            ClickTime = GetCookieClickTime(EnumDisplayModuleType.Campaigns.ToString());
            if (ClickTime != DateTime.MinValue)
            {
                CampaignQP.Where.Add(new SearchParam("StartTime", ClickTime, SearchType.GtEqual));
            }
            DictFiles.Add("CampaignCount", Playngo_ClientZone_Campaign.FindCount(CampaignQP));



            //创建 Event 查询的浅度拷贝
            var EventQP = SqlQueryTable(xf, Context);
            //Cookie中保存的最后点击事件
            ClickTime = GetCookieClickTime(EnumDisplayModuleType.Events.ToString());
            if (ClickTime != DateTime.MinValue)
            {
                EventQP.Where.Add(new SearchParam("StartTime", ClickTime, SearchType.GtEqual));
            }
            DictFiles.Add("EventCount", Playngo_ClientZone_Event.FindCount(EventQP));



            //创建 GameSheet 查询的浅度拷贝
            var GameSheetQP = SqlQueryTable(xf, Context);
            //Cookie中保存的最后点击事件
            ClickTime = GetCookieClickTime(EnumDisplayModuleType.GameSheets.ToString());
            if (ClickTime != DateTime.MinValue)
            {
                GameSheetQP.Where.Add(new SearchParam("StartTime", ClickTime, SearchType.GtEqual));
            }
            DictFiles.Add("GameSheetCount", Playngo_ClientZone_GameSheet.FindCount(GameSheetQP));




            ResponseString = jsSerializer.Serialize(DictFiles);
        }

        /// <summary>
        /// 获取Cookie中的最后点击时间
        /// </summary>
        /// <param name="Action">动作界面</param>
        /// <returns>最后点击的时间,否则返回最小时间</returns>
        public DateTime GetCookieClickTime(String Action)
        {

            var ClickTimeStr = HttpUtility.UrlDecode( CookieHelper.GetCookieValue(String.Format("{0}-ClickTime", Action)));
            if (!String.IsNullOrEmpty(ClickTimeStr))
            {
                DateTime ClickTime = xUserTime.LocalTime();
                string[] expectedFormats = { "G", "g", "f", "F" };
                if (DateTime.TryParseExact(ClickTimeStr, "MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out ClickTime))
                {
                    return ClickTime;
                }
            }
            return DateTime.MinValue;
        }





        public QueryParam SqlQueryTable(TemplateFormat xf,BasePage Context)
        {
            //4组数据的角标(根据Cookie传过来的时间计算)
            QueryParam qp = new QueryParam();
            qp.Where.Add(new SearchParam("Status", (Int32)EnumStatus.Published, SearchType.Equal));
            qp.Where.Add(new SearchParam("PortalId", Context.PortalId, SearchType.Equal));
            qp.Where.Add(new SearchParam("NotifyInclude", 1, SearchType.Equal));
            //qp.Where.Add(new SearchParam("StartTime", xUserTime.UtcTime().AddDays(-xf.ViewSettingT<Int32>("General.ExpiryTimeNotification", 7)), SearchType.GtEqual));

            //未到开始时间的不显示数据
            qp.Where.Add(new SearchParam("StartTime", xUserTime.UtcTime(), SearchType.LtEqual));


            //权限筛选
            qp = CreateQueryByRoles(qp, Context);


            return qp;
        }






    }
}