using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.NoteContext.ReadModels;
using MediatR;

namespace Handy.Domain.NoteContext.Queries
{
    public class ShowNote : IRequest<NoteRead>
    {
        [Required]
        public Guid AccountId { get; set; }
        
        [Required]
        public Guid NoteId { get; set; }
    }
}