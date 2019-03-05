using AutoMapper;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.AccountContext.ReadModels;
using Handy.Domain.NoteContext.Entities;
using Handy.Domain.NoteContext.ReadModels;

namespace Handy.Domain.SharedContext.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, UserProfile>();
            CreateMap<Note, NoteRead>();
        }
    }
}