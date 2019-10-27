using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Policies.Domain.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyRepository;
        public PolicyService(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }
        public async Task<bool> Delete(int id)
        {
            return await _policyRepository.Delete(id);
        }

        public async Task<IEnumerable<Policy>> GetAll()
        {
            return await _policyRepository.GetAll();
        }

        public async Task<bool> Save(Policy policy)
        {
            return await _policyRepository.Save(policy); 
        }

        public async Task<bool> Update(Policy policy)
        {
            return await _policyRepository.Update(policy);
        }
    }
}
