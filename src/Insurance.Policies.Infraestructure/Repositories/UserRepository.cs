
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Infraestructure.Interfaces;
using System;
using System.Threading.Tasks;

namespace Insurance.Policies.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDb _db;
        public UserRepository(IDb db)

        {
            _db = db;
        }



        public Task<User> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
