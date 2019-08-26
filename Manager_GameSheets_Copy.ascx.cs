using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_GameSheets_Copy : BaseModule
    {

        /// <summary>
        /// 提示操作类
        /// </summary>
        MessageTips mTips = new MessageTips();



        /// <summary>
        /// 文章编号
        /// </summary>
        public Int32 GameSheetID = WebHelper.GetIntParam(HttpContext.Current.Request, "ID", 0);


        private Playngo_ClientZone_GameSheet _GameSheetItem;
        /// <summary>
        /// 文章项
        /// </summary>
        public Playngo_ClientZone_GameSheet GameSheetItem
        {
            get
            {
                if (!(_GameSheetItem != null && _GameSheetItem.ID > 0))
                {
                    if (GameSheetID > 0)
                        _GameSheetItem = Playngo_ClientZone_GameSheet.FindByKeyForEdit(GameSheetID);
                    else
                        _GameSheetItem = new Playngo_ClientZone_GameSheet();
                }
                return _GameSheetItem;
            }
            set { _GameSheetItem = value; }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_PreRender(object sender, EventArgs e)
        {

            try
            {
                if (GameSheetID > 0)
                {
                    if (GameSheetItem != null && GameSheetItem.ID > 0)
                    {
                        //拷贝需要复制的记录
                        var DBGameSheet = GameSheetItem.Clone() as Playngo_ClientZone_GameSheet;

                        String Title = DBGameSheet.Title;
                        String UrlSlug = DBGameSheet.UrlSlug;

                        DBGameSheet.ID = 0;
 
                        DBGameSheet.Title = String.Format("Copy - {0}", Title);
                        DBGameSheet.UrlSlug = String.Format("copy-{0}", UrlSlug);

                        Int32 i = 1;

                        while (Playngo_ClientZone_GameSheet.FindExists(DBGameSheet))
                        {
                            i = i + 1;
                            DBGameSheet.Title = String.Format("Copy {0} - {1}",i, Title);
                            DBGameSheet.UrlSlug = String.Format("copy-{0}-{1}", i, UrlSlug);
                        }

                        DBGameSheet.Status = (Int32)EnumStatus.Pending;
                        DBGameSheet.CreateUser = UserId;
                        DBGameSheet.CreateTime = xUserTime.UtcTime();


                        List<KeyValueEntity> list = ConvertTo.Deserialize<List<KeyValueEntity>>(DBGameSheet.Options);
                        if (list != null && list.Count > 0)
                        {
                            if (list.Exists(r => r.Key == "FriendlyUrl"))
                            {
                                list[list.FindIndex(r => r.Key == "FriendlyUrl")].Value = DBGameSheet.UrlSlug;
                            }
 
                            DBGameSheet.Options = ConvertTo.Serialize<List<KeyValueEntity>>(list);
                        }
                        


                        DBGameSheet.ID = DBGameSheet.Insert();


                        if (DBGameSheet.ID > 0)
                        {


                            //复制文件关系
                            CopyDownFileRelations(DBGameSheet.ID, GameSheetID);

                            //复制动态模块及内容
                            CopyDynamicModules(DBGameSheet.ID, GameSheetID);

                            mTips.IsPostBack = false;
                            mTips.LoadMessage("CopyGameSheetSuccess", EnumTips.Success, this, new String[] { GameSheetItem.Title });

                            Response.Redirect(xUrl("ID", DBGameSheet.ID.ToString(), "GameSheets-Edit"), false);
                        }
                        else
                        {
                            mTips.IsPostBack = false;
                            mTips.LoadMessage("CopyGameSheetError", EnumTips.Warning, this, new String[] { GameSheetItem.Title });
                            Response.Redirect(xUrl("GameSheets"), false);
                        }





                    }
                    else
                    {
                        //没有查找到数据
                    }
                }
                else
                {
                    //ID传输得不对
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }

        /// <summary>
        /// 复制文件关系
        /// </summary>
        /// <param name="NewID">新的编号</param>
        /// <param name="OldID">老的编号</param>
        public void CopyDownFileRelations(Int32 NewID, Int32 OldID)
        {

            var DownloadRelations = Playngo_ClientZone_DownloadRelation.FindListByItem(OldID, (Int32)EnumDisplayModuleType.GameSheets);
            if (DownloadRelations != null && DownloadRelations.Count > 0)
            {
                foreach (var DownloadRelation in DownloadRelations)
                {
                    var DownloadRelationNew = DownloadRelation.Clone() as Playngo_ClientZone_DownloadRelation;
                    DownloadRelationNew.ItemID = NewID;
                    DownloadRelationNew.ID = 0;
                    DownloadRelationNew.CreateUser = UserId;
                    DownloadRelationNew.CreateTime = xUserTime.UtcTime();
                    DownloadRelationNew.Insert();

                }

            }




        }

        /// <summary>
        /// 复制动态模块
        /// </summary>
        /// <param name="NewID">新的编号</param>
        /// <param name="OldID">老的编号</param>
        public void CopyDynamicModules(Int32 NewID, Int32 OldID)
        {
            //找到动态模块老数据
            var DynamicModules = Playngo_ClientZone_DynamicModule.FindListByFilter(OldID, (Int32)EnumDynamicModuleType.GameSheet, ModuleId);
            if (DynamicModules != null && DynamicModules.Count > 0)
            {
                foreach (var DynamicModule in DynamicModules)
                {
                    //查找动态项老数据
                    var DynamicItems = Playngo_ClientZone_DynamicItem.FindListByFilter(DynamicModule.ID, ModuleId);
                    //构造新的动态模块
                    var DynamicModuleNew = DynamicModule.Clone() as Playngo_ClientZone_DynamicModule;
                    DynamicModuleNew.ID = 0;
                    DynamicModuleNew.LinkID = NewID;
                    DynamicModuleNew.LastTime = xUserTime.UtcTime();
                    DynamicModuleNew.LastIP = WebHelper.UserHost;
                    DynamicModuleNew.LastUser = UserId;
                    DynamicModuleNew.ID =  DynamicModuleNew.Insert();

                    if (DynamicModuleNew.ID > 0  && DynamicItems != null && DynamicItems.Count >0)
                    {
                        foreach (var DynamicItem in DynamicItems)
                        {
                            //构造性的动态项
                            var DynamicItemNew = DynamicItem.Clone() as Playngo_ClientZone_DynamicItem;
                            DynamicItemNew.ID = 0;
                            DynamicItemNew.DynamicID = DynamicModuleNew.ID;
                            DynamicItemNew.LastTime = xUserTime.UtcTime();
                            DynamicItemNew.LastIP = WebHelper.UserHost;
                            DynamicItemNew.LastUser = UserId;

                            DynamicItemNew.Insert();

                        }

                           



                    }


                }
            }

        }
















    }
}