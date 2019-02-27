using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.NoteContext.ReadModels;
using MediatR;

namespace Handy.Domain.NoteContext.Commands
{
    public class ModifyNote : IRequest<NoteRead>
    {
        public Guid NoteId { get; set; }
        
        public Guid AccountId { get; set; }
        
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }
    }
}