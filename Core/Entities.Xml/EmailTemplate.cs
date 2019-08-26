using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 邮件模版
    /// </summary>
    public class EmailTemplate
    {

        private String _TemplateName = String.Empty;
        /// <summary>
        /// 模版名称
        /// </summary>
        public String TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }


        private String _Language = String.Empty;
        /// <summary>
        /// 模版语言
        /// </summary>
        public String Language
        {
            get { return _Language; }
            set { _Language = value; }
        }


        private String _Title = String.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }


        private String _Body = String.Empty;
        /// <summary>
        /// 内容
        /// </summary>
        public String Body
        {
            get { return _Body; }
            set { _Body = value; }
        }
 
        private Int32 _MailType = 1;
        /// <summary>
        /// 邮件类型
        /// </summary>
        public Int32 MailType
        {
            get { return _MailType; }
            set { _MailType = value; }
        }



        private String _MailTo = String.Empty;
        /// <summary>
        /// 发送给谁
        /// </summary>
        public String MailTo
        {
            get { return _MailTo; }
            set { _MailTo = value; }
        }


        private String _MailCC = String.Empty;
        /// <summary>
        /// 抄送给谁
        /// </summary>
        public String MailCC
        {
            get { return _MailCC; }
            set { _MailCC = value; }
        }


        private Int32 _MailTime = 1;
        /// <summary>
        /// 倒计时
        /// </summary>
        public Int32 MailTime
        {
            get { return _MailTime; }
            set { _MailTime = value; }
        }


        public EmailTemplate(String ___TemplateName, BaseModule baseModule)
              : this(___TemplateName, String.Format("{0}Resource\\xml\\Mail.Template.xml",
            baseModule.MapPath(baseModule.ModulePath)),
            baseModule.language)
        { }


        /// <summary>
        /// 邮件模版构造
        /// </summary>
        /// <param name="___TemplateName">模版名称</param>
        /// <param name="baseModule">基类</param>
        public EmailTemplate(String ___TemplateName, BaseModule baseModule, String __Language = "en-US")
            : this(___TemplateName, String.Format("{0}Resource\\xml\\Mail.Template.xml", 
            baseModule.MapPath(baseModule.ModulePath)),
            __Language)
        { }


        /// <summary>
        /// 邮件模版构造
        /// </summary>
        /// <param name="___TemplateName">模版名称</param>
        /// <param name="FileFullPath">完整路径</param>
        /// <param name="__Language">语言</param>
        public EmailTemplate(String ___TemplateName, String FileFullPath, String __Language = "en-US")
        {
            if (System.IO.File.Exists(FileFullPath))
            {
                var document = XDocument.Parse(Common.ReplaceLowOrderASCIICharacters(System.IO.File.ReadAllText(FileFullPath)));

                var Templates = (

                       from p in document.Descendants("Template")
                       where String.Equals(p.Attribute("Name").Value, ___TemplateName, StringComparison.CurrentCultureIgnoreCase)
                       select new
                       {
                           Title = p.Element("Title").Value,
                           Body = HttpUtility.HtmlDecode(p.Element("Body").Value),
                         
                           Language = p.Attribute("Language").Value,
                           MailTo = p.Attribute("MailTo").Value,
                           MailCC = p.Attribute("MailCC").Value,
                           MailTime = Convert.ToInt32(p.Attribute("MailTime").Value),
                           MailType = Convert.ToInt32( p.Attribute("MailType").Value)
                       }
                        ).ToList();


                if (Templates != null && Templates.Count > 0)
                {
                    var Template = Templates.Find(r => String.Equals(r.Language, __Language, StringComparison.CurrentCultureIgnoreCase));
                    if (!(Template != null && !String.IsNullOrEmpty(Template.Title)))//如果当前语言没有读取相应的，则应该取默认的英文
                    {
                        Template = Templates.Find(r => String.Equals(r.Language, "en-US", StringComparison.CurrentCultureIgnoreCase));
                    }

                    //以下是赋值
                    TemplateName = ___TemplateName;
                    Title = Template.Title;
                    Body = Template.Body;
                    MailTime = Template.MailTime;
                    Language = Template.Language;
                    MailTo = Template.MailTo;
                    MailCC = Template.MailCC;
                    MailType = Template.MailType;
                }




            }
        }


    }
}