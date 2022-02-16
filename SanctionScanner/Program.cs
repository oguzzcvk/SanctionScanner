using SanctionScanner.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            CrawlService.GetAllPage();
            List<string> linklist = new List<string>();
            linklist.Add("https://www.sahibinden.com/ilan/vasita-otomobil-opel-sahibinden-hatasiz-boyasiz-son-fiyat-998982854/detay");
            CrawlService.GetDetails(linklist);
            
            

            

           
        }
    }
}
