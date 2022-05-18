using ECommerce.Infrastructure.EF;
using ECommerce.IntegratedTests.Helpers;
using ECommerce.Models.DTO;
using ECommerce.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.IntegratedTests.Controllers
{
    public class AccountControllerFixture
    {
        public HttpClient Client { get; set; }
        public Mock<IAccountService> AccountServiceMock { get; set; }

        public AccountControllerFixture()
        {
            var factory = new WebApplicationFactory<Program>();

            AccountServiceMock = new Mock<IAccountService>();

            factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContext = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ECommerceDbContext>));
                    if (dbContext != null)
                    {
                        services.Remove(dbContext);
                    }
                    services.AddDbContext<ECommerceDbContext>(options =>
                        options.UseInMemoryDatabase("test"));
                    services.AddSingleton(AccountServiceMock.Object);
                });
            });

            Client = factory.CreateClient();
        }
    }

    public class AccountControllerTests : IClassFixture<AccountControllerFixture>
    {
        private readonly HttpClient _client;
        private readonly Mock<IAccountService> _accountServiceMock;

        public AccountControllerTests(AccountControllerFixture fixture)
        {
            _client = fixture.Client;
            _accountServiceMock = fixture.AccountServiceMock;
        }

        [Fact]
        public async Task Register_ForValidModel_ShouldReturnOkResponse()
        {
            //arange
            var registerModel = new RegisterUser()
            {
                Email = "test@test.com",
                FirstName = "test",
                LastName = "test",
                Password = "test123",
                ConfirmPassword = "test123",
                Phone = "test"
            };

            var content = registerModel.ToJsonStringContent();

            //act
            var response = await _client.PostAsync("/api/account", content);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Register_ForInvalidModel_ShouldReturnBadRequestResposne()
        {
            //arange
            var registerModel = new RegisterUser()
            {
                Email = "test@test.com"
            };

            var content = registerModel.ToJsonStringContent();

            //act
            var response = await _client.PostAsync("/api/account", content);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Login_ForRegisteredUser_ShoudReturnOkRespond()
        {
            _accountServiceMock.Setup(x => x.Login(It.IsAny<LoginUser>()))
                .Returns(Task.FromResult("jwt"));

            var loginDto = new LoginUser()
            {
                Email = "test@test.com",
                Password = "test123"
            };

            var content = loginDto.ToJsonStringContent();

            var response = await _client.PostAsync("/api/account/login", content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}