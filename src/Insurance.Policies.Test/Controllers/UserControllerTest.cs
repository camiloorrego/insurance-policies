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
            CredentialsDto request = new CredentialsDto()
            {
                User = "camilo.orrego",
                Password = "camilo1234"
            }; ;

            var expected = new User()
            {
                Name = "camilo.orrego",
                Password = "camilo1234"
            };

            _userService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expected);

            // Act
            var result = await _usersController.Post(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expected, okResult.Value);
        }
    }
}
