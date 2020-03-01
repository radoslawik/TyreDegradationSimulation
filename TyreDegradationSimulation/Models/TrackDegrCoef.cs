using System.Collections.Generic;

namespace TyreDegradationSimulation.Models
{
    public class TrackDegrCoef
    {
        public string TrackName { get; set; }
        public string TrackLocation { get; set; }
        public List<int> Values { get; set; }
    }
}
