using System;

namespace MyDotnet8Api.DTOs
{
    public class WeatherForecastDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }

        // to be added time permitting
    }
}