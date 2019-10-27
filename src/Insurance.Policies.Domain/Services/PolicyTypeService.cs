using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Policies.Domain.Services
{
    public class PolicyTypeService : IPolicyTypeService
    {
        private readonly IPolicyTypeRepository _policyTypeRepository;
        public PolicyTypeService(IPolicyTypeRepository policyTypeRepository)
        {
            _policyTypeRepository = policyTypeRepository;
        }
        public async Task<IEnumerable<PolicyType>> GetAll()
        {
            return await _policyTypeRepository.GetAll();
        }
    }
}
