using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyreDegradationSimulation.Models;

namespace TyreDegradationSimulation.Handlers
{
    public class TrackDegrCoefHandler
    {
        public List<TrackDegrCoef> DeserializeTxt(string filename)
        {
            List<TrackDegrCoef> tdCoefs = new List<TrackDegrCoef>();
            string[] coefs = File.ReadAllLines(filename);
            for(int i = 0; i < coefs.Length; i++)
            {
                TrackDegrCoef coef = new TrackDegrCoef();
                char[] delimiter = {'|'};
                int count = 3;
                string[] coefFields = coefs[i].Split(delimiter, count);
                string[] coefValues = null;
                coef.TrackName = coefFields[0];
                coef.TrackLocation = coefFields[1];

                char[] valDelimiter = { ',' };
                coefValues = coefs[i].Split(valDelimiter);
                coef.Values = coefValues.ToList();

                tdCoefs.Add(coef);
            }

            return tdCoefs;
         
        }

    }
}
