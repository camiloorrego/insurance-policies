using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Dto;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Post([FromBody] CredentialsDto value)
        {
            return null;
        }

    }
}
