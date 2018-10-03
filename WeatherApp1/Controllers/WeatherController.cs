using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WeatherApp1.Controllers
{
    [Produces("application/json")]
    [Route("api/Weather")]
    public class WeatherController : Controller
    {
        public static IConfigurationRoot Configuration;
        // GET: api/authors/search?namelike=th http://localhost:51262/api/weather/forecast?city=6548737
        [HttpGet("forecast")]
        public IActionResult Forecast(string city)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            var apiKey = Configuration["apiKey"];
            //HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?id=" + city + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;
            HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/forecast?id=" + city + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }

            return Content(apiResponse);
        }
    }
}