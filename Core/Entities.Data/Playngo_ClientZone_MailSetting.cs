using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 邮件设置
    /// </summary>
    [Serializable]
    [DataObject]
    [Description("邮件设置")]
    [BindTable("Playngo_ClientZone_MailSetting", Description = "邮件设置", ConnName = "SiteSqlServer")]
    public partial class Playngo_ClientZone_MailSetting : Entity<Playngo_ClientZone_MailSetting>
    {
        #region 属性
        private Int32 _ID;
        /// <summary>
        /// 邮件编号
        /// </summary>
        [Description("邮件编号")]
        [DataObjectField(true, true, false, 10)]
        [BindColumn("ID", Description = "邮件编号", DefaultValue = "", Order = 1)]
        public Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChange("ID", value)) _ID = value; }
        }

        private String _Name;
        /// <summary>
        /// 模板名称
        /// </summary>
        [Description("模板名称")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("Name", Description = "模板名称", DefaultValue = "", Order = 2)]
        public String Name
        {
            get { return _Name; }
            set { if (OnPropertyChange("Name", value)) _Name = value; }
        }

        private String _language;
        /// <summary>
        /// 邮件语言
        /// </summary>
        [Description("邮件语言")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("language", Description = "邮件语言", DefaultValue = "", Order = 3)]
        public String language
        {
            get { return _language; }
            set { if (OnPropertyChange("language", value)) _language = value; }
        }

        private String _MailTo;
        /// <summary>
        /// 发送给谁
        /// </summary>
        [Description("发送给谁")]
        [DataObjectField(false, false, false, 100)]
        [BindColumn("MailTo", Description = "发送给谁", DefaultValue = "", Order = 4)]
        public String MailTo
        {
            get { return _MailTo; }
            set { if (OnPropertyChange("MailTo", value)) _MailTo = value; }
        }

        private String _MailCC;
        /// <summary>
        /// 抄送给谁
        /// </summary>
        [Description("抄送给谁")]
        [DataObjectField(false, false, false, 100)]
        [BindColumn("MailCC", Description = "抄送给谁", DefaultValue = "", Order = 5)]
        public String MailCC
        {
            get { return _MailCC; }
            set { if (OnPropertyChange("MailCC", value)) _MailCC = value; }
        }

        private String _MailSubject;
        /// <summary>
        /// 邮件标题
        /// </summary>
        [Description("邮件标题")]
        [DataObjectField(false, false, false, 200)]
        [BindColumn("MailSubject", Description = "邮件标题", DefaultValue = "", Order = 6)]
        public String MailSubject
        {
            get { return _MailSubject; }
            set { if (OnPropertyChange("MailSubject", value)) _MailSubject = value; }
        }

        private String _MailBody;
        /// <summary>
        /// 邮件内容
        /// </summary>
        [Description("邮件内容")]
        [DataObjectField(false, false, false, 1073741823)]
        [BindColumn("MailBody", Description = "邮件内容", DefaultValue = "", Order = 7)]
        public String MailBody
        {
            get { return _MailBody; }
            set { if (OnPropertyChange("MailBody", value)) _MailBody = value; }
        }

        private Int32 _MailTime;
        /// <summary>
        /// 发送时间
        /// </summary>
        [Description("发送时间")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn("MailTime", Description = "发送时间", DefaultValue = "", Order = 8)]
        public Int32 MailTime
        {
            get { return _MailTime; }
            set { if (OnPropertyChange("MailTime", value)) _MailTime = value; }
        }

        private Int32 _MailType;
        /// <summary>
        /// 邮件类型(管理员提醒/注册活动提醒/活动时间到达提醒)
        /// </summary>
        [Description("邮件类型(管理员提醒/注册活动提醒/活动时间到达提醒)")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn("MailType", Description = "邮件类型(管理员提醒/注册活动提醒/活动时间到达提醒)", DefaultValue = "", Order = 9)]
        public Int32 MailType
        {
            get { return _MailType; }
            set { if (OnPropertyChange("MailType", value)) _MailType = value; }
        }

        private DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        [DataObjectField(false, false, false, 23)]
        [BindColumn("CreateTime", Description = "创建时间", DefaultValue = "", Order = 10)]
        public DateTime CreateTime
        {
            get { return xUserTime.LocalTime(_CreateTime); }
            set { if (OnPropertyChange("CreateTime", value)) _CreateTime = xUserTime.ServerTime(value); }
        }

        private Int32 _CreateUser;
        /// <summary>
        /// 创建用户
        /// </summary>
        [Description("创建用户")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("CreateUser", Description = "创建用户", DefaultValue = "", Order = 11)]
        public Int32 CreateUser
        {
            get { return _CreateUser; }
            set { if (OnPropertyChange("CreateUser", value)) _CreateUser = value; }
        }

        private Int32 _ModuleId;
        /// <summary>
        /// 模块编号
        /// </summary>
        [Description("模块编号")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("ModuleId", Description = "模块编号", DefaultValue = "", Order = 12)]
        public Int32 ModuleId
        {
            get { return _ModuleId; }
            set { if (OnPropertyChange("ModuleId", value)) _ModuleId = value; }
        }

        private Int32 _PortalId;
        /// <summary>
        /// 站点编号
        /// </summary>
        [Description("站点编号")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("PortalId", Description = "站点编号", DefaultValue = "", Order = 13)]
        public Int32 PortalId
        {
            get { return _PortalId; }
            set { if (OnPropertyChange("PortalId", value)) _PortalId = value; }
        }

        private Int32 _TabID;
        /// <summary>
        /// 页面编号
        /// </summary>
        [Description("页面编号")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("TabID", Description = "页面编号", DefaultValue = "", Order = 14)]
        public Int32 TabID
        {
            get { return _TabID; }
            set { if (OnPropertyChange("TabID", value)) _TabID = value; }
        }

        private String _ModulePath;
        /// <summary>
        /// 模块路径
        /// </summary>
        [Description("模块路径")]
        [DataObjectField(false, false, false, 100)]
        [BindColumn("ModulePath", Description = "模块路径", DefaultValue = "", Order = 15)]
        public String ModulePath
        {
            get { return _ModulePath; }
            set { if (OnPropertyChange("ModulePath", value)) _ModulePath = value; }
        }

        private Int32 _Status;
        /// <summary>
        /// 事件状态(草稿、正式、锁定、删除)
        /// </summary>
        [Description("事件状态(草稿、正式、锁定、删除)")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn("Status", Description = "事件状态(草稿、正式、锁定、删除)", DefaultValue = "", Order = 16)]
        public Int32 Status
        {
            get { return _Status; }
            set { if (OnPropertyChange("Status", value)) _Status = value; }
        }

        private DateTime _LastTime = xUserTime.UtcTime();
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Description("最后更新时间")]
        [DataObjectField(false, false, false, 23)]
        [BindColumn("LastTime", Description = "最后更新时间", DefaultValue = "", Order = 17)]
        public DateTime LastTime
        {
            get { return _LastTime; }
            set { if (OnPropertyChange("LastTime", value)) _LastTime = value; }
        }

        private Int32 _LastUser;
        /// <summary>
        /// 最后更新用户
        /// </summary>
        [Description("最后更新用户")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("LastUser", Description = "最后更新用户", DefaultValue = "", Order = 18)]
        public Int32 LastUser
        {
            get { return _LastUser; }
            set { if (OnPropertyChange("LastUser", value)) _LastUser = value; }
        }

        private String _LastIP;
        /// <summary>
        /// 最后更新IP
        /// </summary>
        [Description("最后更新IP")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("LastIP", Description = "最后更新IP", DefaultValue = "", Order = 19)]
        public String LastIP
        {
            get { return _LastIP; }
            set { if (OnPropertyChange("LastIP", value)) _LastIP = value; }
        }
        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case "ID": return _ID;
                    case "Name": return _Name;
                    case "language": return _language;
                    case "MailTo": return _MailTo;
                    case "MailCC": return _MailCC;
                    case "MailSubject": return _MailSubject;
                    case "MailBody": return _MailBody;
                    case "MailTime": return _MailTime;
                    case "MailType": return _MailType;
                    case "CreateTime": return _CreateTime;
                    case "CreateUser": return _CreateUser;
                    case "ModuleId": return _ModuleId;
                    case "PortalId": return _PortalId;
                    case "TabID": return _TabID;
                    case "ModulePath": return _ModulePath;
                    case "Status": return _Status;
                    case "LastTime": return _LastTime;
                    case "LastUser": return _LastUser;
                    case "LastIP": return _LastIP;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "ID": _ID = Convert.ToInt32(value); break;
                    case "Name": _Name = Convert.ToString(value); break;
                    case "language": _language = Convert.ToString(value); break;
                    case "MailTo": _MailTo = Convert.ToString(value); break;
                    case "MailCC": _MailCC = Convert.ToString(value); break;
                    case "MailSubject": _MailSubject = Convert.ToString(value); break;
                    case "MailBody": _MailBody = Convert.ToString(value); break;
                    case "MailTime": _MailTime = Convert.ToInt32(value); break;
                    case "MailType": _MailType = Convert.ToInt32(value); break;
                    case "CreateTime": _CreateTime = Convert.ToDateTime(value); break;
                    case "CreateUser": _CreateUser = Convert.ToInt32(value); break;
                    case "ModuleId": _ModuleId = Convert.ToInt32(value); break;
                    case "PortalId": _PortalId = Convert.ToInt32(value); break;
                    case "TabID": _TabID = Convert.ToInt32(value); break;
                    case "ModulePath": _ModulePath = Convert.ToString(value); break;
                    case "Status": _Status = Convert.ToInt32(value); break;
                    case "LastTime": _LastTime = Convert.ToDateTime(value); break;
                    case "LastUser": _LastUser = Convert.ToInt32(value); break;
                    case "LastIP": _LastIP = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>
        /// 取得邮件设置字段名的快捷方式
        /// </summary>
        public class _
        {
            ///<summary>
            /// 邮件编号
            ///</summary>
            public const String ID = "ID";

            ///<summary>
            /// 模板名称
            ///</summary>
            public const String Name = "Name";

            ///<summary>
            /// 邮件语言
            ///</summary>
            public const String language = "language";

            ///<summary>
            /// 发送给谁
            ///</summary>
            public const String MailTo = "MailTo";

            ///<summary>
            /// 抄送给谁
            ///</summary>
            public const String MailCC = "MailCC";

            ///<summary>
            /// 邮件标题
            ///</summary>
            public const String MailSubject = "MailSubject";

            ///<summary>
            /// 邮件内容
            ///</summary>
            public const String MailBody = "MailBody";

            ///<summary>
            /// 发送时间
            ///</summary>
            public const String MailTime = "MailTime";

            ///<summary>
            /// 邮件类型(管理员提醒/注册活动提醒/活动时间到达提醒)
            ///</summary>
            public const String MailType = "MailType";

            ///<summary>
            /// 创建时间
            ///</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>
            /// 创建用户
            ///</summary>
            public const String CreateUser = "CreateUser";

            ///<summary>
            /// 模块编号
            ///</summary>
            public const String ModuleId = "ModuleId";

            ///<summary>
            /// 站点编号
            ///</summary>
            public const String PortalId = "PortalId";

            ///<summary>
            /// 页面编号
            ///</summary>
            public const String TabID = "TabID";

            ///<summary>
            /// 模块路径
            ///</summary>
            public const String ModulePath = "ModulePath";

            ///<summary>
            /// 事件状态(草稿、正式、锁定、删除)
            ///</summary>
            public const String Status = "Status";

            ///<summary>
            /// 最后更新时间
            ///</summary>
            public const String LastTime = "LastTime";

            ///<summary>
            /// 最后更新用户
            ///</summary>
            public const String LastUser = "LastUser";

            ///<summary>
            /// 最后更新IP
            ///</summary>
            public const String LastIP = "LastIP";
        }
        #endregion
    }
}