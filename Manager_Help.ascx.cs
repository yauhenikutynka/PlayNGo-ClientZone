using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_Help : BaseModule
    {
 
        #region "事件"

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    //绑定页面项
                    BindPageItem();
                    // 绑定方案项
                    //BindDataItem();
                }
                ////绑定设置参数到页面
                //BindGroupsToPage();
                BindDynamicModulesToPage();
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }

        protected void Page_Init(System.Object sender, System.EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }
         


        /// <summary>
        /// 保存草稿
        /// </summary>
        protected void cmdSaveDraft_Click(object sender, EventArgs e)
        {
            try
            {
                //Int32 EventStatus = (Int32)EnumStatus.Draft;
                //Playngo_ClientZone_Events Event = new Playngo_ClientZone_Events();
                //Boolean SaveResult = SaveDataItem(EventStatus, out Event);



                //if (SaveResult)
                //{

                //    mTips.LoadMessage("SaveSuccess", EnumTips.Success, this, new String[] { Event.Title });
                //    Response.Redirect(xUrl("EventID", Event.ID.ToString(), "Event"), false);
                //}
                //else
                //{
                //    mTips.IsPostBack = false;
                //    mTips.LoadMessage("SaveError", EnumTips.Success, this, new String[] { Event.Title });
                //}
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        /// <summary>
        /// 文章软删除(删除到回收站)
        /// </summary>
        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {

                //Playngo_ClientZone_Events Event = Playngo_ClientZone_Events.FindByKeyForEdit(EventID);
                //Event.Status = (Int32)EnumStatus.Recycle;

                //if (Event != null && Event.ID > 0 && Event.Update() > 0)
                //{
                //    //更新关联数据的状态
                //    Event.UpdateAllStatus();

                //    //操作成功
                //    mTips.LoadMessage("DeleteEventSuccess", EnumTips.Success, this, new String[] { Event.Title });
                //    Response.Redirect(xUrl("EventList"), false);
                //}
                //else
                //{
                //    mTips.IsPostBack = false;
                //    //操作失败
                //    mTips.LoadMessage("DeleteEventError", EnumTips.Success, this, new String[] { Event.Title });
                //}



            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        protected void RepeaterOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //SettingEntity ThemeSetting = e.Item.DataItem as SettingEntity;

                //KeyValueEntity KeyValue = ItemSettings.Find(r1 => r1.Key == ThemeSetting.Name);
                //if (KeyValue != null && !String.IsNullOrEmpty(KeyValue.Key))
                //{
                //    ThemeSetting.DefaultValue = KeyValue.Value.ToString();
                //}

                ////构造输入控件
                //PlaceHolder ThemePH = e.Item.FindControl("ThemePH") as PlaceHolder;

                //#region "创建控件"
                //ControlHelper ctl = new ControlHelper(this);

                //ThemePH.Controls.Add((Control)ctl.ViewControl(ThemeSetting));
                //#endregion

                //Literal liTitle = e.Item.FindControl("liTitle") as Literal;
                //liTitle.Text = String.Format("<label class=\"col-sm-3 control-label\" for=\"{1}\">{0}:</label>", !String.IsNullOrEmpty(ThemeSetting.Alias) ? ThemeSetting.Alias : ThemeSetting.Name, ctl.ViewControlID(ThemeSetting));

                //if (!String.IsNullOrEmpty(ThemeSetting.Description))
                //{
                //    Literal liHelp = e.Item.FindControl("liHelp") as Literal;
                //    liHelp.Text = String.Format("<span class=\"help-block\"><i class=\"fa fa-info-circle\"></i> {0}</span>", ThemeSetting.Description);
                //}
            }
        }


        /// <summary>
        /// 分组绑定事件
        /// </summary>
        protected void RepeaterGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Repeater RepeaterOptions = e.Item.FindControl("RepeaterOptions") as Repeater;
                //KeyValueEntity GroupItem = e.Item.DataItem as KeyValueEntity;
                //int OptionCount = 0;
                //BindOptionsToPage(RepeaterOptions, GroupItem.Key, out OptionCount);

                //if (OptionCount == 0)
                //{
                //    e.Item.Visible = false;
                //}

            }
        }

         
        #endregion

        #region "方法"
        /// <summary>
        /// 绑定页面项
        /// </summary>
        private void BindPageItem()
        {

            //绑定状态代码
            //WebHelper.BindList(cblIncludedSections, typeof(EnumIncludedSections));

        
        }


        /// <summary>
        /// 绑定数据项
        /// </summary>
        private void BindDataItem()
        {





        }

         





        /// <summary>
        /// 绑定选项分组框到页面
        /// </summary>
        private void BindGroupsToPage()
        {
            //获取效果参数
            List<SettingEntity> ItemSettingDB = new List<SettingEntity>();// Settings_ItemSettingDB;

            if (ItemSettingDB != null && ItemSettingDB.Count > 0)
            {

                List<KeyValueEntity> Items = new List<KeyValueEntity>();
                foreach (SettingEntity ItemSetting in ItemSettingDB)
                {
                    if (!Items.Exists(r1 => r1.Key == ItemSetting.Group))
                    {
                        Items.Add(new KeyValueEntity(ItemSetting.Group, ""));
                    }
                }

                if (Items != null && Items.Count > 0)
                {

                    //这里需要根据需要过滤掉某些分组
                    if (!IsAdministrator && !IsAdmin && IsEdit)
                    {
                        if (!ViewSettingT<Boolean>("General.AuthorPanel_Rating", true))
                        {
                            Items.RemoveAll(r => r.Key == "Rating");
                        }

                        if (!ViewSettingT<Boolean>("General.AuthorPanel_Sitemap", true))
                        {
                            Items.RemoveAll(r => r.Key == "Sitemap Settings");
                        }

                        if (!ViewSettingT<Boolean>("General.AuthorPanel_Items", true))
                        {
                            Items.RemoveAll(r => r.Key == "Display Items");
                        }
                    }




                    //绑定参数项
                    RepeaterGroup.DataSource = Items;
                    RepeaterGroup.DataBind();
                }
                divOptions.Visible = true;
            }
        }




        /// <summary>
        /// 绑定选项集合到页面
        /// </summary>
        private void BindOptionsToPage(Repeater RepeaterOptions, String Group, out int OptionCount)
        {
            OptionCount = 0;
            //获取效果参数
            List<SettingEntity> ItemSettingDB = new List<SettingEntity>();

            if (ItemSettingDB != null && ItemSettingDB.Count > 0)
            {
                ItemSettingDB = ItemSettingDB.FindAll(r1 => r1.Group == Group);
                OptionCount = ItemSettingDB.Count;
                //绑定参数项
                RepeaterOptions.DataSource = ItemSettingDB;
                RepeaterOptions.DataBind();
            }
        }



        /// <summary>
        /// 绑定项集合到页面
        /// </summary>
        private void BindItemsToPage(Repeater RepeaterItems, String Group, out int OptionCount)
        {
            OptionCount = 0;
            //获取效果参数
            List<SettingEntity> ItemSettingDB = new List<SettingEntity>();

            if (ItemSettingDB != null && ItemSettingDB.Count > 0)
            {
                //ItemSettingDB = ItemSettingDB.FindAll(r1 => r1.Group == Group);
                //OptionCount = ItemSettingDB.Count;
                OptionCount = 1;
                //绑定参数项
                RepeaterItems.DataSource = ItemSettingDB;
                RepeaterItems.DataBind();
            }
        }





        /// <summary>
        /// 拼接数据项的设置参数
        /// </summary>
        /// <returns></returns>
        public String SetItemSettings(ref List<KeyValueEntity> list)
        {
            //获取效果参数
            List<SettingEntity> ItemSettingDB = new List<SettingEntity>();


            if (ItemSettingDB != null && ItemSettingDB.Count > 0)
            {
                ControlHelper ControlItem = new ControlHelper(ModuleId);

                foreach (SettingEntity ri in ItemSettingDB)
                {
                    KeyValueEntity item = new KeyValueEntity();
                    item.Key = ri.Name;
                    item.Value = ControlHelper.GetWebFormValue(ri, this);
                    list.Add(item);
                }
            }
            return ConvertTo.Serialize<List<KeyValueEntity>>(list);
        }



        #endregion
 
        #region "==动态模块函数及事件集合=="





        /// <summary>
        /// 绑定项集合到页面
        /// </summary>
        private void BindItemsToPage(Repeater RepeaterItems, Int32 DynamicModule, out int OptionCount)
        {
            OptionCount = 0;
            //获取效果参数
            var DynamicItems = Playngo_ClientZone_DynamicItem.FindListByFilter(DynamicModule, ModuleId);

            if (DynamicItems != null && DynamicItems.Count > 0)
            {
                OptionCount = DynamicItems.Count;
                //绑定参数项
                RepeaterItems.DataSource = DynamicItems;
                RepeaterItems.DataBind();
            }
        }




        /// <summary>
        /// 绑定动态模块到页面
        /// </summary>
        private void BindDynamicModulesToPage()
        {



            //绑定动态模块的模态窗口
            hlAddNewLink.Attributes.Add("data-href", String.Format("{0}Resource_Masters.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}&Master=Manager_Modal_DynamicModule&Type={5}&LinkID={6}", ModulePath, PortalId, TabId, ModuleId, language, (Int32)EnumDynamicModuleType.Help, 0));

            var DynamicModules = Playngo_ClientZone_DynamicModule.FindListByFilter(0, (Int32)EnumDynamicModuleType.Help, ModuleId);

            if (DynamicModules != null && DynamicModules.Count > 0)
            {
                //绑定参数项
                RepeaterModules.DataSource = DynamicModules;
                RepeaterModules.DataBind();

            }




        }



        protected void RepeaterItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Playngo_ClientZone_DynamicItem DynamicItem = e.Item.DataItem as Playngo_ClientZone_DynamicItem;


                HyperLink hlDynamicItemEdit = e.Item.FindControl("hlDynamicItemEdit") as HyperLink;
                if (hlDynamicItemEdit != null)
                {
                    hlDynamicItemEdit.Attributes.Add("data-href", DynamicItem_IframeUrl(DynamicItem.ID, DynamicItem.DynamicID, ((EnumDynamicItemType)DynamicItem.Type).ToString()));
                }

                Literal liItemType = e.Item.FindControl("liItemType") as Literal;
                if (liItemType != null)
                {
                    liItemType.Text = EnumHelper.GetEnumTextVal(DynamicItem.Type, typeof(EnumDynamicItemType));
                }


            }
        }


        /// <summary>
        /// 分组绑定事件
        /// </summary>
        protected void RepeaterModules_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater RepeaterItems = e.Item.FindControl("RepeaterItems") as Repeater;
                Playngo_ClientZone_DynamicModule DynamicModule = e.Item.DataItem as Playngo_ClientZone_DynamicModule;
                int OptionCount = 0;
                BindItemsToPage(RepeaterItems, DynamicModule.ID, out OptionCount);

                HyperLink hlDynamicModuleEdit = e.Item.FindControl("hlDynamicModuleEdit") as HyperLink;
                if (hlDynamicModuleEdit != null)
                {
                    hlDynamicModuleEdit.Attributes.Add("data-href", DynamicModule_IframeUrl(DynamicModule.ID));
                }

                #region "动态项添加按钮绑定"


                HyperLink hlDynamicItemAddText = e.Item.FindControl("hlDynamicItemAddText") as HyperLink;
                if (hlDynamicItemAddText != null)
                {
                    hlDynamicItemAddText.Attributes.Add("data-href", DynamicItem_IframeUrl(0, DynamicModule.ID, "Text"));
                }

                HyperLink hlDynamicItemAddImage = e.Item.FindControl("hlDynamicItemAddImage") as HyperLink;
                if (hlDynamicItemAddImage != null)
                {
                    hlDynamicItemAddImage.Attributes.Add("data-href", DynamicItem_IframeUrl(0, DynamicModule.ID, "Image"));
                }

                HyperLink hlDynamicItemAddImageText = e.Item.FindControl("hlDynamicItemAddImageText") as HyperLink;
                if (hlDynamicItemAddImageText != null)
                {
                    hlDynamicItemAddImageText.Attributes.Add("data-href", DynamicItem_IframeUrl(0, DynamicModule.ID, "ImageText"));
                }

                HyperLink hlDynamicItemAddVideo = e.Item.FindControl("hlDynamicItemAddVideo") as HyperLink;
                if (hlDynamicItemAddVideo != null)
                {
                    hlDynamicItemAddVideo.Attributes.Add("data-href", DynamicItem_IframeUrl(0, DynamicModule.ID, "Video"));
                }

                HyperLink hlDynamicItemAddiFrame = e.Item.FindControl("hlDynamicItemAddiFrame") as HyperLink;
                if (hlDynamicItemAddiFrame != null)
                {
                    hlDynamicItemAddiFrame.Attributes.Add("data-href", DynamicItem_IframeUrl(0, DynamicModule.ID, "xFrame"));
                }

                #endregion


            }
        }







        #endregion
    }
}