using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 下载关联
    /// </summary>
    [Serializable]
    [DataObject]
    [Description("下载关联")]
    [BindTable("Playngo_ClientZone_DownloadRelation", Description = "下载关联", ConnName = "SiteSqlServer")]
    public partial class Playngo_ClientZone_DownloadRelation : Entity<Playngo_ClientZone_DownloadRelation>
    {
        #region 属性
        private Int32 _ID;
        /// <summary>
        /// 编号
        /// </summary>
        [Description("编号")]
        [DataObjectField(true, true, false, 10)]
        [BindColumn("ID", Description = "编号", DefaultValue = "", Order = 1)]
        public Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChange("ID", value)) _ID = value; }
        }

        private Int32 _ItemID;
        /// <summary>
        /// 关联项编号
        /// </summary>
        [Description("关联项编号")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("ItemID", Description = "关联项编号", DefaultValue = "", Order = 2)]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { if (OnPropertyChange("ItemID", value)) _ItemID = value; }
        }

        private Int32 _DownloadID;
        /// <summary>
        /// 下载项编号
        /// </summary>
        [Description("下载项编号")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("DownloadID", Description = "下载项编号", DefaultValue = "", Order = 3)]
        public Int32 DownloadID
        {
            get { return _DownloadID; }
            set { if (OnPropertyChange("DownloadID", value)) _DownloadID = value; }
        }

        private Int32 _Sort;
        /// <summary>
        /// 排序
        /// </summary>
        [Description("排序")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("Sort", Description = "排序", DefaultValue = "", Order = 4)]
        public Int32 Sort
        {
            get { return _Sort; }
            set { if (OnPropertyChange("Sort", value)) _Sort = value; }
        }

        private Int32 _PageType;
        /// <summary>
        /// 页面项类型
        /// </summary>
        [Description("页面项类型")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn("PageType", Description = "页面项类型", DefaultValue = "", Order = 5)]
        public Int32 PageType
        {
            get { return _PageType; }
            set { if (OnPropertyChange("PageType", value)) _PageType = value; }
        }

        private Int32 _ModuleId;
        /// <summary>
        /// 模块编号
        /// </summary>
        [Description("模块编号")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("ModuleId", Description = "模块编号", DefaultValue = "", Order = 6)]
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
        [BindColumn("PortalId", Description = "站点编号", DefaultValue = "", Order = 7)]
        public Int32 PortalId
        {
            get { return _PortalId; }
            set { if (OnPropertyChange("PortalId", value)) _PortalId = value; }
        }

        private DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        [DataObjectField(false, false, false, 23)]
        [BindColumn("CreateTime", Description = "创建时间", DefaultValue = "", Order = 8)]
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { if (OnPropertyChange("CreateTime", value)) _CreateTime = value; }
        }

        private Int32 _CreateUser;
        /// <summary>
        /// 创建用户
        /// </summary>
        [Description("创建用户")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("CreateUser", Description = "创建用户", DefaultValue = "", Order = 9)]
        public Int32 CreateUser
        {
            get { return _CreateUser; }
            set { if (OnPropertyChange("CreateUser", value)) _CreateUser = value; }
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
                    case "ItemID": return _ItemID;
                    case "DownloadID": return _DownloadID;
                    case "Sort": return _Sort;
                    case "PageType": return _PageType;
                    case "ModuleId": return _ModuleId;
                    case "PortalId": return _PortalId;
                    case "CreateTime": return _CreateTime;
                    case "CreateUser": return _CreateUser;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "ID": _ID = Convert.ToInt32(value); break;
                    case "ItemID": _ItemID = Convert.ToInt32(value); break;
                    case "DownloadID": _DownloadID = Convert.ToInt32(value); break;
                    case "Sort": _Sort = Convert.ToInt32(value); break;
                    case "PageType": _PageType = Convert.ToInt32(value); break;
                    case "ModuleId": _ModuleId = Convert.ToInt32(value); break;
                    case "PortalId": _PortalId = Convert.ToInt32(value); break;
                    case "CreateTime": _CreateTime = Convert.ToDateTime(value); break;
                    case "CreateUser": _CreateUser = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>
        /// 取得下载关联字段名的快捷方式
        /// </summary>
        public class _
        {
            ///<summary>
            /// 编号
            ///</summary>
            public const String ID = "ID";

            ///<summary>
            /// 关联项编号
            ///</summary>
            public const String ItemID = "ItemID";

            ///<summary>
            /// 下载项编号
            ///</summary>
            public const String DownloadID = "DownloadID";

            ///<summary>
            /// 排序
            ///</summary>
            public const String Sort = "Sort";

            ///<summary>
            /// 页面项类型
            ///</summary>
            public const String PageType = "PageType";

            ///<summary>
            /// 模块编号
            ///</summary>
            public const String ModuleId = "ModuleId";

            ///<summary>
            /// 站点编号
            ///</summary>
            public const String PortalId = "PortalId";

            ///<summary>
            /// 创建时间
            ///</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>
            /// 创建用户
            ///</summary>
            public const String CreateUser = "CreateUser";
        }
        #endregion
    }
}