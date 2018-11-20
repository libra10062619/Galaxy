using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Galaxy.Infrastructure.Events;
using Galaxy.Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Galaxy.Infrastructure.Services
{
    public abstract class Microservice : DisposableObject, IMicroservice
    {
        readonly IServiceProvider _provider;
        readonly IMessageConsumerFactory _consumerFactory;

        readonly CancellationTokenSource cancellationTokenSource;
        readonly TimeSpan pollingDelay;
        readonly Lazy<IList<string>> _topics;
        readonly ILogger _logger;

        Task compositeTask;

        protected abstract IEnumerable<Type> RegisteredEventHandlers { get; }

        public string GroupId { get; protected set; }

        protected Microservice(IServiceProvider provider, ILogger logger)
        {
            _provider = provider;
            _consumerFactory = provider.GetRequiredService<IMessageConsumerFactory>();
            _logger = logger;
            GroupId = $"{GetType().FullName}";

            pollingDelay = TimeSpan.FromMilliseconds(1*1000);
            cancellationTokenSource = new CancellationTokenSource();
            _topics = new Lazy<IList<string>>(() =>
            {
                var topics = new List<string>();

                if (null != RegisteredEventHandlers && RegisteredEventHandlers.Any())
                {
                    topics = RegisteredEventHandlers
                        //.Select(p => $"{p.Name.Replace("Handler", "")}")
                        //.ToList();
                        .SelectMany(p => p.GetInterfaces())
                        .Where(p => p.Name.Equals("IHandler`1") && p.IsGenericType)
                        .Select(p => p.GetGenericArguments()[0].FullName)
                        .ToList();
                }

                return topics;
            });
        }

        public void Start()
        {
            var topics = _topics.Value;

            if(topics.Any())
            {
                Task.Factory.StartNew(() =>
                {
                    using (var client = _consumerFactory.Create(GroupId))
                    {
                        InitEventConsumer(client);

                        client.Subscribe(topics);

                        client.Listening(pollingDelay, cancellationTokenSource.Token);
                    }
                }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            }
            compositeTask = Task.CompletedTask;
        }

        protected override void Disposing()
        {
            cancellationTokenSource.Cancel();
            try
            {
                compositeTask.Wait(TimeSpan.FromSeconds(2));
            }
            catch
            {
                ;
            }
        }

        void InitEventConsumer(IMessageConsumer client)
        {
            EventHandler<MessageEventArgs> p = async (sender, e) =>
               {
                   using (var scope = _provider.CreateScope())
                   {
                       try
                       {
                           var scopeProvider = scope.ServiceProvider;
                           var eventHandlers = scopeProvider.GetServices<IDomainEventHandler>();
                           if (null != eventHandlers && eventHandlers.Any())
                           {
                               foreach (var handler in eventHandlers)
                               {
                                   var handlerType = handler.GetType();
                                   var messageType = e.EventName;
                                   var methodInfo = EventHandlerHelper.GetAsyncHandlingMethod(handler, e.EventName);
                                   var eventType = EventHandlerHelper.GetEventType(handler, e.EventName);
                                   var param = JsonConvert.DeserializeObject(e.Message.ToString(), eventType);
                                   await (Task)methodInfo?.Invoke(handler, new[] { param });
                               }
                           }
                           client.Commit();
                       }
                       catch (Exception ex)
                       {
                           _logger.LogError($"Error in microservice {this.GetType().FullName}: {ex.Message}");
                           client.Reject();
                       }
                   }
               };
            client.OnMessageReceived += p;
        }
    }
}
