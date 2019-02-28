using System;
using System.Collections.Generic;
using Handy.Domain.TodoContext.Entities;

namespace Handy.Domain.TodoContext.ReadModels
{
    public class TodoListRead
    {
        public Guid Id { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public List<TodoRead> Todos { get; private set; }
    }
}