using System;
using System.Threading;
using System.Threading.Tasks;
using Handy.Domain.AccountContext.Commands;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.Services;
using MediatR;

namespace Handy.Domain.AccountContext.Services
{
    public class AccountCommandHandler : IRequestHandler<RegisterAccount, Account>
    {
        private readonly IRepository<Account> _accountRepository;
        
        public AccountCommandHandler(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }
        
        public async Task<Account> Handle(RegisterAccount command, CancellationToken cancellationToken)
        {
            if (_accountRepository.GetByCriteria(x => x.Login == command.Login) != null)
                throw new ConflictException("User with this login is already registered");
            var account = new Account(command.Login, command.Password, command.ScreenName);
            await _accountRepository.Persist(account);
            return account;
        }
    }
}