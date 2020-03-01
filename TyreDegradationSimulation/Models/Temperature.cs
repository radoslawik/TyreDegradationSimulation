namespace TyreDegradationSimulation.Models
{
    public class Temperature
    {
        public string Name { get; set; }
        public MainTempInfo Main { get; set; }
    }

    public class MainTempInfo
    {
        public double Temp { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
    }
}
