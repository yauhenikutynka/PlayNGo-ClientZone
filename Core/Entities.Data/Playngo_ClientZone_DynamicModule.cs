using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Playngo.Modules.ClientZone
{
	/// <summary>
	/// 动态模块
	/// </summary>
	[Serializable]
	[DataObject]
	[Description("动态模块")]
	[BindTable("Playngo_ClientZone_DynamicModule", Description = "动态模块", ConnName = "SiteSqlServer")]
	public partial class Playngo_ClientZone_DynamicModule : Entity<Playngo_ClientZone_DynamicModule>
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
		[DataObjectField(false, false, false, 512)]
		[BindColumn("Title", Description = "标题", DefaultValue = "", Order = 2)]
		public String Title
		{
			get { return _Title; }
			set { if (OnPropertyChange("Title", value)) _Title = value; }
		}

		private Int32 _Per_AllUsers = 0;
		/// <summary>
		/// 允许所有用户
		/// </summary>
		[Description("允许所有用户")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("Per_AllUsers", Description = "允许所有用户", DefaultValue = "", Order = 3)]
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
		[BindColumn("Per_Roles", Description = "允许角色", DefaultValue = "", Order = 4)]
		public String Per_Roles
		{
			get { return _Per_Roles; }
			set { if (OnPropertyChange("Per_Roles", value)) _Per_Roles = value; }
		}

		private Int32 _PDFGenerator;
		/// <summary>
		/// PDF生成按钮
		/// </summary>
		[Description("PDF生成按钮")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("PDFGenerator", Description = "PDF生成按钮", DefaultValue = "", Order = 5)]
		public Int32 PDFGenerator
		{
			get { return _PDFGenerator; }
			set { if (OnPropertyChange("PDFGenerator", value)) _PDFGenerator = value; }
		}

		private Int32 _IncludeTabLink = 1;
		/// <summary>
		/// 包含TAB
		/// </summary>
		[Description("包含TAB")]
		[DataObjectField(false, false, true, 3)]
		[BindColumn("IncludeTabLink", Description = "包含TAB", DefaultValue = "", Order = 6)]
		public Int32 IncludeTabLink
		{
			get { return _IncludeTabLink; }
			set { if (OnPropertyChange("IncludeTabLink", value)) _IncludeTabLink = value; }
		}

		private Int32 _ModuleId;
		/// <summary>
		/// 模块编号
		/// </summary>
		[Description("模块编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("ModuleId", Description = "模块编号", DefaultValue = "", Order = 7)]
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
		[BindColumn("PortalId", Description = "站点编号", DefaultValue = "", Order = 8)]
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
		[BindColumn("Options", Description = "选项集合", DefaultValue = "", Order = 9)]
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
		[BindColumn("Sort", Description = "排序", DefaultValue = "", Order = 10)]
		public Int32 Sort
		{
			get { return _Sort; }
			set { if (OnPropertyChange("Sort", value)) _Sort = value; }
		}

		private DateTime _LastTime;
		/// <summary>
		/// 最后更新时间
		/// </summary>
		[Description("最后更新时间")]
		[DataObjectField(false, false, false, 23)]
		[BindColumn("LastTime", Description = "最后更新时间", DefaultValue = "", Order = 11)]
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
		[BindColumn("LastUser", Description = "最后更新用户", DefaultValue = "", Order = 12)]
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
		[BindColumn("LastIP", Description = "最后更新IP", DefaultValue = "", Order = 13)]
		public String LastIP
		{
			get { return _LastIP; }
			set { if (OnPropertyChange("LastIP", value)) _LastIP = value; }
		}

		private Int32 _LinkID;
		/// <summary>
		/// 关联编号
		/// </summary>
		[Description("关联编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("LinkID", Description = "关联编号", DefaultValue = "", Order = 14)]
		public Int32 LinkID
		{
			get { return _LinkID; }
			set { if (OnPropertyChange("LinkID", value)) _LinkID = value; }
		}

		private Int32 _Type;
		/// <summary>
		/// 类型
		/// </summary>
		[Description("类型")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("Type", Description = "类型", DefaultValue = "", Order = 15)]
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
					case "Title" : return _Title;
					case "Per_AllUsers" : return _Per_AllUsers;
					case "Per_Roles" : return _Per_Roles;
					case "PDFGenerator" : return _PDFGenerator;
					case "IncludeTabLink" : return _IncludeTabLink;
					case "ModuleId" : return _ModuleId;
					case "PortalId" : return _PortalId;
					case "Options" : return _Options;
					case "Sort" : return _Sort;
					case "LastTime" : return _LastTime;
					case "LastUser" : return _LastUser;
					case "LastIP" : return _LastIP;
					case "LinkID" : return _LinkID;
					case "Type" : return _Type;
					default: return base[name];
				}
			}
			set
			{
				switch (name)
				{
					case "ID" : _ID = Convert.ToInt32(value); break;
					case "Title" : _Title = Convert.ToString(value); break;
					case "Per_AllUsers" : _Per_AllUsers = Convert.ToInt32(value); break;
					case "Per_Roles" : _Per_Roles = Convert.ToString(value); break;
					case "PDFGenerator" : _PDFGenerator = Convert.ToInt32(value); break;
					case "IncludeTabLink" : _IncludeTabLink = Convert.ToInt32(value); break;
					case "ModuleId" : _ModuleId = Convert.ToInt32(value); break;
					case "PortalId" : _PortalId = Convert.ToInt32(value); break;
					case "Options" : _Options = Convert.ToString(value); break;
					case "Sort" : _Sort = Convert.ToInt32(value); break;
					case "LastTime" : _LastTime = Convert.ToDateTime(value); break;
					case "LastUser" : _LastUser = Convert.ToInt32(value); break;
					case "LastIP" : _LastIP = Convert.ToString(value); break;
					case "LinkID" : _LinkID = Convert.ToInt32(value); break;
					case "Type" : _Type = Convert.ToInt32(value); break;
					default: base[name] = value; break;
				}
			}
		}
		#endregion

		#region 字段名
		/// <summary>
		/// 取得动态模块字段名的快捷方式
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
			/// 允许所有用户
			///</summary>
			public const String Per_AllUsers = "Per_AllUsers";

			///<summary>
			/// 允许角色
			///</summary>
			public const String Per_Roles = "Per_Roles";

			///<summary>
			/// PDF生成按钮
			///</summary>
			public const String PDFGenerator = "PDFGenerator";

			///<summary>
			/// 包含TAB
			///</summary>
			public const String IncludeTabLink = "IncludeTabLink";

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
			/// 排序
			///</summary>
			public const String Sort = "Sort";

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
			/// 关联编号
			///</summary>
			public const String LinkID = "LinkID";

			///<summary>
			/// 类型
			///</summary>
			public const String Type = "Type";
		}
		#endregion
	}
}