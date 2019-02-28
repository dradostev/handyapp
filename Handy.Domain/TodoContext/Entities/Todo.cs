using System;

namespace Handy.Domain.TodoContext.Entities
{
    public class Todo
    {
        public Guid Id { get; private set; }
        public Guid TodoListId { get; private set; }
        public TodoList TodoList { get; private set; }
        public string Title { get; private set; }
        public bool Done { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        public Todo(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            Done = false;
            Created = DateTime.Now;
        }

        public void Rename(string newTitle)
        {
            Title = newTitle;
            Modified = DateTime.Now;
        }

        public void Check()
        {
            Done = !Done;
            Modified = DateTime.Now;
        }
    }
}