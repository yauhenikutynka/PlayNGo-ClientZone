using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Playngo.Modules.ClientZone
{
	/// <summary>
	/// 竞赛表
	/// </summary>
	public partial class Playngo_ClientZone_Campaign : Entity<Playngo_ClientZone_Campaign>
	{
		#region 对象操作
		//基类Entity中包含三个对象操作：Insert、Update、Delete
		//你可以重载它们，以改变它们的行为
		//如：
		/*
		/// <summary>
		/// 已重载。把该对象插入到数据库。这里可以做数据插入前的检查
		/// </summary>
		/// <returns>影响的行数</returns>
		public override Int32 Insert()
		{
			return base.Insert();
		}
		 * */
		#endregion
		
		#region 扩展属性
		//TODO: 本类与哪些类有关联，可以在这里放置一个属性，使用延迟加载的方式获取关联对象

		/*
		private Category _Category;
		/// <summary>该商品所对应的类别</summary>
		public Category Category
		{
			get
			{
				if (_Category == null && CategoryID > 0 && !Dirtys.ContainKey("Category"))
				{
					_Category = Category.FindByKey(CategoryID);
					Dirtys.Add("Category", true);
				}
				return _Category;
			}
			set { _Category = value; }
		}
		 * */
		#endregion

		#region 扩展查询
		/// <summary>
		/// 根据主键查询一个竞赛表实体对象用于表单编辑
		/// </summary>
		/// <returns></returns>
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Playngo_ClientZone_Campaign FindByKeyForEdit()
		{
			Playngo_ClientZone_Campaign entity=Find(new String[]{}, new Object[]{});
			if (entity == null)
			{
				entity = new Playngo_ClientZone_Campaign();
			}
			return entity;
		}     

		/// <summary>
		/// 根据编号查找
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Playngo_ClientZone_Campaign FindByID(Int32 id)
		{
			return Find(_.ID, id);
			// 实体缓存
			//return Meta.Cache.Entities.Find(_.ID, id);
			// 单对象缓存
			//return Meta.SingleCache[id];
		}
		#endregion

		#region 高级查询
		/// <summary>
		/// 查询满足条件的记录集，分页、排序
		/// </summary>
		/// <param name="key">关键字</param>
		/// <param name="orderClause">排序，不带Order By</param>
		/// <param name="startRowIndex">开始行，0开始</param>
		/// <param name="maximumRows">最大返回行数</param>
		/// <returns>实体集</returns>
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static List<Playngo_ClientZone_Campaign> Search(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
		{
		    return FindAll(SearchWhere(key), orderClause, null, startRowIndex, maximumRows);
		}

		/// <summary>
		/// 查询满足条件的记录总数，分页和排序无效，带参数是因为ObjectDataSource要求它跟Search统一
		/// </summary>
		/// <param name="key">关键字</param>
		/// <param name="orderClause">排序，不带Order By</param>
		/// <param name="startRowIndex">开始行，0开始</param>
		/// <param name="maximumRows">最大返回行数</param>
		/// <returns>记录数</returns>
		public static Int32 SearchCount(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
		{
		    return FindCount(SearchWhere(key), null, null, 0, 0);
		}

		/// <summary>
		/// 构造搜索条件
		/// </summary>
		/// <param name="key">关键字</param>
		/// <returns></returns>
		private static String SearchWhere(String key)
		{
            if (String.IsNullOrEmpty(key)) return null;
            key = key.Replace("'", "''");
            String[] keys = key.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

		    StringBuilder sb = new StringBuilder();
		    sb.Append("1=1");

            //if (!String.IsNullOrEmpty(name)) sb.AppendFormat(" And {0} like '%{1}%'", _.Name, name.Replace("'", "''"));

            for (int i = 0; i < keys.Length; i++)
            {
                sb.Append(" And ");

                if (keys.Length > 1) sb.Append("(");
                Int32 n = 0;
                foreach (FieldItem item in Meta.Fields)
                {
                    if (item.Property.PropertyType != typeof(String)) continue;
                    // 只要前五项
                    if (++n > 5) break;

                    if (n > 1) sb.Append(" Or ");
                    sb.AppendFormat("{0} like '%{1}%'", item.Name, keys[i]);
                }
                if (keys.Length > 1) sb.Append(")");
            }

            if (sb.Length == "1=1".Length)
                return null;
            else
                return sb.ToString();
		}




        /// <summary>
        /// 查找标题和URL是否存在
        /// </summary>
        /// <param name="CampaignItem"></param>
        /// <returns></returns>
        public static Boolean FindExists(Playngo_ClientZone_Campaign CampaignItem)
        {
            QueryParam qp = new QueryParam();



            qp.WhereSql.Append(new SearchParam(_.UrlSlug, CampaignItem.UrlSlug, SearchType.Equal).ToSql());

            qp.WhereSql.Append(" OR ");


            qp.WhereSql.Append(new SearchParam(_.Title, CampaignItem.Title, SearchType.Equal).ToSql());






            return FindCount(qp) > 0;
        }
        #endregion

        #region 扩展操作



        /// <summary>
        /// 根据状态统计数量
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static Int32 FindCountByStatus(Int32 ModuleId, Int32 Status, Boolean IsAdmin, Int32 UserId)
        {
            QueryParam qp = new QueryParam();
            qp.Where.Add(new SearchParam(_.ModuleId, ModuleId, SearchType.Equal));
            if (Status >= 0)
            {
                qp.Where.Add(new SearchParam(_.Status, Status, SearchType.Equal));
            }

            //不是超级管理员也不是普通管理员时，只能看到自己发布的文章
            if (IsAdmin)
            {
                qp.Where.Add(new SearchParam(_.CreateUser, UserId, SearchType.Equal));
            }
            return FindCount(qp);
        }



        /// <summary>
        /// 根据编号和URL查找数据
        /// </summary>
        /// <param name="ItemId"></param>
        /// <param name="FriendlyUrl"></param>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public static Playngo_ClientZone_Campaign FindItemByFriendlyUrl(Int32 ItemId, String FriendlyUrl, Int32 ModuleId)
        {
            int RecordCount = 0;
            QueryParam qp = new QueryParam();

            qp.Orderfld = _.ID;
            qp.OrderType = 1;

            qp.Where.Add(new SearchParam(_.ModuleId, ModuleId, SearchType.Equal));
            //qp.Where.Add(new SearchParam(_.Status, (Int32)EnumStatus.Published, SearchType.Equal));

            if (ItemId > 0)
            {
                qp.Where.Add(new SearchParam(_.ID, ItemId, SearchType.Equal));
            }
            else if (!String.IsNullOrEmpty(FriendlyUrl))
            {
                qp.Where.Add(new SearchParam(_.UrlSlug, FriendlyUrl, SearchType.Equal));
            }
            else
            {
                qp.Where.Add(new SearchParam(_.ID, 0, SearchType.Equal));
            }



            return FindItem(qp, out RecordCount);
        }

        #endregion

        #region 业务
        #endregion
    }
}