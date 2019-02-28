using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.TodoContext.ReadModels;
using MediatR;

namespace Handy.Domain.TodoContext.Commands
{
    public class AddTodo : IRequest<TodoRead>
    {
        [Required]
        public Guid TodoListId { get; set; }
        [Required, MinLength(3)]
        public string Title { get; set; }
    }
}