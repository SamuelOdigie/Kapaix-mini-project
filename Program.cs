using System;
using System.Threading.Tasks;
using kapaix_mini_project.Helpers;

namespace kapaix_mini_project
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // URL to scrape data from
            DateTime desiredDate = new DateTime(2024, 10, 07);
            string url = "https://empire.britned.com/public/aggregated-nominations-overview?deliveryDay=2024-10-12&timescales=DAY_AHEAD";


            // Using Playwright to scrape the "Net transfer capacity (MNS)" column
            Console.WriteLine("Fetching data using Playwright...");
            var scraper = new PlaywrightScraper();
            var scrapedData = await scraper.ScrapeData(url);

            // Print the extracted data
            Console.WriteLine("Extracted Data:");
            foreach (var data in scrapedData)
            {
                Console.WriteLine($"{data.TimeUtc} - {data.Capacity}");
            }
        }
    }
}