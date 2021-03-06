using System;
using System.Threading.Tasks;
using ASP_DotNET_WebAPI_Template.DTOs;
using ASP_DotNET_WebAPI_Template.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP_DotNET_WebAPI_Template.Controllers.v2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetForecastById(int id)
        {
            _logger.LogInformation($"Called Route: {nameof(GetForecastById)}:{id}");

            var forecast = await _service.GetForecastById(id);

            if (forecast == null)
                return NotFound();

            return Ok(forecast);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InsertForecast([FromBody] CreateForecastDto dto)
        {
            _logger.LogInformation($"Called Route: {nameof(InsertForecast)}");
            await _service.AddForecast(dto);
            return Ok("Created Successfully");
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteForecast(int id)
        {
            _logger.LogInformation($"Called Route: {nameof(DeleteForecast)}:{id}");

            var forecast = await _service.GetForecastById(id);

            if (forecast == null)
                return NotFound();

            await _service.DeleteForecast(id);
            return Ok(new { Message = "Delete Success" });
        }
    }
}