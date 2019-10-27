using Insurance.Policies.Controllers;
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Domain.Exceptions;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Domain.Settings;
using Insurance.Policies.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using Xunit;

namespace Insurance.Policies.Test.Controllers
{
    public class UserControllerTest
    {

        private readonly Mock<IUserService> _userService;
        private readonly IOptions<AppSettings> _settings;
        private readonly UsersController _usersController;
        public UserControllerTest()
        {
            _settings = Options.Create(new AppSettings()
            {
                AuthSettings = new AuthSettings()
                {
                    Key= "KeyC4miloOrregoInsurencePolices",
                    ValidAudience="GAP",
                    ValidIssuer="GAP"
                }
            });
            _userService = new Mock<IUserService>();
            _usersController = new UsersController(_userService.Object, _settings);
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

            var auth = _settings.Value.AuthSettings;

            var expected = new CredentialsResponseDto()
            {
                Token = userExpected.CreateToken(auth.Key, auth.ValidAudience, auth.ValidIssuer)
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
