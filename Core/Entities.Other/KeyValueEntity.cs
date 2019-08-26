using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel;

namespace Playngo.Modules.ClientZone
{

    /// <summary>
    /// 键值对实体
    /// </summary>
    [Serializable]
    [DataObject]
    public class KeyValueEntity
    {

        private String _Key = String.Empty;
        /// <summary>
        /// 键
        /// </summary>
        public String Key
        {
            get { return _Key; }
            set { _Key = value; }
        }


        private object _Value = String.Empty;
        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public KeyValueEntity()
        { 
        }


        public KeyValueEntity(String __Key, object __Value)
        {
            _Key = __Key;
            _Value = __Value;
        }

    }
}