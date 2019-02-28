using System;
using System.Collections.Generic;
using Handy.Domain.AccountContext.Entities;

namespace Handy.Domain.TodoContext.Entities
{
    public class TodoList
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }
        public Account Account { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public List<Todo> Todos { get; private set; } = new List<Todo>();

        public TodoList(Guid accountId)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Created = DateTime.Now;
        }

        public void AddTodo(Todo todo)
        {
            Todos.Add(todo);
            Modified = DateTime.Now;
        }
    }
}