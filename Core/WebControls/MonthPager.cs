using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using DotNetNuke.Common;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 按月份翻页
    /// </summary>
    public class MonthPager
    {

        private BaseModule _baseModule = new BaseModule();
        public BaseModule baseModule
        {
            get
            {
                return _baseModule;
            }

            set
            {
                _baseModule = value;
            }
        }

        private EnumPageType PageType = EnumPageType.DnnURL;
        private Boolean IsParameter = false;

        public MonthPager()
        {

        }

        public MonthPager(BaseModule __baseModule, EnumPageType __PageType, Boolean __IsParameter)
        {
            baseModule = __baseModule;
            PageType = __PageType;
            IsParameter = __IsParameter;
        }

        public MonthPager(BaseModule __baseModule, EnumPageType __PageType)
            : this(__baseModule, __PageType, false)
        { }


        /// <summary>
        /// 创建HTML
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public String CreateHtml()
        {
            System.Text.StringBuilder html = new System.Text.StringBuilder();
            html.Append("<div class=\"pager\">");
            //首先需要构造上一年下一年
            //然后是构造一年中的12个月

            //当前选择的年/月
            Int32 CurrentYear = WebHelper.GetIntParam(HttpContext.Current.Request, String.Format("PageYear{0}", baseModule.ModuleId), DateTime.Now.Year);
            Int32 CurrentMonth = WebHelper.GetIntParam(HttpContext.Current.Request, String.Format("PageMonth{0}", baseModule.ModuleId), DateTime.Now.Month);

            DateTime CurrentDateTime = new DateTime(CurrentYear, CurrentMonth, 1);
 
            html.AppendFormat("<a class=\"first button\" href=\"{0}\" title=\"{2}\">{1}</a>", CreateUrl(CurrentYear-1, CurrentMonth), "<em class=\"fa fa-angle-double-left\"></em>", DateTimeTitle(CurrentYear - 1, CurrentMonth));
            html.AppendFormat("<a class=\"first button\" href=\"{0}\" title=\"{2}\">{1}</a>", CreateUrl(CurrentDateTime.AddMonths(-1).Year, CurrentDateTime.AddMonths(-1).Month), "<em class=\"fa fa-angle-left\"></em>", DateTimeTitle(CurrentDateTime.AddMonths(-1).Year, CurrentDateTime.AddMonths(-1).Month));

            for (int i = 1; i <= 12; i++)
            {
                String CurrentUrl = CreateUrl(CurrentYear,i);//当前的Url

                if (CurrentMonth == i) //如果是当前页，用粗体和红色显示
                {
                    html.AppendFormat("<span class=\"index disabled\" title=\"{1}\">{0}</span>", DateTimeMonth( i), DateTimeTitle(CurrentYear,i));
                }
                else
                {
                    html.AppendFormat("<a class=\"index button\" href=\"{0}\" title=\"{2}\">{1}</a>", CurrentUrl, DateTimeMonth( i), DateTimeTitle(CurrentYear, i));
                }

            }
 
            html.AppendFormat("<a class=\"next button\" href=\"{0}\" title=\"{2}\">{1}</a>", CreateUrl(CurrentDateTime.AddMonths(1).Year, CurrentDateTime.AddMonths(1).Month), "<em class=\"fa fa-angle-right\"></em>", DateTimeTitle(CurrentDateTime.AddMonths(1).Year, CurrentDateTime.AddMonths(1).Month));
            html.AppendFormat("<a class=\"last button\" href=\"{0}\" title=\"{2}\">{1}</a>", CreateUrl(CurrentYear + 1, CurrentMonth), "<em class=\"fa fa-angle-double-right\"></em>", DateTimeTitle(CurrentYear + 1, CurrentMonth));
            


            html.Append("</div>");
            return html.ToString();
        }

        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="_PageIndex"></param>
        /// <returns></returns>
        private String CreateUrl(Int32 Year, Int32 Month)
        {
            String PageYearString = String.Format("PageYear{0}", baseModule.ModuleId);
            String PageMonthString = String.Format("PageMonth{0}", baseModule.ModuleId);

            if (PageType == EnumPageType.Other)
                return Globals.NavigateURL("", String.Format("PageYear{0}={1}&PageMonth{0}={2}", baseModule.ModuleId, Year,Month));
            else if (PageType == EnumPageType.NormalURL)
            {
     
                String QueryString = String.Empty;

                if (IsParameter)
                {
                    List<String> Querys = new List<String>();

                    NameValueCollection QueryStrings = HttpContext.Current.Request.QueryString;

                    foreach (String Qkey in QueryStrings.AllKeys)
                    {
                        if (!String.IsNullOrEmpty(Qkey) && PageYearString != Qkey.ToLower() && PageMonthString != Qkey.ToLower())
                        {
                            QueryString += String.Format("{0}{1}={2}", QueryString == String.Empty ? "?" : "&", Qkey, QueryStrings[Qkey]);
                        }
                    }
                }

                QueryString += String.Format("{0}{1}={2}&{3}={4}", QueryString == String.Empty ? "?" : "&", PageYearString, Year, PageMonthString, Month);
                return String.Format("{0}{1}", HttpContext.Current.Request.Path, QueryString);
            }
            else
            {
         
                String QueryString = String.Empty;
                NameValueCollection QueryStrings = HttpContext.Current.Request.QueryString;
                List<String> Querys = new List<String>();

                foreach (String Qkey in QueryStrings.AllKeys)
                {
                    if (!String.IsNullOrEmpty(Qkey))
                    {
                        if (IsParameter || (("calendar,archive,categoryid,search,searchtag,token,EventID,author,ID").IndexOf(Qkey.ToLower()) >= 0))
                        {
                            if (PageYearString != Qkey.ToLower() && PageMonthString != Qkey.ToLower())
                            {
                                Querys.Add(String.Format("{0}={1}", Qkey, QueryStrings[Qkey]));
                            }
                        }
                    }
                }
                Querys.Add(String.Format("PageYear{0}={1}&PageMonth{0}={2}", baseModule.ModuleId, Year, Month));
                return Globals.NavigateURL("", Querys.ToArray());

            }
        }

        /// <summary>
        /// 日期提示
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public String DateTimeTitle(Int32 Year, Int32 Month)
        {
            return new DateTime(Year, Month, 1).ToString("MMMM,yyyy");
        }

        public String DateTimeMonth(Int32 Month)
        {
            //String[] Months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec" };


            DateTime d = new DateTime(2015, Month, 1);
            return d.ToString("MMM");

        }

 



    }
}