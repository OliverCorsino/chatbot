using Boundary.Persistence.Contexts;
using Core.Boundaries.Persistence;
using Core.Models;

namespace Boundary.Persistence.Repositories
{
    /// <inheritdoc cref="GenericRepository{T}"/>
    public sealed class ChatRoomRepository : GenericRepository<ChatRoom>, IChatRoomRepository
    {
        private DefaultDbContext _defaultDbContext;

        /// <summary>
        /// Initializes a new instance of a ChatRoomRepository class.
        /// </summary>
        /// <param name="defaultDbContext">Represents the default DB context.</param>
        public ChatRoomRepository(DefaultDbContext defaultDbContext) : base(defaultDbContext) => _defaultDbContext = defaultDbContext;
    }
}
