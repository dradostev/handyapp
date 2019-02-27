using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.AccountContext.Queries;
using Handy.Domain.AccountContext.ReadModels;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.Services;
using MediatR;

namespace Handy.Domain.AccountContext.Services
{
    public class AccountQueryHandler : IRequestHandler<ShowMyProfile, UserProfile>
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public AccountQueryHandler(IRepository<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        
        public async Task<UserProfile> Handle(ShowMyProfile query, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByCriteria(x => x.Login == query.Login);
            if (account == null) throw new NotFoundException("Profile not found");
            return _mapper.Map<UserProfile>(account);
        }
    }
}