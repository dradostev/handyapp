using System;

namespace Handy.Domain.TodoContext.ReadModels
{
    public class TodoRead
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public bool Done { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
    }
}