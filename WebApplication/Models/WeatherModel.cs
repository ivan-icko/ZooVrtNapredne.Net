using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApplication.Models
{

    public class Weather
    {
        //public int Id { get; set; }
        public string Description { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
    }

    public class O
    {
        public string Name { get; set; }
        public List<Weather> Weather { get; set; }
        public Main Main { get; set; }
    }

    public class WeatherModel
    {
        public Object GetWeatherforecast()
        {
            string url = "https://api.openweathermap.org/data/2.5/weather?lat=44.82&lon=20.46&appid=986fe86da587a336f3efae218d62611f";
            var client = new WebClient();
            var content = client.DownloadString(url);
            var o = JsonConvert.DeserializeObject<O>(content);
            return o;
        }
    }
}
