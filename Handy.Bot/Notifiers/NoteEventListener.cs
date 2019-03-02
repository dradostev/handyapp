using System.Threading;
using System.Threading.Tasks;
using Handy.Bot.Core;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.NoteContext.Entities;
using Handy.Domain.NoteContext.Events;
using Handy.Domain.SharedContext.Services;
using MediatR;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Handy.Bot.Notifiers
{
    public class NoteEventListener : INotificationHandler<NoteAdded>
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IRepository<Account> _accountRepository;
        private readonly HandyBot _bot;

        public NoteEventListener(IRepository<Note> noteRepository, IRepository<Account> accountRepository, HandyBot bot)
        {
            _noteRepository = noteRepository;
            _accountRepository = accountRepository;
            _bot = bot;
        }
        
        public async Task Handle(NoteAdded evt, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetById(evt.NoteId);
            var account = await _accountRepository.GetById(note.AccountId);

            await _bot.Client.SendTextMessageAsync(new ChatId(account.BotChatId), $"*{note.Title}*\n\n{note.Content}", ParseMode.Markdown);
        }
    }
}