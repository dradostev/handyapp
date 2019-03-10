using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.ReminderContext.ReadModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Handy.Domain.ReminderContext.Queries
{
    public class ListReminders : IRequest<IEnumerable<ReminderRead>>
    {
        [Required]
        public Guid AccountId { get; set; }
        
        [Required]
        public int Limit { get; set; } = 10;
        
        [Required]
        public int Offset { get; set; } = 0;
        
        public List<string> Filter { get; set; }
    }
}