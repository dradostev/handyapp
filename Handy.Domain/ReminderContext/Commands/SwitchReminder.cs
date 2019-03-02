using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.ReminderContext.ReadModels;
using MediatR;

namespace Handy.Domain.ReminderContext.Commands
{
    public class SwitchReminder : IRequest<ReminderRead>
    {
        [Required]
        public Guid ReminderId { get; set; }        
    }
}