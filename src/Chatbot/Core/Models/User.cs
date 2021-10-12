using Microsoft.AspNetCore.Identity;
using System;

namespace Core.Models
{
    /// <summary>
    /// Represents the entity for the user that will be chatting in the application.
    /// </summary>
    public sealed class User : IdentityUser
    {
        /// <summary>
        /// Represents the navigation property for accessing to the associated data.
        /// </summary>
        public Guid ChatRoomId { get; set; }

        /// <summary>
        /// Represents the relationship between the <see cref="User"/> entity and <see cref="Models.ChatRoom"/> entity.
        /// </summary>
        public ChatRoom ChatRoom { get; set; }
    }
}