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
        public async Task<bool> Delete(List<int> id)
        {
            return await _clientPolicyRepository.Delete(id);
        }
        
        public async Task<IEnumerable<ClientPolicy>> GetByClientId(int id)
        {
            return await _clientPolicyRepository.GetByClientId(id);
        }

        public async Task<bool> Save(List<ClientPolicy> clientPolicy)
        {
            return await _clientPolicyRepository.Save(clientPolicy);
        }

    }
}
