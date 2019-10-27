using System.Collections.Generic;
using System.Threading.Tasks;
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;

namespace Insurance.Policies.Domain.Services
{
    public class RiskTypeService : IRiskTypeService
    {
        private readonly IRiskTypeRepository _riskTypeRepository;
        public RiskTypeService(IRiskTypeRepository riskTypeRepository)
        {
            _riskTypeRepository = riskTypeRepository;
        }
        public async Task<IEnumerable<RiskType>> GetAll()
        {
            return await _riskTypeRepository.GetAll();
        }
    }
}
