using System;
using System.Collections.Generic;
using System.Web;
using CNVelocity;
using CNVelocity.App;
using Commons.Collections;
using CNVelocity.Runtime;
using System.IO;
using CNVelocity.Context;
using System.Text;
using DotNetNuke.Entities.Controllers;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// CNVelocity模板工具类 VelocityHelper
    /// </summary>
    public class VelocityHelper
    {
        private VelocityEngine velocity = null;
        private IContext context = null;
        private BaseModule pmb = new BaseModule();
        private TemplateDB XmlTheme = new TemplateDB();
   

 




        public VelocityHelper(BaseModule _bpm, TemplateDB _Theme)
        {
            XmlTheme = _Theme;
            pmb = _bpm;

            Init(_bpm, _Theme);
        }
 

        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public VelocityHelper() { }






        /// <summary>
        /// 初始话CNVelocity模块
        /// </summary>
        public void Init(BaseModule _bpm, TemplateDB _Theme)
        {
            //创建VelocityEngine实例对象
            velocity = new VelocityEngine();


            //使用设置初始化VelocityEngine
            ExtendedProperties props = new ExtendedProperties();

            props.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, HttpContext.Current.Server.MapPath(String.Format("{0}Templates/{1}/", _bpm.ModulePath,XmlTheme.Name)));
            props.AddProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
            props.AddProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");

            //模板的缓存设置
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_CACHE, false);              //是否缓存
            props.AddProperty("file.resource.loader.modificationCheckInterval", (Int64)600);    //缓存时间(秒)

            velocity.Init(props);

            //为模板变量赋值
            context = new VelocityContext();
        }
         

        /// <summary>
        /// 给模板变量赋值
        /// </summary>
        /// <param name="key">模板变量</param>
        /// <param name="value">模板变量值</param>
        public void Put(string key, object value)
        {
            if (context == null)
                context = new VelocityContext();
            context.Put(key, value);
        }

        /// <summary>
        /// 显示模版
        /// </summary>
        /// <param name="templatFileName"></param>
        /// <returns></returns>
        public String Display(String templatFileName)
        {
            //从文件中读取模板
            Template template = velocity.GetTemplate(templatFileName);
            //添加共用变量
             context.Put("Module", pmb); 
             context.Put("ModuleConfiguration", pmb.ModuleConfiguration); 
             context.Put("UserInfo", pmb.UserInfo);
             context.Put("Portal", pmb.PortalSettings);
            context.Put("Host", HostController.Instance.GetSettingsDictionary());
            context.Put("DateTimeNow", xUserTime.LocalTime());

            StringWriter writer = new StringWriter();
             lock (this)
             {
                 //合并模板
                 template.Merge(context, writer);
             }
            return writer.ToString();
        }



        
    }

}




