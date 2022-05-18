using ECommerce.Infrastructure.EF;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;

namespace ECommerce.IntegratedTests.Controllers
{
    public class ProductControllerTests
    {
        public class ClientFixture
        {
            public HttpClient Client { get; }
            public IServiceScopeFactory ServiceFactory { get; }

            public ClientFixture()
            {
                var factory = new WebApplicationFactory<Program>();

                ServiceProvider provider;

                factory = factory
                    .WithWebHostBuilder(builder =>
                    {
                        builder.ConfigureServices(services =>
                        {
                            var contextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<ECommerceDbContext>));
                            if (contextOptions != null)
                            {
                                services.Remove(contextOptions);
                            }

                            services.AddDbContext<ECommerceDbContext>(options =>
                            {
                                options.UseInMemoryDatabase("ECommerceDb");
                            });

                            services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                            provider = services.BuildServiceProvider();

                            services.AddMvc(options => options.Filters.Add(new FakeUserFilter()));
                        });
                    });

                Client = factory.CreateClient();
                ServiceFactory = factory.Services.GetService<IServiceScopeFactory>();
            }
        }
    }
}