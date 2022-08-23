using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class RandomAnimalViewModel
    {
        public string Name { get; set; }
        public string Latin_Name { get; set; }
        public string Animal_Type { get; set; }
        public string Active_Time { get; set; }
        public string Length_Min { get; set; }
        public string Length_Max { get; set; }
        public string Weight_Min { get; set; }
        public string Weight_Max { get; set; }
        public string Lifespan { get; set; }
        public string Habitat { get; set; }
        public string Diet { get; set; }
        public string Geo_Range { get; set; }
        public string Image_Link { get; set; }
    }

    public class RandomAnimal
    {
        public Object GetRandomAnimal()
        {
            string url = "https://zoo-animal-api.herokuapp.com/animals/rand";
            var client = new WebClient();
            var content = client.DownloadString(url);
            var animal = JsonConvert.DeserializeObject<RandomAnimalViewModel>(content);
            return animal;
        }
    }
}
