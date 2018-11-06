using System;
using System.Collections.Generic;

namespace MyStroem
{
    public class Configuration
    {
        public IEnumerable<MyStrom> DeviceList { get; set; }

        public string InfluxDbAddress { get; set; }

        public string InfluxDbDatabase { get; set; }

        public string InfluxDbUsername { get; set; }

        public string InfluxDbPassword { get; set; }

        public int IntervalSeconds { get; set; } = 60;

        public static Configuration ReadFromEnv()
        {
            var config = new Configuration
            {
                DeviceList = ParseDevicesFromEnv(Environment.GetEnvironmentVariable("MYSTROM_DEVICES")),
                IntervalSeconds = Convert.ToInt32(Environment.GetEnvironmentVariable("INTERVAL_SECONDS")),
                InfluxDbAddress = Environment.GetEnvironmentVariable("INFLUXDB_ADDRESS"),
                InfluxDbDatabase = Environment.GetEnvironmentVariable("INFLUXDB_DATABASE"),
                InfluxDbUsername = Environment.GetEnvironmentVariable("INFLUXDB_USERNAME"),
                InfluxDbPassword = Environment.GetEnvironmentVariable("INFLUXDB_PASSWORD")
            };

            return config;
        }

        private static IEnumerable<MyStrom> ParseDevicesFromEnv(string envString)
        {
            var list = new List<MyStrom>();
            var splitPairs = envString.Split(';');
            foreach (var splitPair in splitPairs)
            {
                var keyValue = splitPair.Split('=');
                var myStromDevice = new MyStrom
                {
                    Name = keyValue[0],
                    Address = keyValue[1]
                };
                list.Add(myStromDevice);
            }

            return list;
        }
    }
}