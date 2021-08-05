using System;
using System.ComponentModel.DataAnnotations;

namespace ASP_DotNET_WebAPI_Template.Models
{
    public class WeatherForecast
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string CountryName { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
