using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using TyreDegradationSimulation.Models;

namespace TyreDegradationSimulation.Handlers
{
    public class TemperatureHandler
    {
        static readonly HttpClient client = new HttpClient();
        public async Task<Temperature> GetTemperatureInfo(string cityname)
        {
            string path = "http://api.openweathermap.org/data/2.5/weather?q=" + cityname + "&units=metric&appid=449ee6bcdb1d7a41022ad8216f07546e";
            Console.WriteLine(path);
            string responseBody = "";
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show(e.Message, "Connection error");
                return null;
            }

            return JsonConvert.DeserializeObject<Temperature>(responseBody);
        }
    }
}
