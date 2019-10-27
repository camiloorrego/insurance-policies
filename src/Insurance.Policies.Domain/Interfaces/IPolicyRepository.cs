using Insurance.Policies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Policies.Domain.Interfaces
{
   public interface IPolicyRepository
    {
        Task<IEnumerable<Policy>> GetAll();
        Task<bool> Save(Policy policy);
        Task<bool> Update(Policy policy);
        Task<bool> Delete(int id);
    }
}
