using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Entities;

namespace CleanArch.Utilities.GenericCrud.Services.Create
{
    public interface ICreateRequest<TEntity, TId> : IServiceRequest<TEntity>
        where TEntity: class, IIdentifiable<TId> { }
}