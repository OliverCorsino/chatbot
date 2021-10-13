using Core.Boundaries.MessengerService;
using Core.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using ChatBot.Core;
using Microsoft.Extensions.Options;

namespace Boundaries.MessengerService.Handlers
{
    /// <inheritdoc/>
    public sealed class MessageDelivery : IMessageDelivery
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;

        public MessageDelivery(IOptions<RabbitMqConfiguration> rabbitOptions)
        {
            _hostname =  rabbitOptions.Value.HostName;
            _queueName = rabbitOptions.Value.QueueName;
            _password =  rabbitOptions.Value.Password;
            _username =  rabbitOptions.Value.UserName;

            CreateConnection();
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
                return true;

            CreateConnection();

            return _connection != null;
        }

        /// <inheritdoc/>
        public void SendMessage(ChatMessage message)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }
    }
}
