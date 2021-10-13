using Core.Boundaries.MessengerService;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boundaries.MessengerService.Hubs
{
    /// <summary>
    /// Represents the hub for handling the message requests.
    /// </summary>
    public sealed class MessengerHub : Hub
    {
        private readonly IMessageDelivery _sender;

        private static IList<ConnectedUser> _connectedUsers = new List<ConnectedUser>();
        private static readonly IList<ChatMessage> CurrentMessages = new List<ChatMessage>();

        /// <summary>
        /// Initializes a new instance of the MessengerHub class.
        /// </summary>
        /// <param name="sender">An implementation of <see cref="IMessageDelivery"/>.</param>
        public MessengerHub(IMessageDelivery sender) => _sender = sender;

        /// <inheritdoc />
        public override Task OnConnectedAsync()
        {
            string username = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            if (_connectedUsers.All(connectedUser => connectedUser.Username != username))
            {
                _connectedUsers.Add(new ConnectedUser { ConnectionId = connectionId, Username = username });

                Clients.All.SendAsync("UpdateUsersConnected", _connectedUsers);
            }
            else
            {
                Clients.Caller.SendAsync("UpdateUsersConnected", _connectedUsers);
            }

            Clients.Caller.SendAsync("CurrentMessages", CurrentMessages);

            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Sends a message to a chat room.
        /// </summary>
        /// <param name="message">Represents the message that want to be send.</param>
        /// <returns>A resolved task with the message sent.</returns>
        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.SendAsync("NewMessage", message);

            AddToCurrentMessages(message);

            if (message.Message.Contains("/stock="))
            {
                _sender.SendMessage(message);
            }
        }

        private void AddToCurrentMessages(ChatMessage message)
        {
            CurrentMessages.Add(message);

            if (CurrentMessages.Count > 50)
            {
                CurrentMessages.RemoveAt(0);
            }
        }

        public async Task DisconnectUser(string userName)
        {
            if (_connectedUsers.Any(currentUser => currentUser.Username == userName))
            {
                _connectedUsers = _connectedUsers.Where(currentUser => currentUser.Username != userName).ToList();

                await Clients.All.SendAsync("UpdateUsersConnected", _connectedUsers);
            }
        }
    }
}
