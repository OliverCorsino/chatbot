using System;

namespace Core.Models
{
    /// <summary>
    /// Represents a simple message sent for an user in a chat room.
    /// </summary>
    public sealed class ChatMessage
    {
        /// <summary>
        /// Represents the author of the message.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Represents when a message has been sent in a chat room.
        /// </summary>
        public DateTime SentOnUtc { get; set; }

        /// <summary>
        /// Represents a simple message.
        /// </summary>
        public string Message { get; set; }
    }
}
