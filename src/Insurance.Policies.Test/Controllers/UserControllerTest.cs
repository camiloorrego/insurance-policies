using Insurance.Policies.Controllers;
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Insurance.Policies.Test.Controllers
{
    public class UserControllerTest
    {

        private readonly Mock<IUserService> _userService;
        private readonly UsersController _usersController;
        public UserControllerTest()
        {
            _userService = new Mock<IUserService>();
            _usersController = new UsersController(_userService.Object);
        }

        [Fact]
        public async void ValidateLogin()
        {
            CredentialsRequestDto request = new CredentialsRequestDto()
            {
                User = "camilo.orrego",
                Password = "camilo1234"
            }; ;

            var userExpected = new User()
            {
                UserId = 1,
                Username = "camilo.orrego",
                Password = "camilo1234"
            };

            var expected = new CredentialsResponseDto()
            {
                Token = userExpected.CreateToken()
            };

            _userService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(userExpected);

            // Act
            var result = await _usersController.Post(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);


            Assert.Equal(expected.Token, (okResult.Value as CredentialsResponseDto).Token);
        }
    }
}
