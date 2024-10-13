using System;
using System.Threading.Tasks;
using kapaix_mini_project.Helpers;

namespace kapaix_mini_project
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Set the date for which data needs to be scraped (October 7, 2024)
            DateTime desiredDate = new DateTime(2024, 10, 07);
            
            // Construct the URL dynamically using the specified date and a constant format
            string url = $"https://empire.britned.com/public/aggregated-nominations-overview?deliveryDay={desiredDate:yyyy-MM-dd}&timescales=DAY_AHEAD";

            // Inform the user that data scraping is in progress
            Console.WriteLine("Fetching data using Playwright...");

            // Create an instance of PlaywrightScraper to handle the web scraping task
            var scraper = new PlaywrightScraper();

            // Call the ScrapeData method asynchronously to fetch the required data from the provided URL
            var scrapedData = await scraper.ScrapeData(url);

            // Display the header for the extracted data in a structured format
            Console.WriteLine("Extracted Data:");
            Console.WriteLine("      UTC        GB-NL        NL-GB");

            // Loop through each scraped data entry and output the values in a table format
            foreach (var data in scrapedData)
            {
                // Print each row of data with proper spacing to align the columns (UTC, GB-NL, NL-GB)
                Console.WriteLine($"{data.TimeUtc,-8}    {data.GB_NL,-8}     {data.NL_GB,-8}");
            }
            
        // Ask the user if they want to save the data to a CSV file
        Console.WriteLine("Would you like to save this data to a CSV file? (yes/no): ");
        var saveToFile = Console.ReadLine()?.Trim().ToLower();

        if (saveToFile == "yes")
        {
            // Save the data to CSV in the Documents folder
            scraper.SaveDataToCSV(scrapedData);
        }
        else
        {
            Console.WriteLine("Data will not be saved.");
        }
    }
}
}
