using System;

namespace Handy.Domain.ReminderContext.ReadModels
{
    public class ReminderRead
    {
        public Guid Id { get; private set; }
        public string Content { get; private set; }
        public DateTime FireOn { get; private set; }
        public bool Enabled { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
    }
}