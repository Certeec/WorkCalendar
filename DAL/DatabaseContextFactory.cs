using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            // Wczytaj konfigurację z lokalnego appsettings.json w DAL
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Katalog DAL
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return new DatabaseContext(configuration);
        }
    }
}