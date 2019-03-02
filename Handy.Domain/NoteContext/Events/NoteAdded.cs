using System;
using MediatR;

namespace Handy.Domain.NoteContext.Events
{
    public class NoteAdded : INotification
    {
        public Guid NoteId { get; set; }
    }
}