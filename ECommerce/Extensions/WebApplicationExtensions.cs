using ECommerce.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseSeeder(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();

                seeder.Seed();
            }
        }
    }
}