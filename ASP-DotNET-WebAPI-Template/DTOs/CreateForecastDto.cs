using System;

namespace ASP_DotNET_WebAPI_Template.DTOs
{
    public class CreateForecastDto
    {
        public string CountryName { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
    }
}