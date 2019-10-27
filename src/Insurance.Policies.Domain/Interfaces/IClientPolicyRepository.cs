using Insurance.Policies.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Policies.Domain.Interfaces
{
    public interface IClientPolicyRepository
    {
        Task<IEnumerable<ClientPolicy>> GetAll();
        Task<bool> Save(ClientPolicy clientPolicy);
        Task<bool> Update(ClientPolicy clientPolicy);
        Task<bool> Delete(ClientPolicy clientPolicy);
    }
}
