using System.Collections.Generic;
using ASP_DotNET_WebAPI_Template.DTOs;
using ASP_DotNET_WebAPI_Template.Models;
using ASP_DotNET_WebAPI_Template.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ASP_DotNET_WebAPI_Template.Services
{
    public class WeatherForecastService
    {
        private readonly ILogger<WeatherForecastService> _logger;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public WeatherForecastService(ILogger<WeatherForecastService> logger, IUnitOfWork unit, IMapper mapper)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _unit = unit ?? throw new System.ArgumentNullException(nameof(unit));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<GetForecastDTO> GetForecastById(int id)
        {
            _logger.LogInformation($"Invoked {nameof(GetForecastById)}:{id}");
            var forecasts = _unit.WeatherForecasts.Get(x => x.ID == id);
            var dto = _mapper.Map<IEnumerable<GetForecastDTO>>(forecasts);
            return dto;
        }

        public void AddForecast(CreateForecastDto dto)
        {
            var forecast = _mapper.Map<WeatherForecast>(dto);
            _unit.WeatherForecasts.Insert(forecast);
        }

        public void DeleteForecast(int id)
        {
            _logger.LogInformation($"Invoked {nameof(DeleteForecast)}:{id}");
            var forecast = _unit.WeatherForecasts.Get(x => x.ID == id);

            if (forecast == null)
                return;

            _unit.WeatherForecasts.Delete(id);
        }
    }
}