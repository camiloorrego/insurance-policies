
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



        public async Task<User> GetUserByUsername(string username)
        {
            return await _db.GetFistAsync<User>("SELECT [UserId],[Username],[Password],[CreateDate] FROM [dbo].[Users]WHERE [Username] = @Username", new { Username= username });

        }
    }
}
