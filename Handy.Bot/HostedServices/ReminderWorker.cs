using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Handy.Domain.ReminderContext.Commands;
using Handy.Domain.ReminderContext.Entities;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Handy.Bot.HostedServices
{
    public class ReminderWorker : IHostedService, IDisposable
    {
        private readonly ILogger<ReminderWorker> _logger;
        private readonly IServiceProvider _services;
        private Timer _timer;

        public ReminderWorker(ILogger<ReminderWorker> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Worker started on {DateTime.Now:g}");
            _timer = new Timer(CheckReminder, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        private async void CheckReminder(object state)
        {
            using (var scope = _services.CreateScope())
            {
                var reminderRepository = scope.ServiceProvider.GetRequiredService<IRepository<Reminder>>();
                var bus = scope.ServiceProvider.GetRequiredService<IMediator>();
                
                var reminders = await reminderRepository.ListByCriteria(
                    x => x.Enabled && x.FireOn <= DateTime.Now.AddHours(x.Account.TimeZone));

                foreach (var reminder in reminders)
                {
                    try
                    {
                        await bus.Send(new FireReminder {ReminderId = reminder.Id});
                    }
                    catch (NotFoundException e)
                    {
                        _logger.LogError(e.Message ?? "Reminder not found");
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            _logger.LogInformation($"Worker stopped on {DateTime.Now:g}");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}