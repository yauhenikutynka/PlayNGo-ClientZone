
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 生成缩略图
    /// </summary>
    public class ServiceThumbnail : iService
    {
        public ServiceThumbnail()
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

            //根据ID查询出缩略的方式
            Int32 PhotoID = WebHelper.GetIntParam(Context.Request, "ID", 0);
            Int32 Width = WebHelper.GetIntParam(Context.Request, "width", 200);
            Int32 height = WebHelper.GetIntParam(Context.Request, "height", 200);
            String Mode = WebHelper.GetStringParam(Context.Request, "mode", "AUTO");


            String ImagePath = Context.MapPath(String.Format("{0}/Resource/images/no_image.png", Context.TemplateSourceDirectory));

            Boolean isPush = false;

            if (PhotoID > 0)
            {
                Playngo_ClientZone_Files PhotoItem = Playngo_ClientZone_Files.FindByID(PhotoID);
                if (PhotoItem != null && PhotoItem.ID > 0)
                {
                    //拼接图片的地址
                    //ImagePath = String.Format("{0}ClientZone\\{1}\\{2}\\{3}", PortalSettings.HomeDirectoryMapPath, PhotoItem.ModuleId, PhotoItem.AlbumID, PhotoItem.FileName);
                    ImagePath = Context.MapPath(String.Format("{0}{1}", Context.PortalSettings.HomeDirectory, PhotoItem.FilePath));


                    String ThumbnailPath = String.Format("{0}ClientZone\\thumbnails\\{1}\\{2}_{3}_{4}_{5}.{6}", Context.PortalSettings.HomeDirectoryMapPath, PhotoItem.ModuleId, PhotoItem.ID, Width, height, Mode, PhotoItem.FileExtension);

                    FileInfo ThumbnailFile = new FileInfo(ThumbnailPath);

                    if (!ThumbnailFile.Directory.Exists)
                    {
                        ThumbnailFile.Directory.Create();
                    }


                    isPush = true;

                    if (ThumbnailFile.Exists)
                    {
                        Context.Response.Clear();
                        Context.Response.AddHeader("content-disposition", "attachment;filename=" + Context.Server.UrlEncode(ThumbnailFile.Name));
                        Context.Response.AddHeader("content-length", ThumbnailFile.Length.ToString());
                        Context.Response.ContentType = PhotoItem.FileMate;
                        Context.Response.Charset = "utf-8";
                        Context.Response.ContentEncoding = Encoding.UTF8;
                        Context.Response.WriteFile(ThumbnailFile.FullName);
                        Context.Response.Flush();
                        Context.Response.End();
                    }
                    else
                    {
                        GenerateThumbnail.PushThumbnail(ImagePath, ThumbnailPath, Width, height, Mode);
                    }


                }
            }


            if (!isPush)
            {
                FileInfo NoFile = new FileInfo(ImagePath);

                Context.Response.Clear();
                Context.Response.AddHeader("content-disposition", "attachment;filename=" + Context.Server.UrlEncode(NoFile.Name));
                Context.Response.AddHeader("content-length", NoFile.Length.ToString());
                Context.Response.ContentType = FileSystemUtils.GetContentType(NoFile.Extension.Replace(".", ""));
                Context.Response.Charset = "utf-8";
                Context.Response.ContentEncoding = Encoding.UTF8;
                Context.Response.WriteFile(NoFile.FullName);
                Context.Response.Flush();
                Context.Response.End();
            }
        }
 



    }
}