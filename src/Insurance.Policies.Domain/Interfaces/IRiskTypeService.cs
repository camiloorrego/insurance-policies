using Insurance.Policies.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Policies.Domain.Interfaces
{
    public interface IRiskTypeService
    {
        Task<IEnumerable<RiskType>> GetAll();
    }
}
