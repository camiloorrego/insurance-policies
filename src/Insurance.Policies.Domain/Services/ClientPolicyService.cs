using System.Collections.Generic;
using System.Threading.Tasks;
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;

namespace Insurance.Policies.Domain.Services
{
    public class ClientPolicyService : IClientPolicyService
    {
        private readonly IClientPolicyRepository _clientPolicyRepository;
        public ClientPolicyService(IClientPolicyRepository clientPolicyRepository)
        {
            _clientPolicyRepository = clientPolicyRepository;
        }
        public Task<bool> Delete(ClientPolicy clientPolicy)
        {
            throw new System.NotImplementedException();
        }

        public async  Task<IEnumerable<ClientPolicy>> GetAll()
        {
            return await _clientPolicyRepository.GetAll();
        }

        public Task<bool> Save(ClientPolicy clientPolicy)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(ClientPolicy clientPolicy)
        {
            throw new System.NotImplementedException();
        }
    }
}
