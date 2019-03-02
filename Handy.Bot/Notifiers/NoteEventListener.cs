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
    public class NoteEventListener : INotificationHandler<NoteAdded>,
                                     INotificationHandler<NoteModified>,
                                     INotificationHandler<NoteDeleted>
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

            var message = await _bot.Client.SendTextMessageAsync(
                chatId: new ChatId(account.BotChatId), 
                parseMode: ParseMode.Markdown,
                text:$"*{note.Title}*\n\n{note.Content}",
                cancellationToken: cancellationToken
            );
            
            note.SetMessageId(message.MessageId);
            await _noteRepository.Update(note);
        }

        public async Task Handle(NoteModified evt, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetById(evt.NoteId);
            var account = await _accountRepository.GetById(note.AccountId);

            await _bot.Client.EditMessageTextAsync(
                chatId: new ChatId(account.BotChatId),
                messageId: note.MessageId,
                parseMode: ParseMode.Markdown,
                text: $"*{note.Title}*\n\n{note.Content}",
                cancellationToken: cancellationToken
            );
        }


        public async Task Handle(NoteDeleted evt, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetById(evt.AccountId);

            await _bot.Client.DeleteMessageAsync(
                chatId: new ChatId(account.BotChatId),
                messageId: evt.MessageId,
                cancellationToken: cancellationToken
            );
        }
    }
}