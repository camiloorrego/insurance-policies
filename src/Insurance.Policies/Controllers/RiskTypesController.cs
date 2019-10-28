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
    public class RiskTypesController : ControllerBase
    {

        private readonly IRiskTypeRepository _riskTypeRepository;
        public RiskTypesController(IRiskTypeRepository riskTypeRepository)
        {
            _riskTypeRepository = riskTypeRepository;
        }

        // GET: api/RiskTypes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _riskTypeRepository.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "PT500", Message = e.Message });
            }
        }
    }
}
