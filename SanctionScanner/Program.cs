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
            
            CrawlService.GetDetails();

            

           
        }
    }
}
