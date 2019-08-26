using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 下载文件(批量)
    /// </summary>
    public class ServiceDownloadFiles : iService
    {
        public ServiceDownloadFiles()
        {
            IsResponseWrite = false;
        }


        public string ResponseString
        {
            get;
            set;
        }

        /// <summary>
        /// 是否写入输出
        /// </summary>
        public bool IsResponseWrite
        {
            get;
            set;
        }

        public void Execute(BasePage Context)
        {

            var FileList = new List<Playngo_ClientZone_DownloadFile>();

 
            //将文件列表取出
            String FileIds = WebHelper.GetStringParam(Context.Request, "FileIds", "");
            if (!String.IsNullOrEmpty(FileIds))
            {
                FileIds = FileIds.Trim(',');
                if (!String.IsNullOrEmpty(FileIds))
                {
                    FileList = Playngo_ClientZone_DownloadFile.FindAllByIds(FileIds);
                }   
            }

            //待打包的文件路径
            var FilePathList = new List<String>();

            if (FileList != null && FileList.Count > 0)
            {
                //将文件的路径收集着
                foreach (var FileItem in FileList)
                {
                    if (FileItem != null && FileItem.ID > 0 && !String.IsNullOrEmpty(FileItem.Options))
                    {

                        if (Context.IsPreRoleView(FileItem.Per_AllUsers, FileItem.Per_Roles) && Context.IsPreJurisdictionView(FileItem.Per_AllJurisdictions, FileItem.Per_Jurisdictions))
                        {

                            var FileItemOptions = ConvertTo.Deserialize<List<KeyValueEntity>>(FileItem.Options);
                            if (FileItemOptions != null)
                            {
                                TemplateFormat xf = new TemplateFormat(Context);
                                String UploadFile = xf.GetFilePath(xf.ViewItemSettingT<String>(FileItem.Options, "UploadFile", ""), Context);
                                if (!String.IsNullOrEmpty(UploadFile))
                                {
                                    UploadFile = Context.Server.MapPath(UploadFile);
                                    if (File.Exists(UploadFile) && !FilePathList.Exists(r => r.ToLower() == UploadFile.ToLower()))
                                    {
                                        FilePathList.Add(UploadFile);
                                    }
                                }

                            }
                        }
                        else
                        {
                           //没有权限下载
                        }
                    }
                }


                if (FilePathList != null && FilePathList.Count > 0)
                {

                    //整理压缩包文件路径等信息
                    String ZipPath = Context.Server.MapPath(String.Format("{0}ClientZone/temporary/download-{1}files-{2}.zip", Context.PortalSettings.HomeDirectory, FilePathList.Count, DateTime.Now.ToString("yyyyMMdd-Hms")));
                    FileInfo ZipFile = new FileInfo(ZipPath);
                    if (!ZipFile.Directory.Exists)
                    {
                        ZipFile.Directory.Create();
                    }
                    else
                    {
                        //清楚很多天前的文件
                        FileSystemUtils.ClearFiles(ZipFile.Directory, 1);
                    }

                    //创建压缩包
                    FileSystemUtils.CreateZipFiles(FilePathList, ZipPath);


                    //将压缩好的文件列表下载
                    FileSystemUtils.DownloadFile(ZipPath, Path.GetFileName(ZipPath));

                }
                else
                {
                    IsResponseWrite = true;
                    ResponseString = "No files found.";
                }

            }




        }

        




    }
}