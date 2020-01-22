using CleanArch.Utilities.GenericCrud.Services.ReadSingle;

namespace CleanArch.Domain
{
    public class MyEntityReadSingleRequest : IReadSingleRequest<MyEntity, int>
    {
        public int Id { get; set; }
    }

    //TODO It will always be the same class. The interface could be replaced by a class. DI should be adapted but it must work.
}
