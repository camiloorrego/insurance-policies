using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurance.Policies.Domain.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Insurance.Policies.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePoliciesController : ControllerBase
    {
        public InsurancePoliciesController()
        {
            
        }
        // GET: api/InsurancePolicies
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/InsurancePolicies/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/InsurancePolicies
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/InsurancePolicies/5
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
