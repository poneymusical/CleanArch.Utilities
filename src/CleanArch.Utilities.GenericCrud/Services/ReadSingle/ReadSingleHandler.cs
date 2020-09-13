using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Entities;
using CleanArch.Utilities.GenericCrud.Repository;

namespace CleanArch.Utilities.GenericCrud.Services.ReadSingle
{
    public class ReadSingleHandler<TRequest, TEntity, TId> : IServiceRequestHandler<TRequest, TEntity>
        where TRequest : IReadSingleRequest<TEntity, TId>
        where TEntity : class, IIdentifiable<TId>
    {
        private readonly IGenericRepository<TEntity, TId> _repository;

        public ReadSingleHandler(IGenericRepository<TEntity, TId> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<TEntity>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindAsync(request.Id);
            if (entity == null)
                return ServiceResponseFactory.NotFound<TEntity>();

            return ServiceResponseFactory.Ok(entity);
        }
    }
}
