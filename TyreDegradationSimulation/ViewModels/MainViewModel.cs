using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
        }

        public void GetAndSortTyres()
        {
            
            List<Tyre> allTyres = XmlHandler.DeserializeXml("Resources/TyresXML.xml");

            List<Tyre> fl = new List<Tyre>();
            List<Tyre> fr = new List<Tyre>();
            List<Tyre> rl = new List<Tyre>();
            List<Tyre> rr = new List<Tyre>();
            
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
            AvailableTyres = new AvailableTyresList(fl,fr,rl,rr);
            return;
        }

        public void GetDegradationData()
        {      
            TrackCoefPoints = CoefHandler.DeserializeTxt("Resources/TrackDegradationCoefficients.txt");
            return;
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

        public bool CheckRules()
        {
            bool fulfilled = true;

            if (SelectedFL != null && SelectedFR != null)
                if ((SelectedFL.Family != SelectedFR.Family) || (SelectedFL.Type != SelectedFR.Type))
                    fulfilled = false;

            if (SelectedRL != null && SelectedRR != null)
                if ((SelectedRL.Family != SelectedRR.Family) || (SelectedRL.Type != SelectedRR.Type))
                    fulfilled = false;

            if (SelectedRL != null && SelectedFR != null)
                if (SelectedRL.Type != SelectedFR.Type)
                    fulfilled = false;


            if (SelectedFL != null && SelectedRR != null)
                if (SelectedFL.Type != SelectedRR.Type)
                    fulfilled = false;

            return fulfilled;

        }

        public bool CheckSelection()
        {
            bool fulfilled = false;
            if (SelectedFL != null && SelectedFR != null
                && SelectedRL != null && SelectedRR != null
                && Temperature != "" && SelectedTrackIndex != -1)
                fulfilled = true;
            return fulfilled;
        }

        public Result CalculateTyreDegradation(Tyre tyreData)
        {
            Result retval = new Result();
            TrackDegrCoef trackData = TrackCoefPoints[SelectedTrackIndex];
            List<int> trackPoints = trackData.Values;
            if (trackPoints.Count < 0)
                return null;

            double average = trackPoints.Average();
            double range = trackPoints.Max() - trackPoints.Min();
            double mode =
                trackPoints
                    .GroupBy(x => x)
                    .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
                    .Select(x => x.Key)
                    .FirstOrDefault();

            double degPercentage;
            switch (tyreData.Type)
            {
                case "SuperSoft":
                    degPercentage = 0.8;
                    break;
                case "Soft":
                    degPercentage = 0.8;
                    break;
                case "Medium":
                    degPercentage = 0.9;
                    break;
                case "Hard":
                    degPercentage = 0.75;
                    break;
                default:
                    degPercentage = 1;
                    break;
            }
            double tyreCoef = degPercentage * tyreData.DegradationCoefficient;

            int trackTemperature = Int32.Parse(Temperature);

            retval.Average = (int)Math.Ceiling((average * trackTemperature) / tyreCoef);
            retval.Mode = (int)Math.Ceiling((mode * trackTemperature) / tyreCoef);
            retval.Range = (int)Math.Ceiling((range * trackTemperature) / tyreCoef);
            return retval;
        }

    }
}
