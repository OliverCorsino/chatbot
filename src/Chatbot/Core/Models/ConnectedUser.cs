namespace Core.Models
{
    /// <summary>
    /// Represents an user that has entered into a chat room.
    /// </summary>
    public sealed class ConnectedUser
    {
        /// <summary>
        /// Represents the unique connection id for an user.
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// Represents who is connected.
        /// </summary>
        public string Username { get; set; }
    }
}
