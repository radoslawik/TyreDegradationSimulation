using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyreDegradationSimulation.Handlers;
using TyreDegradationSimulation.Models;

namespace TyreDegradationSimulation.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public XmlHandler XmlHandler;
        public TrackDegrCoefHandler CoefHandler;
        public TemperatureHandler TempHandler;
        public List<Tyre> AvailableTyres { get; set; }
        public List<TrackDegrCoef> CoefPoints { get; set; }
        public MainViewModel() : base()
        {
            XmlHandler = new XmlHandler();
            AvailableTyres = XmlHandler.DeserializeXml("Resources/TyresXML.xml");
            Console.WriteLine(AvailableTyres[0].Name);

            CoefHandler = new TrackDegrCoefHandler();
            CoefPoints = CoefHandler.DeserializeTxt("Resources/TrackDegradationCoefficients.txt");
            Console.WriteLine(CoefPoints[0].TrackLocation);
            Console.WriteLine(CoefPoints[0].TrackName);

            TempHandler = new TemperatureHandler();
        }



    }
}
