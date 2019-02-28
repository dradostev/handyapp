using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.TodoContext.ReadModels;
using MediatR;

namespace Handy.Domain.TodoContext.Queries
{
    public class ShowTodoList : IRequest<TodoListRead>
    {
        [Required]
        public Guid AccountId { get; set; }
        [Required]
        public Guid TodoListId { get; set; }
    }
}