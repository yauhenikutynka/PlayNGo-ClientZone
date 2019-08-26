using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_ProductRoadmap : BaseModule
    {


        #region "==属性=="
        /// <summary>
        /// 模块操作类
        /// </summary>
        private static ModuleController controller = new ModuleController();





        /// <summary>提示操作类</summary>
        MessageTips mTips = new MessageTips();

        #endregion






        #region "==事件=="

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
                    BindDataItem();
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
        /// 更新文章
        /// </summary>
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {



                String IncludedSectionTexts, IncludedSections = String.Empty;
                WebHelper.GetSelected(cblIncludedSections, out IncludedSectionTexts, out IncludedSections);
                UpdateModuleSetting("Roadmap_IncludedSections", IncludedSections);


                //发布状态和时间
                DateTime oTime = xUserTime.LocalTime().AddDays(-15);
                string[] expectedFormats = { "G", "g", "f", "F" };
                string StartDate = WebHelper.GetStringParam(Request, txtStartDate.UniqueID, oTime.ToString("MM/dd/yyyy"));
                string StartTime = WebHelper.GetStringParam(Request, txtStartTime.UniqueID, oTime.ToString("HH:mm:ss"));
                if (DateTime.TryParseExact(String.Format("{0} {1}", StartDate, StartTime), "MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out oTime))
                {
                    UpdateModuleSetting("Roadmap_StartTime", xUserTime.ServerTime(oTime).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));
                }


                //结束状态和时间
                DateTime EndTime = xUserTime.LocalTime().AddDays(15);
                string DisableDate = WebHelper.GetStringParam(Request, txtDisableDate.UniqueID, EndTime.ToString("MM/dd/yyyy"));
                string DisableTime = WebHelper.GetStringParam(Request, txtDisableTime.UniqueID, EndTime.ToString("HH:mm:ss"));
                if (DateTime.TryParseExact(String.Format("{0} {1}", DisableDate, DisableTime), "MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out EndTime))
                {
                    UpdateModuleSetting("Roadmap_EndTime", xUserTime.ServerTime(EndTime).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));
                }




                mTips.LoadMessage("UpdateSettingsSuccess", EnumTips.Success, this, new String[] { "" });

           

                Response.Redirect(xUrl("ProductRoadmap"), false);
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {

                Response.Redirect(xUrl("ProductRoadmap"), false);
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
            WebHelper.BindList(cblIncludedSections, typeof(EnumIncludedSections));

        
        }


        /// <summary>
        /// 绑定数据项
        /// </summary>
        private void BindDataItem()
        {


            WebHelper.SelectedListMultiByValue(cblIncludedSections, Settings["Roadmap_IncludedSections"] != null  ? Settings["Roadmap_IncludedSections"].ToString() : "0,2,3");


            DateTime StartTime = Settings["Roadmap_StartTime"] != null ? xUserTime.LocalTime(Convert.ToDateTime( Settings["Roadmap_StartTime"].ToString())) : xUserTime.LocalTime().AddDays(-30);
            txtStartDate.Text = StartTime.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            txtStartTime.Text = StartTime.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            DateTime EndTime = Settings["Roadmap_EndTime"] != null ? xUserTime.LocalTime(Convert.ToDateTime(Settings["Roadmap_EndTime"].ToString())) : xUserTime.LocalTime().AddDays(30);
            txtDisableDate.Text = EndTime.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            txtDisableTime.Text = EndTime.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);


        }







        /// <summary>
        /// 绑定选项分组框到页面
        /// </summary>
        private void BindGroupsToPage()
        {
            //获取效果参数
            List<SettingEntity> ItemSettingDB = new List<SettingEntity>();

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
                hlAddNewLink.Attributes.Add("data-href", String.Format("{0}Resource_Masters.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}&Master=Manager_Modal_DynamicModule&Type={5}&LinkID={6}", ModulePath, PortalId, TabId, ModuleId, language, (Int32)EnumDynamicModuleType.ProductRoadmap, 0));

                var DynamicModules = Playngo_ClientZone_DynamicModule.FindListByFilter(0, (Int32)EnumDynamicModuleType.ProductRoadmap, ModuleId);

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