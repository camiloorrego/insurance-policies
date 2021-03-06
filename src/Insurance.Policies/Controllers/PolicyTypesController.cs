﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Policies.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyTypesController : ControllerBase
    {
        private readonly IPolicyTypeRepository _policyTypeRepository;
        public PolicyTypesController(IPolicyTypeRepository policyTypeRepository )
        {
            _policyTypeRepository = policyTypeRepository;
        }
        // GET: api/PolicyTypes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _policyTypeRepository.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "PT500", Message = e.Message });
            }
        }
    }
}
