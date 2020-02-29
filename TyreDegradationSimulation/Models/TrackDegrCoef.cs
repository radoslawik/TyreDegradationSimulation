using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyreDegradationSimulation.Models
{
    public class TrackDegrCoef
    {
        public string TrackName { get; set; }
        public string TrackLocation { get; set; }
        public List<int> Values { get; set; }
    }
}
