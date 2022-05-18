using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System.Text.Json;

namespace ECommerce.Infrastructure.EF
{
    public class ECommerceDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Adress> Adresses { get; set; }

        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.Products)
                .HasConversion(
                    l => JsonConvert.SerializeObject(l),
                    l => JsonConvert.DeserializeObject<List<Product>>(l),
                    new ValueComparer<List<Product>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));

            modelBuilder.Entity<Order>()
                .HasOne<User>()
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasOne<Adress>()
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.AdressId);

            modelBuilder.Entity<Adress>()
                .HasOne<User>()
                .WithMany(u => u.Adresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}