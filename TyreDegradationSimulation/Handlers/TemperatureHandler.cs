using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TyreDegradationSimulation.Models;

namespace TyreDegradationSimulation.Handlers
{
    public class TemperatureHandler
    {
        public string ErrorMsg = "";
        public async Task<Temperature> GetTemperatureInfo(string cityname)
        {
            string path = "http://api.openweathermap.org/data/2.5/weather?q=" + cityname + "&units=metric&appid=449ee6bcdb1d7a41022ad8216f07546e";
            Console.WriteLine(path);
            string responseBody = "";
            try
            {
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(path);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(response.StatusCode);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                ErrorMsg = e.Message;
            }

            if (responseBody != null)
            {
                return JsonConvert.DeserializeObject<Temperature>(responseBody);
            }
            else
            {
                return null;
            }
        }
    }
}
