namespace Core.Contracts
{
    /// <summary>
    /// Represents a generic operation result.
    /// </summary>
    /// <typeparam name="T">Represents an entity that could be use for this Operation Result.</typeparam>
    public interface IOperationResult<T>
    {
        /// <summary>
        /// Represents any message you want to send after an operation is completed.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Represents if an operation complete either success or not.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Represent the entity used for an operation.
        /// </summary>
        T Entity { get; }
    }
}
