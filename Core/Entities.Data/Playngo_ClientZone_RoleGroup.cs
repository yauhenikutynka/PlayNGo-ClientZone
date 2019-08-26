using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Playngo.Modules.ClientZone
{
	/// <summary>
	/// 角色分组关联
	/// </summary>
	[Serializable]
	[DataObject]
	[Description("角色分组关联")]
	[BindTable("Playngo_ClientZone_RoleGroup", Description = "角色分组关联", ConnName = "SiteSqlServer")]
	public partial class Playngo_ClientZone_RoleGroup : Entity<Playngo_ClientZone_RoleGroup>
	{
		#region 属性
		private Int32 _ID;
		/// <summary>
		/// 关联编号
		/// </summary>
		[Description("关联编号")]
		[DataObjectField(true, true, false, 10)]
		[BindColumn("ID", Description = "关联编号", DefaultValue = "", Order = 1)]
		public Int32 ID
		{
			get { return _ID; }
			set { if (OnPropertyChange("ID", value)) _ID = value; }
		}

		private Int32 _RoleId;
		/// <summary>
		/// 角色编号
		/// </summary>
		[Description("角色编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("RoleId", Description = "角色编号", DefaultValue = "", Order = 2)]
		public Int32 RoleId
		{
			get { return _RoleId; }
			set { if (OnPropertyChange("RoleId", value)) _RoleId = value; }
		}

		private Int32 _GroupId;
		/// <summary>
		/// 分组编号
		/// </summary>
		[Description("分组编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("GroupId", Description = "分组编号", DefaultValue = "", Order = 3)]
		public Int32 GroupId
		{
			get { return _GroupId; }
			set { if (OnPropertyChange("GroupId", value)) _GroupId = value; }
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
					case "RoleId" : return _RoleId;
					case "GroupId" : return _GroupId;
					default: return base[name];
				}
			}
			set
			{
				switch (name)
				{
					case "ID" : _ID = Convert.ToInt32(value); break;
					case "RoleId" : _RoleId = Convert.ToInt32(value); break;
					case "GroupId" : _GroupId = Convert.ToInt32(value); break;
					default: base[name] = value; break;
				}
			}
		}
		#endregion

		#region 字段名
		/// <summary>
		/// 取得角色分组关联字段名的快捷方式
		/// </summary>
		public class _
		{
			///<summary>
			/// 关联编号
			///</summary>
			public const String ID = "ID";

			///<summary>
			/// 角色编号
			///</summary>
			public const String RoleId = "RoleId";

			///<summary>
			/// 分组编号
			///</summary>
			public const String GroupId = "GroupId";
		}
		#endregion
	}
}