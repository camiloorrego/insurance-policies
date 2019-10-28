using AutoMapper;
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Domain.Settings;
using Insurance.Policies.Infraestructure.DataModels;
using Insurance.Policies.Infraestructure.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Policies.Infraestructure.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly IMapper _mapper;
        private readonly IDb _db;
        private readonly IOptions<AppSettings> _settings;
        public PolicyRepository(IDb db, IMapper mapper, IOptions<AppSettings> settings)
        {
            _db = db;
            _mapper = mapper;
            _settings = settings;
        }

        public async Task<bool> Delete(int id)
        {
            await _db.ExecuteAsync("DELETE dbo.Polices WHERE PoliceId = @Id", new { Id = id });
            return true;
        }

        public async Task<IEnumerable<Policy>> GetAll()
        {
            var sql = @"SELECT p.PoliceId, 
                        p.Description, 
                        p.Name, 
                        p.PolicyTypeId, 
                        p.EffectiveDate, 
                        p.CreateDate, 
                        p.Terms, 
                        p.Cost, 
                        p.RiskTypeId, 
                        p.UserId, 
                        t.Name AS PolicyType, 
                        t.Coverage, 
                        r.Name AS RiskType
                        FROM     dbo.Polices p  INNER JOIN
                        dbo.PolicyTypes t ON p.PolicyTypeId = t.PolicyTypeId INNER JOIN
                        dbo.RiskTypes r ON p.RiskTypeId = r.RiskTypeId";


            var response = await _db.SelectAsync<PolicyDataModel>(sql, new { });

            return _mapper.Map<IEnumerable<Policy>>(response);
        }

        public async Task<bool> Save(Policy policy)
        {
            policy.UserId = _settings.Value.UserId;

            var sql = @"INSERT INTO dbo.Polices(Name,Description,PolicyTypeId,EffectiveDate,Terms,Cost,RiskTypeId,UserId)
                        VALUES(@Name, @Description, @PolicyTypeId, @EffectiveDate, @Terms, @Cost, @RiskTypeId, @UserId)";

            await _db.ExecuteAsync(sql, policy);
            return true;
        }

        public async Task<bool> Update(Policy policy)
        {
            var sql = @"UPDATE dbo.Polices
                        SET Name = @Name, 
                        Description = @Description,
                        PolicyTypeId = @PolicyTypeId, 
                        EffectiveDate = @EffectiveDate, 
                        Terms = @Terms, 
                        Cost = @Cost, 
                        RiskTypeId = @RiskTypeId
                        WHERE PoliceId=@PoliceId";

            await _db.ExecuteAsync(sql, policy);
            return true;
        }
    }
}
