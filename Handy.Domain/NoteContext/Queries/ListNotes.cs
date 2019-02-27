using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.NoteContext.ReadModels;
using MediatR;

namespace Handy.Domain.NoteContext.Queries
{
    public class ListNotes : IRequest<IEnumerable<NoteRead>>
    {
        [Required]
        public Guid AccountId { get; set; }
    }
}