using Core.Models;

namespace Core.Boundaries.MessengerService
{
    /// <summary>
    /// Represents the messenger interface responsible for handler the chat message 
    /// </summary>
    public interface IMessageDelivery
    {
        /// <summary>
        /// Sends a message into the chat room.
        /// </summary>
        /// <param name="message">Represents the message a user wants to send into a chat room.</param>
        void SendMessage(ChatMessage message);
    }
}
