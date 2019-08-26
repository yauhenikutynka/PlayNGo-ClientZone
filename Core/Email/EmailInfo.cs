using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using System.Collections;
using DotNetNuke.Entities.Host;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 邮箱实体
    /// </summary>
    public class EmailInfo
    {
        /// <summary>
        /// 模块配置
        /// </summary>
        public Hashtable Settings
        {
            get;
            set;
        }


        private Boolean _SMTPEnableSSL = false;
        /// <summary>
        /// 保存SMTP的属性
        /// </summary>
        public bool SMTPEnableSSL
        {
            get
            {
                return _SMTPEnableSSL;
            }

            set
            {
                _SMTPEnableSSL = value;
            }
        }


        public string SMTPServer
        {
            get;
            set;
        }


        public string SMTPAuthentication
        {
            get;
            set;
        }

        public string SMTPUsername
        {
            get;
            set;
        }

        public string SMTPPassword
        {
            get;
            set;
        }



        /// <summary>
        /// 发送给**邮箱
        /// </summary>
        public string MailTo
        {
            get;
            set;
        }

        public string MailCC
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject
        {
            get;
            set;
        }
        /// <summary>
        /// 发件人
        /// </summary>
        public string MailFrom
        {
            get;
            set;
        }


        private String _Attachments = "";
        /// <summary>
        /// 附件
        /// </summary>
        public String Attachments
        {
            get { return _Attachments; }
            set { _Attachments = value; }
        }

        public EmailInfo()
        {

        }

        public EmailInfo(string mailTo, string content, string subject, string mailFrom, Hashtable settings)
        {
            MailTo = mailTo;
            Content = content;
            Subject = subject;
            MailFrom = mailFrom;
            Settings = settings;
        }

        public EmailInfo(string mailTo, string content, string subject)
        {
            MailTo = mailTo;
            Content = content;
            Subject = subject;
        }

        public EmailInfo(EmailInfo mInfo)
        {
            MailTo = mInfo.MailTo;
            Content = mInfo.Content;
            Subject = mInfo.Subject;
            MailFrom = mInfo.MailFrom;
        }


        /// <summary>
        /// 推送参数到这里
        /// </summary>
        public void PushSettings()
        {

            SMTPEnableSSL = Host.EnableSMTPSSL;
            SMTPServer = Host.SMTPServer;
            SMTPAuthentication = Host.SMTPAuthentication;
            SMTPUsername = Host.SMTPUsername;
            SMTPPassword = Host.SMTPPassword;

             


        }


    }
}
