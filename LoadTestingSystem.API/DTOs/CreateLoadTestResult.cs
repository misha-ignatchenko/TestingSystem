namespace LoadTestingSystem.API.DTOs
{
    public class CreateLoadTestResult
    {
        public string TestName { get; set; } = string.Empty;

        public int RequestsPerSecond { get; set; }

        public double AverageResponseTime { get; set; }

        public int ErrorCount { get; set; }

        public DateTime TestDate { get; set; }
    }
}
