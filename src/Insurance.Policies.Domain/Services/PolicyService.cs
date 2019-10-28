using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Exceptions;
using Insurance.Policies.Domain.Factories;
using Insurance.Policies.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Policies.Domain.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IRiskTypeRepository _riskTypeRepository;
        private readonly IRuleRepository _ruleRepository;
        private readonly IPolicyTypeRepository _policyTypeRepository;
        public PolicyService(IPolicyRepository policyRepository,
            IRiskTypeRepository riskTypeRepository,
            IRuleRepository ruleRepository,
            IPolicyTypeRepository policyTypeRepository)
        {
            _policyRepository = policyRepository;
            _riskTypeRepository = riskTypeRepository;
            _ruleRepository = ruleRepository;
            _policyTypeRepository = policyTypeRepository;
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
            var type = await _policyTypeRepository.GetById(policy.PolicyTypeId);

            var rules = await _ruleRepository.GetAll();


            rules.Where(x => x.RiskTypeId == policy.RiskTypeId).ToList()
                .ForEach(r =>
                {
                    var rule = RulesFactory.Build(r.Type, r.Value);

                    var x = rule.Invoke(type.Coverage);

                    if (x)
                    {
                        throw new PolicyInvalidException($"{r.Type} {r.Value}");
                    }

                });


            return await _policyRepository.Save(policy);
        }

        public async Task<bool> Update(Policy policy)
        {
            return await _policyRepository.Update(policy);
        }
    }
}
