using AutoMapper;
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Domain.Settings;
using Insurance.Policies.Infraestructure.DataModels;
using Insurance.Policies.Infraestructure.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Policies.Infraestructure.Repositories
{
    public class ClientPolicyRepository : IClientPolicyRepository
    {

        private readonly IMapper _mapper;
        private readonly IDb _db;
        private readonly IOptions<AppSettings> _settings;
        public ClientPolicyRepository(IDb db, IMapper mapper, IOptions<AppSettings> settings)
        {
            _db = db;
            _mapper = mapper;
            _settings = settings;
        }

        public async Task<bool> Delete(List<int> id)
        {
            await _db.ExecuteAsync("SELECT  dbo.ClientsPolices WHERE Id IN @ids", new { ids = id.ToArray() });
            return true;
        }

        public async Task<IEnumerable<ClientPolicy>> GetByClientId(int id)
        {
            var sql = @"SELECT cp.Id, c.Name Client, 
                        p.Name AS PolicyName, 
                        p.Description,
                        p.PoliceId
                        FROM     dbo.ClientsPolices cp INNER JOIN
                        dbo.Clients c ON cp.ClientId = c.ClientId INNER JOIN
                        dbo.Polices p ON cp.PoliceId = p.PoliceId
                        WHERE cp.ClientId=@Id";


            var response = await _db.SelectAsync<ClientPolicyDataModel>(sql, new { Id = id });

            return _mapper.Map<IEnumerable<ClientPolicy>>(response); ;
        }

        public async Task<bool> Save(List<ClientPolicy> clientPolicy)
        {
            var b = "INSERT INTO dbo.ClientsPolices (ClientId ,PoliceId ,UserId) VALUES ({0} ,{1},{2});";
            var sql = "";
            foreach (var item in clientPolicy)
            {
                sql += string.Format(b, item.ClientId, item.PoliceId, _settings.Value.UserId);
            }

            await _db.ExecuteAsync(sql, new { });
            return true;
        }
    }
}
