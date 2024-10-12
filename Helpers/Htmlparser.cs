namespace kapaix_mini_project.Helpers
{
    // This class is responsible for parsing and holding data related to transfer capacities.
    public class HtmlParser
    {
        // Inner class that defines the structure for capacity data.
        // It stores the time (converted to UTC) and the capacities for both GB-NL and NL-GB directions.
        public class CapacityData
        {
            public string? TimeUtc { get; set; } // Stores time in UTC as string.
            public string? GB_NL { get; set; }   // Stores capacity from GB to NL (in MW).
            public string? NL_GB { get; set; }   // Stores capacity from NL to GB (in MW).
        }
    }
}
