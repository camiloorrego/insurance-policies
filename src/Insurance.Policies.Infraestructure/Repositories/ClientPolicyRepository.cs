using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Infraestructure.DataModels;
using Insurance.Policies.Infraestructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Policies.Infraestructure.Repositories
{
    class ClientPolicyRepository : IClientPolicyRepository
    {

        private readonly IDb _db;
        public ClientPolicyRepository(IDb db)
        {
            _db = db;
        }

        public Task<bool> Delete(ClientPolicy clientPolicy)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ClientPolicy>> GetAll()
        {
            var sql = @"SELECT 
                        cp.Id,
                        dbo.Clients.Name AS ClientName, 
                        cp.PoliceId, 
                        cp.CreateDate, 
                        dbo.Polices.Name, 
                        dbo.Polices.Description,
                        dbo.PolicyTypes.Name AS PolicyType, 
                        dbo.RiskTypes.Name AS RiskType, 
                        dbo.PolicyTypes.Coverage, 
                        dbo.Polices.Cost
                        FROM     dbo.ClientsPolices cp INNER JOIN
                        dbo.Clients ON cp.ClientId = dbo.Clients.ClientId INNER JOIN
                        dbo.Polices ON cp.PoliceId = dbo.Polices.PoliceId INNER JOIN
                        dbo.PolicyTypes ON dbo.Polices.PolicyTypeId = dbo.PolicyTypes.PolicyTypeId INNER JOIN
                        dbo.RiskTypes ON dbo.Polices.RiskTypeId = dbo.RiskTypes.RiskTypeId";


            var response = await _db.SelectAsync<ClientPolicyDataModel>(sql, new { });

            return null;
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
