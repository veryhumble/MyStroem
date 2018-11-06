using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyStroem
{
    public class InfluxDb
    {
        private readonly Configuration _configuration;

        public InfluxDb(Configuration configuration)
        {
            _configuration = configuration;
        }

        public void SendMetric(MyStrom deviceReport)
        {
            var webClient = new WebClient
            {
                QueryString = new NameValueCollection
                {
                    {"db", _configuration.InfluxDbDatabase},
                    {"u", _configuration.InfluxDbUsername},
                    {"p", _configuration.InfluxDbPassword},
                    {"precision", "s"}
                }
            };

            var uri = new Uri(_configuration.InfluxDbAddress + "/write");
            Console.WriteLine($"sending metric to InfluxDB for {deviceReport.Name}");
            webClient.UploadString(uri, deviceReport.ToLineProtocolString(_configuration.InfluxDbMeasurement));
        }
    }

    public static class InfluxDbExtensions
    {
        public static string ToLineProtocolString(this MyStrom device, string measurement)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(measurement);
            stringBuilder.Append(",");
            stringBuilder.Append($"{nameof(device.Name).ToLowerInvariant()}={device.Name} ");
            stringBuilder.Append($"{nameof(device.Report.Power).ToLowerInvariant()}={device.Report.Power.ToString("0.000000", CultureInfo.InvariantCulture)},");
            stringBuilder.Append($"{nameof(device.Report.Relay).ToLowerInvariant()}={device.Report.Relay.ToString().ToLowerInvariant()} ");
            stringBuilder.Append(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            return stringBuilder.ToString();
        }
    }
}