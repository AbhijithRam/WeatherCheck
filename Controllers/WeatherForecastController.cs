using Microsoft.AspNetCore.Mvc;
using MyDotnet8Api.DTOs;
using MyDotnet8Api.Interfaces;
using MyDotnet8Api.Models;

namespace MyDotnet8Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _service;

        public WeatherForecastController(IWeatherService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecastDto>> Get()
        {
            var items = await _service.GetWeatherForecastsAsync();
            return items.Select(m => new WeatherForecastDto
            {
                Id = m.Id,
                Date = m.Date,
                TemperatureC = m.TemperatureC,
                Summary = m.Summary
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<WeatherForecastDto>> Get(int id)
        {
            var item = await _service.GetWeatherForecastByIdAsync(id);
            if (item == null) return NotFound();
            return new WeatherForecastDto
            {
                Id = item.Id,
                Date = item.Date,
                TemperatureC = item.TemperatureC,
                Summary = item.Summary
            };
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WeatherForecastDto dto)
        {
            var model = new WeatherForecast
            {
                Date = dto.Date,
                TemperatureC = dto.TemperatureC,
                Summary = dto.Summary
            };
            await _service.AddWeatherForecastAsync(model);
            dto.Id = model.Id;
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] WeatherForecastDto dto)
        {
            var existing = await _service.GetWeatherForecastByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Date = dto.Date;
            existing.TemperatureC = dto.TemperatureC;
            existing.Summary = dto.Summary;

            await _service.UpdateWeatherForecastAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetWeatherForecastByIdAsync(id);
            if (existing == null) return NotFound();

            await _service.DeleteWeatherForecastAsync(id);
            return NoContent();
        }
    }
}