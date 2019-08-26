using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// RFC-822 日期转换类
    /// 客户要求定制Ticket 15690 RFC-822   2015.3.13
    /// </summary>
    public class RFC_822
    {


        public static String DateToRFC822(DateTime DateNow)
        {
         
            var dWeek = new ArrayList(){"Sun","Mon","Tue","Wes","Thu","Fri","Sat"};
            var dMonth=new String[]{"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};

            var strZone = "+0800";
 
            return String.Format("{1}, {0:dd} {2} {0:yyyy} {0:HH:mm:ss} GMT"
                ,DateNow
                ,dWeek[(Int32)DateNow.DayOfWeek] 
                ,dMonth[DateNow.Month - 1]);

        }

 


    }
}