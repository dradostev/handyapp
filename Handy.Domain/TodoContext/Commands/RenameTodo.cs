using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.TodoContext.ReadModels;
using MediatR;

namespace Handy.Domain.TodoContext.Commands
{
    public class RenameTodo : IRequest<TodoRead>
    {
        [Required]
        public Guid TodoId { get; set; }
        [Required]
        public string NewTitle { get; set; }
    }
}