using CommonService.Utility;
using MasterServiceDemo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

namespace MasterServiceDemo.Services
{
    public class ConsumerService
    {
        private readonly IConnection _connection;

        private readonly RabbitMQConnectionHelper rabbitMQHelper;

        public ConsumerService()
        {
            _connection = rabbitMQHelper.GetConnection();
        }

        public async Task ConsumeQueueAsync(string queueName)
        {
            using var channel = await _connection.CreateChannelAsync();
            channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: true,
                autoDelete: true,
                arguments : null
            );

            var consumer = new AsyncEventingBasicConsumer( channel );
            consumer.ReceivedAsync += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString( body );
                var order = JsonSerializer.Deserialize<OrderModel>(message);

                Console.WriteLine($"Received Order: {order.Id} - {order.ProductName} (Quantity: {order.Quantity})");
                await Task.Yield();
            };

            channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);
        }
    }
}
