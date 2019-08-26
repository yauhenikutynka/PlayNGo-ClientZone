using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_Modal_DynamicItem : BaseModule
    {


        #region "==属性=="

        /// <summary>
        /// 模块操作类
        /// </summary>
        private static ModuleController controller = new ModuleController();




        /// <summary>提示操作类</summary>
        MessageTips mTips = new MessageTips();

        /// <summary>
        /// 配置文件名
        /// </summary>
        public String ConfigName = WebHelper.GetStringParam(HttpContext.Current.Request, "ConfigName", "Text");

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public String ConfigPath
        {
            get { return Server.MapPath(String.Format("{0}Resource/xml/Config.DynamicItem.{1}.xml", ModulePath, ConfigName)); }
        }


        private List<SettingEntity> _Setting_ExtendSettingDB = new List<SettingEntity>();
        /// <summary>
        /// 获取扩展设置项
        /// </summary>
        public List<SettingEntity> Setting_ExtendSettingDB
        {
            get
            {
                if (!(_Setting_ExtendSettingDB != null && _Setting_ExtendSettingDB.Count > 0))
                {
                    String ExtendSettingDBPath = ConfigPath;
                    if (File.Exists(ExtendSettingDBPath))
                    {
                        XmlFormat xf = new XmlFormat(ExtendSettingDBPath);
                        _Setting_ExtendSettingDB = xf.ToList<SettingEntity>();
                    }
                }
                return _Setting_ExtendSettingDB;
            }
        }


        private ExtendConfigDB _ExtendConfig = new ExtendConfigDB();
        /// <summary>
        /// 获取当前扩展配置
        /// </summary>
        public ExtendConfigDB ExtendConfig
        {
            get
            {
                if (!(_ExtendConfig != null && !String.IsNullOrEmpty(_ExtendConfig.Name)))
                {
                    String ExtendSettingDBPath = ConfigPath;
                    if (File.Exists(ExtendSettingDBPath))
                    {
                        XmlFormat xf = new XmlFormat(ExtendSettingDBPath);
                        _ExtendConfig = xf.ToItem<ExtendConfigDB>();
                    }
                }
                return _ExtendConfig;
            }
        }

        /// <summary>
        /// 项编号
        /// </summary>
        public Int32 ItemID = WebHelper.GetIntParam(HttpContext.Current.Request, "ID", 0);

        /// <summary>
        /// 链接ID
        /// </summary>
        public Int32 DynamicID = WebHelper.GetIntParam(HttpContext.Current.Request, "DynamicID", 0);


        private Playngo_ClientZone_DynamicItem _DynamicItem;
        /// <summary>
        /// 动态模块项
        /// </summary>
        public Playngo_ClientZone_DynamicItem DynamicItem
        {
            get
            {
                if (!(_DynamicItem != null && _DynamicItem.ID > 0))
                {
                    if (ItemID > 0)
                        _DynamicItem = Playngo_ClientZone_DynamicItem.FindByKeyForEdit(ItemID);
                    else
                        _DynamicItem = new Playngo_ClientZone_DynamicItem();
                }
                return _DynamicItem;
            }
            set { _DynamicItem = value; }
        }



        private List<KeyValueEntity> _ItemSettings;
        /// <summary>
        /// 封装的参数集合
        /// </summary>
        public List<KeyValueEntity> ItemSettings
        {
            get
            {
                if (!(_ItemSettings != null && _ItemSettings.Count > 0))
                {
                    if (DynamicItem != null && DynamicItem.ID > 0 && !String.IsNullOrEmpty(DynamicItem.Options))
                    {
                        try
                        {
                            _ItemSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(DynamicItem.Options);
                        }
                        catch
                        {
                            _ItemSettings = new List<KeyValueEntity>();
                        }
                    }
                    else
                        _ItemSettings = new List<KeyValueEntity>();
                }
                return _ItemSettings;
            }
        }



        /// <summary>
        /// 获取设置的资源文件
        /// </summary>
        public String SettingResourceFile
        {
            get { return ""; }
        }


        #endregion


        #region "==方法=="

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindDataToPage()
        {
 

        }


        /// <summary>
        /// 显示字段标题
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="ClassName"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewTitleSpan(String Content, String ClassName, String ControlName)
        {

            return String.Format("<label  {2}><span {1} >{0}</span></label>",
                        Content,
                        !String.IsNullOrEmpty(ClassName) ? String.Format("class=\"{0}\"", ClassName) : "",
                      !String.IsNullOrEmpty(ControlName) ? String.Format("for=\"{0}\"", ControlName) : ""
                        );
        }


        /// <summary>
        /// 设置数据项
        /// </summary>
        private void SetDataItem()
        {

            Dictionary<String, Object> DynamicItemJsons = new Dictionary<String, Object>();

            Playngo_ClientZone_DynamicItem Dynamic = DynamicItem;


            List<KeyValueEntity> list = new List<KeyValueEntity>();
            if (divOptions.Visible) Dynamic.Options = SetItemSettings(ref list);



            if (list.Exists(r => r.Key == "Title"))
            {
               var TitleItem = list.Find(r => r.Key == "Title");
                Dynamic.Title = TitleItem.Value.ToString();
            }



            //更新项
            Dynamic.LastIP = WebHelper.UserHost;
            Dynamic.LastTime = xUserTime.UtcTime();
            Dynamic.LastUser = UserId;


            if (Dynamic.ID > 0)
            {
                //更新
            }
            else
            {
                //新增
                Dynamic.ModuleId = ModuleId;
                Dynamic.PortalId = PortalId;

                Dynamic.DynamicID = DynamicID;

                if (ConfigName == EnumDynamicItemType.ImageText.ToString())
                {
                    Dynamic.Type = (Int32)EnumDynamicItemType.ImageText;
                }
                else if (ConfigName == EnumDynamicItemType.Video.ToString())
                {
                    Dynamic.Type = (Int32)EnumDynamicItemType.Video;
                }
                else if (ConfigName == EnumDynamicItemType.Image.ToString())
                {
                    Dynamic.Type = (Int32)EnumDynamicItemType.Image;
                }
                else if (ConfigName == EnumDynamicItemType.xFrame.ToString())
                {
                    Dynamic.Type = (Int32)EnumDynamicItemType.xFrame;
                } else
                {
                    Dynamic.Type = (Int32)EnumDynamicItemType.Text;
                }

               


                QueryParam qp = new QueryParam();
                qp.Where.Add(new SearchParam(Playngo_ClientZone_DynamicItem._.ModuleId, ModuleId, SearchType.Equal));
                qp.Where.Add(new SearchParam(Playngo_ClientZone_DynamicItem._.DynamicID, DynamicID, SearchType.Equal));
  
                Dynamic.Sort = Playngo_ClientZone_DynamicItem.FindCount(qp) + 10;
            }


            int ResultDynamic = 0;

            if (Dynamic.ID > 0)
            {
                ResultDynamic = Dynamic.Update();
                DynamicItemJsons.Add("Action", "Update");
            } else
            {
                ResultDynamic = Dynamic.ID = Dynamic.Insert();
                DynamicItemJsons.Add("Action", "Insert");
            }
               
         

            foreach (var Field in Playngo_ClientZone_DynamicItem.Meta.Fields)
            {
                DynamicItemJsons.Add(Field.ColumnName, Dynamic[Field.ColumnName]);
            }

            DynamicItemJsons.Add("TypeText", EnumHelper.GetEnumTextVal(Dynamic.Type, typeof(EnumDynamicItemType)));
            DynamicItemJsons.Add("EditUrl", DynamicItem_IframeUrl(Dynamic.ID, Dynamic.DynamicID, ((EnumDynamicItemType)Dynamic.Type).ToString()));

   

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.MaxJsonLength = Int32.MaxValue;
            String JsonString = jsSerializer.Serialize(DynamicItemJsons);

            Response.Write(String.Format("<script>window.parent.EditDynamicItems({0});</script>", JsonString));
       

        }




        /// <summary>
        /// 拼接数据项的设置参数
        /// </summary>
        /// <returns></returns>
        public String SetItemSettings(ref List<KeyValueEntity> list)
        {
            //获取效果参数
            List<SettingEntity> ItemSettingDB = Setting_ExtendSettingDB;


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


        #region "==事件=="


        /// <summary>
        /// 页面加载事件
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //绑定数据
                    BindDataToPage();
                }
                BindGroupsToPage();
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        /// <summary>
        /// 更新绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 设置需要绑定的方案项
                SetDataItem();

                //mTips.LoadMessage("UpdateSettingsSuccess", EnumTips.Success, this, new String[] { "" });

        
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {

                Response.Redirect(xUrl("ConfigName", ConfigName, Token), false);
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }




        /// <summary>
        /// 绑定选项分组框到页面
        /// </summary>
        private void BindGroupsToPage()
        {
            //获取效果参数
            List<SettingEntity> ItemSettingDB = Setting_ExtendSettingDB;

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
            List<SettingEntity> ItemSettingDB = Setting_ExtendSettingDB;

            if (ItemSettingDB != null && ItemSettingDB.Count > 0)
            {
                ItemSettingDB = ItemSettingDB.FindAll(r1 => r1.Group == Group);
                OptionCount = ItemSettingDB.Count;
                //绑定参数项
                RepeaterOptions.DataSource = ItemSettingDB;
                RepeaterOptions.DataBind();
            }
        }



        protected void RepeaterOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SettingEntity ThemeSetting = e.Item.DataItem as SettingEntity;

                KeyValueEntity KeyValue = ItemSettings.Find(r1 => r1.Key == ThemeSetting.Name);
                if (KeyValue != null && !String.IsNullOrEmpty(KeyValue.Key))
                {
                    ThemeSetting.DefaultValue = KeyValue.Value.ToString();
                }



                //构造输入控件
                PlaceHolder ThemePH = e.Item.FindControl("ThemePH") as PlaceHolder;

                #region "创建控件"
                ControlHelper ctl = new ControlHelper(this);
                ctl.IconPath = "Extends";

                ThemePH.Controls.Add((Control)ctl.ViewControl(ThemeSetting));
                #endregion

                Literal liTitle = e.Item.FindControl("liTitle") as Literal;
                liTitle.Text = String.Format("<label class=\"col-sm-2 control-label\" for=\"{1}\">{0}:</label>", ViewLanguage(ThemeSetting.Name, !String.IsNullOrEmpty(ThemeSetting.Alias) ? ThemeSetting.Alias : ThemeSetting.Name), ctl.ViewControlID(ThemeSetting));


                String Description = ViewLanguage(ThemeSetting.Name, ThemeSetting.Description, "Help");
                if (!String.IsNullOrEmpty(Description))
                {
                    Literal liHelp = e.Item.FindControl("liHelp") as Literal;
                    liHelp.Text = String.Format("<span class=\"help-block\"><i class=\"fa fa-info-circle\"></i> {0}</span>", Description);
                }



            }
        }


        /// <summary>
        /// 分组绑定事件
        /// </summary>
        protected void RepeaterGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater RepeaterOptions = e.Item.FindControl("RepeaterOptions") as Repeater;
                KeyValueEntity GroupItem = e.Item.DataItem as KeyValueEntity;


                Literal liGroupName = e.Item.FindControl("liGroupName") as Literal;
                if (liGroupName != null)
                {
                    liGroupName.Text = ViewLanguage(String.Format("Group_{0}", GroupItem.Key.Replace(" ", "")), GroupItem.Key);
                }

                int OptionCount = 0;
                BindOptionsToPage(RepeaterOptions, GroupItem.Key, out OptionCount);

                if (OptionCount == 0)
                {
                    e.Item.Visible = false;
                }

            }
        }


        #endregion


        #region "语言翻译"

        /// <summary>
        /// 显示多语言
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewLanguage(String Title, String DefaultValue)
        {
            return ViewLanguage(Title, DefaultValue, "Text");
        }

        public String ViewLanguage(String Title, String DefaultValue, String TextType)
        {
            String LocalResourceFile = String.Format("{0}App_LocalResources/{1}.ascx.resx", ModulePath, ConfigName);

            return ViewResourceText(Title, DefaultValue, TextType, LocalResourceFile);
        }


        #endregion






    }
}