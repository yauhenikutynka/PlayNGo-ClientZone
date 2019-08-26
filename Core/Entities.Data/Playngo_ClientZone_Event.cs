using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Playngo.Modules.ClientZone
{
	/// <summary>
	/// 事件表
	/// </summary>
	[Serializable]
	[DataObject]
	[Description("事件表")]
	[BindTable("Playngo_ClientZone_Event", Description = "事件表", ConnName = "SiteSqlServer")]
	public partial class Playngo_ClientZone_Event : Entity<Playngo_ClientZone_Event>
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

		private String _Title;
		/// <summary>
		/// 标题
		/// </summary>
		[Description("标题")]
		[DataObjectField(false, false, true, 512)]
		[BindColumn("Title", Description = "标题", DefaultValue = "", Order = 2)]
		public String Title
		{
			get { return _Title; }
			set { if (OnPropertyChange("Title", value)) _Title = value; }
		}

		private String _ContentText;
		/// <summary>
		/// 内容
		/// </summary>
		[Description("内容")]
		[DataObjectField(false, false, true, 1073741823)]
		[BindColumn("ContentText", Description = "内容", DefaultValue = "", Order = 3)]
		public String ContentText
		{
			get { return _ContentText; }
			set { if (OnPropertyChange("ContentText", value)) _ContentText = value; }
		}

		private String _Address;
		/// <summary>
		/// 地址
		/// </summary>
		[Description("地址")]
		[DataObjectField(false, false, true, 500)]
		[BindColumn("Address", Description = "地址", DefaultValue = "", Order = 4)]
		public String Address
		{
			get { return _Address; }
			set { if (OnPropertyChange("Address", value)) _Address = value; }
		}

		private String _LocationX;
		/// <summary>
		/// 位置X
		/// </summary>
		[Description("位置X")]
		[DataObjectField(false, false, false, 50)]
		[BindColumn("LocationX", Description = "位置X", DefaultValue = "", Order = 5)]
		public String LocationX
		{
			get { return _LocationX; }
			set { if (OnPropertyChange("LocationX", value)) _LocationX = value; }
		}

		private String _LocationY;
		/// <summary>
		/// 位置Y
		/// </summary>
		[Description("位置Y")]
		[DataObjectField(false, false, false, 50)]
		[BindColumn("LocationY", Description = "位置Y", DefaultValue = "", Order = 6)]
		public String LocationY
		{
			get { return _LocationY; }
			set { if (OnPropertyChange("LocationY", value)) _LocationY = value; }
		}

		private Int32 _IncludeRoadmap;
		/// <summary>
		/// 包含到Roadmap
		/// </summary>
		[Description("包含到Roadmap")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("IncludeRoadmap", Description = "包含到Roadmap", DefaultValue = "", Order = 7)]
		public Int32 IncludeRoadmap
		{
			get { return _IncludeRoadmap; }
			set { if (OnPropertyChange("IncludeRoadmap", value)) _IncludeRoadmap = value; }
		}

        private DateTime _StartTime = xUserTime.UtcTime();
        /// <summary>
        /// 开始时间
        /// </summary>
        [Description("开始时间")]
        [DataObjectField(false, false, false, 23)]
        [BindColumn("StartTime", Description = "开始时间", DefaultValue = "", Order = 8)]
        public DateTime StartTime
        {
            get { return xUserTime.LocalTime(_StartTime); }
            set { if (OnPropertyChange("StartTime", value)) _StartTime = xUserTime.ServerTime(value); }
        }

        private DateTime _EndTime = xUserTime.UtcTime().AddYears(10);
        /// <summary>
        /// 结束时间
        /// </summary>
        [Description("结束时间")]
        [DataObjectField(false, false, false, 23)]
        [BindColumn("EndTime", Description = "结束时间", DefaultValue = "", Order = 9)]
        public DateTime EndTime
        {
            get { return xUserTime.LocalTime(_EndTime); }
            set { if (OnPropertyChange("EndTime", value)) _EndTime = xUserTime.ServerTime(value); }
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
			get { return _CreateTime; }
			set { if (OnPropertyChange("CreateTime", value)) _CreateTime = value; }
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

		private Int32 _Per_AllUsers;
		/// <summary>
		/// 允许所有用户
		/// </summary>
		[Description("允许所有用户")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("Per_AllUsers", Description = "允许所有用户", DefaultValue = "", Order = 12)]
		public Int32 Per_AllUsers
		{
			get { return _Per_AllUsers; }
			set { if (OnPropertyChange("Per_AllUsers", value)) _Per_AllUsers = value; }
		}

		private String _Per_Roles;
		/// <summary>
		/// 允许角色
		/// </summary>
		[Description("允许角色")]
		[DataObjectField(false, false, true, 1073741823)]
		[BindColumn("Per_Roles", Description = "允许角色", DefaultValue = "", Order = 13)]
		public String Per_Roles
		{
			get { return _Per_Roles; }
			set { if (OnPropertyChange("Per_Roles", value)) _Per_Roles = value; }
		}

		private Int32 _Per_AllJurisdictions;
		/// <summary>
		/// 所有区域
		/// </summary>
		[Description("所有区域")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("Per_AllJurisdictions", Description = "所有区域", DefaultValue = "", Order = 14)]
		public Int32 Per_AllJurisdictions
		{
			get { return _Per_AllJurisdictions; }
			set { if (OnPropertyChange("Per_AllJurisdictions", value)) _Per_AllJurisdictions = value; }
		}

		private String _Per_Jurisdictions;
		/// <summary>
		/// 允许的区域集合
		/// </summary>
		[Description("允许的区域集合")]
		[DataObjectField(false, false, true, 1073741823)]
		[BindColumn("Per_Jurisdictions", Description = "允许的区域集合", DefaultValue = "", Order = 15)]
		public String Per_Jurisdictions
		{
			get { return _Per_Jurisdictions; }
			set { if (OnPropertyChange("Per_Jurisdictions", value)) _Per_Jurisdictions = value; }
		}

		private Int32 _ModuleId;
		/// <summary>
		/// 模块编号
		/// </summary>
		[Description("模块编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("ModuleId", Description = "模块编号", DefaultValue = "", Order = 16)]
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
		[BindColumn("PortalId", Description = "站点编号", DefaultValue = "", Order = 17)]
		public Int32 PortalId
		{
			get { return _PortalId; }
			set { if (OnPropertyChange("PortalId", value)) _PortalId = value; }
		}

		private String _Options;
		/// <summary>
		/// 选项集合
		/// </summary>
		[Description("选项集合")]
		[DataObjectField(false, false, true, 1073741823)]
		[BindColumn("Options", Description = "选项集合", DefaultValue = "", Order = 18)]
		public String Options
		{
			get { return _Options; }
			set { if (OnPropertyChange("Options", value)) _Options = value; }
		}

        private Int32 _Status = (Int32)EnumStatus.Published;
		/// <summary>
		/// 状态
		/// </summary>
		[Description("状态")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("Status", Description = "状态", DefaultValue = "", Order = 19)]
		public Int32 Status
		{
			get { return _Status; }
			set { if (OnPropertyChange("Status", value)) _Status = value; }
		}

		private Int32 _NotifyInclude =0;
		/// <summary>
		/// 包含提醒
		/// </summary>
		[Description("包含提醒")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("NotifyInclude", Description = "包含提醒", DefaultValue = "", Order = 20)]
		public Int32 NotifyInclude
		{
			get { return _NotifyInclude; }
			set { if (OnPropertyChange("NotifyInclude", value)) _NotifyInclude = value; }
		}

		private Int32 _NotifyStatus = 1;
		/// <summary>
		/// 提醒状态
		/// </summary>
		[Description("提醒状态")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("NotifyStatus", Description = "提醒状态", DefaultValue = "", Order = 21)]
		public Int32 NotifyStatus
		{
			get { return _NotifyStatus; }
			set { if (OnPropertyChange("NotifyStatus", value)) _NotifyStatus = value; }
		}

		private Int32 _NotifySubscribers;
		/// <summary>
		/// 提醒订阅
		/// </summary>
		[Description("提醒订阅")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("NotifySubscribers", Description = "提醒订阅", DefaultValue = "", Order = 22)]
		public Int32 NotifySubscribers
		{
			get { return _NotifySubscribers; }
			set { if (OnPropertyChange("NotifySubscribers", value)) _NotifySubscribers = value; }
		}

        private DateTime _LastTime = xUserTime.UtcTime();
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Description("最后更新时间")]
        [DataObjectField(false, false, false, 23)]
        [BindColumn("LastTime", Description = "最后更新时间", DefaultValue = "", Order = 23)]
        public DateTime LastTime
        {
            get { return xUserTime.LocalTime(_LastTime); }
            set { if (OnPropertyChange("LastTime", value)) _LastTime = xUserTime.ServerTime(value); }
        }

        private Int32 _LastUser;
		/// <summary>
		/// 最后更新用户
		/// </summary>
		[Description("最后更新用户")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("LastUser", Description = "最后更新用户", DefaultValue = "", Order = 24)]
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
		[BindColumn("LastIP", Description = "最后更新IP", DefaultValue = "", Order = 25)]
		public String LastIP
		{
			get { return _LastIP; }
			set { if (OnPropertyChange("LastIP", value)) _LastIP = value; }
		}


        private String _UrlSlug;
        /// <summary>
        /// 自定义URL
        /// </summary>
        [Description("自定义URL")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn("UrlSlug", Description = "自定义URL", DefaultValue = "", Order = 26)]
        public String UrlSlug
        {
            get { return _UrlSlug; }
            set { if (OnPropertyChange("UrlSlug", value)) _UrlSlug = value; }
        }


        private Int32 _SendMail = 0;
        /// <summary>
        /// 邮件发送状态
        /// </summary>
        [Description("邮件发送状态")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn("SendMail", Description = "邮件发送状态", DefaultValue = "", Order = 27)]
        public Int32 SendMail
        {
            get { return _SendMail; }
            set { if (OnPropertyChange("SendMail", value)) _SendMail = value; }
        }


        private DateTime _ReleaseDate = xUserTime.UtcTime();
        /// <summary>
        /// 发布时间
        /// </summary>
        [Description("发布时间")]
        [DataObjectField(false, false, false, 23)]
        [BindColumn("ReleaseDate", Description = "发布时间", DefaultValue = "", Order = 28)]
        public DateTime ReleaseDate
        {
            get { return xUserTime.LocalTime(_ReleaseDate); }
            set { if (OnPropertyChange("ReleaseDate", value)) _ReleaseDate = xUserTime.ServerTime(value); }
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
					case "Title" : return _Title;
					case "ContentText" : return _ContentText;
					case "Address" : return _Address;
					case "LocationX" : return _LocationX;
					case "LocationY" : return _LocationY;
					case "IncludeRoadmap" : return _IncludeRoadmap;
					case "StartTime" : return _StartTime;
					case "EndTime" : return _EndTime;
					case "CreateTime" : return _CreateTime;
					case "CreateUser" : return _CreateUser;
					case "Per_AllUsers" : return _Per_AllUsers;
					case "Per_Roles" : return _Per_Roles;
					case "Per_AllJurisdictions" : return _Per_AllJurisdictions;
					case "Per_Jurisdictions" : return _Per_Jurisdictions;
					case "ModuleId" : return _ModuleId;
					case "PortalId" : return _PortalId;
					case "Options" : return _Options;
					case "Status" : return _Status;
					case "NotifyInclude" : return _NotifyInclude;
					case "NotifyStatus" : return _NotifyStatus;
					case "NotifySubscribers" : return _NotifySubscribers;
					case "LastTime" : return _LastTime;
					case "LastUser" : return _LastUser;
					case "LastIP" : return _LastIP;
                    case "UrlSlug": return _UrlSlug;
                    case "SendMail": return _SendMail;
                    case "ReleaseDate": return _ReleaseDate;
                    default: return base[name];
				}
			}
			set
			{
				switch (name)
				{
					case "ID" : _ID = Convert.ToInt32(value); break;
					case "Title" : _Title = Convert.ToString(value); break;
					case "ContentText" : _ContentText = Convert.ToString(value); break;
					case "Address" : _Address = Convert.ToString(value); break;
					case "LocationX" : _LocationX = Convert.ToString(value); break;
					case "LocationY" : _LocationY = Convert.ToString(value); break;
					case "IncludeRoadmap" : _IncludeRoadmap = Convert.ToInt32(value); break;
					case "StartTime" : _StartTime = Convert.ToDateTime(value); break;
					case "EndTime" : _EndTime = Convert.ToDateTime(value); break;
					case "CreateTime" : _CreateTime = Convert.ToDateTime(value); break;
					case "CreateUser" : _CreateUser = Convert.ToInt32(value); break;
					case "Per_AllUsers" : _Per_AllUsers = Convert.ToInt32(value); break;
					case "Per_Roles" : _Per_Roles = Convert.ToString(value); break;
					case "Per_AllJurisdictions" : _Per_AllJurisdictions = Convert.ToInt32(value); break;
					case "Per_Jurisdictions" : _Per_Jurisdictions = Convert.ToString(value); break;
					case "ModuleId" : _ModuleId = Convert.ToInt32(value); break;
					case "PortalId" : _PortalId = Convert.ToInt32(value); break;
					case "Options" : _Options = Convert.ToString(value); break;
					case "Status" : _Status = Convert.ToInt32(value); break;
					case "NotifyInclude" : _NotifyInclude = Convert.ToInt32(value); break;
					case "NotifyStatus" : _NotifyStatus = Convert.ToInt32(value); break;
					case "NotifySubscribers" : _NotifySubscribers = Convert.ToInt32(value); break;
					case "LastTime" : _LastTime = Convert.ToDateTime(value); break;
					case "LastUser" : _LastUser = Convert.ToInt32(value); break;
					case "LastIP" : _LastIP = Convert.ToString(value); break;
                    case "UrlSlug": _UrlSlug = Convert.ToString(value); break;
                    case "SendMail": _SendMail = Convert.ToInt32(value); break;
                    case "ReleaseDate": _ReleaseDate = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
				}
			}
		}
		#endregion

		#region 字段名
		/// <summary>
		/// 取得事件表字段名的快捷方式
		/// </summary>
		public class _
		{
			///<summary>
			/// 编号
			///</summary>
			public const String ID = "ID";

			///<summary>
			/// 标题
			///</summary>
			public const String Title = "Title";

			///<summary>
			/// 内容
			///</summary>
			public const String ContentText = "ContentText";

			///<summary>
			/// 地址
			///</summary>
			public const String Address = "Address";

			///<summary>
			/// 位置X
			///</summary>
			public const String LocationX = "LocationX";

			///<summary>
			/// 位置Y
			///</summary>
			public const String LocationY = "LocationY";

			///<summary>
			/// 包含到Roadmap
			///</summary>
			public const String IncludeRoadmap = "IncludeRoadmap";

			///<summary>
			/// 开始时间
			///</summary>
			public const String StartTime = "StartTime";

			///<summary>
			/// 结束时间
			///</summary>
			public const String EndTime = "EndTime";

			///<summary>
			/// 创建时间
			///</summary>
			public const String CreateTime = "CreateTime";

			///<summary>
			/// 创建用户
			///</summary>
			public const String CreateUser = "CreateUser";

			///<summary>
			/// 允许所有用户
			///</summary>
			public const String Per_AllUsers = "Per_AllUsers";

			///<summary>
			/// 允许角色
			///</summary>
			public const String Per_Roles = "Per_Roles";

			///<summary>
			/// 所有区域
			///</summary>
			public const String Per_AllJurisdictions = "Per_AllJurisdictions";

			///<summary>
			/// 允许的区域集合
			///</summary>
			public const String Per_Jurisdictions = "Per_Jurisdictions";

			///<summary>
			/// 模块编号
			///</summary>
			public const String ModuleId = "ModuleId";

			///<summary>
			/// 站点编号
			///</summary>
			public const String PortalId = "PortalId";

			///<summary>
			/// 选项集合
			///</summary>
			public const String Options = "Options";

			///<summary>
			/// 状态
			///</summary>
			public const String Status = "Status";

			///<summary>
			/// 包含提醒
			///</summary>
			public const String NotifyInclude = "NotifyInclude";

			///<summary>
			/// 提醒状态
			///</summary>
			public const String NotifyStatus = "NotifyStatus";

			///<summary>
			/// 提醒订阅
			///</summary>
			public const String NotifySubscribers = "NotifySubscribers";

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
            /// 自定义URL
            ///</summary>
            public const String UrlSlug = "UrlSlug";

            ///<summary>
            /// 邮件发送状态
            ///</summary>
            public const String SendMail = "SendMail";

            ///<summary>
            /// 发布时间
            ///</summary>
            public const String ReleaseDate = "ReleaseDate";
        }
		#endregion
	}
}