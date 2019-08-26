using System;
using System.Collections.Generic;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 模版的配置XML
    /// </summary>
    [XmlEntityAttributes("Settings//Setting")]
    public class TemplateDB
    {

        private String _Name = String.Empty;
        /// <summary>
        /// 效果名称
        /// </summary>
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }




        private String _Alias = String.Empty;
        /// <summary>
        /// 别名
        /// </summary>
        public String Alias
        {
            get { return !String.IsNullOrEmpty(_Alias) ? _Alias : Name; }
            set { _Alias = value; }
        }


 


        private String _Description = String.Empty;
        /// <summary>
        /// 效果描述
        /// </summary>
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


        private String _Version = String.Empty;
        /// <summary>
        /// 版本号
        /// </summary>
        public String Version
        {
            get { return _Version; }
            set { _Version = value; }
        }


        private String _Thumbnails = String.Empty;
        /// <summary>
        /// 缩略图
        /// </summary>
        public String Thumbnails
        {
            get { return _Thumbnails; }
            set { _Thumbnails = value; }
        }


        private String _TemplateScript = String.Empty;
        /// <summary>
        /// 效果附带脚本
        /// </summary>
        public String TemplateScript
        {
            get { return _TemplateScript; }
            set { _TemplateScript = value; }
        }


        private String _GlobalScript = String.Empty;
        /// <summary>
        /// 全局附带脚本
        /// </summary>
        public String GlobalScript
        {
            get { return _GlobalScript; }
            set { _GlobalScript = value; }
        }


        private String _DemoUrl = String.Empty;
        /// <summary>
        /// 演示地址
        /// </summary>
        public String DemoUrl
        {
            get { return _DemoUrl; }
            set { _DemoUrl = value; }
        }

        private Int32 _Sort = 1;
        /// <summary>
        /// 排序
        /// </summary>
        public Int32 Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }


        private Int32 _TemplateAttribute = (Int32)EnumTemplateAttribute.DashBoard;
        /// <summary>
        /// 模块属性(仪表盘/通用/列表/详情)
        /// </summary>
        public Int32 TemplateAttribute
        {
            get { return _TemplateAttribute; }
            set { _TemplateAttribute = value; }
        }



        private Boolean _Responsive = false;
        /// <summary>
        /// 是否响应式
        /// </summary>
        public Boolean Responsive
        {
            get { return _Responsive; }
            set { _Responsive = value; }
        }

        private Boolean _GoogleMap = false;
        /// <summary>
        /// 是否支持谷歌地图
        /// </summary>
        public Boolean GoogleMap
        {
            get { return _GoogleMap; }
            set { _GoogleMap = value; }
        }


        private Boolean _Pager = false;
        /// <summary>
        /// 是否带有翻页
        /// </summary>
        public Boolean Pager
        {
            get { return _Pager; }
            set { _Pager = value; }
        }


        private Boolean _Groups = false;
        /// <summary>
        /// 是否分组
        /// </summary>
        public Boolean Groups
        {
            get { return _Groups; }
            set { _Groups = value; }
        }

        private Boolean _Ajax = false;
        /// <summary>
        /// 是否支持Ajax
        /// </summary>
        public Boolean Ajax
        {
            get { return _Ajax; }
            set { _Ajax = value; }
        }


        private Boolean _DataList = true;
        /// <summary>
        /// 第一屏列表时候需要读取数据
        /// (默认是需要读取的,当有ajax时，某些效果不需要第一屏打印数据)
        /// </summary>
        public Boolean DataList
        {
            get { return _DataList; }
            set { _DataList = value; }
        }

        private String _Filter = String.Empty;
        /// <summary>
        /// 数据筛选
        /// </summary>
        public String Filter
        {
            get { return _Filter; }
            set { _Filter = value; }
        }
        
 
    }
}