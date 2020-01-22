using AutoMapper;

namespace CleanArch.Domain
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<MyEntityCreateRequest, MyEntity>();
            CreateMap<MyEntityUpdateRequest, MyEntity>();
        }
    }
}