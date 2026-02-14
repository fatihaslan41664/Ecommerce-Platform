using EticaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EticaretAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EticaretAPIDbContext>
    {
        public EticaretAPIDbContext CreateDbContext(string[] args)
        {
            // Konfigürasyonu oluştur
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EticaretAPI.API"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Bağlantı dizesini al
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // DbContext yapılandırması
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<EticaretAPIDbContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionString);

            return new EticaretAPIDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
