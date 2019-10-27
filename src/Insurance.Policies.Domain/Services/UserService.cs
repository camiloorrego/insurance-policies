using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Exceptions;
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
                throw new UserNotFoundException($"User {username} not found");
            }

            if (user.Password != Decoder.Encode(password))
            {
                throw new UserUnAuthException($"Invalid credentials"); ;
            }

            return user;
        }
    }
}
