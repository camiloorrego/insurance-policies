using Insurance.Policies.Controllers;
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Exceptions;
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
        public async void ValidateLoginOk()
        {
            CredentialsRequestDto request = new CredentialsRequestDto()
            {
                Username = "admin",
                Password = "admin"
            }; ;

            var userExpected = new User()
            {
                UserId = 1,
                Username = "admin",
                Password = "YWRtaW4="
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


        [Fact]
        public async void ValidateLoginNotFound()
        {
            CredentialsRequestDto request = new CredentialsRequestDto()
            {
                Username = "myuser",
                Password = "mypass"
            }; ;

            var expected = new ErrorResponseDto()
            {
                Code = "U404"
            };

            _userService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
            .Callback(() =>
            {
                throw new UserNotFoundException($"User {request.Username} not found");
            });

            // Act
            var result = await _usersController.Post(request);

            // Assert
            var okResult = Assert.IsType<NotFoundObjectResult>(result);


            Assert.Equal(expected.Code, (okResult.Value as ErrorResponseDto).Code);
        }

        [Fact]
        public async void ValidateLoginUnAuthorized()
        {
            CredentialsRequestDto request = new CredentialsRequestDto()
            {
                Username = "myuser",
                Password = "mypass"
            }; ;

            var expected = new ErrorResponseDto()
            {
                Code = "U401"
            };

            _userService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
            .Callback(() =>
            {
                throw new UserUnAuthException($"Invalid credentials");
            });

            // Act
            var result = await _usersController.Post(request);

            // Assert
            var okResult = Assert.IsType<UnauthorizedObjectResult>(result);


            Assert.Equal(expected.Code, (okResult.Value as ErrorResponseDto).Code);
        }
    }
}
