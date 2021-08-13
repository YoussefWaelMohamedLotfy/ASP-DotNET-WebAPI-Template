using System;
using System.Collections.Generic;
using System.Linq;
using ASP_DotNET_WebAPI_Template.DTOs;
using ASP_DotNET_WebAPI_Template.Models;
using ASP_DotNET_WebAPI_Template.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP_DotNET_WebAPI_Template.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper _mapper;
        private readonly ISendGridEmailService _mailService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper, ISendGridEmailService mailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        [HttpGet]
        public IEnumerable<GetForecastDTO> Get()
        {
            _logger.LogInformation("Accessed Get Forecast");
            _mailService.SendEmailAsync("recipientemail@gmail.com", "Hello from Sendgrid", "you tried to access /api/weatherforecast");

            var rng = new Random();
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            var dto = _mapper.Map<IEnumerable<GetForecastDTO>>(forecasts);
            return dto;
        }
    }
}
