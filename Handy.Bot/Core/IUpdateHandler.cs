using Telegram.Bot.Types;

namespace Handy.Bot.Core
{
    public interface IUpdateHandler
    {
        void Handle(Update update);
    }
}