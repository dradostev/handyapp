using System;

namespace Handy.Domain.NoteContext.ReadModels
{
    public class NoteRead
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
    }
}