using System;

namespace MyStroem
{
    public class MyStrom
    {   
        public string Name { get; set; }
        
        public string Address { get; set; }

        public Uri ReportUrl => new Uri($"http://{Address}/report");

        public Report Report { get; set; }
        
    }

    public struct Report
    {
        public double Power { get; set; }

        public bool Relay { get; set; }
    }
}