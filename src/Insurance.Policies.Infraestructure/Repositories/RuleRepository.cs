using AutoMapper;
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Infraestructure.DataModels;
using Insurance.Policies.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Policies.Infraestructure.Repositories
{
   public class RuleRepository: IRuleRepository
    {
        private readonly IMapper _mapper;
        private readonly IDb _db;
        public RuleRepository(IDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Rule>> GetAll()
        {
            var response = await _db.SelectAsync<RuleDataModel>("select RiskTypeId, Type, Value from  [dbo].[Rules]", new { });

            return _mapper.Map<IEnumerable<Rule>>(response);
        }
    }
}
