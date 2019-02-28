using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.TodoContext.ReadModels;
using MediatR;

namespace Handy.Domain.TodoContext.Queries
{
    public class ShowMyTodoLists : IRequest<IEnumerable<TodoListRead>>
    {
        [Required]
        public Guid AccountId { get; set; }
    }
}