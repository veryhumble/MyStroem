using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace MyStroem
{
    class Program
    {
        private static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        private static readonly AutoResetEvent ResetEvent = new AutoResetEvent(false);

        static async Task Main(string[] args)
        {
            var configuration = Configuration.ReadFromEnv();

            AppDomain.CurrentDomain.ProcessExit += ProcessExit;
            var crawler = new Crawler(new Client(), new InfluxDb(configuration));

            var timer = new Timer(Callback, ResetEvent, configuration.Interval, configuration.Interval);

            while (!CancellationTokenSource.IsCancellationRequested)
            {
                ResetEvent.WaitOne();

                var result = await crawler.Run(configuration.DeviceList, DateTime.UtcNow).ConfigureAwait(false);
                Console.WriteLine(result ? $"[{DateTime.UtcNow}] Crawling was successful!" : $"[{DateTime.UtcNow}] Crawling failed!");
            }

            timer.Dispose();
        }

        private static void Callback(object state)
        {
            ResetEvent.Set();
        }

        private static void ProcessExit(object sender, EventArgs e)
        {
            CancellationTokenSource.Cancel();
        }
    }
}