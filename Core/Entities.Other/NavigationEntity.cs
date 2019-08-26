using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playngo.Modules.ClientZone 
{
    /// <summary>
    /// 列表导航类的实体
    /// </summary>
    public class NavigationEntity
    {
        
        private Boolean _IsView = false;
        /// <summary>
        /// 是否出现导航
        /// </summary>
        public Boolean IsView
        {
            get { return _IsView; }
            set { _IsView = value; }
        }
      
        private String _Title = String.Empty;
        /// <summary>
        /// 导航的标题
        /// </summary>
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
       
        private String _Content = String.Empty;
        /// <summary>
        /// 导航的内容
        /// </summary>
        public String Content
        {
            get { return _Content; }
            set { _Content = value; }
        }


        private Int32 _AuthorID = 0;
        /// <summary>
        /// 作者编号
        /// 按作者搜索时才出现
        /// </summary>
        public Int32 AuthorID
        {
            get { return _AuthorID; }
            set { _AuthorID = value; }
        }




    }
}