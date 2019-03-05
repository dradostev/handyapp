namespace Handy.Bot.Messages
{
    public class NoteMessage : IMessage
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}