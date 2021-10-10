using Boundary.Persistence.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Reflection;

namespace Boundary.Persistence.Contexts
{
    /// <summary>
    /// Represents the default database context used for this application.
    /// </summary>
    public sealed class DefaultDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Creates a new instance for the DefaultDbContext class.
        /// </summary>
        /// <param name="options">Represents the <see cref="DbContextOptions"/> this context will be using.</param>
        public DefaultDbContext(DbContextOptions options) : base(options) { }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const int maxLength = 300;
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DefaultDbContext)));

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string)))
            {
                if (!property.GetMaxLength().HasValue)
                {
                    property.AsProperty().Builder.HasMaxLength(maxLength, ConfigurationSource.Convention);
                }

                property.SetColumnType($"varchar({property.GetMaxLength().Value})");
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
