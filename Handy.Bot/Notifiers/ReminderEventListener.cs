using System.Threading;
using System.Threading.Tasks;
using Handy.Bot.Core;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.ReminderContext.Entities;
using Handy.Domain.ReminderContext.Events;
using Handy.Domain.SharedContext.Services;
using MediatR;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Handy.Bot.Notifiers
{
    public class ReminderEventListener : INotificationHandler<ReminderAdded>
    {
        private readonly IRepository<Reminder> _reminderRepository;
        private readonly IRepository<Account> _accountRepository;
        private readonly HandyBot _bot;

        public ReminderEventListener(IRepository<Reminder> reminderRepository, IRepository<Account> accountRepository, HandyBot bot)
        {
            _reminderRepository = reminderRepository;
            _accountRepository = accountRepository;
            _bot = bot;
        }
        
        public async Task Handle(ReminderAdded evt, CancellationToken cancellationToken)
        {
            var reminder = await _reminderRepository.GetById(evt.ReminderId);
            var account = await _accountRepository.GetById(evt.AccountId);
            
            var message = await _bot.Api.SendTextMessageAsync(
                chatId: new ChatId(account.BotChatId), 
                parseMode: ParseMode.Markdown,
                text:$"_{reminder.Content} on {reminder.FireOn:f}_",
                cancellationToken: cancellationToken
            );
            
            reminder.ConnectMessage(message.MessageId);
            await _reminderRepository.Update(reminder);
        }
    }
}