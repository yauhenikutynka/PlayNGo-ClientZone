using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common;
using DotNetNuke.Entities.Tabs;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 友好URL
    /// </summary>
    public class CommonFriendlyUrls
    {

     

        public static String FriendlyUrl(Playngo_ClientZone_Event DataItem,Int32 TabId, Boolean Preview, BaseModule pmb)
        {
            return FriendlyUrl(DataItem.ID, DataItem.Title, DataItem.UrlSlug, TabId, Preview, pmb);
        }

        public static String FriendlyUrl(Playngo_ClientZone_Campaign DataItem, Int32 TabId, Boolean Preview, BaseModule pmb)
        {
            return FriendlyUrl(DataItem.ID, DataItem.Title, DataItem.UrlSlug, TabId, Preview, pmb);
        }

        public static String FriendlyUrl(Playngo_ClientZone_GameSheet DataItem, Int32 TabId, Boolean Preview, BaseModule pmb)
        {
            return FriendlyUrl(DataItem.ID, DataItem.Title, DataItem.UrlSlug, TabId, Preview, pmb);
        }


        public static String FriendlyUrl(Int32 ID, String Title,String _UrlSlug, Int32 TabId,Boolean Preview, BaseModule pmb)
        {
            String FriendlyUrls = String.Empty;
            System.Text.StringBuilder Urls = new System.Text.StringBuilder();

            if (String.IsNullOrEmpty(_UrlSlug))
            {
                Urls.AppendFormat("&ID={0}", ID);
            }

            if (Preview)
            {
                Urls.Append("&Preview=true");
            }

  
            String UrlSlug = _UrlSlug;
            if (String.IsNullOrEmpty(UrlSlug))
            {
                UrlSlug = Common.CreateFriendlySlugTitle(Title);
            }

            if (pmb.PortalSettings != null && pmb.PortalSettings.DefaultLanguage != Common.GetCurrentCulture())
            {
                FriendlyUrls = "~/default.aspx?tabid=" + TabId + "&language=" + Common.GetCurrentCulture() + Urls.ToString();
            }
            else
            {
                FriendlyUrls = "~/default.aspx?tabid=" + TabId + Urls.ToString();
            }

            return Globals.FriendlyUrl(new TabController().GetTab(TabId, pmb.PortalId, true), FriendlyUrls, String.Format("{0}.aspx", UrlSlug), pmb.PortalSettings);
        }




        public static String FriendlyUrl(Playngo_ClientZone_Event DataItem, Int32 TabId, Boolean Preview, BasePage page)
        {
            return FriendlyUrl(DataItem.ID, DataItem.Title, DataItem.UrlSlug, TabId, Preview, page);
        }

        public static String FriendlyUrl(Playngo_ClientZone_Campaign DataItem, Int32 TabId, Boolean Preview, BasePage page)
        {
            return FriendlyUrl(DataItem.ID, DataItem.Title, DataItem.UrlSlug, TabId, Preview, page);
        }

        public static String FriendlyUrl(Playngo_ClientZone_GameSheet DataItem, Int32 TabId, Boolean Preview, BasePage page)
        {
            return FriendlyUrl(DataItem.ID, DataItem.Title, DataItem.UrlSlug, TabId, Preview, page);
        }


        public static String FriendlyUrl(Int32 ID, String Title, String _UrlSlug, Int32 TabId, Boolean Preview, BasePage page)
        {
            String FriendlyUrls = String.Empty;
            System.Text.StringBuilder Urls = new System.Text.StringBuilder();

            if (String.IsNullOrEmpty(_UrlSlug))
            {
                Urls.AppendFormat("&ID={0}", ID);
            }

            if (Preview)
            {
                Urls.Append("&Preview=true");
            }


            String UrlSlug = _UrlSlug;
            if (String.IsNullOrEmpty(UrlSlug))
            {
                UrlSlug = Common.CreateFriendlySlugTitle(Title);
            }

        

            if (page.PortalSettings != null && page.PortalSettings.DefaultLanguage != Common.GetCurrentCulture())
            {
                FriendlyUrls = "~/default.aspx?tabid=" + TabId + "&language=" + Common.GetCurrentCulture() + Urls.ToString();
            }
            else
            {
                FriendlyUrls = "~/default.aspx?tabid=" + TabId + Urls.ToString();
            }

            return Globals.FriendlyUrl(new TabController().GetTab(TabId, page.PortalId, true), FriendlyUrls, String.Format("{0}.aspx", UrlSlug), page.PortalSettings);
        }

    }
}