using Insurance.Policies.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Policies.Domain.Interfaces
{
    public interface IClientPolicyRepository
    {
        Task<bool> Save(List<ClientPolicy> clientPolicy);
        Task<bool> Delete(List<int> id);
        Task<IEnumerable<ClientPolicy>> GetByClientId(int id);
    }
}
