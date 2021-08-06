using System;
using System.Threading.Tasks;
using ASP_DotNET_WebAPI_Template.DTOs;
using ASP_DotNET_WebAPI_Template.Models;
using ASP_DotNET_WebAPI_Template.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP_DotNET_WebAPI_Template.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
                                 IAuthManager authManager,
                                 ILogger<AccountController> logger,
                                 IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _authManager = authManager ?? throw new ArgumentNullException(nameof(authManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            _logger.LogInformation($"Registration Attempt for {userDto.Email}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<AppUser>(userDto);
            user.UserName = userDto.Email;
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest("User Registration Attempt failed");
            }

            await _userManager.AddToRolesAsync(user, userDto.Roles);
            return Accepted();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDto)
        {
            _logger.LogInformation($"Login Attempt for {loginUserDto.Email}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _authManager.ValidateUser(loginUserDto))
                return Unauthorized("User Login Attempt failed");

            return Accepted(new { Token = await _authManager.CreateToken() });
        }
    }
}