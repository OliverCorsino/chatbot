using System;
using System.Collections.Generic;

namespace Core.Models
{
    /// <summary>
    /// Represents a room where an user can join for chatting.
    /// </summary>
    public sealed class ChatRoom
    {
        /// <summary>
        /// Represents the chat room unique identification.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Represents the name associated to a chat room.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents a brief information about a chat room is about.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Represents the relationship between the <see cref="User"/> entity and <see cref="ChatRoom"/> entity.
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}
