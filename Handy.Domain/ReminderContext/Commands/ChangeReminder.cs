using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.ReminderContext.ReadModels;
using MediatR;

namespace Handy.Domain.ReminderContext.Commands
{
    public class ChangeReminder : IRequest<ReminderRead>
    {
        [Required]
        public Guid ReminderId { get; set; }
        [Required, MinLength(3)]
        public string Content { get; set; }
    }
}