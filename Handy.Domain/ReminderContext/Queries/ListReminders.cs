using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.ReminderContext.ReadModels;
using MediatR;

namespace Handy.Domain.ReminderContext.Queries
{
    public class ListReminders : IRequest<IEnumerable<ReminderRead>>
    {
        [Required]
        public Guid AccountId { get; set; }
    }
}