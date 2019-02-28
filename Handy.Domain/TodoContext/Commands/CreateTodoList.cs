using System;
using Handy.Domain.TodoContext.ReadModels;
using MediatR;

namespace Handy.Domain.TodoContext.Commands
{
    public class CreateTodoList : IRequest<TodoListRead>
    {
        public Guid AccountId { get; set; }
    }
}