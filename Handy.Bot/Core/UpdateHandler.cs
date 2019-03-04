using Handy.Domain.AccountContext.Entities;
using Handy.Domain.NoteContext.Commands;
using Handy.Domain.SharedContext.Services;
using MediatR;
using Telegram.Bot.Types;

namespace Handy.Bot.Core
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly IMediator _bus;
        private readonly IRepository<Account> _accRepo;

        public UpdateHandler(IMediator bus, IRepository<Account> accRepo)
        {
            _bus = bus;
            _accRepo = accRepo;
        }
        
        public void Handle(Update update)
        {
            if (update.Message != null && update.Message.Text.Contains("/note"))
            {
                var text = update.Message.Text.Replace("/note", "");
                var account = _accRepo.GetByCriteria(x => x.BotChatId == update.Message.Chat.Id).Result;
                _bus.Send(new AddNote {Content = text, AccountId = account.Id}).Wait();
            }
        }
    }
}