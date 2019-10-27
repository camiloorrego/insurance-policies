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

                var user = await _userService.ValidateUser(value.User, value.Password);

                return Ok(new CredentialsResponseDto() { Token = user.CreateToken() });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

    }
}
