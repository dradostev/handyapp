using System;
using MediatR;

namespace Handy.Domain.NoteContext.Events
{
    public class NoteDeleted : INotification
    {
        public Guid AccountId { get; set; }
        public int MessageId { get; set; }
    }
}