using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace kapaix_mini_project.Helpers
{
    public class HttpService
    {
        public static async Task<string> GetAsync(string url)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                try
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return $"Error: Unable to fetch data. Status code: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }
        }
    }
}
