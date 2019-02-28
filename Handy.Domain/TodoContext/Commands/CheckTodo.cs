using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.TodoContext.ReadModels;
using MediatR;

namespace Handy.Domain.TodoContext.Commands
{
    public class CheckTodo : IRequest<TodoRead>
    {
        [Required]
        public Guid TodoId { get; set; }
    }
}