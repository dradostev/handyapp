using System.Threading;
using System.Threading.Tasks;
using Handy.Domain.AccountContext.Commands;
using Handy.Domain.AccountContext.Entities;
using MediatR;

namespace Handy.Domain.AccountContext.Services
{
    public class AccountCommandHandler : IRequestHandler<RegisterAccount, Account>
    {
        public async Task<Account> Handle(RegisterAccount command, CancellationToken cancellationToken)
        {
            return new Account(command.Login, command.Password, command.ScreenName);
        }
    }
}