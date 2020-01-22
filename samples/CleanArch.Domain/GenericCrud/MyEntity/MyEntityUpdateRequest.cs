using CleanArch.Utilities.GenericCrud.Services.Update;

namespace CleanArch.Domain.GenericCrud.MyEntity
{
    public class MyEntityUpdateRequest : IUpdateRequest<MyEntity, int>
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
