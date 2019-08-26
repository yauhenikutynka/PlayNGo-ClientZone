using DotNetNuke.Common;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Services.Installer.Log;
using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    public class FileManager : DotNetNuke.Services.FileSystem.FileManager
    {


        /// <summary>
        /// Downloads the specified file.
        /// </summary>
        /// <param name="file">The file to download.</param>
        /// <param name="contentDisposition">Indicates how to display the document once downloaded.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when file is null.</exception>
        /// <exception cref="DotNetNuke.Services.FileSystem.PermissionsNotMetException">Thrown when permissions are not met.</exception>
        public virtual void WriteFileToResponse(System.IO.FileInfo file, ContentDisposition contentDisposition)
        {

            Requires.NotNull("file", file);


            WriteFileToHttpContext(file, contentDisposition);
        }


        /// <summary>This member is reserved for internal use and is not intended to be used directly from your code.</summary>
        public void WriteFileToHttpContext(System.IO.FileInfo file, ContentDisposition contentDisposition)
        {
            var scriptTimeOut = HttpContext.Current.Server.ScriptTimeout;

            HttpContext.Current.Server.ScriptTimeout = int.MaxValue;
            var objResponse = HttpContext.Current.Response;
        
            objResponse.ClearContent();
            objResponse.ClearHeaders();

            switch (contentDisposition)
            {
                case ContentDisposition.Attachment:
                    objResponse.AppendHeader("content-disposition", "attachment; filename=\"" + file.Name + "\"");
                    break;
                case ContentDisposition.Inline:
                    objResponse.AppendHeader("content-disposition", "inline; filename=\"" + file.Name + "\"");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("contentDisposition");
            }

            // Do not send negative Content-Length (file.Size could be negative due to integer overflow for files > 2GB)
            if (file.Length >= 0) objResponse.AppendHeader("Content-Length", file.Length.ToString(CultureInfo.InvariantCulture));
            objResponse.ContentType = GetContentType(file.Extension.Replace(".", ""));

            try
            {
                using (var fileContent = GetFileContent(file))
                {
                    WriteStream(objResponse, fileContent);
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);

                objResponse.Write("Error : " + ex.Message);
            }

            objResponse.Flush();
            objResponse.End();

            HttpContext.Current.Server.ScriptTimeout = scriptTimeOut;
        }

        /// <summary>This member is reserved for internal use and is not intended to be used directly from your code.</summary>
        public void WriteStream(HttpResponse objResponse, Stream objStream)
        {
            var bytBuffer = new byte[10000];
            try
            {
                if (objResponse.IsClientConnected)
                {
                    var intLength = objStream.Read(bytBuffer, 0, 10000);

                    while (objResponse.IsClientConnected && intLength > 0)
                    {
                        objResponse.OutputStream.Write(bytBuffer, 0, intLength);
                        objResponse.Flush();

                        intLength = objStream.Read(bytBuffer, 0, 10000);
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
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
        /// Gets the content of the specified file.
        /// </summary>
        /// <param name="file">The file to get the content from.</param>
        /// <returns>A stream with the content of the file.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when file is null.</exception>
        /// <exception cref="DotNetNuke.Services.FileSystem.FolderProviderException">Thrown when the underlying system throw an exception.</exception>
        public virtual Stream GetFileContent(System.IO.FileInfo file)
        {
            Requires.NotNull("file", file);

            Stream stream = null;

            try
            {
                stream = new System.IO.FileStream(file.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);


            }
            catch (Exception ex)
            {
                //Logger.Error(ex);

                //throw new FolderProviderException(Localization.Localization.GetExceptionMessage("UnderlyingSystemError", "The underlying system threw an exception"), ex);
            }


            return stream;
        }
    }
}