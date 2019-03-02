using System;
using MediatR;

namespace Handy.Domain.NoteContext.Events
{
    public class NoteModified : INotification
    {
        public Guid NoteId { get; set; }
    }
}