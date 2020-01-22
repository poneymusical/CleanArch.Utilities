using CleanArch.Utilities.GenericCrud.Services.Create;

namespace CleanArch.Domain.GenericCrud.MyEntity
{
    public class MyEntityCreateRequest : ICreateRequest<MyEntity, int>
    {
        public string Value { get; set; }
    }
}