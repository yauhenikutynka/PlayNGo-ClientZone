using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Threading;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_Library_Synchronize : BaseModule
    {


        #region "属性"

        /// <summary>
        /// 提示操作类
        /// </summary>
        MessageTips mTips = new MessageTips();


        /// <summary>
        /// 可见性
        /// </summary>
        public Int32 Visibility = WebHelper.GetIntParam(HttpContext.Current.Request, "Visibility", 0);

        #endregion




        #region "事件"



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDataItem();


                   
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }




        protected void cmdSynchronizeFiles_Click(object sender, EventArgs e)
        {
            try
            {
          

               int SynchronizeCount =  SynchronizeAllFiles();
               if (SynchronizeCount > 0)
               {
                   
                   mTips.LoadMessage("SynchronizeFilesSuccess", EnumTips.Success, this, new object[] { SynchronizeCount });
                   Response.Redirect(xUrl("Visibility", Visibility.ToString(), Visibility == 1 ? "LibraryDownloads" : "Library"), false);
               }
               else
               {
                   mTips.LoadMessage("SynchronizeFilesError", EnumTips.Success, this);
                   Response.Redirect(xUrl("Visibility", Visibility.ToString(), "Synchronize"), false);
               }

            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        #endregion




        #region "方法"
        /// <summary>
        /// 绑定页面数据项
        /// </summary>
        public void BindDataItem()
        {
            String WebPath = String.Format("{0}ClientZone/SynchronizeFiles/", PortalSettings.HomeDirectory);
            String ServerPath = MapPath(WebPath);

            DirectoryInfo dir = new DirectoryInfo(ServerPath);
            if (!dir.Exists)
            {
                dir.Create();
            }

            BindFileList(dir);


            labShowWebFolderPath.Text =String.Format("WebSite Root: {0}", WebPath);
            labShowFolderPath.Text = String.Format("Server Path: {0}", ServerPath); ;

            cmdSynchronizeFiles.Attributes.Add("onClick", "javascript:return confirm('" + ViewResourceText("cmdSynchronizeFilesConfirm", "Are you sure you want to synchronize all files?") + "');");

            hlReturnLibrary.NavigateUrl = xUrl("Visibility", Visibility.ToString(),  Visibility == 1 ? "LibraryDownloads" :  "Library");
        }

        /// <summary>
        /// 绑定文件列表
        /// </summary>
        /// <param name="FolderPath"></param>
        public void BindFileList(DirectoryInfo FolderPath)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

           List< FileInfo> SynchronizeFiles = Common.Split<FileInfo>(FolderPath.GetFiles(), 1, Int32.MaxValue); ;


           SynchronizeFiles.RemoveAll(r => r.Name.IndexOf("Thumbs.db", StringComparison.CurrentCultureIgnoreCase) >= 0);

           if (SynchronizeFiles != null && SynchronizeFiles.Count > 0)
            {
                sb.Append("<ul>").AppendLine();
                for (int i = 0; i < SynchronizeFiles.Count && i < 5; i++)
                {
                    FileInfo SynchronizeFile = SynchronizeFiles[i];

                    if (!(SynchronizeFile.Name.IndexOf("Thumbs.db", StringComparison.CurrentCultureIgnoreCase) >= 0))
                    {

                        String FileWebPath = String.Format("ClientZone/SynchronizeFiles/{0}", SynchronizeFile.Name);

                        sb.AppendFormat("<li><img src=\"{1}\" style=\"border-width:0px; max-height:40px;max-width:80px;\"/> {0}</li>", SynchronizeFile.Name, GetPhotoExtension(SynchronizeFile.Extension.Replace(".", ""), FileWebPath)).AppendLine();
                    }
                }

                if (SynchronizeFiles.Count > 5)
                {
                    sb.Append("<li>...</li>").AppendLine();
                }
                sb.Append("</ul>").AppendLine();
                labShowFileCount.Text = String.Format(ViewResourceText("labShowFileCount", "There are {0} files waiting to be imported in current folder."), SynchronizeFiles.Count);
            }
            else
            {
                labShowFileCount.Text = ViewResourceText("labShowFileCountEmpty", "There isn’t any file in  current folder, you can can not click synchronize button.");
                cmdSynchronizeFiles.Enabled = false;
            }

            liShowFileList.Text = sb.ToString();
        }

        /// <summary>
        /// 同步所有的文件
        /// </summary>
        public Int32 SynchronizeAllFiles()
        {
            Int32 SynchronizeFileCount = 0;

            String WebPath = String.Format("{0}ClientZone/SynchronizeFiles/", PortalSettings.HomeDirectory);
            String ServerPath = MapPath(WebPath);

            DirectoryInfo dir = new DirectoryInfo(ServerPath);
            if (!dir.Exists)
            {
                dir.Create();
            }

            List<FileInfo> SynchronizeFiles = Common.Split<FileInfo>(dir.GetFiles(), 1, int.MaxValue);

            foreach (FileInfo SynchronizeFile in SynchronizeFiles)
            {
                SynchronizeFileCount += SynchronizeAllFiles(SynchronizeFile);
            }
            return SynchronizeFileCount;
        }

        public Int32 SynchronizeAllFiles(FileInfo SynchronizeFile)
        {
            Int32 SynchronizeFileCount = 0;


            Playngo_ClientZone_Files PhotoItem = new Playngo_ClientZone_Files();

            PhotoItem.ModuleId = ModuleId;
            PhotoItem.PortalId = PortalId;


            PhotoItem.FileName = SynchronizeFile.Name;
            PhotoItem.FileSize =Convert.ToInt32(SynchronizeFile.Length / 1024);
            PhotoItem.FileMate = WebHelper.leftx(FileSystemUtils.GetContentType(Path.GetExtension(PhotoItem.FileName).Replace(".", "")), 30);

            PhotoItem.FileExtension = System.IO.Path.GetExtension(PhotoItem.FileName).Replace(".", "");
            PhotoItem.Name = System.IO.Path.GetFileName(PhotoItem.FileName).Replace(Path.GetExtension(PhotoItem.FileName), "");

            PhotoItem.Status = (Int32)EnumFileStatus.Approved;

            try
            {
                if (("png,gif,jpg,jpeg,bmp").IndexOf(PhotoItem.FileExtension) >= 0)
                {
                    //图片的流
                    System.Drawing.Image image = System.Drawing.Image.FromFile(SynchronizeFile.FullName);
                    PhotoItem.ImageWidth = image.Width;
                    PhotoItem.ImageHeight = image.Height;

                    PhotoItem.Exif = Common.Serialize<EXIFMetaData.Metadata>(new EXIFMetaData().GetEXIFMetaData(image));
                }
            }
            catch
            {
                 
            }

            PhotoItem.LastTime = xUserTime.UtcTime();
            PhotoItem.LastIP = WebHelper.UserHost;
            PhotoItem.LastUser = UserInfo.UserID;


            PhotoItem.Extension1 = Visibility;


            //根据菜单来选择导入的文件夹
            String SaveDirPath = "uploads";

            if (Visibility == 1)
            {
                SaveDirPath = "downloads";
            }




            //将文件存储的路径整理好
            String fileName = PhotoItem.Name; //文件名称
            String WebPath = String.Format("ClientZone/{0}/{1}/{2}/{3}/", SaveDirPath, PhotoItem.LastTime.Year, PhotoItem.LastTime.Month, PhotoItem.LastTime.Day);
            //检测文件存储路径是否有相关的文件
            FileInfo fInfo = new FileInfo(HttpContext.Current.Server.MapPath(String.Format("{0}{1}{2}.{3}", PortalSettings.HomeDirectory, WebPath, fileName, PhotoItem.FileExtension)));

            //检测文件夹是否存在
            if (!System.IO.Directory.Exists(fInfo.Directory.FullName))
            {
                System.IO.Directory.CreateDirectory(fInfo.Directory.FullName);
            }
            else
            {
                Int32 j = 1;
                while (fInfo.Exists)
                {
                    //文件已经存在了
                    fileName = String.Format("{0}_{1}", PhotoItem.Name, j);
                    fInfo = new FileInfo(HttpContext.Current.Server.MapPath(String.Format("{0}{1}{2}.{3}", PortalSettings.HomeDirectory, WebPath, fileName, PhotoItem.FileExtension)));
                    j++;
                }
            }

            PhotoItem.FilePath = String.Format("{0}{1}.{2}", WebPath, fileName, PhotoItem.FileExtension);
            PhotoItem.FileName = String.Format("{0}.{1}", fileName, PhotoItem.FileExtension);

            try
            {
                if (!fInfo.Directory.Exists)
                {
                    fInfo.Directory.Create();
                }
                //异步移动文件到文件夹中
                List<String> SynchronizeFileQueue = new List<string>();
                SynchronizeFileQueue.Add(SynchronizeFile.FullName);
                SynchronizeFileQueue.Add(fInfo.FullName);
                ManagedThreadPool.QueueUserWorkItem(new WaitCallback(ThreadMoveTo), SynchronizeFileQueue);
            }
            catch (Exception ex)
            {

            }

            //给上传的相片设置初始的顺序
            QueryParam qp = new QueryParam();
            qp.ReturnFields = qp.Orderfld = Playngo_ClientZone_Files._.Sort;
            qp.OrderType = 1;
            qp.Where.Add(new SearchParam(Playngo_ClientZone_Files._.PortalId, PhotoItem.PortalId, SearchType.Equal));
            PhotoItem.Sort = Convert.ToInt32(Playngo_ClientZone_Files.FindScalar(qp)) + 2;
            Int32 PhotoId = PhotoItem.Insert();
            if (PhotoId > 0)
            {
                SynchronizeFileCount++;
            }


            return SynchronizeFileCount;
        }
        /// <summary>
        /// 利用线程来移动文件(这个过程略废CPU)
        /// </summary>
        /// <param name="oSynchronizeFileQueue"></param>
        public void ThreadMoveTo(object oSynchronizeFileQueue)
        {
            List<String> SynchronizeFileQueue = oSynchronizeFileQueue as List<String>;
            if (SynchronizeFileQueue != null && SynchronizeFileQueue.Count == 2)
            {
                FileInfo SynchronizeFile = new FileInfo(SynchronizeFileQueue[0]);
                if (SynchronizeFile.Exists)
                {
                    SynchronizeFile.MoveTo(SynchronizeFileQueue[1]);
                }

            }
        }

        #endregion
















    }
}