using ECommerce.Entities;
using ECommerce.Infrastructure.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure
{
    public class Seeder
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public Seeder(ECommerceDbContext context, IPasswordHasher<User> passwordHasher)
            => (_dbContext, _passwordHasher) = (context, passwordHasher);

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (_dbContext.Database.IsRelational())
                {
                    var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                    if (pendingMigrations != null && pendingMigrations.Any())
                    {
                        _dbContext.Database.Migrate();
                    }
                }
                if (!_dbContext.Products.Any())
                {
                    _dbContext.Products.AddRange(GetProducts());
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Users.Any())
                {
                    _dbContext.Users.Add(GetUser());
                    _dbContext.SaveChanges();
                }
            }
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product("Product",10.5, "Description", 2)
            };
        }

        private User GetUser()
        {
            var admin = new User("admin@admin.pl", "Admin", "Admin", "fakePhone");
            var password = "Password";

            var hash = _passwordHasher.HashPassword(admin, password);
            admin.SetPasswordHash(hash);
            admin.SetRole("Admin");

            return admin;
        }
    }
}