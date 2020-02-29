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
     
        public MainViewModel() : base()
        {
            GetAndSortTyres();
            GetDegradationData();


            Console.WriteLine(TrackCoefPoints[0].TrackLocation);
            Console.WriteLine(TrackCoefPoints[0].TrackName);

            TempHandler = new TemperatureHandler();
        }

        public void GetAndSortTyres()
        {
            XmlHandler = new XmlHandler();
            List<Tyre> allTyres = XmlHandler.DeserializeXml("Resources/TyresXML.xml");
            Console.WriteLine(allTyres[0].Name);

            List<Tyre> fl = new List<Tyre>();
            List<Tyre> fr = new List<Tyre>();
            List<Tyre> rl = new List<Tyre>();
            List<Tyre> rr = new List<Tyre>();
            AvailableTyresList sortedTyres = new AvailableTyresList();

            foreach (Tyre tyre in allTyres)
            {
                switch (tyre.Placement)
                {
                    case "FL":
                        fl.Add(tyre);
                        break;
                    case "FR":
                        fr.Add(tyre);
                        break;
                    case "RL":
                        rl.Add(tyre);
                        break;
                    case "RR":
                        rr.Add(tyre);
                        break;
                    default:
                        break;
                }

            }
            sortedTyres.TyresFL = fl;
            sortedTyres.TyresFR = fr;
            sortedTyres.TyresRL = rl;
            sortedTyres.TyresRR = rr;
            AvailableTyres = sortedTyres;
            return;
        }

        public void GetDegradationData()
        {
            CoefHandler = new TrackDegrCoefHandler();
            TrackCoefPoints = CoefHandler.DeserializeTxt("Resources/TrackDegradationCoefficients.txt");
        }

    }
}
