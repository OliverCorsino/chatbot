using Core.Contracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Immutable;
using System.Linq;

namespace Boundary.Persistence.Repositories
{
    /// <summary>
    /// Implements the basic operations of <see cref="T"/> storage functionality
    /// </summary>
    /// <typeparam name="T">Represents the type that be management for the repository.</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        /// <summary>
        /// Represents the <see cref="DbContext"/> used for performing the base operations.
        /// </summary>
        public readonly DbContext Context;

        /// <summary>
        /// Represents the DB Set that we are working with.
        /// </summary>
        public readonly DbSet<T> Set;

        /// <summary>
        /// Initializes an new instance of <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="context">Represents the database context we will working on.</param>
        public GenericRepository(DbContext context)
        {
            Context = context;
            Set = context.Set<T>();
        }

        /// <inheritdoc />
        public IQueryable<T> Table { get; }

        /// <inheritdoc />
        public IImmutableList<T> GetAllAsync() => Set.ToImmutableList();

        /// <inheritdoc />
        public IOperationResult<T> InsertAsync(T entity)
        {
            Context.Add(entity);

            Context.SaveChanges();

            return BasicOperationResult<T>.Ok(entity);
        }

        /// <inheritdoc />
        public IOperationResult<T> UpdateAsync(T entity)
        {
            EntityEntry entityEntry = Context.Entry(entity);
            entityEntry.State = EntityState.Modified;

            Context.SaveChanges();

            return BasicOperationResult<T>.Ok();
        }

        /// <inheritdoc />
        public IOperationResult<T> DeleteAsync(T entity)
        {
            Context.Remove(entity);

            Context.SaveChanges();

            return BasicOperationResult<T>.Ok();
        }
    }
}
