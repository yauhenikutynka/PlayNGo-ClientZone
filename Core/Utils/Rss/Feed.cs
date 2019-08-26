using System;
using System.Xml;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// rssFeed 的摘要说明。
    /// </summary>
    public class Feed
    {
        private string _url;
        private System.DateTime _lastModified;
        private System.DateTime _lastRssDate;
        private Channel channel = new Channel();

        #region 公共属性
        public string url
        {
            get { return _url; }
            set { _url = value; }
        }
        public System.DateTime lastModified
        {
            get { return _lastModified; }
        }
        public System.DateTime lstRssDate
        {
            set { _lastRssDate = value; }
        }
        public Channel Channel
        {
            get { return channel; }
            set { value = channel; }
        }
        #endregion


        public Feed()
        {
        }

        public Feed(string url, System.DateTime dt)
        {
            this._url = url;
            this._lastRssDate = dt;
        }

        public void Read()
        {
            XmlDocument xDoc = new XmlDocument();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.Timeout = 15000;
            request.UserAgent = @"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.40607; .NET CLR 1.1.4322)";
            Stream stream;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            this._lastModified = response.LastModified;
            stream = response.GetResponseStream();
            StreamReader sr;
            //System.Xml.XmlReader = new XmlReader();
            //stream=Encoding.Convert(Encoding.GetEncoding("GBK"),Encoding.GetEncoding("gb2312"),Convert.ToSByte(stream));
            if (this.Get_CH(response.Headers["Content-Type"].ToString()) == "GBK")
            {
                sr = new StreamReader(stream, System.Text.Encoding.GetEncoding("GB2312"));
                xDoc.Load(sr);
            }
            else
            {
                //                sr= new StreamReader(stream,System.Text.Encoding.UTF8);
                xDoc.Load(stream);
            }

            if (this._lastRssDate < this._lastModified)
            {
                XmlNodeList xnList = xDoc.DocumentElement["channel"].SelectNodes("item");
                //                XmlNodeList xnList=xDoc.SelectNodes("items");
                int a = xnList.Count;
                foreach (XmlNode xNode in xnList)
                {
                    Item rt = new Item();
                    rt.title = xNode.SelectSingleNode("title").InnerText.Replace("'", "''");
                    rt.link = xNode.SelectSingleNode("link").InnerText.Replace("'", "''");
                    rt.description = xNode.SelectSingleNode("description").InnerText.Replace("'", "''");
                    try
                    {
                        rt.pubDate = xNode.SelectSingleNode("pubDate").InnerText;
                    }
                    catch
                    {
                        rt.pubDate = this._lastModified.ToString();
                    }
                    Channel.Items.Add(rt);
                }
            }
        }



        public string Create()
        {
            return "";
        }

        private string Get_CH(string s)
        {
            int l = s.IndexOf("charset=") + 8;
            return s.Substring(l, s.Length - l);
        }





        /// <summary>
        /// 输出RSS信息到页面
        /// </summary>
        public XmlDocument ReturnRSS(String Stylesheet,String StyleXsl)
        { 
            XmlDocument domDoc = new XmlDocument();
            XmlDeclaration nodeDeclar = domDoc.CreateXmlDeclaration("1.0", System.Text.Encoding.UTF8.BodyName, "yes");
            domDoc.AppendChild(nodeDeclar);

            //如果rss有样式表文件的话，加上这两句
            if (!String.IsNullOrEmpty(Stylesheet))
            {
                XmlProcessingInstruction nodeStylesheet = domDoc.CreateProcessingInstruction("xml-stylesheet", String.Format( "type=\"text/css\" href=\"{0}\"",Stylesheet));
                domDoc.AppendChild(nodeStylesheet);
            }

            if (!String.IsNullOrEmpty(StyleXsl))
            {
                XmlProcessingInstruction nodeStylesheet = domDoc.CreateProcessingInstruction("xml-stylesheet", String.Format( "type=\"text/xsl\" href=\"{0}\"",StyleXsl));
                domDoc.AppendChild(nodeStylesheet);
            }

     

            XmlElement root = domDoc.CreateElement("rss");
            root.SetAttribute("version", "2.0"); //添加属性结点
            domDoc.AppendChild(root);

            XmlElement chnode = domDoc.CreateElement("channel");
            root.AppendChild(chnode);

            XmlElement element = domDoc.CreateElement("title");
            XmlNode textNode = domDoc.CreateTextNode(Channel.title);    //文本结点
            element.AppendChild(textNode);
            chnode.AppendChild(element);

            element = domDoc.CreateElement("link");
            textNode = domDoc.CreateTextNode(Channel.link);
            element.AppendChild(textNode);
            chnode.AppendChild(element);

            element = domDoc.CreateElement("description"); //引用结点
            XmlNode cDataNode = domDoc.CreateCDataSection(Channel.description);
            element.AppendChild(cDataNode);
            chnode.AppendChild(element);

           

            foreach (Item item in channel.Items)
            {
                element = domDoc.CreateElement("item");

                XmlElement Itemelement = domDoc.CreateElement("title");
                textNode = domDoc.CreateTextNode(item.title);
                Itemelement.AppendChild(textNode);
                element.AppendChild(Itemelement);

                Itemelement = domDoc.CreateElement("link");
                textNode = domDoc.CreateTextNode(item.link);
                Itemelement.AppendChild(textNode);
                element.AppendChild(Itemelement);

                Itemelement = domDoc.CreateElement("pubDate");
                textNode = domDoc.CreateTextNode(item.pubDate);
                Itemelement.AppendChild(textNode);
                element.AppendChild(Itemelement);

                Itemelement = domDoc.CreateElement("description");
                textNode = domDoc.CreateCDataSection(item.description);//CreateCDataSection
                Itemelement.AppendChild(textNode);
                element.AppendChild(Itemelement);


                if (!String.IsNullOrEmpty(item.category))
                {
                    foreach (string category in WebHelper.GetList(item.category))
                    {
                        Itemelement = domDoc.CreateElement("category");
                        textNode = domDoc.CreateTextNode(category);
                        Itemelement.AppendChild(textNode);
                        element.AppendChild(Itemelement);
                    }
                }

                //...
                //创建内容结点，常见的如title,description,link,pubDate,创建方法同上
                //...
                chnode.AppendChild(element);
              
               
            }
            return domDoc;


         
    
        }


        public void FlushRSS(String Stylesheet, String StyleXsl)
        {

            XmlDocument domDoc = ReturnRSS(Stylesheet, StyleXsl);

            //输出
            HttpContext.Current.Response.ContentType = "application/xml";
            XmlTextWriter objTextWrite = new XmlTextWriter(HttpContext.Current.Response.OutputStream, System.Text.Encoding.UTF8);
            domDoc.WriteContentTo(objTextWrite);

            objTextWrite.Flush();
            objTextWrite.Close();

            
        }













    }//
}//