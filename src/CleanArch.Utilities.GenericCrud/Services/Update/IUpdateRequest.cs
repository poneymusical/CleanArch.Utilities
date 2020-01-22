using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Entities;

namespace CleanArch.Utilities.GenericCrud.Services.Update
{
    public interface IUpdateRequest<TEntity, TId> : IServiceRequest<TEntity>
        where TEntity : class, IIdentifiable<TId>
    {
        TId Id { get; set; }
    }
}
