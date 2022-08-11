using Geekburger.Dashboard.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geekburger.Dashboard.Database
{
    public class DashboardDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<Restriction> Restrictions { get; set; }
        public DbSet<Order> Orders { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DashboardDbContext(IConfiguration config)
        {
            _config = config;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_config.GetConnectionString("DashboardDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restriction>().HasKey(s => new { s.UserId, s.Name });
            modelBuilder.Entity<Order>().HasKey(s => new { s.OrderId, s.StoreName });
        }

        public static void EnsureCreated(IApplicationBuilder app)
        {
            var factory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            if (factory is not null)
            {
                using var scope = factory.CreateScope();

                var ctx = scope.ServiceProvider.GetService<DashboardDbContext>();
                if (ctx is not null)
                {
                    if (ctx is not null)
                    {
                        ctx.Database.EnsureCreated();
                    }
                }
            }
        }
    }
}