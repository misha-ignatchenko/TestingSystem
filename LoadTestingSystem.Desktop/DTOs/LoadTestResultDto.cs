using System;
using System.Collections.Generic;
using System.Text;

namespace LoadTestingSystem.Desktop.DTOs
{
    public class LoadTestResultDto
    {
        public int Id { get; set; }

        public string TestName { get; set; } = string.Empty;

        public int RequestsPerSecond { get; set; }

        public double AverageResponseTime { get; set; }

        public int ErrorCount { get; set; }

        public DateTime TestDate { get; set; }
    }
}
