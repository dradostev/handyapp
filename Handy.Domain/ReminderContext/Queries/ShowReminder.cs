using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.ReminderContext.ReadModels;
using MediatR;

namespace Handy.Domain.ReminderContext.Queries
{
    public class ShowReminder : IRequest<ReminderRead>
    {
        [Required]
        public Guid ReminderId { get; set; }
        [Required]
        public Guid AccountId { get; set; }
    }
}