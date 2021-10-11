using Core.Contracts;

namespace Core.Models
{
    /// <summary>
    /// Represents a generic response.
    /// </summary>
    /// <typeparam name="T">Represent the entity you want to return in the response.</typeparam>
    public sealed class BasicOperationResult<T> : IOperationResult<T>
    {
        private BasicOperationResult(string message, bool success, T entity)
        {
            Message = message;
            Success = success;
            Entity = entity;
        }

        /// <summary>
        /// Represents the message's operation result.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Represents if the operation was successful or not.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Represents the operation result
        /// </summary>
        public T Entity { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="BasicOperationResult{T}"/> for successful operations
        /// with the <see cref="T"/> default value
        /// </summary>
        /// <returns>An instance of <see cref="BasicOperationResult{T}"/> successful</returns>
        public static BasicOperationResult<T> Ok()
            => new BasicOperationResult<T>(string.Empty, true, default(T));

        /// <summary>
        /// Initializes a new instance of <see cref="BasicOperationResult{T}"/> for successful operations.
        /// </summary>
        /// <param name="entity">An instance of <see cref="T"/></param>
        /// <returns>An instance of <see cref="BasicOperationResult{T}"/> successful</returns>
        public static BasicOperationResult<T> Ok(T entity)
            => new BasicOperationResult<T>(string.Empty, true, entity);

        /// <summary>
        /// Initializes a new instance of <see cref="BasicOperationResult{T}"/> for a fail case.
        /// </summary>
        /// <param name="message">An <see cref="string"/> value that represents a error message.</param>
        /// <returns>An instance of <see cref="BasicOperationResult{T}"/> for a failed operation.</returns>
        public static BasicOperationResult<T> Fail(string message) =>
            new BasicOperationResult<T>(message, false, default(T));

        /// <summary>
        /// Initializes a new instance of <see cref="BasicOperationResult{T}"/> for a fail case.
        /// </summary>
        /// <param name="message">An <see cref="string"/> value that represents a error message</param>
        /// <param name="errorDetail"> error detail</param>
        /// <returns>An instance of <see cref="BasicOperationResult{T}"/> for a failed operation.</returns>
        public static BasicOperationResult<T> Fail(string message, T errorDetail) =>
            new BasicOperationResult<T>(message, false, errorDetail);
    }
}