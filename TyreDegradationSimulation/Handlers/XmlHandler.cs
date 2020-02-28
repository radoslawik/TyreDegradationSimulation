using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TyreDegradationSimulation.Models;

namespace TyreDegradationSimulation.Handlers
{
    public class XmlHandler
    {
        public List<Tyre> DeserializeXml(string filename)
        {
            string xmlString = File.ReadAllText(filename);
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Tyres");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Tyre>), xmlRoot);
            StringReader stringReader = new StringReader(xmlString);
            return (List<Tyre>)xmlSerializer.Deserialize(stringReader);
        }
    }
}
