using AutoMapper;
using CleanArch.Domain.GenericCrud.MyEntity;

namespace CleanArch.Domain.GenericCrud
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<MyEntityCreateRequest, MyEntity.MyEntity>();
            CreateMap<MyEntityUpdateRequest, MyEntity.MyEntity>();
        }
    }
}