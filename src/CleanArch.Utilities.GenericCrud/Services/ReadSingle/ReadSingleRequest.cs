using CleanArch.Utilities.GenericCrud.Entities;

namespace CleanArch.Utilities.GenericCrud.Services.ReadSingle
{
    public class ReadSingleRequest<TEntity, TId> : IReadSingleRequest<TEntity, TId>
        where TEntity : class, IIdentifiable<TId>
    {
        public TId Id { get; set; }
    }
}