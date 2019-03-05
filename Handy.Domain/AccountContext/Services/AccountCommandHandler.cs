using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Handy.Domain.AccountContext.Commands;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.AccountContext.ReadModels;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.Services;
using MediatR;

namespace Handy.Domain.AccountContext.Services
{
    public class AccountCommandHandler : IRequestHandler<RegisterAccount, UserProfile>
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public AccountCommandHandler(IRepository<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        
        public async Task<UserProfile> Handle(RegisterAccount command, CancellationToken cancellationToken)
        {
            if (await _accountRepository.GetByCriteria(x => x.Login == command.Login) != null)
                throw new ConflictException("User with this login is already registered");
            var account = new Account(command.Login, command.Password, command.Tz, command.ChatId, command.ScreenName);
            await _accountRepository.Persist(account);
            return _mapper.Map<UserProfile>(account);
        }
    }
}