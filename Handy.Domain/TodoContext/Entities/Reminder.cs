using System;
using Handy.Domain.SharedContext.Exceptions;

namespace Handy.Domain.TodoContext.Entities
{
    public class Reminder
    {
        public Guid Id { get; private set; }
        public Guid TodoId { get; private set; }
        public Todo Todo { get; private set; }
        public DateTime FireOn { get; private set; }
        public bool Enabled { get; private set; }

        public Reminder(Guid todoId, DateTime fireOn)
        {
            Id = Guid.NewGuid();
            TodoId = todoId;
            FireOn = fireOn;
            Enabled = false;
        }

        public void Fire()
        {
            if (FireOn >= DateTime.Now)
                Enabled = false;
        }

        public void ChangeFireTime(DateTime newTime)
        {
            if (newTime > DateTime.Now)
                FireOn = newTime;
            else
                throw new DomainLogicException("Given time is already left");
        }
    }
}