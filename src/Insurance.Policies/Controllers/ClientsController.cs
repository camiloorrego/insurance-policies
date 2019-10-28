using System;
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
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _repository;
        public ClientsController(IClientRepository repository)
        {
            _repository = repository;
        }
        // GET: api/PolicyTypes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _repository.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponseDto() { Code = "PT500", Message = e.Message });
            }
        }
    }
}
