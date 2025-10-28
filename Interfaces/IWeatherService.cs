using MyDotnet8Api.Models;

namespace MyDotnet8Api.Interfaces
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync();
        Task<WeatherForecast?> GetWeatherForecastByIdAsync(int id);
        Task AddWeatherForecastAsync(WeatherForecast weatherForecast);
        Task UpdateWeatherForecastAsync(WeatherForecast weatherForecast);
        Task DeleteWeatherForecastAsync(int id);
    }
}