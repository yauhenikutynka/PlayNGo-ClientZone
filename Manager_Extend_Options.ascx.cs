using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using System.IO;
using DotNetNuke.Services.Localization;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_Extend_Options : BaseModule
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
        public String ConfigName = WebHelper.GetStringParam(HttpContext.Current.Request, "ConfigName", "");

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public String ConfigPath
        {
            get { return Server.MapPath(String.Format("{0}Resource/xml/{1}.xml", ModulePath, ConfigName)); }
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
            get {
                if (!(_ExtendConfig != null && !String.IsNullOrEmpty(_ExtendConfig.Name)))
                {
                    String ExtendSettingDBPath = ConfigPath;
                    if (File.Exists(ExtendSettingDBPath))
                    {
                        XmlFormat xf = new XmlFormat(ExtendSettingDBPath);
                        _ExtendConfig = xf.ToItem<ExtendConfigDB>();
                    }
                }
                return _ExtendConfig; }
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
            liExtendConfigAlias.Text = ViewLanguage(ExtendConfig.Name, ExtendConfig.Alias);

            liExtendConfigDescription.Text = ViewLanguage(ExtendConfig.Name, ExtendConfig.Description,"Help");

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
            SetThemeSettings();




        }




        /// <summary>
        /// 设置模版参数的值
        /// </summary>
        private void SetThemeSettings()
        {


            //获取效果参数
            List<SettingEntity> ExtendSettingDB = Setting_ExtendSettingDB;

            if (ExtendSettingDB != null && ExtendSettingDB.Count > 0)
            {
                ControlHelper ControlItem = new ControlHelper(this);
                ControlItem.IconPath = "";

                foreach (SettingEntity ri in ExtendSettingDB)
                {
                    String SettingName =  String.Format("{0}.{1}", ExtendConfig.Name, ri.Name);
                    controller.UpdateModuleSetting(this.ModuleId, SettingName, ControlHelper.GetWebFormValue(ri, this));
                }
            }

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
                ProcessModuleLoadException( ex);
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

                mTips.LoadMessage("UpdateSettingsSuccess", EnumTips.Success, this, new String[] { "" });

                //refresh cache
                SynchronizeModule();

                Response.Redirect(xUrl("ConfigName", ConfigName, Token), false);
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException( ex);
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
                ProcessModuleLoadException( ex);
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

                String SettingKey = String.Format("{0}.{1}", ExtendConfig.Name, ThemeSetting.Name);
                ThemeSetting.DefaultValue = Settings[SettingKey] != null ? Convert.ToString(Settings[SettingKey]) : ThemeSetting.DefaultValue;



                //构造输入控件
                PlaceHolder ThemePH = e.Item.FindControl("ThemePH") as PlaceHolder;

                #region "创建控件"
                ControlHelper ctl = new ControlHelper(this);
                ctl.IconPath = "Extends";

                ThemePH.Controls.Add((Control)ctl.ViewControl(ThemeSetting));
                #endregion

                Literal liTitle = e.Item.FindControl("liTitle") as Literal;
                liTitle.Text = String.Format("<label class=\"col-sm-3 control-label\" for=\"{1}\">{0}:</label>", ViewLanguage(ThemeSetting.Name, !String.IsNullOrEmpty(ThemeSetting.Alias) ? ThemeSetting.Alias : ThemeSetting.Name), ctl.ViewControlID(ThemeSetting));


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
                    liGroupName.Text = ViewLanguage(String.Format("Group_{0}", GroupItem.Key.Replace(" ","")), GroupItem.Key);
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