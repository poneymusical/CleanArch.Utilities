using CleanArch.Utilities.GenericCrud.Services.Update;

namespace CleanArch.Domain
{
    public class MyEntityUpdateRequest : IUpdateRequest<MyEntity, int>
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
