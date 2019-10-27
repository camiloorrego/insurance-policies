using Insurance.Policies.Domain.Exceptions;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Insurance.Policies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CredentialsRequestDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userService.ValidateUser(value.Username, value.Password);

                return Ok(new CredentialsResponseDto() { Token = user.CreateToken() });
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
