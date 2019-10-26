using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Utilities;
using System.Threading.Tasks;

namespace Insurance.Policies.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> ValidateUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return null;
            }

            if (user.Password != Decoder.Encode(password))
            {
                return null;
            }

            return user;
        }
    }
}
