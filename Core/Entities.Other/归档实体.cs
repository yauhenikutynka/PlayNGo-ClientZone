using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 归档实体
    /// </summary>
    [Serializable]
    public class Playngo_ClientZone_Archive
    {

        private Int32 _Year = DateTime.Now.Year;
        /// <summary>
        /// 年
        /// </summary>
        public Int32 Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        private Int32 _Month = DateTime.Now.Month;
        /// <summary>
        /// 月
        /// </summary>
        public Int32 Month
        {
            get { return _Month; }
            set { _Month = value; }
        }

        private Int32 _QuoteCount = 0;
        /// <summary>
        /// 引用数量
        /// </summary>
        public Int32 QuoteCount
        {
            get { return _QuoteCount; }
            set { _QuoteCount = value; }
        }



    }
}