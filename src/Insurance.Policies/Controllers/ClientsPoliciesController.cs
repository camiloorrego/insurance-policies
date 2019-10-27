using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Insurance.Policies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsPoliciesController : ControllerBase
    {

        private readonly IClientPolicyService _clientPolicyService;
        public ClientsPoliciesController(IClientPolicyService clientPolicyService)
        {
            _clientPolicyService = clientPolicyService;
        }

        // GET: api/ClientsPolicies
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _clientPolicyService.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "CP500", Message = e.Message });
            }
        }


        // POST: api/ClientsPolicies
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ClientsPolicies/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
