using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boundary.Persistence.Configurations
{
    internal class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        void IEntityTypeConfiguration<ChatRoom>.Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(chatRoom => chatRoom.Id);

            builder.Property(chatRoom => chatRoom.Id).HasDefaultValueSql("NEWID()");
            builder.Property(chatRoom => chatRoom.Name).IsRequired().HasMaxLength(50);
            builder.Property(chatRoom => chatRoom.Description).IsRequired().HasMaxLength(150);
        }
    }
}
