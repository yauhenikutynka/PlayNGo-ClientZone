using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Common.Utilities;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Localization;
using System.Web.UI;


namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 控件操作类
    /// </summary>
    public class ControlHelper
    {

        private Int32 _ModuleId = 0;
        /// <summary>
        /// 模块编号
        /// </summary>
        public Int32 ModuleId
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }
        }


        private String _IconPath = "Template_Effect";
        /// <summary>
        /// 图标路径,默认是Skins
        /// </summary>
        public String IconPath
        {
            get { return _IconPath; }
            set { _IconPath = value; }
        }

        private BaseModule _baseModule = new BaseModule();
        /// <summary>
        /// 基础类
        /// </summary>
        public BaseModule BaseModule
        {
            get {return _baseModule; }
            set { _baseModule = value; }
        }



        public ControlHelper(Int32 __ModuleId)
        {
            ModuleId = __ModuleId;
        }


        public ControlHelper(BaseModule __baseModule)
        {
            ModuleId = __baseModule.ModuleId;
            BaseModule = __baseModule;
        }



        #region "--关于控件格式化--"

        /// <summary>
        /// 显示输入控件内容
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <returns></returns>
        public Control ViewControl(SettingEntity FieldItem)
        {
            String ControlName = ViewControlName(FieldItem);
            String ControlID = ViewControlID(FieldItem);
            Control CreateControl = new Control();
            String ControlHtml = String.Empty;//控件的HTML
            if (String.Equals(FieldItem.ControlType , EnumControlType.CheckBox.ToString(), StringComparison.OrdinalIgnoreCase))
                ControlHtml = ViewCreateCheckBox(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.CheckBoxList.ToString(), StringComparison.OrdinalIgnoreCase))
                ControlHtml = ViewCreateCheckBoxList(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.DatePicker.ToString(), StringComparison.OrdinalIgnoreCase))
                ControlHtml = ViewCreateDatePicker(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.ColorPicker.ToString(), StringComparison.OrdinalIgnoreCase))
                ControlHtml = ViewCreateColorPicker(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.IconPicker.ToString(), StringComparison.OrdinalIgnoreCase))
                ControlHtml = ViewCreateIconPicker(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.DropDownList.ToString(), StringComparison.OrdinalIgnoreCase))
                ControlHtml = ViewCreateDropDownList(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.FileUpload.ToString(), StringComparison.OrdinalIgnoreCase))
                ControlHtml = ViewCreateFileUpload(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.Urls.ToString(), StringComparison.OrdinalIgnoreCase))
                CreateControl = ViewCreateUrls(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.Images.ToString(), StringComparison.OrdinalIgnoreCase))
                CreateControl = ViewCreateImages(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.Label.ToString(), StringComparison.OrdinalIgnoreCase))
                ControlHtml = ViewCreateLabel(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.ListBox.ToString(), StringComparison.OrdinalIgnoreCase))
                ControlHtml = ViewCreateListBox(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.RadioButtonList.ToString(), StringComparison.OrdinalIgnoreCase))
                ControlHtml = ViewCreateRadioButtonList(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.RichTextBox.ToString(), StringComparison.OrdinalIgnoreCase))
                ControlHtml = ViewCreateRichTextBox(FieldItem, ControlName, ControlID);
            else if (String.Equals(FieldItem.ControlType, EnumControlType.TextBox.ToString(), StringComparison.OrdinalIgnoreCase) || FieldItem.ControlType.IndexOf("text") >= 0)
                ControlHtml = ViewCreateTextBox(FieldItem, ControlName, ControlID);

            if (!String.IsNullOrEmpty(ControlHtml))
            {
                Literal liHtml = new Literal();
                liHtml.Text = ControlHtml;
                CreateControl = liHtml;
            }

            return CreateControl;



        }


        #region "创建HTML控件方法集合"
        /// <summary>
        /// 创建TextBox
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <returns></returns>
        public String ViewCreateTextBox(SettingEntity FieldItem, String ControlName, String ControlID)
        {

            StringBuilder ControlHtml = new StringBuilder();//控件的HTML

            //看行数决定控件的是什么控件
            if (FieldItem.Rows > 1)
            {
                ControlHtml.AppendFormat("<textarea  name=\"{0}\" id=\"{1}\"", ControlName, ControlID);

                //if (!String.IsNullOrEmpty(FieldItem.ToolTip)) ControlHtml.AppendFormat(" title=\"{0}\"", FieldItem.ToolTip);

                ControlHtml.AppendFormat(" class=\"form-control input_text {0}\"", ViewVerification(FieldItem));

                ControlHtml.AppendFormat(" style=\"width:{0}px;height:{1}px;\" rows=\"{2}\"", FieldItem.Width, FieldItem.Rows * 20, FieldItem.Rows);

                ControlHtml.Append(" >");

                //默认值
                if (!String.IsNullOrEmpty(FieldItem.DefaultValue)) ControlHtml.Append(FieldItem.DefaultValue);

                ControlHtml.Append("</textarea>");
            }
            else
            {
                ControlHtml.AppendFormat("<input type=\"text\" name=\"{0}\" id=\"{1}\"", ControlName, ControlID);

                //if (!String.IsNullOrEmpty(FieldItem.ToolTip)) ControlHtml.AppendFormat(" title=\"{0}\"", FieldItem.ToolTip);

                ControlHtml.AppendFormat(" class=\"form-control  input_text {0}\"", ViewVerification(FieldItem));

                ControlHtml.AppendFormat(" style=\"width:{0}px;\"", FieldItem.Width);

                if (!String.IsNullOrEmpty(FieldItem.DefaultValue)) ControlHtml.AppendFormat(" value=\"{0}\"", FieldItem.DefaultValue);

                ControlHtml.Append(" />");
            }

            return ControlHtml.ToString();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewCreateCheckBox(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            StringBuilder ControlHtml = new StringBuilder();//控件的HTML

            ControlHtml.AppendFormat("<label class=\"checkbox-inline\" for=\"{0}\">", ControlID);
      
            ControlHtml.AppendFormat("<input type=\"checkbox\" name=\"{0}\" id=\"{1}\"", ControlName, ControlID);

            ControlHtml.AppendFormat(" class=\"square-green grey {0}\"", ViewVerification(FieldItem));

            if (!String.IsNullOrEmpty(FieldItem.DefaultValue))
            {
                Boolean DefaultValue, b; DefaultValue = b = false;
                if (FieldItem.DefaultValue == "1" || FieldItem.DefaultValue.Equals(Boolean.TrueString, StringComparison.CurrentCultureIgnoreCase))
                    DefaultValue = true;
                else if (FieldItem.DefaultValue == "0" || FieldItem.DefaultValue.Equals(Boolean.FalseString, StringComparison.CurrentCultureIgnoreCase))
                    DefaultValue = false;
                else if (Boolean.TryParse(FieldItem.DefaultValue.ToLower(), out b))
                    DefaultValue = b;

                if (DefaultValue) ControlHtml.Append("checked=\"checked\"");
            }


            ControlHtml.Append(" />");

            //提示的关键字用作是后面的描述
            //if (!String.IsNullOrEmpty(FieldItem.Description))
            //{
            //    ControlHtml.Append( FieldItem.Description);
            //}

            ControlHtml.Append("</label>");

            //提示的关键字用作是后面的描述
            //if (!String.IsNullOrEmpty(FieldItem.ToolTip))
            //{
            //    ControlHtml.AppendFormat("<label for=\"{0}\" title=\"{1}\" style=\"display:inline;\">{1}</label>", ControlID, FieldItem.ToolTip);
            //}
            //if (!String.IsNullOrEmpty(FieldItem.Description))
            //{
            //    ControlHtml.AppendFormat("<label for=\"{0}\" title=\"{1}\" style=\"display:inline;\">{1}</label>", ControlID, FieldItem.Description);
            //}

            //ControlHtml.Append("</div>");

            return ControlHtml.ToString();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewCreateCheckBoxList(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            StringBuilder ControlHtml = new StringBuilder();//控件的HTML
            ControlHtml.AppendFormat("<span id=\"{1}\" name=\"{0}\" class=\"auto\">", ControlName, ControlID);



            if (!String.IsNullOrEmpty(FieldItem.ListContent))
            {
                List<String> list = Common.GetList(FieldItem.ListContent.Replace(",", "|").Replace("\r\n", "|").Replace("\r", "|"), "|");
                List<String> DefaultListValue = Common.GetList(FieldItem.DefaultValue);
                for (Int32 i = 0; i < list.Count; i++)
                {
                    if (!String.IsNullOrEmpty(list[i]))
                    {
                        String OptionText = list[i];
                        String  OptionValue = list[i];
                         //判断是否包含有键值对,将键值对分离开
                        if (list[i].IndexOf(":") >= 0)
                        {
                            List<String> ItemKeyValue = Common.GetList(list[i], ":");
                            OptionText = ItemKeyValue[0];
                            OptionValue = ItemKeyValue[1];

                            //如果有图标就检测,没有就忽略
                            OptionText = FormatIcon(OptionText);
                        }


                        ControlHtml.AppendFormat("<label  for=\"{0}_{1}\"  class=\"checkbox-inline\" >", ControlID, i);

                        String CheckedStr = DefaultListValue.Count > 0 && DefaultListValue.Contains(OptionValue) ? "checked=\"checked\"" : "";

                        ControlHtml.AppendFormat("<input id=\"{1}_{2}\" type=\"checkbox\" name=\"{0}\"", ControlName, ControlID, i);

                        if (FieldItem.Required) ControlHtml.AppendFormat(" class=\"square-green grey {0}\" ", "validate[minCheckbox[1]]");

                        ControlHtml.AppendFormat("value=\"{0}\" {1} />", OptionValue, CheckedStr);

                        ControlHtml.AppendFormat(" {0}</label>", OptionText);

                        if (FieldItem.Direction == EnumControlDirection.Vertical.ToString()) ControlHtml.Append("<br />");
                    }
                }
            }

            ControlHtml.Append(" </span>");
            return ControlHtml.ToString();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewCreateDatePicker(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            StringBuilder ControlHtml = new StringBuilder();//控件的HTML
            ControlHtml.Append(ViewCreateTextBox(FieldItem, ControlName, ControlID));

            ControlHtml.Append("<script type=\"text/javascript\">");
            ControlHtml.Append("jQuery(document).ready(function(){").AppendLine();
            ControlHtml.Append("	    jQuery(function() {").AppendLine();
            ControlHtml.AppendFormat("		    jQuery(\"#{0}\").datepicker({{showButtonPanel: true,changeMonth: true,changeYear: true}});", ControlID).AppendLine();
            ControlHtml.Append("	    });").AppendLine();
            ControlHtml.Append("});").AppendLine();
            ControlHtml.Append("</script>");
            return ControlHtml.ToString();
        }

        
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewCreateColorPicker(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            StringBuilder ControlHtml = new StringBuilder();//控件的HTML


            
          
            Boolean IsRgba = false;
            String ControlClass= "color-picker-auto";
            if(!String.IsNullOrEmpty(FieldItem.ListContent))
            {
                if (FieldItem.ListContent == "rgba")
                {
                    IsRgba = true;
                    ControlClass = "color-picker-rgba-auto";
                }
                else if (FieldItem.ListContent == "rgb")
                {
                    IsRgba = true;
                    ControlClass = "color-picker-rgb-auto";
                }
            }


            if (String.IsNullOrEmpty(FieldItem.DefaultValue))
            {
                if(IsRgba)
                {
                    FieldItem.DefaultValue = "rgb(0,0,0,1)";
                }else
                {
                    FieldItem.DefaultValue = "#ffffff";
                }
            }

            

            


            ControlHtml.Append("<div class=\"input-group\">");
            ControlHtml.AppendFormat("<label class=\"input-group-addon color-picker-box tooltips\" for=\"{0}\" style=\"background-color: {1};\"> <i class=\"clip-eyedropper-2 \" ></i> </label>", ControlID, FieldItem.DefaultValue);


            ControlHtml.AppendFormat("<input type=\"text\" name=\"{0}\" id=\"{1}\"", ControlName, ControlID);

            //if (!String.IsNullOrEmpty(FieldItem.ToolTip)) ControlHtml.AppendFormat(" title=\"{0}\"", FieldItem.ToolTip);

            ControlHtml.AppendFormat(" class=\"form-control {1} input_text {0}\"", ViewVerification(FieldItem), ControlClass);

            if (IsRgba) ControlHtml.AppendFormat(" data-color-format=\"{0}\"", FieldItem.ListContent);

            ControlHtml.AppendFormat(" style=\"width:{0}px;\"", FieldItem.Width -40);

            if (!String.IsNullOrEmpty(FieldItem.DefaultValue)) ControlHtml.AppendFormat(" value=\"{0}\"", FieldItem.DefaultValue);


            ControlHtml.Append(" />");


            ControlHtml.Append("</div>");





            //ControlHtml.Append("<div class=\"input-group colorpicker-component\" data-color=\"rgb(81, 145, 185)\" data-color-format=\"rgb\">");
            //ControlHtml.Append(ViewCreateTextBox(FieldItem, ControlName, ControlID));
            //ControlHtml.Append("<span class=\"input-group-addon\"><i style=\"background-color: rgb(81, 145, 185)\"></i></span>");
            //ControlHtml.Append("</div>");
            return ControlHtml.ToString();
        }

         /// <summary>
        /// 创建
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewCreateIconPicker(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            StringBuilder ControlHtml = new StringBuilder();//控件的HTML


            ControlHtml.AppendFormat("<select id=\"{0}\" name=\"{1}\" class=\"form-control search-select search-select-auto {2}\" ", ControlID, ControlName, ViewVerification(FieldItem));


            ControlHtml.AppendFormat(" style=\"width:{0}px;\" >", FieldItem.Width);
 

            List<OptionItem> Optins = new List<OptionItem>();

            if (String.IsNullOrEmpty(FieldItem.ListContent))//没有列表项的时候,需要加载默认的字体
            {
                String xml_url = BaseModule.Server.MapPath( String.Format("{0}Resource/xml/SelectIcons.xml", BaseModule.ModulePath));
                XmlFormat xf = new XmlFormat(xml_url);

                Optins =  xf.ToList<OptionItem>();
            }
            else if (!String.IsNullOrEmpty(FieldItem.ListContent))
            {
                List<String> list = Common.GetList(FieldItem.ListContent.Replace(",", "|").Replace("\r\n", "|").Replace("\r", "|"), "|");

                for (Int32 i = 0; i < list.Count; i++)
                {
                    if (!String.IsNullOrEmpty(list[i]))
                    {
                        OptionItem Optin = new OptionItem();
                        Optin.Text = list[i];
                        Optin.Value = list[i];
                        //判断是否包含有键值对,将键值对分离开
                        if (list[i].IndexOf(":") >= 0)
                        {
                            List<String> ItemKeyValue = Common.GetList(list[i], ":");
                            Optin.Text = ItemKeyValue[0];
                            Optin.Value = ItemKeyValue[1];
                        }
                        Optins.Add(Optin);
                    }
                }
            }


            foreach (var Optin in Optins)
            {
                String DisabledStr = String.IsNullOrEmpty(Optin.Value) ? "disabled=\"disabled\"" : "";
                String CheckedStr = !String.IsNullOrEmpty(Optin.Value) && FieldItem.DefaultValue.IndexOf(Optin.Value, StringComparison.CurrentCultureIgnoreCase) >= 0 ? "selected=\"selected\"" : "";
                ControlHtml.AppendFormat("<option value=\"{0}\" {2} {3}> {1}</option>", Optin.Value, Optin.Text, CheckedStr, DisabledStr).AppendLine();
            }

            ControlHtml.Append("</select>");

            return ControlHtml.ToString();
        }


        
        

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewCreateDropDownList(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            StringBuilder ControlHtml = new StringBuilder();//控件的HTML
            ControlHtml.AppendFormat("<select name=\"{0}\" id=\"{1}\"", ControlName, ControlID);

            ControlHtml.AppendFormat(" style=\"width:{0}px;\"", FieldItem.Width);

            ControlHtml.AppendFormat(" class=\"form-control  input_text {0}\">", ViewVerification(FieldItem));

            if (!String.IsNullOrEmpty(FieldItem.ListContent))
            {
                List<String> list = Common.GetList(FieldItem.ListContent.Replace(",", "|").Replace("\r\n", "|").Replace("\r", "|"), "|");
           
                for (Int32 i = 0; i < list.Count; i++)
                {
                    if (!String.IsNullOrEmpty(list[i]))
                    {
                        String OptionText = list[i].Trim();
                        String OptionValue = list[i].Trim();
                        //判断是否包含有键值对,将键值对分离开
                        if (list[i].IndexOf(":") >= 0)
                        {
                            List<String> ItemKeyValue = Common.GetList(list[i], ":");
                            OptionText = ItemKeyValue[0].Trim();
                            OptionValue = ItemKeyValue[1].Trim();
                        }

                        String CheckedStr = FieldItem.DefaultValue.IndexOf(OptionValue, StringComparison.CurrentCultureIgnoreCase) >= 0 ? "selected=\"selected\"" : "";
                        ControlHtml.AppendFormat("<option {0} value=\"{1}\">{2}</option>", CheckedStr, OptionValue, OptionText);

                    }
                }
            }
            ControlHtml.Append(" </select>");
            return ControlHtml.ToString();
        }


         
 






        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewCreateFileUpload(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            StringBuilder ControlHtml = new StringBuilder();//控件的HTML
            ControlHtml.AppendFormat("<input type=\"file\" name=\"{0}\" id=\"{1}\"", ControlName, ControlID);

            //if (!String.IsNullOrEmpty(FieldItem.ToolTip)) ControlHtml.AppendFormat(" title=\"{0}\"", FieldItem.ToolTip);

            ControlHtml.AppendFormat(" class=\"file-input validate[{0}ajax[ajaxUpload]]\"", FieldItem.Required ? "required," : "");

            ControlHtml.AppendFormat(" style=\"width:{0}px;\"", FieldItem.Width < 250 ? 250 : FieldItem.Width);

            ControlHtml.Append(" />");
            return ControlHtml.ToString();
        }

        /// <summary>
        /// 创建URLs
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <param name="ControlID"></param>
        /// <returns></returns>
        public Control ViewCreateUrls(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            string ContentSrc = BaseModule.ResolveClientUrl(string.Format("{0}Resource_RichUrls.ascx", BaseModule.ModulePath));
            if (System.IO.File.Exists(BaseModule.  MapPath(ContentSrc)))
            {
                Resource_RichUrls ManageContent = new Resource_RichUrls();
                ManageContent = (Resource_RichUrls)BaseModule.LoadControl(ContentSrc);
                ManageContent.ModuleConfiguration = BaseModule.ModuleConfiguration;
                ManageContent.ID = ControlID;
                ManageContent.LocalResourceFile = Localization.GetResourceFile(ManageContent.Control, string.Format("{0}.resx", "Resource_RichUrls.ascx"));
                ManageContent.FieldItem = FieldItem;
                return ManageContent;
            }
            return new Literal();
        }



        /// <summary>
        /// 创建Images
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <param name="ControlID"></param>
        /// <returns></returns>
        public Control ViewCreateImages(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            string ContentSrc = BaseModule.ResolveClientUrl(string.Format("{0}Resource_MultiImages.ascx", BaseModule.ModulePath));
            if (System.IO.File.Exists(BaseModule.MapPath(ContentSrc)))
            {
                Resource_MultiImages ManageContent = new Resource_MultiImages();
                ManageContent = (Resource_MultiImages)BaseModule.LoadControl(ContentSrc);
                ManageContent.ModuleConfiguration = BaseModule.ModuleConfiguration;
                ManageContent.ID = ControlID;
                ManageContent.LocalResourceFile = Localization.GetResourceFile(ManageContent.Control, string.Format("{0}.resx", "Resource_MultiImages.ascx"));
                ManageContent.FieldItem = FieldItem;
                return ManageContent;
            }
            return new Literal();
        }

         

        private void ViewCreateListTree_Load(object sender, EventArgs e)
        {
            if (BaseModule.IsPostBack)
            {
                ListBox lb = (ListBox)sender;
                
                String urllink = WebHelper.GetStringParam(BaseModule.Request,lb.UniqueID,"");
                if (BaseModule.HttpContext.Items.Contains(lb.ID))
                {
                  BaseModule.HttpContext.Items[lb.ID] = urllink;
                }
                else
                {
                    BaseModule.HttpContext.Items.Add(lb.ID, urllink);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewCreateLabel(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            StringBuilder ControlHtml = new StringBuilder();//控件的HTML

            ControlHtml.AppendFormat("<span  name=\"{0}\" id=\"{1}\"", ControlName, ControlID);
            //if (!String.IsNullOrEmpty(FieldItem.ToolTip)) ControlHtml.AppendFormat(" title=\"{0}\"", FieldItem.ToolTip);

            String DisplayValue =  FieldItem.DefaultValue;
            //需要解析Label中不同的标签含义
            if (!String.IsNullOrEmpty(DisplayValue))
            {
                if (DisplayValue.IndexOf("{SitemapUrl}", StringComparison.CurrentCultureIgnoreCase) >= 0)//Sitemap 地址的替换
                {
                    //TemplateFormat xf = new TemplateFormat(BaseModule);
                    //DisplayValue = Common.ReplaceNoCase(DisplayValue, "{SitemapUrl}", String.Format("<a href=\"{0}\" title=\"sitemap url\" target=\"_blank\">{0}</a>  ", xf.GoSitemapUrl()));
                }
            }


            ControlHtml.AppendFormat(">{0}</span>", DisplayValue);
            return ControlHtml.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewCreateListBox(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            StringBuilder ControlHtml = new StringBuilder();//控件的HTML
            ControlHtml.AppendFormat("<select name=\"{0}\" id=\"{1}\"", ControlName, ControlID);

            ControlHtml.AppendFormat(" class=\"form-control input_text {0}\"", ViewVerification(FieldItem));

            ControlHtml.AppendFormat(" style=\"width:{0}px;\"", FieldItem.Width);

            ControlHtml.AppendFormat(" size=\"{0}\" multiple=\"multiple\">", FieldItem.Rows);

            if (!String.IsNullOrEmpty(FieldItem.ListContent))
            {
                List<String> list = Common.GetList(FieldItem.ListContent.Replace(",", "|").Replace("\r\n", "|").Replace("\r", "|"), "|");
                List<String> DefaultListValue = Common.GetList(FieldItem.DefaultValue);
                for (Int32 i = 0; i < list.Count; i++)
                {
                    if (!String.IsNullOrEmpty(list[i]))
                    {

                        String OptionText = list[i];
                        String OptionValue = list[i];
                        //判断是否包含有键值对,将键值对分离开
                        if (list[i].IndexOf(":") >= 0)
                        {
                            List<String> ItemKeyValue = Common.GetList(list[i], ":");
                            OptionText = ItemKeyValue[0];
                            OptionValue = ItemKeyValue[1];
                        }

                            
                            String CheckedStr = DefaultListValue.Count > 0 && DefaultListValue.Contains(OptionValue) ? "selected=\"selected\"" : "";
                            String DisabledStr = String.IsNullOrEmpty(OptionValue) ? "disabled=\"disabled\"" : "";
                            ControlHtml.AppendFormat("<option {0} {1} value=\"{2}\">{3}</option>", CheckedStr,DisabledStr, OptionValue, OptionText);
                    }
                }
            }
            ControlHtml.Append(" </select>");
            return ControlHtml.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewCreateRadioButtonList(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            StringBuilder ControlHtml = new StringBuilder();//控件的HTML
            ControlHtml.AppendFormat("<span id=\"{0}\"  class=\"{1} auto\" >", ControlID, ViewVerification(FieldItem));


            if (!String.IsNullOrEmpty(FieldItem.ListContent))
            {
                List<String> list = Common.GetList(FieldItem.ListContent.Replace(",", "|").Replace("\r\n", "|").Replace("\r", "|"), "|");
                for (Int32 i = 0; i < list.Count; i++)
                {
                    if (!String.IsNullOrEmpty(list[i]))
                    {
                        String OptionText = list[i];
                        String OptionValue = list[i];
                        String OptionImage = String.Empty;
                        //判断是否包含有键值对,将键值对分离开
                        if (list[i].IndexOf(":") >= 0)
                        {
                            List<String> ItemKeyValue = Common.GetList(list[i], ":");
                            OptionText = ItemKeyValue[0];
                            OptionValue = ItemKeyValue[1];

                            //如果有图标就检测,没有就忽略
                            OptionText = FormatIcon(OptionText);

                        }

                        String CheckedStr = FieldItem.DefaultValue.IndexOf(OptionValue, StringComparison.CurrentCultureIgnoreCase) >= 0 ? "checked=\"checked\"" : "";
                        ControlHtml.AppendFormat("<label  class=\"radio-inline\" for=\"{1}_{2}\" ><input id=\"{1}_{2}\"  type=\"radio\" name=\"{0}\" value=\"{3}\" {4} /> {5}</label>", ControlName, ControlID, i, OptionValue, CheckedStr, OptionText);
                        if (FieldItem.Direction == EnumControlDirection.Vertical.ToString()) ControlHtml.Append("<br />");

                    }
                }
            }

            ControlHtml.Append(" </span>");
            return ControlHtml.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewCreateRichTextBox(SettingEntity FieldItem, String ControlName, String ControlID)
        {
            StringBuilder ControlHtml = new StringBuilder();//控件的HTML

            ControlHtml.AppendFormat("<textarea  name=\"{0}\" id=\"{1}\"", ControlName, ControlID);

            //if (!String.IsNullOrEmpty(FieldItem.ToolTip)) ControlHtml.AppendFormat(" title=\"{0}\"", FieldItem.ToolTip);

            ControlHtml.AppendFormat(" class=\"form-control input_text {0}\"", ViewVerification(FieldItem));

            ControlHtml.AppendFormat(" style=\"width:{0}px;height:{1}px;\" rows=\"{2}\"", FieldItem.Width, FieldItem.Rows * 30, FieldItem.Rows);

            ControlHtml.Append(" >");

            //默认值
            if (!String.IsNullOrEmpty(FieldItem.DefaultValue)) ControlHtml.Append(FieldItem.DefaultValue);

            ControlHtml.Append("</textarea>");


            ControlHtml.Append("<script type=\"text/javascript\">").AppendLine();
            ControlHtml.Append("jQuery(function ($) {").AppendLine();
            ControlHtml.AppendFormat(" CKEDITOR.replace('{0}', {{ height: '{1}px',width:'{2}px' }});", ControlID, FieldItem.Rows * 30, FieldItem.Width).AppendLine();
            ControlHtml.Append(" });").AppendLine();
            ControlHtml.Append("</script>").AppendLine();


            //ControlHtml.Append(ViewCreateTextBox(FieldItem, ControlName, ControlID));
             

            return ControlHtml.ToString();
        }

        /// <summary>
        /// 验证字符
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <returns></returns>
        public String ViewVerification(SettingEntity FieldItem)
        {
            //无验证时退出
            //if (FieldItem.Verification == EnumVerification.optional.ToString() && !FieldItem.Required) return "";

            String custom = String.Empty;
            if (!String.IsNullOrEmpty(FieldItem.Verification) && FieldItem.Verification != EnumVerification.optional.ToString())
            {
                if (!(("DropDownList,ListBox,RadioButtonList,FileUpload,CheckBox,CheckBoxList,Label").IndexOf(FieldItem.ControlType) >= 0))
                {
                    custom = String.Format("custom[{0}]", FieldItem.Verification);
                }
            }
            if (!String.IsNullOrEmpty(custom))
            {
                if (!FieldItem.Required)
                    return String.Format("validate[{0}]", String.IsNullOrEmpty(custom) ? "optional" : custom);
                else
                    return String.Format("validate[required,{0}]", custom);
            }
            else
            {
                if (FieldItem.Required)
                    return "validate[required]";
                else
                    return "";
            }

        }


        #endregion

 
        #endregion


        #region 获取Form传值
        /// <summary>
        /// 获取Form传值
        /// </summary>
        /// <param name="fieldItem"></param>
        /// <returns></returns>
        public static String GetWebFormValue(SettingEntity fieldItem, BaseModule bpm)
        {
            String WebFormValue = String.Empty;

            //创建控件的Name和ID
            ControlHelper ControlItem = new ControlHelper(bpm.ModuleId);
            String ControlName = ControlItem.ViewControlName(fieldItem);
            String ControlID = ControlItem.ViewControlID(fieldItem);

            if (fieldItem.ControlType == EnumControlType.CheckBox.ToString())
            {
                WebFormValue = WebHelper.GetStringParam(HttpContext.Current.Request, ControlName, "");
                WebFormValue = !String.IsNullOrEmpty(WebFormValue) && WebFormValue == "on" ? "true" : "false";
            }
            else if (fieldItem.ControlType == EnumControlType.FileUpload.ToString())
            {
                HttpPostedFile hpFile = HttpContext.Current.Request.Files[ControlName];
                if (hpFile != null && hpFile.ContentLength > 0) WebFormValue = String.Format("Url://{0}", FileSystemUtils.UploadFile(hpFile, bpm));                //存放到目录中，并返回
            }
            else if (fieldItem.ControlType == EnumControlType.Urls.ToString())
            {
                String ClientName = GetRichUrlsName(fieldItem);
                if (bpm.HttpContext.Items.Contains(ClientName))
                {
                    WebFormValue = Convert.ToString( bpm.HttpContext.Items[ClientName]);
                }
            }
            else if (fieldItem.ControlType == EnumControlType.Images.ToString())
            {
                String ClientName = GetMultiImagesName(fieldItem);
                if (bpm.HttpContext.Items.Contains(ClientName))
                {
                    WebFormValue = Convert.ToString(bpm.HttpContext.Items[ClientName]);
                }
            }
            else if (fieldItem.ControlType == EnumControlType.ListTree_Categorys.ToString())
            {
                String ClientName = String.Format("ListTree{0}", fieldItem.Name);
                if (bpm.HttpContext.Items.Contains(ClientName))
                {
                    WebFormValue = Convert.ToString(bpm.HttpContext.Items[ClientName]);
                }
            }
            else if (fieldItem.ControlType == EnumControlType.Label.ToString())
            {
                WebFormValue = fieldItem.DefaultValue;
            }
            else
            {
                WebFormValue = WebHelper.GetStringParam(HttpContext.Current.Request, ControlName, "");
            }
            return WebFormValue;

        }

        /// <summary>
        /// 获取URLs控件的名称
        /// </summary>
        /// <param name="fieldItem"></param>
        /// <returns></returns>
        public static String GetRichUrlsName(SettingEntity fieldItem)
        {
            return String.Format("RichUrls{0}", fieldItem.Name).Replace("_", "").Replace("-", "");
        }


        public static String GetMultiImagesName(SettingEntity fieldItem)
        {
            return String.Format("MultiImages{0}", fieldItem.Name).Replace("_", "").Replace("-", "");
        }

        #endregion


        #region "辅助的方法"


        /// <summary>
        /// 显示标题控件
        /// </summary>
        /// <param name="FieldItem">显示字段</param>
        /// <param name="Suffix">后缀名</param>
        /// <returns></returns>
        public String ViewLable(SettingEntity FieldItem, String Suffix)
        {
            String ControlName = ViewControlID(FieldItem);
            return String.Format("<label for=\"{0}\">{1}{2}</label>", ControlName, FieldItem.Alias, Suffix);
        }
        /// <summary>
        /// 显示标题控件
        /// </summary>
        /// <param name="FieldItem"></param>
        /// <returns></returns>
        public String ViewLable(SettingEntity FieldItem)
        {
            return ViewLable(FieldItem, ""); ;
        }

        /// <summary>
        /// 显示控件名
        /// </summary>
        /// <param name="FieldItem">字段</param>
        /// <returns></returns>
        public String ViewControlName(SettingEntity FieldItem)
        {
            return String.Format("Ctl${0}${1}", FieldItem.Name, ModuleId);
        }

        public String ViewControlID(SettingEntity FieldItem)
        {
            return String.Format("Ctl_{0}_{1}", FieldItem.Name, ModuleId);
        }

        /// <summary>
        /// 检测并过滤图标
        /// </summary>
        /// <param name="OptionText"></param>
        /// <returns></returns>
        public String FormatIcon(String OptionText)
        {
            if (!String.IsNullOrEmpty(OptionText) && OptionText.IndexOf("[*]") >= 0)
            {
                List<String> TextImages = Common.GetList(OptionText, "[*]");
                OptionText = FormatIcon(TextImages[0],TextImages[1]);
            }
            return OptionText;
        }
        /// <summary>
        /// 过滤图标
        /// </summary>
        /// <param name="_Text"></param>
        /// <param name="_Icon"></param>
        /// <returns></returns>
        public String FormatIcon(String _Text,String _Icon)
        {


            String ImagePath = String.Format("{0}Resource/images/{1}", BaseModule.ModulePath, _Icon);
          
            return String.Format("<img src=\"{0}\" class=\"img-responsive tooltips\" title=\"{1}\" data-placement=\"top\" data-rel=\"tooltip\" data-original-title=\"{1}\" />", ImagePath, _Text);
        }



        #endregion
    }
}