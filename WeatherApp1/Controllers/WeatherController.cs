using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherApp1.Models;

namespace WeatherApp1.Controllers
{
    [Produces("application/json")]
    [Route("api/Weather")]
    public class WeatherController : Controller
    {
        public List<DECities> dECities = new List<DECities>();
        public List<DECities> dE = new List<DECities>();
        public static IConfigurationRoot Configuration;
        // GET: api/authors/search?namelike=th http://localhost:51262/api/weather/forecast?city=6548737
        [HttpGet("forecast")]
        public IActionResult Forecast(string city)
        {
            string forecast = "forecast";
            return Content(CallApi(getApiKey(), city, forecast));
        }

        // GET: api/authors/search?namelike=th http://localhost:51262/api/weather/weather?city=6548737
        [HttpGet("weather")]
        public IActionResult Weather(string city)
        {
            string weather = "weather";
            return Content(CallApi(getApiKey(), city, weather));
        }

        // GET: api/authors/search?namelike=th http://localhost:51262/api/weather/allCities
        [HttpGet("allCities")]
        public IActionResult allCities(string city)
        {
            string cits = "";
            using (StreamReader r = new StreamReader(@"C:\work\city.list.json"))
            {
                string json = r.ReadToEnd();
                List<Cities> cities = JsonConvert.DeserializeObject<List<Cities>>(json);
                cits = JsonConvert.SerializeObject(cities);
            }

            return Content(cits);
        }

        // GET: api/authors/search?namelike=th http://localhost:51262/api/weather/deCitiesList
        [HttpGet("deCitiesList")]
        public IActionResult deCitiesList(string city)
        {
            string cits = "";
            string decits = "";
            using (StreamReader r = new StreamReader(@"C:\work\city.list.json"))
            {
                string json = r.ReadToEnd();
                List<Cities> cities = JsonConvert.DeserializeObject<List<Cities>>(json);
                for (int i = 0; i < cities.Count; i++)
                {
                    Cities item = cities[i];
                    if (item.country == "DE")
                    {
                        dECities.Add(new DECities() { id = item.id, name = item.name });
                    }
                }
                cits = JsonConvert.SerializeObject(cities);
                //dECities = JsonConvert.DeserializeObject<List<DECities>>(json);
                decits = JsonConvert.SerializeObject(dECities);
            }
            
            //write string to file
            //System.IO.File.WriteAllText(@"C:\work\decity.list.json", decits);
            //return Content(cits);
            return Content(decits);
        }

        // GET: api/authors/search?namelike=th http://localhost:51262/api/weather/deCity?city=Leipzig
        [HttpGet("deCity")]
        public IActionResult deCity(string city)
        {
            string cits = "";
            string decits = "";
            using (StreamReader r = new StreamReader(@"C:\work\decity.list.json"))
            {
                string json = r.ReadToEnd();
                List<DECities> decities = JsonConvert.DeserializeObject<List<DECities>>(json);
                for (int i = 0; i < decities.Count; i++)
                {
                    DECities item = decities[i];
                    string factMessage = item.name.ToLower();
                    if (factMessage.Contains(city))
                    {
                        dECities.Add(new DECities() { id = item.id, name = item.name });
                    }
                }
                //cits = JsonConvert.SerializeObject(dECities);
                //dECities = JsonConvert.DeserializeObject<List<DECities>>(json);
                decits = JsonConvert.SerializeObject(dECities);
            }
            
            //write string to file
            //System.IO.File.WriteAllText(@"C:\work\decity.list.json", decits);
            //return Content(cits);
            return Content(decits);
        }

        public string CallApi(string apiKey, string city, string type) {
            HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/" + type + "?id=" + city + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;
            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }
            return apiResponse;
        }
        public string getApiKey()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            var apiKey = Configuration["apiKey"];
            return apiKey;
        }
    }
}