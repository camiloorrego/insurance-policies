using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Exceptions;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Insurance.Policies.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        public PoliciesController(IPolicyService policyService)
        {
            _policyService = policyService;
        }
        // GET: api/InsurancePolicies
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _policyService.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "P500", Message = e.Message });
            }
        }

        // POST: api/InsurancePolicies
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Policy value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await _policyService.Save(value));
            }
            catch (PolicyInvalidException e)
            {
                return Conflict(new ErrorResponseDto() { Code = "P409", Message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "P500", Message = e.Message });
            }
        }

        // PUT: api/InsurancePolicies/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Policy value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await _policyService.Update(value));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "P500", Message = e.Message });
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await _policyService.Delete(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "P500", Message = e.Message });
            }
        }
    }
}
