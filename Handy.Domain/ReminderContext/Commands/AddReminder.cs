using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.ReminderContext.ReadModels;
using MediatR;

namespace Handy.Domain.ReminderContext.Commands
{
    public class AddReminder : IRequest<ReminderRead>
    {
        [Required]
        public Guid AccountId { get; set; }
        [Required, MinLength(3)]
        public string Content { get; set; }
        [Required]
        public DateTime FireOn { get; set; }
    }
}