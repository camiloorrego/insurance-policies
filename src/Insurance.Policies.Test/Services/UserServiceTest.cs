using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Domain.Services;
using Moq;
using Xunit;

namespace Insurance.Policies.Test.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly UserService _userService;
        public UserServiceTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _userService = new UserService(_userRepository.Object);
        }
        
        [Fact]
        public async void ValidateLogin()
        {
            var user = new User()
            {
                UserId = 1,
                Username = "camilo.orrego",
                Password = "Y2FtaWxvMTIzNA=="
            };
            
            _userRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _userService.ValidateUser("camilo.orrego", "camilo1234");
            
            Assert.NotNull(result);
            Assert.Equal(user.Password, result.Password);
        }
    }
}
