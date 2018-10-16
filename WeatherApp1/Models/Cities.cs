using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp1.Models
{
    public class Cities
    {
        public string id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        //public Coord coord { get; set; }
    }

    public class Coord
    {
        public string lon { get; set; }
        public string lat { get; set; }
    }
}
