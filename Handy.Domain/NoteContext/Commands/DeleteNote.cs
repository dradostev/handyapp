using System;
using MediatR;

namespace Handy.Domain.NoteContext.Commands
{
    public class DeleteNote : IRequest<bool>
    {
        public Guid NoteId { get; set; }
        public Guid AccountId { get; set; }
    }
}