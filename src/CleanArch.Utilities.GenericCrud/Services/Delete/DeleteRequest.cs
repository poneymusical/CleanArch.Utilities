using CleanArch.Utilities.GenericCrud.Entities;

namespace CleanArch.Utilities.GenericCrud.Services.Delete
{
    public class DeleteRequest<TEntity, TId> : IDeleteRequest<TEntity, TId>
        where TEntity : class, IIdentifiable<TId>
    {
        public TId Id { get; set; }
    }
}
