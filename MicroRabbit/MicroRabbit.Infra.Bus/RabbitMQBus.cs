using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Command;
using MicroRabbit.Domain.Core.Event;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Infra.Bus
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handler;
        private readonly List<Type> _eventTypes;

        public RabbitMQBus(IMediator mediator)
        {
            _mediator = mediator;
            _handler = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public Task SendCommand<T>(T command) where T : Command
        {
           return _mediator.Send(command);
        }


        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection=factory.CreateConnection())
            using(var channel=connection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.QueueDeclare(eventName, false, false, false, null);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("",eventName,body);
                
            }
        }


        public void Subscibe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_handler.ContainsKey(eventName))
            {
                _handler.Add(eventName, new List<Type>());
            }

            if (_handler[eventName].Any(x=>x.GetType()==handlerType))
            {
                throw new ArgumentException
                    ($"Handler Type {handlerType.Name} already is registered for '{eventName}'");
            }

            _handler[eventName].Add(handlerType);

            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory() {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var eventName = typeof(T).Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;

            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;

            var message = Encoding.UTF8.GetString(e.Body);

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handler.ContainsKey(eventName))
            {
                var subscriptions = _handler[eventName];

                foreach (var subscription in subscriptions)
                {
                    var handler = Activator.CreateInstance(subscription);


                    if (handler == null) continue;

                    var eventType = _eventTypes.SingleOrDefault(s => s.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);

                    var conreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                    await (Task)conreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                        

                }
            }
        }
    }
}
