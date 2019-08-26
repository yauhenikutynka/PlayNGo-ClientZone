using System;
using System.Collections.Generic;
using System.Web;
using System.Globalization;
using System.Threading;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 扩展日历,主要计算波斯日历
    /// </summary>
    public class xCalendar
    {
        #region "转换波斯日历"
        /// <summary>
        /// 转换日期到波斯时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public  DateTime GetDateOfPersian(DateTime date)
        {

            String CurrentName = Thread.CurrentThread.CurrentCulture.Name;
            String CurrentCalendar = Thread.CurrentThread.CurrentCulture.Calendar.ToString();

            
            //如果客户是波斯日历需要开启这里
            if (CurrentName == "fa-IR" && (CurrentCalendar == "DotNetNuke.PersianLibrary.DNNPersianDate" || CurrentCalendar == "System.Globalization.PersianCalendar"  ))
            {
                System.Globalization.PersianCalendar jc = new System.Globalization.PersianCalendar();
                return jc.ToDateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
            }

            //如果客户是阿拉伯日历需要开启这里
           if (CurrentName == "ar-SA")
            {
                System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar(System.Globalization.GregorianCalendarTypes.Arabic);
                return gc.ToDateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
            }

             


            return date;
        }
        /// <summary>
        /// 转换日期到波斯时间（获取当前时间）
        /// </summary>
        /// <returns></returns>
        public  DateTime GetDateOfPersian()
        {
            return GetDateOfPersian(DateTime.UtcNow);
        }

        #endregion

    

        /// <summary>
        /// 处理波斯历
        /// </summary>
        /// <param name="Time"></param>
        public static DateTime Process(DateTime Time)
        {
            xCalendar x = new xCalendar();
            return x.GetDateOfPersian(Time);
        }

 
    }


     
}