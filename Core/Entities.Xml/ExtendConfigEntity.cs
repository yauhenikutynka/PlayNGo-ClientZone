using System;
using System.Collections.Generic;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 效果展示数据
    /// </summary>
    [XmlEntityAttributes("Playngo_ClientZone//Config")]
    public class ExtendConfigDB
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


        private String _Icon = String.Empty;
        /// <summary>
        /// 图标
        /// </summary>
        public String Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }
         

 
    }
}