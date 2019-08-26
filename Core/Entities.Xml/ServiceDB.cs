using System;
using System.Collections.Generic;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 调用服务的配置
    /// </summary>
    [XmlEntityAttributes("Services//Service")]
    public class ServiceDB
    {

        private String _Token = String.Empty;
        /// <summary>
        /// 服务标记
        /// </summary>
        public String Token
        {
            get { return _Token; }
            set { _Token = value; }
        }


        private String _Name = String.Empty;
        /// <summary>
        /// 服务名称
        /// </summary>
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }




        private String _assemblyName = String.Empty;
        /// <summary>
        /// 效果描述
        /// </summary>
        public String assemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; }
        }


        private String _typeName = String.Empty;
        /// <summary>
        /// 版本号
        /// </summary>
        public String typeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }


        private Boolean _Validate = false;
        /// <summary>
        /// 是否需要模块编辑权限验证
        /// </summary>
        public Boolean Validate
        {
            get { return _Validate; }
            set { _Validate = value; }
        }


        private Boolean _Login = false;
        /// <summary>
        /// 是否登录
        /// </summary>
        public Boolean Login
        {
            get { return _Login; }
            set { _Login = value; }
        }




    }
}