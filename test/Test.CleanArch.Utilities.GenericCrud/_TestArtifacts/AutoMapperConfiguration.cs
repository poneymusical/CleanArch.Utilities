using AutoMapper;

namespace Test.CleanArch.Utilities.GenericCrud._TestArtifacts
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<MockCreateRequest, MockEntity>();
        }
    }
}