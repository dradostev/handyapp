using System;
using Handy.Domain.AccountContext.Entities;

namespace Handy.Domain.NoteContext.Entities
{
    public class Note
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }
        public Account Account { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public int MessageId { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        public Note(Guid accountId, string title, string content)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Title = title;
            Content = content;
            Created = DateTime.Now;
        }

        public void ChangeTitle(string newTitle)
        {
            Title = newTitle;
            Modified = DateTime.Now;
        }

        public void ChangeContent(string newContent)
        {
            Content = newContent;
            Modified = DateTime.Now;
        }

        public void ConnectMessage(int messageId)
        {
            MessageId = messageId;
        }
    }
}