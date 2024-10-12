using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kapaix_mini_project.Helpers
{
    public class PlaywrightScraper
    {
        public const int FixedRowCount = 24; // Set to 24 to extract data for each hour in a day

        // ScrapeData method: Fetches capacity data from the specified URL using Playwright.
        // This method targets a specific table on the web page to gather required data.
        public async Task<List<HtmlParser.CapacityData>> ScrapeData(string url)
        {
            var data = new List<HtmlParser.CapacityData>(); // List to hold the capacity data

            // Initialize Playwright and launch a Chromium browser in headless mode for scraping
            var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });

            var page = await browser.NewPageAsync(); // Open a new page in the browser
            await page.GotoAsync(url); // Navigate to the specified URL

            // Wait for the relevant table to load on the webpage
            await page.WaitForSelectorAsync("table.border-separate");

            // Select all tables with the class 'border-separate' and check if we have more than one
            var tables = await page.QuerySelectorAllAsync("table.border-separate");
            if (tables.Count > 1) // Ensure that the desired table is present
            {
                var rows = await tables[1].QuerySelectorAllAsync("tbody tr"); // Get rows from the second table

                // Iterate through each row to extract data for the GB-NL and NL-GB columns
                for (int i = 0; i < Math.Min(FixedRowCount, rows.Count); i++) // Limit to 24 rows
                {
                    var row = rows[i]; // Select the current row
                    var gbNlCell = await row.QuerySelectorAsync("td:nth-child(1)"); // Get the first cell for GB-NL data
                    var nlGbCell = await row.QuerySelectorAsync("td:nth-child(2)"); // Get the second cell for NL-GB data

                    // Check if both cells are available before extracting their values
                    if (gbNlCell != null && nlGbCell != null)
                    {
                        var gbNlValue = await gbNlCell.InnerTextAsync(); // Fetch the GB-NL capacity value
                        var nlGbValue = await nlGbCell.InnerTextAsync(); // Fetch the NL-GB capacity value
                        var timeRange = $"{i:00}:00 - {i + 1:00}:00";  // Create a time range for the current hour

                        // Add the extracted data to the list after converting the time to UTC
                        data.Add(new HtmlParser.CapacityData
                        {
                            TimeUtc = ConvertToUTC(timeRange), // Convert the time range to UTC
                            GB_NL = gbNlValue,                 // Store the GB to NL capacity
                            NL_GB = nlGbValue                  // Store the NL to GB capacity
                        });
                    }
                }
            }

            return data; // Return the list of collected capacity data
        }

        // ConvertToUTC method: Converts a CET/CEST time range to UTC format
        private string ConvertToUTC(string cetTimeRange)
        {
            try
            {
                // Input format example: "00:00 - 01:00"
                string[] times = cetTimeRange.Split(" - "); // Split the input time range into start and end times

                if (times.Length != 2) // Check if the split was successful
                    throw new FormatException("Invalid time range format");

                // Extract and parse the hours from the split time strings
                var startHour = int.Parse(times[0].Substring(0, 2)); // Get the starting hour
                var endHour = int.Parse(times[1].Substring(0, 2));   // Get the ending hour

                // Convert CET/CEST to UTC (considering daylight saving adjustments)
                startHour = (startHour - 2 + 24) % 24;  // Adjust start hour for UTC
                endHour = (endHour - 2 + 24) % 24;      // Adjust end hour for UTC

                // Return the converted time range in UTC format
                return $"{startHour:00}:00 - {endHour:00}:00";
            }
            catch (Exception ex) // Handle any conversion errors
            {
                throw new FormatException($"Error converting CET time range to UTC: {ex.Message}");
            }
        }
    }
}
