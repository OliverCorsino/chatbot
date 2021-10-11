using System.Collections.Immutable;
using System.Linq;

namespace Core.Contracts
{
    /// <summary>
    /// Represents the main operation the different repositories entity should do.
    /// </summary>
    /// <typeparam name="T">Represents the entity that will be using the operations.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Represents the entity.
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Retrieves all the records from the database.
        /// </summary>
        /// <returns>An immutable list with the records found.</returns>
        IImmutableList<T> GetAllAsync();

        /// <summary>
        /// Inserts a new record to the database.
        /// </summary>
        /// <param name="entity">The record that will be inserted.</param>
        /// <returns>An <see cref="IOperationResult{T}"/> for this process.</returns>
        IOperationResult<T> InsertAsync(T entity);

        /// <summary>
        /// Updates an existing record from the database.
        /// </summary>
        /// <param name="entity">The record that will be updated.</param>
        /// <returns>An <see cref="IOperationResult{T}"/> for this process.</returns>
        IOperationResult<T> UpdateAsync(T entity);

        /// <summary>
        /// Deletes an existing record from the database.
        /// </summary>
        /// <param name="entity">The record that will be deleted.</param>
        /// <returns>An <see cref="IOperationResult{T}"/> for this process.</returns>
        IOperationResult<T> DeleteAsync(T entity);
    }
}
