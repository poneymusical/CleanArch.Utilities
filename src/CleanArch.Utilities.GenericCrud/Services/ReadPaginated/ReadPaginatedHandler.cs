using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Entities;
using CleanArch.Utilities.GenericCrud.Repository;

namespace CleanArch.Utilities.GenericCrud.Services.ReadPaginated
{
    public class ReadPaginatedHandler<TRequest, TEntity, TId> : IServiceRequestHandler<TRequest, IEnumerable<TEntity>> 
        where TRequest : IReadPaginatedRequest<TEntity, TId>
        where TEntity : class, IIdentifiable<TId>
    {
        private readonly IGenericRepository<TEntity, TId> _repository;

        public ReadPaginatedHandler(IGenericRepository<TEntity, TId> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<IEnumerable<TEntity>>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetPageAsync(request.PageIndex, request.PageSize);
            return new ServiceResponse<IEnumerable<TEntity>>
            {
                Status = ServiceResponseStatus.Ok,
                Payload = results
            };
        }
    }
}