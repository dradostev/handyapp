using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Handy.Bot.Notifiers;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.NoteContext.Commands;
using Handy.Domain.ReminderContext.Commands;
using Handy.Domain.SharedContext.Services;
using MediatR;
using Telegram.Bot.Types;

namespace Handy.Bot.BotCommands
{
    public class BotCommandExecutor
    {
        private readonly IMediator _bus;
        private readonly IRepository<Account> _accountRepository;
        
        public const string NoteCmdTpl = @"/note";
        public const string ReminderCmdTpl = @"/remind";

        private const string BracketsTpl = @"\[(.*?)\]";

        public BotCommandExecutor(IMediator bus, IRepository<Account> accountRepository)
        {
            _bus = bus;
            _accountRepository = accountRepository;
        }

        public async Task MatchCommand(Update update)
        {
            switch (update.Message.Text)
            {
                case var exp when exp.Contains(NoteCmdTpl):
                    await AddNote(update);
                    break;
                case var exp when exp.Contains(ReminderCmdTpl):
                    await AddReminder(update);
                    break;
                default:
                    await _bus.Publish(new BotErrorOccurred
                    {
                        ChatId = update.Message.Chat.Id,
                        Error = "Unknown command"
                    });
                    break;
            }
        }

        private async Task AddNote(Update update)
        {   
            var body = Regex.Replace(update.Message.Text, NoteCmdTpl, string.Empty);
            var title = Regex.Match(body, BracketsTpl)
                .Value?
                .Replace("[", string.Empty)
                .Replace("]", string.Empty);
            var content = Regex.Replace(body, BracketsTpl, string.Empty).Trim();

            var account = await _accountRepository.GetByCriteria(x => x.BotChatId == update.Message.Chat.Id);
            
            if (account == null) return;

            await _bus.Send(new AddNote
            {
                Title = title,
                Content = content,
                AccountId = account.Id
            });
        }

        private async Task AddReminder(Update update)
        {
            var body = Regex.Replace(update.Message.Text, ReminderCmdTpl, string.Empty);
            var timeStr = Regex.Match(body, BracketsTpl)
                .Value?
                .Replace("[", string.Empty)
                .Replace("]", string.Empty);
            var content = Regex.Replace(body, BracketsTpl, string.Empty).Trim();
            
            var account = await _accountRepository.GetByCriteria(x => x.BotChatId == update.Message.Chat.Id);
            
            if (account == null) return;

            if (!DateTime.TryParse(timeStr, out var fireOn))
            {
                await _bus.Publish(new BotErrorOccurred
                {
                    ChatId = update.Message.Chat.Id,
                    Error = "Invalid date format"
                });
                return;
            }

            await _bus.Send(new AddReminder
            {
                AccountId = account.Id,
                Content = content,
                FireOn = fireOn.ToUniversalTime()
            });
        }
    }
}