
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 跳转链接
    /// </summary>
    public class ServiceItemLinkUrl : iService
    {
        public ServiceItemLinkUrl()
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
            Int32 ItemId = WebHelper.GetIntParam(Context.Request, "ID", 0);
            if (ItemId > 0)
            {
                Int32 ItemType = WebHelper.GetIntParam(Context.Request, "Type",(Int32) EnumDisplayModuleType.GameSheets);

                TemplateFormat xf = new TemplateFormat(Context);

                if (ItemType == (Int32)EnumDisplayModuleType.Events)
                {
                    var Item = Playngo_ClientZone_Event.FindByKeyForEdit(ItemId);
                    Context.Response.Redirect( xf.GoUrl(Item));

                }
                else if (ItemType == (Int32)EnumDisplayModuleType.Campaigns)
                {
                    var Item = Playngo_ClientZone_Campaign.FindByKeyForEdit(ItemId);
                    Context.Response.Redirect(xf.GoUrl(Item));
                }
                else if (ItemType == (Int32)EnumDisplayModuleType.Downloads)
                {
                    Context.Response.Redirect(xf.GoUiUrl("Downloads"));
                }
                else
                {
                    var Item = Playngo_ClientZone_GameSheet.FindByKeyForEdit(ItemId);
                    Context.Response.Redirect(xf.GoUrl(Item));
         
                }


            }
            else
            {
                IsResponseWrite = true;
                ResponseString = "传过来的文件编号都不对";
            }
             
        }



 



    }
}