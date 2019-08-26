using DotNetNuke.Entities.Host;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_Settings_Email : BaseModule
    {


        #region "==属性=="

        /// <summary>
        /// 模块操作类
        /// </summary>
        private static ModuleController controller = new ModuleController();




        /// <summary>提示操作类</summary>
        MessageTips mTips = new MessageTips();

        /// <summary>
        /// 模板名称
        /// </summary>
        public String TemplateName = WebHelper.GetStringParam(HttpContext.Current.Request, "TemplateName", "Admin.Notification");


        public String TemplateLanguage
        {
            //get { return WebHelper.GetStringParam(HttpContext.Current.Request, "TemplateLanguage", language); }
            get { return "en-US"; }
        }

      




        #endregion


        #region "==方法=="

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindDataToPage()
        {

            //Dictionary<string, Locale> Locales = DotNetNuke.Services.Localization.LocaleController.Instance.GetLocales(PortalId);
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("<div class=\"btn-group btn-group-xs\">").AppendLine();
            
 
            //foreach (var locale in Locales)
            //{
            //    sb.AppendFormat("<a href=\"{0}\" class=\"btn btn-default {2}\">{1}</a>", xUrl("TemplateLanguage", locale.Key, Token, "TemplateName="+ TemplateName), locale.Value.NativeName, locale.Key.ToLower() == TemplateLanguage.ToLower() ? "active":"").AppendLine();
            //}
            //sb.Append("</div>").AppendLine();
            //liLanguageLinks.Text = sb.ToString();


            //需要读取模版的设置
            Playngo_ClientZone_MailSetting Template = Playngo_ClientZone_MailSetting.FindByModuleId(ModuleId, TemplateName, TemplateLanguage);
            if (!(Template != null && Template.ID > 0))
            {
                Template = Playngo_ClientZone_MailSetting.FindByModuleId(ModuleId, TemplateName);//需要读取英语的数据库
            }
 
            if (!(Template != null && Template.ID > 0))
            {
                EmailTemplate eTemplate = new EmailTemplate(TemplateName, this, TemplateLanguage);//没有记录时，需要读取XML模板的信息
                Template.Name = eTemplate.TemplateName;
                Template.language = eTemplate.Language;
                Template.MailSubject = eTemplate.Title;
                Template.MailBody = eTemplate.Body;
                Template.MailType = eTemplate.MailType;
                Template.MailTo = eTemplate.MailTo;
                Template.MailCC = eTemplate.MailCC;
                Template.MailTime = eTemplate.MailTime;
            }


            //需要显示时间框
            if (Template.Name == "User.Notification.Upcoming")
            {
                divMailTime.Visible = true;
            }

            


            cbEnable.Checked  = Template.Status == 1;

            lbShowTemplateName.Text = TemplateName;
            lbShowLanguage.Text = TemplateLanguage;

            txtMailSubject.Text = Template.MailSubject;
            txtMailBody.Text = Template.MailBody;

            txtMailCC.Text = Template.MailCC;
            txtMailTo.Text = Template.MailTo;
            txtMailTime.Text = Template.MailTime.ToString();
           

        }






        /// <summary>
        /// 设置数据项
        /// </summary>
        private void SetDataItem()
        {



            #region "邮件设置"


            Playngo_ClientZone_MailSetting Template = Playngo_ClientZone_MailSetting.FindByModuleId(ModuleId, TemplateName, TemplateLanguage);
            if (!(Template != null && Template.ID > 0))
            {
                Template = Playngo_ClientZone_MailSetting.FindByModuleId(ModuleId, TemplateName);//需要读取英语的数据库
            }

            if (!(Template != null && Template.ID > 0))
            {
                EmailTemplate eTemplate = new EmailTemplate(TemplateName, this, TemplateLanguage);//没有记录时，需要读取XML模板的信息
                Template.MailType = eTemplate.MailType;
            }


            Template.MailSubject = txtMailSubject.Text;
            Template.MailBody = txtMailBody.Text;

            Template.MailCC = txtMailCC.Text  ;
            Template.MailTo = txtMailTo.Text  ;
             

            Template.MailTime = WebHelper.GetIntParam(Request, txtMailTime.UniqueID,0);

            Template.Status = cbEnable.Checked ? 1 : 0;


            Template.LastIP = WebHelper.UserHost;
            Template.LastTime = xUserTime.UtcTime();
            Template.LastUser = UserId;

            Template.ModulePath = ModulePath;
            

            if (Template != null && Template.ID > 0)
            {
                Template.Update();
            }
            else
            {
                Template.language = TemplateLanguage;
                Template.Name = TemplateName;

                
                Template.CreateTime = xUserTime.LocalTime(); 
                Template.CreateUser = UserId;
                Template.PortalId = PortalId;
                Template.ModuleId = ModuleId;
                Template.TabID = PortalSettings.ActiveTab.TabID;
                Template.Insert();
            }

            #endregion

            //初始化提醒调度器
            if(!ViewSettingT<Boolean>("NotificationSchedule_Init1", false))
            {
                NotificationSchedule Schedule = new NotificationSchedule();
                Schedule.UpdateScheduler(this);

                UpdateModuleSetting("NotificationSchedule_Init1", "true");
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

                mTips.LoadMessage("UpdateSettingsSuccess", EnumTips.Success, this, new String[] { "" });

                //refresh cache
                SynchronizeModule();

                Response.Redirect(xUrl("TemplateLanguage", TemplateLanguage,Token, "TemplateName=" + TemplateName), false);
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

                Response.Redirect(xUrl("TemplateLanguage", TemplateLanguage, Token, "TemplateName=" + TemplateName), false);
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        #endregion









    }
}