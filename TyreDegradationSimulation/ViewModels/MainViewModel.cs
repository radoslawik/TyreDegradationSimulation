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
            TempHandler = new TemperatureHandler();
            XmlHandler = new XmlHandler();
            CoefHandler = new TrackDegrCoefHandler();

            GetAndSortTyres();
            GetDegradationData();


            Console.WriteLine(TrackCoefPoints[0].TrackLocation);
            Console.WriteLine(TrackCoefPoints[0].TrackName);

            
        }

        public void GetAndSortTyres()
        {
            
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
            TrackCoefPoints = CoefHandler.DeserializeTxt("Resources/TrackDegradationCoefficients.txt");
        }

        public async Task<string> GetCurrentTemperature()
        {
            
            string tempRet = "";
            Temperature tempData = await TempHandler.GetTemperatureInfo(TrackCoefPoints[SelectedTrackIndex].TrackLocation);
            if (tempData != null)
            {
                int tempVal = (int)Math.Ceiling(tempData.Main.Temp);
                tempRet = tempVal.ToString();
            }
            return tempRet;
        }

    }
}
