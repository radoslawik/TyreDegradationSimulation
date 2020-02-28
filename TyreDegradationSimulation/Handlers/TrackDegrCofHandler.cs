using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyreDegradationSimulation.Models;

namespace TyreDegradationSimulation.Handlers
{
    public class TrackDegrCofHandler
    {
        public List<TrackDegrCof> DeserializeTxt(string filename)
        {
            List<TrackDegrCof> tdCofs = new List<TrackDegrCof>();
            string[] cofs = File.ReadAllLines(filename);
            for(int i = 0; i < cofs.Length; i++)
            {
                TrackDegrCof cof = new TrackDegrCof();
                char[] delimiter = {'|'};
                int count = 3;
                string[] cofFields = cofs[i].Split(delimiter, count);
                string[] cofValues = null;
                cof.TrackName = cofFields[0];
                cof.TrackLocation = cofFields[1];

                char[] valDelimiter = { ',' };
                cofValues = cofs[i].Split(valDelimiter);
                cof.Values = cofValues.ToList();

                tdCofs.Add(cof);
            }

            return tdCofs;
         
        }

    }
}
