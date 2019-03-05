using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Handy.Bot.Core
{
    public interface IUpdateHandler
    {
        Task Handle(Update update);
    }
}