using CommonService.Utility;
using MasterServiceDemo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

namespace MasterServiceDemo.Utility
{
    public class ConsumerService
    {
        private readonly Task<IConnection> _connectionTask;
        private readonly RabbitMQConnectionHelper _rabbit;

        public ConsumerService(RabbitMQConnectionHelper rabbitMQConnectionHelper)
        {
            _connectionTask = rabbitMQConnectionHelper.GetConnectionAsync();
            _rabbit = rabbitMQConnectionHelper;
        }

        public async Task ConsumeQueueAsync(string queueName)
        {
            var connection = await _connectionTask;
            using var channel = await connection.CreateChannelAsync();
            channel.QueueDeclareAsync(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var order = JsonSerializer.Deserialize<OrderModel>(message);
                var replyToQueue = eventArgs.BasicProperties.ReplyTo;

                // Console.WriteLine($"Received Order: {order.Id} - {order.ProductName} (Quantity: {order.Quantity})");
                // send Response
                var responseSend = new Utility.RabbitMQResponseSender(_rabbit);
                responseSend.SendResponse(replyToQueue, $"Processed : {message}");
            };

            await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);
            Console.WriteLine($"[Producer] Sent Request:");
        }
    }
}
