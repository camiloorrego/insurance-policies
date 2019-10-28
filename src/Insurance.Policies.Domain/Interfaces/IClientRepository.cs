using Insurance.Policies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Policies.Domain.Interfaces
{
   public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll();
    }
}
