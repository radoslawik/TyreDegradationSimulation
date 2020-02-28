using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyreDegradationSimulation.Models
{
    public class Tyre
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Type { get; set; }
        public string Placement { get; set; }
        public int DegradationCoefficient { get; set; }
    }
}
