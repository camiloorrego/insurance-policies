using AutoMapper;
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Infraestructure.DataModels;
using Insurance.Policies.Infraestructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Policies.Infraestructure.Repositories
{
   public class PolicyTypeRepository : IPolicyTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly IDb _db;
        public PolicyTypeRepository(IDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PolicyType>> GetAll()
        {
            var response = await _db.SelectAsync<PolicyTypeDataModel>("SELECT [PolicyTypeId] Id, [Name], [Coverage] FROM[dbo].[PolicyTypes]", new { });

            return _mapper.Map<IEnumerable<PolicyType>>(response);
        }

        public async Task<PolicyType> GetById(int id)
        {
            var response = await _db.GetAsync<PolicyTypeDataModel>("SELECT [PolicyTypeId] Id, [Name], [Coverage] FROM[dbo].[PolicyTypes] WHERE [PolicyTypeId]=@Id ", new { Id = id });

            return _mapper.Map<PolicyType>(response);
        }
    }
}
