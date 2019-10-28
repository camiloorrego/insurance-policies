using AutoMapper;
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Infraestructure.DataModels;
using Insurance.Policies.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Policies.Infraestructure.Repositories
{
  public  class ClientRepository: IClientRepository
    {
        private readonly IMapper _mapper;
        private readonly IDb _db;
        public ClientRepository(IDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            var response = await _db.SelectAsync<ClientDataModel>("SELECT [ClientId] ,[Name]  FROM [dbo].[Clients]", new { });

            return _mapper.Map<IEnumerable<Client>>(response);
        }
    }
}
