using System;
using MediatR;

namespace Handy.Bot.Notifiers
{
    public class BotErrorOccurred : INotification
    {
        public string Error { get; set; }
        public long ChatId { get; set; }
    }
}