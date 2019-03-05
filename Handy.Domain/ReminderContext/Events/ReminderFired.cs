using System;
using MediatR;

namespace Handy.Domain.ReminderContext.Events
{
    public class ReminderFired : INotification
    {
        public Guid ReminderId { get; set; }
        public Guid AccountId { get; set; }
    }
}