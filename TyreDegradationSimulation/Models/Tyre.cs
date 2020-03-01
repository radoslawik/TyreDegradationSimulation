using System.Collections.Generic;

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

    public class AvailableTyresList
    {
        public AvailableTyresList(List<Tyre> tFL, List<Tyre> tFR, List<Tyre> tRL, List<Tyre> tRR)
        {
            TyresFL = tFL;
            TyresFR = tFR;
            TyresRL = tRL;
            TyresRR = tRR;
        }
        public List<Tyre> TyresFL { get; set; }
        public List<Tyre> TyresFR { get; set; }
        public List<Tyre> TyresRL { get; set; }
        public List<Tyre> TyresRR { get; set; }
    }
}
