using Core.Boundaries.Persistence;
using Core.Models;
using System.Collections.Immutable;

namespace Core.Rules
{
    /// <summary>
    /// Represents the Business rules associated with the <see cref="ChatRoom"/> entity.
    /// </summary>
    public sealed class ChatRoomRules
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        /// <summary>
        /// Initializes a new instance of the ChatRoomRules class.
        /// </summary>
        /// <param name="chatRoomRepository">An implementation of the <see cref="IChatRoomRepository"/>.</param>
        public ChatRoomRules(IChatRoomRepository chatRoomRepository) => _chatRoomRepository = chatRoomRepository;

        /// <summary>
        /// Retrieves all the chat rooms.
        /// </summary>
        /// <returns>An Immutable list with all the chat rooms found.</returns>
        public IImmutableList<ChatRoom> GetAll() => _chatRoomRepository.GetAll();
    }
}
