using AutoMapper;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.AccountContext.ReadModels;

namespace Handy.Domain.SharedContext.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, UserProfile>();
        }
    }
}