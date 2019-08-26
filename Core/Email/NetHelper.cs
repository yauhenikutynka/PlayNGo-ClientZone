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
using System.Net.Mail;
using DotNetNuke.Services.Mail;
using System.Collections;
using System.Text;
using DotNetNuke.Common.Utilities;
using System.Collections.Generic;


namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 网络相关公用类
    /// </summary>
    public class NetHelper
    {

        #region "邮件发送"
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="toemail"></param>
        /// <param name="mailFrom"></param>
        /// <returns></returns>
        public static string SendMail(string subject, string content, string toemail, string mailFrom, Hashtable settings)
        {
            EmailInfo mailInfo = new EmailInfo(toemail, content, subject, mailFrom, settings);

            return SendMail(mailInfo);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="toemail"></param>
        /// <returns></returns>
        public static string SendMail(string subject, string content, string toemail)
        {
            EmailInfo mailInfo = new EmailInfo(toemail, content, subject);

            return SendMail(mailInfo);
        }




 



        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailInfo"></param>
        /// <returns></returns>
        public static string SendMail(EmailInfo mailInfo)
        {
            Hashtable Settings = mailInfo.Settings;

          

    
            Boolean SMTPEnableSSL = mailInfo.SMTPEnableSSL;

            string HostEmail = null;
            HostEmail = Settings["EmalSetting.OutboxAddress"] != null && !string.IsNullOrEmpty(Settings["EmalSetting.OutboxAddress"].ToString()) ? Convert.ToString(Settings["EmalSetting.OutboxAddress"]) : DotNetNuke.Entities.Host.Host.HostEmail;

           

            String MailFrom = String.IsNullOrEmpty(mailInfo.MailFrom) ? HostEmail : mailInfo.MailFrom;

            string statue = null;

            if (!String.IsNullOrEmpty(mailInfo.MailTo))//if (!String.IsNullOrEmpty(mailInfo.MailTo) && Mail.IsValidEmailAddress(mailInfo.MailTo, Null.NullInteger))
            {
                if (SMTPEnableSSL)
                {
                    if (!String.IsNullOrEmpty(mailInfo.Attachments))
                    {

                        statue = Mail.SendMail(MailFrom, mailInfo.MailTo, "", "", DotNetNuke.Services.Mail.MailPriority.Normal, mailInfo.Subject, MailFormat.Html, Encoding.UTF8, mailInfo.Content, mailInfo.Attachments, mailInfo.SMTPServer, mailInfo.SMTPAuthentication, mailInfo.SMTPUsername, mailInfo.SMTPPassword, SMTPEnableSSL);

                    }
                    else
                    {

                        statue = Mail.SendMail(MailFrom, mailInfo.MailTo, "", "", "", DotNetNuke.Services.Mail.MailPriority.Normal, mailInfo.Subject, MailFormat.Html, Encoding.UTF8, mailInfo.Content, new List<System.Net.Mail.Attachment>(), mailInfo.SMTPServer, mailInfo.SMTPAuthentication, mailInfo.SMTPUsername, mailInfo.SMTPPassword, SMTPEnableSSL);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(mailInfo.Attachments))
                    {
                        statue = Mail.SendMail(MailFrom, mailInfo.MailTo, "", mailInfo.Subject, mailInfo.Content, mailInfo.Attachments, "html", mailInfo.SMTPServer, mailInfo.SMTPAuthentication, mailInfo.SMTPUsername, mailInfo.SMTPPassword);
                    }
                    else
                    {
                        statue = Mail.SendMail(MailFrom, mailInfo.MailTo, "", mailInfo.Subject, mailInfo.Content, "", "html", mailInfo.SMTPServer, mailInfo.SMTPAuthentication, mailInfo.SMTPUsername, mailInfo.SMTPPassword);
                    }

                    //statue = Mail.SendMail(MailFrom, mailInfo.MailTo, "", mailInfo.Subject, mailInfo.Content, mailInfo.Attachments, "html", "", "", "", "");
                }



            }
            return statue;

        }



 


 

        #endregion


        /// <summary>
        /// 替换字符串(不区分大小写)
        /// </summary>
        /// <param name="realaceValue"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceNoCase(string realaceValue, string oldValue, string newValue)  //不区分大小写替换
        {
            return Microsoft.VisualBasic.Strings.Replace(realaceValue, oldValue, newValue, 1, -1, Microsoft.VisualBasic.CompareMethod.Text);
        }

    }
}
