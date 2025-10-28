using System.Collections.Generic;
using System.Threading.Tasks;
using MyDotnet8Api.Interfaces;
using MyDotnet8Api.Models;

namespace MyDotnet8Api.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IRepository<WeatherForecast> _weatherRepository;

        public WeatherService(IRepository<WeatherForecast> weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync()
        {
            return await _weatherRepository.GetAllAsync();
        }

        public async Task<WeatherForecast?> GetWeatherForecastByIdAsync(int id)
        {
            return await _weatherRepository.GetByIdAsync(id);
        }

        public async Task AddWeatherForecastAsync(WeatherForecast weatherForecast)
        {
            await _weatherRepository.AddAsync(weatherForecast);
        }

        public async Task UpdateWeatherForecastAsync(WeatherForecast weatherForecast)
        {
            await _weatherRepository.UpdateAsync(weatherForecast);
        }

        public async Task DeleteWeatherForecastAsync(int id)
        {
            await _weatherRepository.DeleteAsync(id);
        }
    }
}