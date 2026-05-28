using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ExpandMicroservice.Infrastructure.EntityFramework;

namespace ExpandMicroservice.DomainApp
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=ExpandMicroserviceDB;Username=postgres;Password=123");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}