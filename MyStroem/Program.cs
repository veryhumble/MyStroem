using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStroem
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = Configuration.ReadFromEnv();

            var crawler = new Crawler(new Client(), new InfluxDb(configuration));
            var result = await crawler.Run(configuration.DeviceList);

            Console.WriteLine(result ? "Crawling was successful!" : "Crawling failed!");
        }
    }
}