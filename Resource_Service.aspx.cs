using DotNetNuke.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Resource_Service : BasePage
    {

        #region "属性"
        /// <summary>
        /// 功能
        /// 文件上传 FileUpload
        /// </summary>
        private String Token = WebHelper.GetStringParam(HttpContext.Current.Request, "Token", "").ToLower();




        private ServiceDB _ServiceDBItem;

        public ServiceDB ServiceDBItem
        {
            get
            {
                if (!(_ServiceDBItem != null && !String.IsNullOrEmpty(_ServiceDBItem.Name)))
                {
                    String XmlPath = MapPath(string.Format("{0}Resource/xml/Service.xml", ModulePath));
                    XmlFormat xf = new XmlFormat(XmlPath);
                    List<ServiceDB> ServiceDBs = xf.ToList<ServiceDB>();
                    if (ServiceDBs != null && ServiceDBs.Count > 0 && ServiceDBs.Exists(r => r.Token.ToLower() == Token.ToLower()))
                    {
                        _ServiceDBItem = ServiceDBs.Find(r => r.Token.ToLower() == Token.ToLower());
                    }
                }
                return _ServiceDBItem;
            }
        }


        #endregion


        protected override void Page_Init(System.Object sender, System.EventArgs e)
        {
            if (!String.IsNullOrEmpty(Token))
            {

                ServiceDB serDB = ServiceDBItem;

                if (serDB != null && !String.IsNullOrEmpty(serDB.Token))
                {
                   

                    //编辑权限验证
                    if (serDB.Validate)
                    {
                        base.Page_Init(sender, e);
                    }
                }



            }



                
                
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!String.IsNullOrEmpty(Token))
                    {

                        ServiceDB serDB = ServiceDBItem;

                        if (serDB != null && !String.IsNullOrEmpty(serDB.Token))
                        {
                            //登录验证
                            if (serDB.Login && UserId <= 0)
                            {
                                Response.Redirect(Globals.LoginURL(HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl), false));
                            }
                            else
                            {
                                //取出需要调用的服务
                                iService Ser = (iService)Activator.CreateInstance(serDB.assemblyName, serDB.typeName).Unwrap();
                                if (Ser != null && !String.IsNullOrEmpty(serDB.Token))
                                {
                                    //执行服务
                                    Ser.Execute(this);

                                    if (!String.IsNullOrEmpty(Ser.ResponseString))
                                    {
                                        if (Ser.IsResponseWrite)
                                        {

                                            //输出字符串
                                            Response.Clear();
                                            Response.Write(Ser.ResponseString);


                                        }
                                        else
                                        {

                                            HttpContext.Current.Response.ContentType = "application/xml";
                                            using (StreamWriter sw = new StreamWriter(HttpContext.Current.Response.OutputStream, Encoding.UTF8))
                                            {
                                                sw.Write(Ser.ResponseString);
                                                sw.Close();
                                                sw.Dispose();
                                                //Response.Flush();
                                                //Response.End();


                                            }




                                        }
                                    }
                                    else
                                    {
                                        //错误,没有输出

                                    }

                                }
                                else
                                {
                                    //没有找到相应的服务
                                }
                            }

                        }
                        else
                        {
                            //没有找到相应的服务
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessPageLoadException(ex);
                Response.Write(String.Format("Exception:{0}", ex.Source));
            }
            finally
            {
                Response.End();
            }
 
        }
    }
}