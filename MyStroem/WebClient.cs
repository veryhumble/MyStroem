using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyStroem
{
    public class Client
    {
        public static async Task<MyStrom> GetReport(MyStrom device)
        {
            Console.WriteLine($"[{DateTime.UtcNow}] GET report of {device.Name} on {device.Address}");
            var client = new HttpClient();
            var response = await client.GetAsync(device.ReportUrl);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var deserializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            
            var result = JsonConvert.DeserializeObject<Report>(content, deserializerSettings);
            
            return new MyStrom
            {
                Name = device.Name,
                Report = result
            };
        }
    }
}