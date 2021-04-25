using AutoMapper;

using HotelListing.Data;
using HotelListing.Models;
using HotelListing.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        //private readonly SignInManager<ApiUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authmanager;

        public AccountController(UserManager<ApiUser> userManager,
            //SignInManager<ApiUser> signInManager,
            ILogger<AccountController> logger,
            IMapper mapper,IAuthManager authManager)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _authmanager = authManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registeraation Attempt for {userDTO.Email}");
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user,userDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    //return BadRequest("$User Registeraation Attempt Failed");
                    return BadRequest(ModelState);

                }

                await _userManager.AddToRolesAsync(user, userDTO.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Registeraation went wroung in the {nameof(Register)}");
                return Problem($"Registeraation went wroung in the {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login Attempt for {userDTO.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authmanager.ValidateUSer(userDTO))
                {
                    return Unauthorized();
                }
                return Accepted(new { Token=await _authmanager.CreateToken()});
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Login went wroung in the {nameof(Login)}");
                return Problem($"Login went wroung in the {nameof(Register)}", statusCode: 500);
            }
        }
    }
}
