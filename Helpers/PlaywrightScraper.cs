using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kapaix_mini_project.Helpers
{
    public class PlaywrightScraper
    {
        public async Task<List<HtmlParser.CapacityData>> ScrapeData(string url)
        {
            var data = new List<HtmlParser.CapacityData>();

            // Initialize Playwright and open the browser
            var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });

            var page = await browser.NewPageAsync();
            await page.GotoAsync(url);

            // Wait for the table with the data to load
            await page.WaitForSelectorAsync("table");

            // Select the rows containing the "Net transfer capacity (MNS)" data
            var rows = await page.QuerySelectorAllAsync("tr");

            foreach (var row in rows)
            {
                var timeElement = await row.QuerySelectorAsync("td.time-column"); // Adjust selector as per actual HTML
                var capacityElement = await row.QuerySelectorAsync("td.capacity-column"); // Adjust selector as per actual HTML

                if (timeElement != null && capacityElement != null)
                {
                    var timeText = await timeElement.InnerTextAsync();
                    var capacityText = await capacityElement.InnerTextAsync();

                    var parsedTime = DateTime.Parse(timeText).ToUniversalTime();  // Parse time and convert to UTC
                    data.Add(new HtmlParser.CapacityData
                    {
                        TimeUtc = parsedTime,
                        Capacity = capacityText
                    });
                }
            }

            return data;
        }
    }
}
