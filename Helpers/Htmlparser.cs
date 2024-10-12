using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace kapaix_mini_project.Helpers
{
    public class HtmlParser
    {
        public class CapacityData
        {
            public DateTime TimeUtc { get; set; }
            public required string Capacity { get; set; } 
        }

        public List<CapacityData> ParseCapacityData(string htmlContent)
        {
            var data = new List<CapacityData>();

            // Regex to capture capacity values (update based on actual HTML structure)
            var capacityPattern = new Regex(@"<td.*?Net transfer capacity.*?<td>(\d+)<\/td>.*?<td>(.*?)<\/td>", RegexOptions.Singleline);
            var matches = capacityPattern.Matches(htmlContent);

            foreach (Match match in matches)
            {
                var capacity = match.Groups[1].Value;
                var time = DateTime.UtcNow;  // Placeholder, extract proper time

                data.Add(new CapacityData
                {
                    TimeUtc = time,
                    Capacity = capacity
                });
            }

            return data;
        }
    }
}
