using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Playngo.Modules.ClientZone
{
	/// <summary>
	/// 分类表
	/// </summary>
	[Serializable]
	[DataObject]
	[Description("分类表")]
	[BindTable("Playngo_ClientZone_FileType", Description = "分类表", ConnName = "SiteSqlServer")]
	public partial class Playngo_ClientZone_FileType : Entity<Playngo_ClientZone_FileType>
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

		private Int32 _ParentID;
		/// <summary>
		/// 
		/// </summary>
		[Description("")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("ParentID", Description = "", DefaultValue = "", Order = 2)]
		public Int32 ParentID
		{
			get { return _ParentID; }
			set { if (OnPropertyChange("ParentID", value)) _ParentID = value; }
		}

		private String _Name;
		/// <summary>
		/// 分类名称
		/// </summary>
		[Description("分类名称")]
		[DataObjectField(false, false, false, 128)]
		[BindColumn("Name", Description = "分类名称", DefaultValue = "", Order = 3)]
		public String Name
		{
			get { return _Name; }
			set { if (OnPropertyChange("Name", value)) _Name = value; }
		}

		private String _ContentText;
		/// <summary>
		/// 分类描述
		/// </summary>
		[Description("分类描述")]
		[DataObjectField(false, false, true, 1073741823)]
		[BindColumn("ContentText", Description = "分类描述", DefaultValue = "", Order = 4)]
		public String ContentText
		{
			get { return _ContentText; }
			set { if (OnPropertyChange("ContentText", value)) _ContentText = value; }
		}

		private String _Options;
		/// <summary>
		/// 选项集合
		/// </summary>
		[Description("选项集合")]
		[DataObjectField(false, false, true, 1073741823)]
		[BindColumn("Options", Description = "选项集合", DefaultValue = "", Order = 5)]
		public String Options
		{
			get { return _Options; }
			set { if (OnPropertyChange("Options", value)) _Options = value; }
		}

		private String _Picture;
		/// <summary>
		/// 图片
		/// </summary>
		[Description("图片")]
		[DataObjectField(false, false, false, 512)]
		[BindColumn("Picture", Description = "图片", DefaultValue = "", Order = 6)]
		public String Picture
		{
			get { return _Picture; }
			set { if (OnPropertyChange("Picture", value)) _Picture = value; }
		}

		private Int32 _Sort;
		/// <summary>
		/// 排序
		/// </summary>
		[Description("排序")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("Sort", Description = "排序", DefaultValue = "", Order = 7)]
		public Int32 Sort
		{
			get { return _Sort; }
			set { if (OnPropertyChange("Sort", value)) _Sort = value; }
		}

		private Int32 _Per_AllUsers;
		/// <summary>
		/// 允许所有用户
		/// </summary>
		[Description("允许所有用户")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("Per_AllUsers", Description = "允许所有用户", DefaultValue = "", Order = 8)]
		public Int32 Per_AllUsers
		{
			get { return _Per_AllUsers; }
			set { if (OnPropertyChange("Per_AllUsers", value)) _Per_AllUsers = value; }
		}

		private String _Per_Roles;
		/// <summary>
		/// 允许角色集合
		/// </summary>
		[Description("允许角色集合")]
		[DataObjectField(false, false, true, 1073741823)]
		[BindColumn("Per_Roles", Description = "允许角色集合", DefaultValue = "", Order = 9)]
		public String Per_Roles
		{
			get { return _Per_Roles; }
			set { if (OnPropertyChange("Per_Roles", value)) _Per_Roles = value; }
		}

		private DateTime _LastTime;
		/// <summary>
		/// 最后更新时间
		/// </summary>
		[Description("最后更新时间")]
		[DataObjectField(false, false, false, 23)]
		[BindColumn("LastTime", Description = "最后更新时间", DefaultValue = "", Order = 10)]
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
		[BindColumn("LastUser", Description = "最后更新用户", DefaultValue = "", Order = 11)]
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
		[BindColumn("LastIP", Description = "最后更新IP", DefaultValue = "", Order = 12)]
		public String LastIP
		{
			get { return _LastIP; }
			set { if (OnPropertyChange("LastIP", value)) _LastIP = value; }
		}

		private Int32 _ModuleId;
		/// <summary>
		/// 模块编号
		/// </summary>
		[Description("模块编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("ModuleId", Description = "模块编号", DefaultValue = "", Order = 13)]
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
		[BindColumn("PortalId", Description = "站点编号", DefaultValue = "", Order = 14)]
		public Int32 PortalId
		{
			get { return _PortalId; }
			set { if (OnPropertyChange("PortalId", value)) _PortalId = value; }
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
					case "ParentID" : return _ParentID;
					case "Name" : return _Name;
					case "ContentText" : return _ContentText;
					case "Options" : return _Options;
					case "Picture" : return _Picture;
					case "Sort" : return _Sort;
					case "Per_AllUsers" : return _Per_AllUsers;
					case "Per_Roles" : return _Per_Roles;
					case "LastTime" : return _LastTime;
					case "LastUser" : return _LastUser;
					case "LastIP" : return _LastIP;
					case "ModuleId" : return _ModuleId;
					case "PortalId" : return _PortalId;
					default: return base[name];
				}
			}
			set
			{
				switch (name)
				{
					case "ID" : _ID = Convert.ToInt32(value); break;
					case "ParentID" : _ParentID = Convert.ToInt32(value); break;
					case "Name" : _Name = Convert.ToString(value); break;
					case "ContentText" : _ContentText = Convert.ToString(value); break;
					case "Options" : _Options = Convert.ToString(value); break;
					case "Picture" : _Picture = Convert.ToString(value); break;
					case "Sort" : _Sort = Convert.ToInt32(value); break;
					case "Per_AllUsers" : _Per_AllUsers = Convert.ToInt32(value); break;
					case "Per_Roles" : _Per_Roles = Convert.ToString(value); break;
					case "LastTime" : _LastTime = Convert.ToDateTime(value); break;
					case "LastUser" : _LastUser = Convert.ToInt32(value); break;
					case "LastIP" : _LastIP = Convert.ToString(value); break;
					case "ModuleId" : _ModuleId = Convert.ToInt32(value); break;
					case "PortalId" : _PortalId = Convert.ToInt32(value); break;
					default: base[name] = value; break;
				}
			}
		}
		#endregion

		#region 字段名
		/// <summary>
		/// 取得分类表字段名的快捷方式
		/// </summary>
		public class _
		{
			///<summary>
			/// 编号
			///</summary>
			public const String ID = "ID";

			///<summary>
			/// 
			///</summary>
			public const String ParentID = "ParentID";

			///<summary>
			/// 分类名称
			///</summary>
			public const String Name = "Name";

			///<summary>
			/// 分类描述
			///</summary>
			public const String ContentText = "ContentText";

			///<summary>
			/// 选项集合
			///</summary>
			public const String Options = "Options";

			///<summary>
			/// 图片
			///</summary>
			public const String Picture = "Picture";

			///<summary>
			/// 排序
			///</summary>
			public const String Sort = "Sort";

			///<summary>
			/// 允许所有用户
			///</summary>
			public const String Per_AllUsers = "Per_AllUsers";

			///<summary>
			/// 允许角色集合
			///</summary>
			public const String Per_Roles = "Per_Roles";

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
			/// 模块编号
			///</summary>
			public const String ModuleId = "ModuleId";

			///<summary>
			/// 站点编号
			///</summary>
			public const String PortalId = "PortalId";
		}
		#endregion
	}
}