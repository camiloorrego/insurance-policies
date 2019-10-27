using Insurance.Policies.Domain.Exceptions;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Domain.Settings;
using Insurance.Policies.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Insurance.Policies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptions<AppSettings> _settings;
        public UsersController(IUserService userService, IOptions<AppSettings> settings)
        {
            _userService = userService;
            _settings = settings;
        }

        // POST: api/Users
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> Post([FromBody] CredentialsRequestDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userService.ValidateUser(value.Username, value.Password);
                var auth = _settings.Value.AuthSettings;

                var response = new CredentialsResponseDto()
                {
                    Token = user.CreateToken(auth.Key, auth.ValidAudience, auth.ValidIssuer)
                };

                return Ok(response);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(new ErrorResponseDto() { Code = "U404", Message = e.Message });
            }
            catch (UserUnAuthException e)
            {
                return Unauthorized(new ErrorResponseDto() { Code = "U401", Message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "U500", Message = e.Message });
            }
        }

    }
}
