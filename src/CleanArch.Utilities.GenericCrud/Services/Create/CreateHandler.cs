using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Entities;
using CleanArch.Utilities.GenericCrud.Repository;

namespace CleanArch.Utilities.GenericCrud.Services.Create
{
    public class CreateHandler<TRequest, TEntity, TId> : IServiceRequestHandler<TRequest, TEntity>
        where TEntity : class, IIdentifiable<TId>
        where TRequest : ICreateRequest<TEntity, TId>
    {
        private readonly IGenericRepository<TEntity, TId> _repository;
        private readonly IMapper _mapper;

        public CreateHandler(IGenericRepository<TEntity, TId> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TEntity>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request);
            await _repository.CreateAsync(entity);
            return ServiceResponse<TEntity>.Ok(entity);
        }
    }
}