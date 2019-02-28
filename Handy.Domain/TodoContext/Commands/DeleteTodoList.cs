using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Handy.Domain.TodoContext.Commands
{
    public class DeleteTodoList : IRequest<bool>
    {
        [Required]
        public Guid TodoListId { get; set; }
    }
}