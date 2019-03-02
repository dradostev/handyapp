using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Handy.Domain.ReminderContext.Commands
{
    public class DeleteReminder : IRequest<bool>
    {
        [Required]
        public Guid ReminderId { get; set; }
    }
}