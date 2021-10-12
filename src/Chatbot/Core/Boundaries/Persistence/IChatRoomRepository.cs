using Core.Models;

namespace Core.Boundaries.Persistence
{
    /// <summary>
    /// Represents the repository interface for working with the chat room's persistence
    /// </summary>
    public interface IChatRoomRepository : IGenericRepository<ChatRoom>
    {
    }
}
