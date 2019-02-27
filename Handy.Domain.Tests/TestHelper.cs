using AutoMapper;
using Handy.Domain.SharedContext.MappingProfiles;

namespace Handy.Domain.Tests
{
    public static class TestHelper
    {
        public static IMapper GetMockMapper()
        {
            return new MapperConfiguration(config => config.AddProfile(new MappingProfile())).CreateMapper();
        }
    }
}