using CleanArch.Utilities.GenericCrud.Services.ReadSingle;

namespace CleanArch.Domain
{
    public class MyEntityReadSingleRequest : IReadSingleRequest<MyEntity, int>
    {
        public int Id { get; set; }
    }
}
