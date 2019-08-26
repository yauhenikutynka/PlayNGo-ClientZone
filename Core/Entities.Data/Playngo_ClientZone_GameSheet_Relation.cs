using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Playngo.Modules.ClientZone
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	[DataObject]
	[Description("")]
	[BindTable("Playngo_ClientZone_GameSheet_Relation", Description = "", ConnName = "SiteSqlServer")]
	public partial class Playngo_ClientZone_GameSheet_Relation : Entity<Playngo_ClientZone_GameSheet_Relation>
	{
		#region 属性
		private Int32 _ID;
		/// <summary>
		/// 
		/// </summary>
		[Description("")]
		[DataObjectField(true, true, false, 10)]
		[BindColumn("ID", Description = "", DefaultValue = "", Order = 1)]
		public Int32 ID
		{
			get { return _ID; }
			set { if (OnPropertyChange("ID", value)) _ID = value; }
		}

		private Int32 _GSID;
		/// <summary>
		/// 
		/// </summary>
		[Description("")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("GSID", Description = "", DefaultValue = "", Order = 2)]
		public Int32 GSID
		{
			get { return _GSID; }
			set { if (OnPropertyChange("GSID", value)) _GSID = value; }
		}

		private Int32 _FileID;
		/// <summary>
		/// 
		/// </summary>
		[Description("")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("FileID", Description = "", DefaultValue = "", Order = 3)]
		public Int32 FileID
		{
			get { return _FileID; }
			set { if (OnPropertyChange("FileID", value)) _FileID = value; }
		}

		private Int32 _Type;
		/// <summary>
		/// 
		/// </summary>
		[Description("")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("Type", Description = "", DefaultValue = "", Order = 4)]
		public Int32 Type
		{
			get { return _Type; }
			set { if (OnPropertyChange("Type", value)) _Type = value; }
		}

		private DateTime _LastTime;
		/// <summary>
		/// 
		/// </summary>
		[Description("")]
		[DataObjectField(false, false, false, 23)]
		[BindColumn("LastTime", Description = "", DefaultValue = "", Order = 5)]
		public DateTime LastTime
		{
			get { return _LastTime; }
			set { if (OnPropertyChange("LastTime", value)) _LastTime = value; }
		}

		private Int32 _LastUser;
		/// <summary>
		/// 
		/// </summary>
		[Description("")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("LastUser", Description = "", DefaultValue = "", Order = 6)]
		public Int32 LastUser
		{
			get { return _LastUser; }
			set { if (OnPropertyChange("LastUser", value)) _LastUser = value; }
		}

		private String _LastIP;
		/// <summary>
		/// 
		/// </summary>
		[Description("")]
		[DataObjectField(false, false, false, 50)]
		[BindColumn("LastIP", Description = "", DefaultValue = "", Order = 7)]
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
					case "ID" : return _ID;
					case "GSID" : return _GSID;
					case "FileID" : return _FileID;
					case "Type" : return _Type;
					case "LastTime" : return _LastTime;
					case "LastUser" : return _LastUser;
					case "LastIP" : return _LastIP;
					default: return base[name];
				}
			}
			set
			{
				switch (name)
				{
					case "ID" : _ID = Convert.ToInt32(value); break;
					case "GSID" : _GSID = Convert.ToInt32(value); break;
					case "FileID" : _FileID = Convert.ToInt32(value); break;
					case "Type" : _Type = Convert.ToInt32(value); break;
					case "LastTime" : _LastTime = Convert.ToDateTime(value); break;
					case "LastUser" : _LastUser = Convert.ToInt32(value); break;
					case "LastIP" : _LastIP = Convert.ToString(value); break;
					default: base[name] = value; break;
				}
			}
		}
		#endregion

		#region 字段名
		/// <summary>
		/// 取得字段名的快捷方式
		/// </summary>
		public class _
		{
			///<summary>
			/// 
			///</summary>
			public const String ID = "ID";

			///<summary>
			/// 
			///</summary>
			public const String GSID = "GSID";

			///<summary>
			/// 
			///</summary>
			public const String FileID = "FileID";

			///<summary>
			/// 
			///</summary>
			public const String Type = "Type";

			///<summary>
			/// 
			///</summary>
			public const String LastTime = "LastTime";

			///<summary>
			/// 
			///</summary>
			public const String LastUser = "LastUser";

			///<summary>
			/// 
			///</summary>
			public const String LastIP = "LastIP";
		}
		#endregion
	}
}