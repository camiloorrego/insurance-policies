using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Policies.Domain.Services
{
    public class ClientPolicyService : IClientPolicyService
    {
        private readonly IClientPolicyRepository _clientPolicyRepository;
        public ClientPolicyService(IClientPolicyRepository clientPolicyRepository)
        {
            _clientPolicyRepository = clientPolicyRepository;
        }
        public Task<bool> Delete(List<int> id)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<IEnumerable<ClientPolicy>> GetByClientId(int id)
        {
            return await _clientPolicyRepository.GetByClientId(id);
        }

        public Task<bool> Save(List<ClientPolicy> clientPolicy)
        {
            throw new System.NotImplementedException();
        }

    }
}
