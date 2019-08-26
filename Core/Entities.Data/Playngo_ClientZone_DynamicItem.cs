using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Playngo.Modules.ClientZone
{
	/// <summary>
	/// 动态项
	/// </summary>
	[Serializable]
	[DataObject]
	[Description("动态项")]
	[BindTable("Playngo_ClientZone_DynamicItem", Description = "动态项", ConnName = "SiteSqlServer")]
	public partial class Playngo_ClientZone_DynamicItem : Entity<Playngo_ClientZone_DynamicItem>
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

		private Int32 _DynamicID;
		/// <summary>
		/// 关联动态编号
		/// </summary>
		[Description("关联动态编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("DynamicID", Description = "关联动态编号", DefaultValue = "", Order = 2)]
		public Int32 DynamicID
		{
			get { return _DynamicID; }
			set { if (OnPropertyChange("DynamicID", value)) _DynamicID = value; }
		}

		private String _Title;
		/// <summary>
		/// 标题
		/// </summary>
		[Description("标题")]
		[DataObjectField(false, false, false, 512)]
		[BindColumn("Title", Description = "标题", DefaultValue = "", Order = 3)]
		public String Title
		{
			get { return _Title; }
			set { if (OnPropertyChange("Title", value)) _Title = value; }
		}

		private String _Options;
		/// <summary>
		/// 选项集合
		/// </summary>
		[Description("选项集合")]
		[DataObjectField(false, false, true, 1073741823)]
		[BindColumn("Options", Description = "选项集合", DefaultValue = "", Order = 4)]
		public String Options
		{
			get { return _Options; }
			set { if (OnPropertyChange("Options", value)) _Options = value; }
		}

		private Int32 _Sort;
		/// <summary>
		/// 排序
		/// </summary>
		[Description("排序")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("Sort", Description = "排序", DefaultValue = "", Order = 5)]
		public Int32 Sort
		{
			get { return _Sort; }
			set { if (OnPropertyChange("Sort", value)) _Sort = value; }
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

		private DateTime _LastTime;
		/// <summary>
		/// 最后更新时间
		/// </summary>
		[Description("最后更新时间")]
		[DataObjectField(false, false, false, 23)]
		[BindColumn("LastTime", Description = "最后更新时间", DefaultValue = "", Order = 8)]
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
		[BindColumn("LastUser", Description = "最后更新用户", DefaultValue = "", Order = 9)]
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
		[BindColumn("LastIP", Description = "最后更新IP", DefaultValue = "", Order = 10)]
		public String LastIP
		{
			get { return _LastIP; }
			set { if (OnPropertyChange("LastIP", value)) _LastIP = value; }
		}



        private Int32 _Type;
        /// <summary>
        /// 类型
        /// </summary>
        [Description("类型")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn("Type", Description = "类型", DefaultValue = "", Order = 11)]
        public Int32 Type
        {
            get { return _Type; }
            set { if (OnPropertyChange("Type", value)) _Type = value; }
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
					case "ID" : return _ID;
					case "DynamicID" : return _DynamicID;
					case "Title" : return _Title;
					case "Options" : return _Options;
					case "Sort" : return _Sort;
					case "ModuleId" : return _ModuleId;
					case "PortalId" : return _PortalId;
					case "LastTime" : return _LastTime;
					case "LastUser" : return _LastUser;
					case "LastIP" : return _LastIP;
                    case "Type": return _Type;
                    default: return base[name];
				}
			}
			set
			{
				switch (name)
				{
					case "ID" : _ID = Convert.ToInt32(value); break;
					case "DynamicID" : _DynamicID = Convert.ToInt32(value); break;
					case "Title" : _Title = Convert.ToString(value); break;
					case "Options" : _Options = Convert.ToString(value); break;
					case "Sort" : _Sort = Convert.ToInt32(value); break;
					case "ModuleId" : _ModuleId = Convert.ToInt32(value); break;
					case "PortalId" : _PortalId = Convert.ToInt32(value); break;
					case "LastTime" : _LastTime = Convert.ToDateTime(value); break;
					case "LastUser" : _LastUser = Convert.ToInt32(value); break;
					case "LastIP" : _LastIP = Convert.ToString(value); break;
                    case "Type": _Type = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
				}
			}
		}
		#endregion

		#region 字段名
		/// <summary>
		/// 取得动态项字段名的快捷方式
		/// </summary>
		public class _
		{
			///<summary>
			/// 编号
			///</summary>
			public const String ID = "ID";

			///<summary>
			/// 关联动态编号
			///</summary>
			public const String DynamicID = "DynamicID";

			///<summary>
			/// 标题
			///</summary>
			public const String Title = "Title";

			///<summary>
			/// 选项集合
			///</summary>
			public const String Options = "Options";

			///<summary>
			/// 排序
			///</summary>
			public const String Sort = "Sort";

			///<summary>
			/// 模块编号
			///</summary>
			public const String ModuleId = "ModuleId";

			///<summary>
			/// 站点编号
			///</summary>
			public const String PortalId = "PortalId";

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


            ///<summary>
            /// 类型
            ///</summary>
            public const String Type = "Type";
        }
		#endregion
	}
}