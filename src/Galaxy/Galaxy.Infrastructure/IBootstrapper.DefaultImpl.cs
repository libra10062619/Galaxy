using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using System.Collections.Generic;
using Galaxy.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace Galaxy.Infrastructure
{
    /// <summary>
    /// Bootstrapper.
    /// </summary>
    internal class Bootstrapper : IBootstrapper
    {
        /// <summary>
        /// The app lifetime.
        /// </summary>
        readonly IApplicationLifetime _appLifetime;
        /// <summary>
        /// The microservices.
        /// </summary>
        readonly IEnumerable<IMicroservice> _microservices;
        /// <summary>
        /// The cancellation token source.
        /// </summary>
        readonly CancellationTokenSource _cancellationTokenSource;
        /// <summary>
        /// The cancellation token registration.
        /// </summary>
        readonly CancellationTokenRegistration _cancellationTokenRegistration;
        /// <summary>
        /// The logger.
        /// </summary>
        readonly ILogger _logger;

        Task _runningTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.Bootstrapper"/> class.
        /// </summary>
        /// <param name="provider">Provider.</param>
        /// <param name="appLiftime">App liftime.</param>
        /// <param name="microservices">Microservices.</param>
        /// <param name="logger">Logger.</param>
        public Bootstrapper(IServiceProvider provider,
                            IApplicationLifetime appLiftime, 
                            IEnumerable<IMicroservice> microservices,
                            ILogger<Bootstrapper> logger)
        {
            _appLifetime = appLiftime;
            _microservices = microservices;
            _logger = logger;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenRegistration = RegisterApplicationStoppingHandler();
        }

        /// <summary>
        /// Starts the async.
        /// </summary>
        /// <returns>The async.</returns>
        public Task StartAsync()
        {
            _runningTask = StartRunningAsync();
            return _runningTask;
        }

        /// <summary>
        /// Starts the running async.
        /// </summary>
        /// <returns>The running async.</returns>
        protected virtual async Task StartRunningAsync()
        {
            if (_cancellationTokenSource.IsCancellationRequested) return;

            _appLifetime.ApplicationStopping.Register(() =>
            {
                foreach(var micorservice in _microservices)
                {
                    micorservice.Dispose();
                }
            });

            if (_cancellationTokenSource.IsCancellationRequested) return;

            await StartRunningTasks();

            _cancellationTokenRegistration.Dispose();
            _cancellationTokenSource.Dispose();
        }

        /// <summary>
        /// Starts the running tasks.
        /// </summary>
        /// <returns>The running tasks.</returns>
        protected virtual Task StartRunningTasks()
        {
            foreach(var microservice in _microservices)
            {
                try
                {
                    microservice.Start();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"微服务启动出错：{ex}");
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Registers the application stopping handler.
        /// </summary>
        /// <returns>The application stopping handler.</returns>
        CancellationTokenRegistration RegisterApplicationStoppingHandler()
        {
            return _appLifetime.ApplicationStopping.Register(() =>
            {
                _cancellationTokenSource.Cancel();
                try
                {
                    _runningTask?.GetAwaiter().GetResult();
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogError($"关闭时发生错误{ex}");
                }
            });
        }
    }
}
