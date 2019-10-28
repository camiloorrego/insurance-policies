using Insurance.Policies.Controllers;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Domain.Services;
using Insurance.Policies.Domain.Settings;
using Insurance.Policies.Dto;
using Insurance.Policies.Infraestructure.Interfaces;
using Insurance.Policies.Infraestructure.Repositories;
using Insurance.Policies.Infraestructure.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Xunit;

namespace Insurance.Policies.Test.Controllers
{
    public class UserIntegrationTest
    {
        private readonly IOptions<AppSettings> _settings;

        public UserIntegrationTest()
        {

            var config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

            _settings = Options.Create(new AppSettings()
            {
                AuthSettings = new AuthSettings()
                {
                    Key = config["authSettings:key"],
                    ValidAudience = config["authSettings:validAudience"],
                    ValidIssuer = config["authSettings:validIssuer"]
                },
                DataBaseSettings = new DataBaseSettings()
                {
                    StringConnection = config["dataBaseSettings:stringConnection"]
                }
            });
            
        }

        [Fact]
        public async void LoginOk()
        {


            IDb _db = new Db(_settings);
            IUserRepository _userRepository = new UserRepository(_db);
            IUserService _userService = new UserService(_userRepository);
            UsersController _usersController = new UsersController(_userService, _settings);

            CredentialsRequestDto request = new CredentialsRequestDto()
            {
                Username = "admin",
                Password = "admin"
            }; ;


            // Act
            var result = await _usersController.Post(request);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(result);

            Assert.NotEmpty((okResult.Value as CredentialsResponseDto).Token);
        }


        [Fact]
        public async void LoginUnauthorized()
        {

            IDb _db = new Db(_settings);
            IUserRepository _userRepository = new UserRepository(_db);
            IUserService _userService = new UserService(_userRepository);
            UsersController _usersController = new UsersController(_userService, _settings);

            CredentialsRequestDto request = new CredentialsRequestDto()
            {
                Username = "admin",
                Password = "admin_"
            }; ;

            var expected = 401;

            // Act
            var result = await _usersController.Post(request);

            var okResult = Assert.IsType<UnauthorizedObjectResult>(result);

            Assert.NotNull(result);

            Assert.Equal(okResult.StatusCode, expected);
        }



        [Fact]
        public async void LoginNoFound()
        {
            IDb _db = new Db(_settings);
            IUserRepository _userRepository = new UserRepository(_db);
            IUserService _userService = new UserService(_userRepository);
            UsersController _usersController = new UsersController(_userService, _settings);


            CredentialsRequestDto request = new CredentialsRequestDto()
            {
                Username = "admin_",
                Password = "admin"
            }; ;

            var expected = 404;

            // Act
            var result = await _usersController.Post(request);

            var okResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.NotNull(result);

            Assert.Equal(okResult.StatusCode, expected);
        }
    }
}
