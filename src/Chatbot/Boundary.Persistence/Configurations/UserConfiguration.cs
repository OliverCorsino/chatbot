using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boundary.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne<ChatRoom>(user => user.ChatRoom)
                .WithMany(chatRoom => chatRoom.Users)
                .HasForeignKey(user => user.ChatRoomId);
        }
    }
}
