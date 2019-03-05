using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Handy.Bot.Messages;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.NoteContext.Commands;
using Handy.Domain.NoteContext.ReadModels;
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
        public const string TodoCmdTpl = @"/todo";

        public BotCommandExecutor(IMediator bus, IRepository<Account> accountRepository)
        {
            _bus = bus;
            _accountRepository = accountRepository;
        }

        public async Task Execute(Update update)
        {
            switch (update.Message.Text)
            {
                case var exp when exp.Contains(NoteCmdTpl):
                    await AddNote(update);
                    break;
                case var exp when exp.Contains(TodoCmdTpl):
                    break;
            }
        }

        private async Task AddNote(Update update)
        {
            var noteTitleTpl = @"t\[\w+\]";
            
            var body = Regex.Replace(update.Message.Text, NoteCmdTpl, string.Empty);
            var title = Regex.Match(body, noteTitleTpl)
                .Value?
                .Replace("t[", string.Empty)
                .Replace("]", string.Empty);
            var content = Regex.Replace(body, noteTitleTpl, string.Empty).Trim();

            var account = await _accountRepository.GetByCriteria(x => x.BotChatId == update.Message.Chat.Id);
            
            if (account == null) return;

            await _bus.Send(new AddNote
            {
                Title = title,
                Content = content,
                AccountId = account.Id
            });
        }
    }
}