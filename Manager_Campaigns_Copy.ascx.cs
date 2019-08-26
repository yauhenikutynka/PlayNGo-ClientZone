using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_Campaigns_Copy : BaseModule
    {

        /// <summary>
        /// 提示操作类
        /// </summary>
        MessageTips mTips = new MessageTips();



        /// <summary>
        /// 文章编号
        /// </summary>
        public Int32 CampaignID = WebHelper.GetIntParam(HttpContext.Current.Request, "ID", 0);


        private Playngo_ClientZone_Campaign _CampaignItem;
        /// <summary>
        /// 文章项
        /// </summary>
        public Playngo_ClientZone_Campaign CampaignItem
        {
            get
            {
                if (!(_CampaignItem != null && _CampaignItem.ID > 0))
                {
                    if (CampaignID > 0)
                        _CampaignItem = Playngo_ClientZone_Campaign.FindByKeyForEdit(CampaignID);
                    else
                        _CampaignItem = new Playngo_ClientZone_Campaign();
                }
                return _CampaignItem;
            }
            set { _CampaignItem = value; }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_PreRender(object sender, EventArgs e)
        {

            try
            {
                if (CampaignID > 0)
                {
                    if (CampaignItem != null && CampaignItem.ID > 0)
                    {
                        //拷贝需要复制的记录
                        var DBCampaign = CampaignItem.Clone() as Playngo_ClientZone_Campaign;

                        String Title = DBCampaign.Title;
                        String UrlSlug = DBCampaign.UrlSlug;

                        DBCampaign.ID = 0;
 
                        DBCampaign.Title = String.Format("Copy - {0}", Title);
                        DBCampaign.UrlSlug = String.Format("copy-{0}", UrlSlug);

                        Int32 i = 1;

                        while (Playngo_ClientZone_Campaign.FindExists(DBCampaign))
                        {
                            i = i + 1;
                            DBCampaign.Title = String.Format("Copy {0} - {1}",i, Title);
                            DBCampaign.UrlSlug = String.Format("copy-{0}-{1}", i, UrlSlug);
                        }

                        DBCampaign.Status = (Int32)EnumStatus.Pending;
                        DBCampaign.CreateUser = UserId;
                        DBCampaign.CreateTime = xUserTime.UtcTime();


                        List<KeyValueEntity> list = ConvertTo.Deserialize<List<KeyValueEntity>>(DBCampaign.Options);
                        if (list != null && list.Count > 0)
                        {
                            if (list.Exists(r => r.Key == "FriendlyUrl"))
                            {
                                list[list.FindIndex(r => r.Key == "FriendlyUrl")].Value = DBCampaign.UrlSlug;
                            }
 
                            DBCampaign.Options = ConvertTo.Serialize<List<KeyValueEntity>>(list);
                        }
                        


                        DBCampaign.ID = DBCampaign.Insert();


                        if (DBCampaign.ID > 0)
                        {


                            //复制文件关系
                            CopyDownFileRelations(DBCampaign.ID, CampaignID);

                            //复制动态模块及内容
                            CopyDynamicModules(DBCampaign.ID, CampaignID);

                            mTips.IsPostBack = false;
                            mTips.LoadMessage("CopyCampaignSuccess", EnumTips.Success, this, new String[] { CampaignItem.Title });

                            Response.Redirect(xUrl("ID", DBCampaign.ID.ToString(), "Campaigns-Edit"), false);
                        }
                        else
                        {
                            mTips.IsPostBack = false;
                            mTips.LoadMessage("CopyCampaignError", EnumTips.Warning, this, new String[] { CampaignItem.Title });
                            Response.Redirect(xUrl("Campaigns"), false);
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

            var DownloadRelations = Playngo_ClientZone_DownloadRelation.FindListByItem(OldID, (Int32)EnumDisplayModuleType.Campaigns);
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
            var DynamicModules = Playngo_ClientZone_DynamicModule.FindListByFilter(OldID, (Int32)EnumDynamicModuleType.Campaign, ModuleId);
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