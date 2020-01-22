using CleanArch.Utilities.GenericCrud.Services.Create;

namespace CleanArch.Domain
{
    public class MyEntityCreateRequest : ICreateRequest<MyEntity, int>
    {
        public string Value { get; set; }
    }
}