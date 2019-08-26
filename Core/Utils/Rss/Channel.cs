using System;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// channel 
    /// </summary>
    [Serializable()]
    public class Channel
    {
        private string _title = "Playngo.ClientZone";
        private string _link = "";
        private string _description;
        private ItemCollection items = new ItemCollection();

        #region 属性
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value.ToString(); }
        }
        /// <summary>
        /// 链接
        /// </summary>
        public string link
        {
            get { return _link; }
            set { _link = value.ToString(); }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string description
        {
            get { return _description; }
            set { _description = value.ToString(); }
        }
        public ItemCollection Items
        {
            get { return items; }
        }
        #endregion

        public Channel() { }


    }//
}//