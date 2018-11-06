using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace MyStroem
{
    class Program
    {
        private static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        static async Task Main(string[] args)
        {
            var configuration = Configuration.ReadFromEnv();

            AppDomain.CurrentDomain.ProcessExit += ProcessExit;

            var crawler = new Crawler(new Client(), new InfluxDb(configuration));

            while (CancellationTokenSource.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(configuration.IntervalSeconds), CancellationTokenSource.Token);
                var result = await crawler.Run(configuration.DeviceList);
                Console.WriteLine(result ? "Crawling was successful!" : "Crawling failed!");
            }           
        }

        private static void ProcessExit(object sender, EventArgs e)
        {
            CancellationTokenSource.Cancel();
        }
    }
}