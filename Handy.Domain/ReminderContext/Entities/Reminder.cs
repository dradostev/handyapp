using System;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.SharedContext.Exceptions;

namespace Handy.Domain.ReminderContext.Entities
{
    public class Reminder
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }
        public Account Account { get; private set; }
        public string Content { get; private set; }
        public DateTime FireOn { get; private set; }
        public bool Enabled { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        
        public Reminder(Guid accountId, string content, DateTime fireOn)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Content = content;
            FireOn = fireOn;
            Enabled = false;
            Created = DateTime.Now;
        }

        public void Fire()
        {
            if (FireOn >= DateTime.Now)
            {
                Enabled = false;
                Modified = DateTime.Now;
            }
        }

        public void ChangeContent(string newContent)
        {
            Content = newContent;
        }

        public void ChangeFireTime(DateTime newTime)
        {
            if (newTime > DateTime.Now)
            {
                FireOn = newTime;
                Modified = DateTime.Now;
            }
            else
            {
                throw new DomainLogicException("Given time is already left");
            }
        }

        public void SwitchEnabled()
        {
            Enabled = !Enabled;
        }
    }
}