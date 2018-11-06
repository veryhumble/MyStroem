using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStroem
{
    public class Crawler
    {
        private readonly Client _client;
        private readonly InfluxDb _influxDb;

        public Crawler(Client client, InfluxDb influxDb)
        {
            _client = client;
            _influxDb = influxDb;
        }

        public async Task<bool> Run(IEnumerable<MyStrom> devices)
        {
            var reportTasksList = new List<Task<MyStrom>>();
            foreach (var device in devices)
            {
                reportTasksList.Add(_client.GetReport(device));
            }

            await Task.WhenAll(reportTasksList);

            foreach (var task in reportTasksList)
            {
                if (task.Exception != null)
                {
                    Console.WriteLine("An error occured" + task.Exception);
                    return false;
                }
                var result = task.Result;
                _influxDb.SendMetric(result);
            }

            return true;
        }
    }
}