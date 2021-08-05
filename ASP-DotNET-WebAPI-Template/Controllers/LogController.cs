using ASP_DotNET_WebAPI_Template.Models;
using ASP_DotNET_WebAPI_Template.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP_DotNET_WebAPI_Template.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly LogService _service;

        public LogController(LogService service)
        {
            _service = service ?? throw new System.ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public IActionResult GetLogs([FromQuery] RequestParams param)
        {
            var logs = _service.GetPaginatedLogs(param);
            return Ok(logs);
        }
    }
}