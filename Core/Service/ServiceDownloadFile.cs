

using DotNetNuke.Services.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public class ServiceDownloadFile : iService
    {
        public ServiceDownloadFile()
        {
            IsResponseWrite = true;
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
            Int32 FileId = WebHelper.GetIntParam(Context.Request, "FileId", 0);
            if (FileId > 0)
            {
                var FileItem = Playngo_ClientZone_DownloadFile.FindByKeyForEdit(FileId);
                if (FileItem != null && FileItem.ID > 0 && !String.IsNullOrEmpty(FileItem.Options))
                {
                    if (Context.IsPreRoleView(FileItem.Per_AllUsers, FileItem.Per_Roles) && Context.IsPreJurisdictionView(FileItem.Per_AllJurisdictions, FileItem.Per_Jurisdictions))
                    {
                        var FileItemOptions = ConvertTo.Deserialize<List<KeyValueEntity>>(FileItem.Options);
                        if (FileItemOptions != null)
                        {
                            TemplateFormat xf = new TemplateFormat(Context);
                            Playngo_ClientZone_Files Multimedia = new Playngo_ClientZone_Files();
                            String UploadFile = xf.GetFilePath(xf.ViewItemSettingT<String>(FileItem.Options, "UploadFile", ""), Context,out Multimedia);
                            if (!String.IsNullOrEmpty(UploadFile))
                            {
                                UploadFile = Context.Server.MapPath(UploadFile);
                                if (File.Exists(UploadFile))
                                {
                                    String FileName = String.Format("{0}.{1}", Multimedia.Name, Multimedia.FileExtension);
                                    //FileSystemUtils.DownloadFile(UploadFile,String.Format("{0}_{1}{2}", FileItem.Title, FileItem.Version,  Path.GetExtension(UploadFile)));
                                    //FileSystemUtils.DownloadFile(UploadFile);

                                    FileManager file = new FileManager();

                                    file.WriteFileToResponse(new System.IO.FileInfo(UploadFile), ContentDisposition.Attachment);
                                    IsResponseWrite = false;
                                }
                            }
                            else
                            {
                                //当前文件找不到
                            }


                        }
                        else
                        {
                            //没有添加文件
                        }


                    }
                    else
                    {
                        //没有权限下载
                    }


                       
                }
                else
                {
                    ResponseString = "没找到数据怎么搞";
                }
            }
            else
            {
                ResponseString = "传过来的文件编号都不对";
            }
             
        }



 



    }
}