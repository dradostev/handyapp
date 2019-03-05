using System.Threading;
using System.Threading.Tasks;
using Handy.Bot.Core;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.SharedContext.Services;
using MediatR;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Handy.Bot.Notifiers
{
    public class SharedEventListener : INotificationHandler<BotErrorOccurred>
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly HandyBot _bot;

        public SharedEventListener(IRepository<Account> accountRepository, HandyBot bot)
        {
            _accountRepository = accountRepository;
            _bot = bot;
        }
        
        public async Task Handle(BotErrorOccurred evt, CancellationToken cancellationToken)
        {
            var message = await _bot.Api.SendTextMessageAsync(
                chatId: new ChatId(evt.ChatId), 
                parseMode: ParseMode.Markdown,
                text:$"*Error occurred:* {evt.Error}",
                cancellationToken: cancellationToken
            );
        }
    }
}