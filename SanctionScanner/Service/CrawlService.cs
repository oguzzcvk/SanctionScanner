using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SanctionScanner.Service
{
    public static class CrawlService
    {
        public static List<string> GetAllPage()
        {
            List<string> LinkList = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            string mainUrl = "https://www.sahibinden.com/";
            int pageCount = 56;



            using (var client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.82 Safari/537.36";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var response = client.DownloadString(mainUrl);
                doc.LoadHtml(response);
            }
            for (int i = 1; i <= pageCount; i++)
            {
                var aList = doc.DocumentNode.SelectNodes("//div[@class='uiBox showcase']//ul[@class='vitrin-list clearfix']//li//a");

                //Console.WriteLine("{0} Adet sayfa bulundu => Alınan {1} Adet Sayfa => Kalan {2} Adet Sayfa", pageCount, i, (pageCount - i));

                foreach (HtmlNode item in aList)
                {
                    string mainUrl2 = "https://www.sahibinden.com.";
                    string ahref = item.Attributes["href"].Value;
                    LinkList.Add(mainUrl2 + ahref);
                }
            }



            return LinkList.Distinct().ToList();


        }
        public static void GetDetails(List<string> LinkList)
        {
            List<Entity> detailList = new List<Entity>();
            HtmlDocument doc = new HtmlDocument();

            foreach (var item in LinkList)
            {
                Entity entity = new Entity()
                {
                    url = item,
                };

                using (var client = new WebClient() { Encoding = Encoding.UTF8 })
                {
                    client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.82 Safari/537.36";
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var response = client.DownloadString(item);
                    doc.LoadHtml(response);
                }

                try
                {
                    entity.name = doc.DocumentNode.SelectSingleNode("//div[@class='classifiedDetailTitle']//h1").InnerText;

                }
                catch (Exception)
                {
                    entity.name = "İlan Başlığı Bulunamadı!";
                }

                try
                {
                    entity.price = doc.DocumentNode.SelectSingleNode("//div[@class='classifiedDetailContent']//div[@class='class='classifiedInfo']//h3").InnerText;



                }
                catch (Exception)
                {
                    entity.price = "Fiyat Bulunamadı.";
                }






            }

        }


    }
}
