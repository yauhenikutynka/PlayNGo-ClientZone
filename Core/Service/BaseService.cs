using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    public class BaseService
    {

        #region "--查询函数--"

        /// <summary>
        /// 创建查询语句(区域)
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public QueryParam CreateQueryByJurisdictions(QueryParam qp, BasePage Context)
        {
            String Jurisdictions = WebHelper.GetStringParam(Context.Request, "Jurisdictions", "");
            if (!String.IsNullOrEmpty(Jurisdictions))
            {
                if (qp.WhereSql.Length > 0) qp.WhereSql.Append(" AND ");

                qp.WhereSql.Append(" ( ");

                qp.WhereSql.Append(new SearchParam("Per_AllJurisdictions", 0, SearchType.Equal).ToSql());


                var RegionIds = Common.GetList(Jurisdictions);
                if (RegionIds != null && RegionIds.Count > 0)
                {
                    foreach (var RegionId in RegionIds)
                    {
                        qp.WhereSql.Append(" OR ");

                        qp.WhereSql.Append(new SearchParam("Per_Jurisdictions", String.Format(",{0},", RegionId), SearchType.Like).ToSql());
                    }
                }


                qp.WhereSql.Append(" ) ");



            }
            else
            {
                //所有区域可见
                //qp.Where.Add(new SearchParam("Per_AllJurisdictions", 0, SearchType.Equal));

                //没有勾选分类时，不能显示任何数据
                qp.Where.Add(new SearchParam("Per_AllJurisdictions", -1, SearchType.Equal));
            }

            return qp;
        }


        /// <summary>
        /// 创建查询语句(权限)
        /// </summary>
        /// <param name="qp"></param>
        /// <returns></returns>
        public QueryParam CreateQueryByRoles(QueryParam qp, BasePage Context)
        {
            if (Context.UserId > 0)
            {
                if (!Context.UserInfo.IsSuperUser)//超级管理员不限制
                {
                    if (qp.WhereSql.Length > 0) qp.WhereSql.Append(" AND ");

                    qp.WhereSql.Append(" ( ");
                    //公开的
                    qp.WhereSql.Append(new SearchParam("Per_AllUsers", 0, SearchType.Equal).ToSql());

                    //有角色的
                    if (Context.UserInfo.Roles != null && Context.UserInfo.Roles.Length > 0)
                    {
                        qp.WhereSql.Append(" OR ");
                        qp.WhereSql.Append(" ( ");

                        Int32 RoleIndex = 0;
                        foreach (var r in Context.UserInfo.Roles)
                        {
                            if (RoleIndex > 0)
                            {
                                qp.WhereSql.Append(" OR ");
                            }

                            qp.WhereSql.Append(new SearchParam("Per_Roles", String.Format(",{0},", r), SearchType.Like).ToSql());

                            qp.WhereSql.Append(" OR ");

                            qp.WhereSql.Append(new SearchParam("Per_Roles", r, SearchType.Like).ToSql());


                            RoleIndex++;
                        }
                        qp.WhereSql.Append(" ) ");
                    }


                    qp.WhereSql.Append(" ) ");
                }
            }
            else
            {
                qp.Where.Add(new SearchParam("Per_AllUsers", 0, SearchType.Equal));
            }
            return qp;
        }






        /// <summary>
        /// 创建查询语句(游戏分类)
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public QueryParam CreateQueryByGameGategorys(QueryParam qp, BasePage Context)
        {
            String GameGategorys = WebHelper.GetStringParam(Context.Request, "GameGategorys", "");
            if (!String.IsNullOrEmpty(GameGategorys))
            {
                var GameGategoryList = Common.GetList(GameGategorys);
                if (GameGategoryList != null && GameGategoryList.Count > 0)
                {
                    if (!GameGategoryList.Exists(r => r == "0"))
                    {
                        System.Text.StringBuilder WhereSql = new System.Text.StringBuilder();
                        foreach (var GameGategoryItem in GameGategoryList)
                        {
                            if (!String.IsNullOrEmpty(GameGategoryItem))
                            {
                                if (WhereSql.Length > 0) WhereSql.Append(" OR ");

                                WhereSql.Append(new SearchParam("GameCategories", String.Format(",{0},", GameGategoryItem), SearchType.Like).ToSql());
                            }
                        }

                        if (WhereSql.Length > 0) qp.WhereSql.AppendFormat(" {0} ( {1} )", qp.WhereSql.Length > 0 ? "AND" : "", WhereSql);
                    }
                }
            }
            else
            {
                //不勾选的时候不出现任何数据
                qp.Where.Add(new SearchParam("GameCategories", "-1", SearchType.Equal));

            }

            return qp;
        }

        #endregion

    }
}