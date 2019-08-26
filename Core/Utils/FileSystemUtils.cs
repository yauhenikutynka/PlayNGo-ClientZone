using System;
using System.Web;
using System.IO;

using DotNetNuke.Entities.Host;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections.Generic;
using DotNetNuke.Common.Utilities;

namespace Playngo.Modules.ClientZone
{
    public class FileSystemUtils : DotNetNuke.Common.Utilities.FileSystemUtils
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="FileLoc">文件真实路径</param>
        /// <param name="FileName">显示文件名</param>
        public static void DownloadFile(string FileLoc, string FileName)
        {
            System.IO.FileInfo objFile = new System.IO.FileInfo(FileLoc);
            System.Web.HttpResponse objResponse = System.Web.HttpContext.Current.Response;
            string truefilename = objFile.Name;
            if (HttpContext.Current.Request.UserAgent.IndexOf("; MSIE ") > 0)
            {
                truefilename = HttpUtility.UrlEncode(truefilename, System.Text.Encoding.UTF8);
            }
            if (objFile.Exists)
            {
                objResponse.ClearContent();
                objResponse.ClearHeaders();
                objResponse.AppendHeader("content-disposition", "attachment; filename=\"" + HttpUtility.UrlEncode(objFile.Name) + "\"");
                objResponse.AppendHeader("Content-Length", objFile.Length.ToString());
                objResponse.ContentType = GetContentType(objFile.Extension.Replace(".", ""));
                WriteFile(objFile.FullName);
                objResponse.Flush();
                objResponse.End();
            }
        }




