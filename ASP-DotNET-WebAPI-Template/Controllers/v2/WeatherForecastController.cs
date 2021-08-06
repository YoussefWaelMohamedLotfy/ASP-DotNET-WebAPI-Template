using System.Collections.Generic;
using ASP_DotNET_WebAPI_Template.DTOs;
using ASP_DotNET_WebAPI_Template.Repositories.Interfaces;
using ASP_DotNET_WebAPI_Template.Services;
using AutoMapper;
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
        private readonly WeatherForecastService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastService service)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _service = service ?? throw new System.ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetForecastById(int id)
        {
            _logger.LogInformation($"Called Route: {nameof(GetForecastById)}:{id}");
            return Ok(_service.GetForecastById(id));
        }

        [HttpPost]
        public IActionResult InsertForecast([FromBody] CreateForecastDto dto)
        {
            _logger.LogInformation($"Called Route: {nameof(InsertForecast)}");
            _service.AddForecast(dto);
            return Ok("Created Successfully");
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteForecast(int id)
        {
            _logger.LogInformation($"Called Route: {nameof(DeleteForecast)}:{id}");
            _service.DeleteForecast(id);
            return Ok();
        }
    }
}