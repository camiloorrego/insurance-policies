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

        private readonly IRiskTypeService _riskTypeService;
        public RiskTypesController(IRiskTypeService riskTypeService)
        {
            _riskTypeService = riskTypeService;
        }

        // GET: api/RiskTypes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _riskTypeService.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "PT500", Message = e.Message });
            }
        }
    }
}
