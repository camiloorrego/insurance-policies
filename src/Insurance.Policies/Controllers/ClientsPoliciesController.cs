using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Policies.Controllers
{
    [Authorize]
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _clientPolicyService.GetByClientId(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "CP500", Message = e.Message });
            }
        }


        // POST: api/ClientsPolicies
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ClientPolicy> value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await _clientPolicyService.Save(value));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "P500", Message = e.Message });
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] List<int> value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await _clientPolicyService.Delete(value));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "P500", Message = e.Message });
            }
        }

    }
}
