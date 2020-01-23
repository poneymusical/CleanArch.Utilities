using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Entities;

namespace CleanArch.Utilities.GenericCrud.Services.Delete
{
    public interface IDeleteRequest<TEntity, TId> : IServiceRequest<TId>
        where TEntity : class, IIdentifiable<TId>
    {
        TId Id { get; set; }
    }
}
