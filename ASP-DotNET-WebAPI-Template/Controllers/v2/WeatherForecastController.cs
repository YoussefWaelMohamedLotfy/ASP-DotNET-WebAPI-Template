using Microsoft.AspNetCore.Mvc;

namespace ASP_DotNET_WebAPI_Template.Controllers.v2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
    }
}