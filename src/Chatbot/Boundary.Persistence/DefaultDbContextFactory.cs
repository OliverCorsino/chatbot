using Boundary.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace Boundary.Persistence
{
    /// <summary>
    /// Represents the default db context factory for this application.
    /// </summary>
    public sealed class DefaultDbContextFactory : IDesignTimeDbContextFactory<DefaultDbContext>
    {
        private readonly IConfigurationRoot _config;

        public DefaultDbContextFactory()
        {
            var basePath = AppContext.BaseDirectory;
            var environmentName = Environment.GetEnvironmentVariable("Hosting:Environment");

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        /// <inheritdoc />
        public DefaultDbContext CreateDbContext(string[] args)
            => Create(_config.GetConnectionString("DefaultDbConnection"));

        private static DefaultDbContext Create(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));
            }

            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new DefaultDbContext(optionsBuilder.Options);
        }
    }
}
