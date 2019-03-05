using System.Linq;
using System.Threading.Tasks;
using Handy.Bot.BotCommands;
using Telegram.Bot.Types;

namespace Handy.Bot.Core
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly BotCommandExecutor _botCommandExecutor;

        public UpdateHandler(BotCommandExecutor botCommandExecutor)
        {
            _botCommandExecutor = botCommandExecutor;
        }
        
        public async Task Handle(Update update)
        {
            await _botCommandExecutor.MatchCommand(update);
        }
    }
}