using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Collections;
using System.Text;

namespace Playngo.Modules.ClientZone 
{
    /// <summary>
    /// 获取RSS订阅信息并入库
    /// </summary>
    public class RssFeeds
    {
         

         





        public ItemCollection LoadRSS(string RssUrl, int RssCount,ref Boolean isError)
        {
            XmlDocument doc = new XmlDocument();
            ItemCollection rssList = new ItemCollection();
            if (!String.IsNullOrEmpty(RssUrl))
            {
                //WebClientX x = new WebClientX();

              
              
                String xml = String.Empty;
                try
                {
                    //x.Encoding = Encoding.UTF8;
                    //xml = x.DownloadString(new Uri(RssUrl));


                    HttpEvent x = new HttpEvent();
                    x.Url = new Uri(RssUrl);
                    xml = x.Event();
                }
                catch {
                    isError = true;
                }

                if (!String.IsNullOrEmpty(xml) && (xml.LastIndexOf("<rss", StringComparison.CurrentCultureIgnoreCase) >= 0 || xml.LastIndexOf("<feed", StringComparison.CurrentCultureIgnoreCase) >= 0))
                {
                    doc.LoadXml(xml);
                    XmlNodeList nodelist = doc.GetElementsByTagName("item");
                    XmlNodeList objItems1;
                    int i = 1;
                    if (doc.HasChildNodes)
                    {
                        foreach (XmlNode node in nodelist)
                        {
                            i += 1;
                            if (node.HasChildNodes)
                            {


                                Item rssItem = new Item();
                                objItems1 = node.ChildNodes;
                                foreach (XmlNode node1 in objItems1)
                                {
                                    switch (node1.Name)
                                    {
                                        case "title":
                                            rssItem.title = node1.InnerText;
                                            break;
                                        case "link":
                                            rssItem.link = node1.InnerText;
                                            break;
                                        case "description":
                                            rssItem.description = node1.InnerText;
                                            break;
                                        case "pubDate":
                                            rssItem.pubDate = node1.InnerText;
                                            break;
                                        case "category":
                                            rssItem.category = String.Format("{0}{1}{2}", rssItem.category, String.IsNullOrEmpty(rssItem.category) ? "" : ",", node1.InnerText);
                                            break;
                                    }
                                }
                                rssList.Add(rssItem);

                            }
                            if (i > RssCount)
                                break;
                        }
                    }
                }
                else
                {
                    //获取的源不是RSS格式
                    isError = true;
                }
            }
            else
            {
                //没有获取到RSS源
                isError = true;
            }
            return rssList;
        } 


    }
}