        public static void WriteFile(string strFileName)
        {
            System.Web.HttpResponse objResponse = System.Web.HttpContext.Current.Response;
            System.IO.Stream objStream = null;
            try
            {
                objStream = new System.IO.FileStream(strFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                WriteStream(objResponse, objStream);
            }
            catch (Exception ex)
            {
                objResponse.Write("Error : " + ex.Message);
            }
            finally
            {
                if (objStream != null)
                {
                    objStream.Close();
                    objStream.Dispose();
                }
            }
        }

        private static void WriteStream(HttpResponse objResponse, Stream objStream)
        {
            byte[] bytBuffer = new byte[1024*1000];
            int intLength;
            long lngDataToRead;
            try
            {
                lngDataToRead = objStream.Length;
                while (lngDataToRead > 0)
                {
                    if (objResponse.IsClientConnected)
                    {
                        intLength = objStream.Read(bytBuffer, 0, 1024 * 1000);
                        objResponse.OutputStream.Write(bytBuffer, 0, intLength);
                        objResponse.Flush();

                        lngDataToRead = lngDataToRead - intLength;
                    }
                    else
                    {
                        lngDataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.Write("Error : " + ex.Message);
            }
            finally
            {
                if (objStream != null)
                {
                    objStream.Close();
                    objStream.Dispose();
                }
            }
        }

        /// <summary>
        /// 获取资源文件夹中的内容
        /// </summary>
        /// <param name="PathName">文件夹名</param>
        /// <param name="FileName">文件名</param>
        /// <param name="pmb">当前模块对象</param>
        /// <returns></returns>
        public static String Resource(String PathName, String FileName, BaseModule pmb)
        {
            return String.Format("{0}{1}/{2}", pmb.ModulePath, PathName, FileName);
        }

        /// <summary>
        /// 创建空文本文件
        /// </summary>
        /// <param name="FullFileName"></param>
        public static void CreateText(String FullFileName)
        {
            FileInfo file = new FileInfo(FullFileName);
            if (!file.Exists)
            {
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }


                File.CreateText(FullFileName);
            }
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="hpFile"></param>
        /// <param name="pmb"></param>
        /// <returns></returns>
        public static String UploadFile(HttpPostedFile httpFile, BaseModule pmb)
        {

            String FileName = httpFile.FileName;
            if (FileName.IndexOf(@"\") >= 0) FileName = FileName.Substring(FileName.LastIndexOf(@"\"), FileName.Length - FileName.LastIndexOf(@"\")).Replace(@"\", "");

            String Extension = Path.GetExtension(FileName).Replace(".", "");


            //构造保存路径
            String FileUrl = FileName;
            FileInfo file = new FileInfo(pmb.MapPath(String.Format("~/Portals/{0}/ClientZone/{1}/{2}", pmb.PortalId, pmb.ModuleId, FileName)));
            if (!file.Directory.Exists) file.Directory.Create();

            int ExistsCount = 1;
            //检测文件名是否存在
            while (file.Exists)
            {
                FileUrl = String.Format("{0}_{1}.{2}", FileName.Replace("." + Extension, ""), ExistsCount, Extension);
                file = new FileInfo(pmb.MapPath(String.Format("~/Portals/{0}/ClientZone/{1}/{2}", pmb.PortalId, pmb.ModuleId, FileUrl)));
                ExistsCount++;
            }

            //保存文件到文件夹
            httpFile.SaveAs(file.FullName);

            return FileUrl;
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="hpFile"></param>
        /// <param name="pmb"></param>
        /// <returns></returns>
        public static String UploadFile(HttpPostedFile httpFile, BasePage pmb)
        {

            String FileName = httpFile.FileName;
            if (FileName.IndexOf(@"\") >= 0) FileName = FileName.Substring(FileName.LastIndexOf(@"\"), FileName.Length - FileName.LastIndexOf(@"\")).Replace(@"\", "");

            String Extension = Path.GetExtension(FileName).Replace(".", "");


            //构造保存路径
            String FileUrl = FileName;
            FileInfo file = new FileInfo(pmb.MapPath(String.Format("~/Portals/{0}/ClientZone/{1}/{2}", pmb.PortalId, pmb.ModuleId, FileName)));
            if (!file.Directory.Exists) file.Directory.Create();

            int ExistsCount = 1;
            //检测文件名是否存在
            while (file.Exists)
            {
                FileUrl = String.Format("{0}_{1}.{2}", FileName.Replace("." + Extension, ""), ExistsCount, Extension);
                file = new FileInfo(pmb.MapPath(String.Format("~/Portals/{0}/ClientZone/{1}/{2}", pmb.PortalId, pmb.ModuleId, FileUrl)));
                ExistsCount++;
            }

            //保存文件到文件夹
            httpFile.SaveAs(file.FullName);

            return FileUrl;
        }
 

        /// <summary>
        /// 保存XML到文件
        /// </summary>
        /// <param name="XmlName">XML文件名</param>
        /// <param name="XmlContent">XML内容</param>
        /// <param name="pmb"></param>
        /// <returns></returns>
        public static String SaveXmlToFile(String XmlName, String XmlContent, BaseModule pmb)
        {
            String FileFullName = String.Format("{0}ClientZone\\Export\\{1}", pmb.PortalSettings.HomeDirectoryMapPath, XmlName);

            FileInfo XmlFile = new FileInfo(FileFullName);

            if (!XmlFile.Directory.Exists) XmlFile.Directory.Create();

            using (StreamWriter sw = new StreamWriter(FileFullName))
            {
                sw.Write(XmlContent);
                sw.Flush();
                sw.Close();

                return FileFullName;
            }

        }


        public static bool CheckValidFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            //regex matches a dot followed by 1 or more chars followed by a semi-colon
            //regex is meant to block files like "foo.asp;.png" which can take advantage
            //of a vulnerability in IIS6 which treasts such files as .asp, not .png
            return !string.IsNullOrEmpty(extension)
                   && Host.AllowedExtensionWhitelist.IsAllowedExtension(extension)
                   && !DotNetNuke.Common.Globals.FileExtensionRegex.IsMatch(fileName);
        }

        public static string GetContentType(string extension)
        {
            return new DotNetNuke.Services.FileSystem.FileManager().GetContentType(extension);
        }



        /// <summary>
        /// 创建压缩包
        /// </summary>
        /// <param name="files"></param>
        /// <param name="ZipFile"></param>
        public static void CreateZipFiles(List<String> files, String ZipFile)
        {
            FileStream strmZipFile = null;

            try
            {

                strmZipFile = File.Create(ZipFile);
                ZipOutputStream strmZipStream = null;
                try
                {
                    strmZipStream = new ZipOutputStream(strmZipFile);
                    strmZipStream.SetLevel(5);

                    //Add Files To zip
                    AddFilesToZip(strmZipStream, files, "");

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (strmZipStream != null)
                    {
                        strmZipStream.Finish();
                        strmZipStream.Close();
                    }
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (strmZipFile != null)
                {
                    strmZipFile.Close();
                }
            }

        }



        private static void AddFilesToZip(ZipOutputStream stream, List<String> files, string basePath)
        {
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    FileSystemUtils.AddToZip(ref stream, file, Path.GetFileName(file), basePath);
                }
            }
        }

        /// <summary>
        /// 清理路径下的文件
        /// </summary>
        /// <param name="Directory"></param>
        /// <param name="Daysago"></param>
        public static void ClearFiles(DirectoryInfo Directory, Int32 Daysago = 30)
        {
            var Files = Directory.GetFiles();
            if (Files != null && Files.Length > 0)
            {
                try
                {
                    foreach (var file in Files)
                    {
                        if (file.Exists && file.LastWriteTime < DateTime.Now.AddDays(-Daysago))
                        {
                            file.Delete();
                        }
                    }
                }
                catch { }

            }
        }

        /// <summary>
        /// 获取系统允许的扩展
        /// </summary>
        /// <returns></returns>
        public static List<String> GetFileExtensionList()
        {
            return (List<String>)Host.AllowedExtensionWhitelist.AllowedExtensions;
        }

        /// <summary>
        /// 获取系统允许的扩展
        /// </summary>
        /// <returns></returns>
        public static String GetFileExtensions()
        {
            List<String> Extensions = new List<string>();
            var AllowedExtensions = (List<String>)Host.AllowedExtensionWhitelist.AllowedExtensions;
            if (AllowedExtensions != null && AllowedExtensions.Count > 0)
            {

                foreach (var AllowedExtension in AllowedExtensions)
                {
                    if (!(AllowedExtension.IndexOf(".", StringComparison.CurrentCultureIgnoreCase) >= 0))
                    {
                        Extensions.Add(String.Format(".{0}", AllowedExtension));
                    }
                    else
                    {
                        Extensions.Add( AllowedExtension);
                    }
                }
            }
            else
            {
                Extensions = AllowedExtensions;
            }
            return Common.GetStringByList(Extensions);
        }

    }
}
