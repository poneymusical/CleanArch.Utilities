﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Entities;
using CleanArch.Utilities.GenericCrud.Repository;

namespace CleanArch.Utilities.GenericCrud.Services.Update
{
    public class UpdateHandler<TRequest, TEntity, TId> : IServiceRequestHandler<TRequest, TEntity>
        where TEntity : class, IIdentifiable<TId>
        where TRequest : IUpdateRequest<TEntity, TId>
    {
        private readonly IGenericRepository<TEntity, TId> _repository;
        private readonly IMapper _mapper;

        public UpdateHandler(IGenericRepository<TEntity, TId> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TEntity>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindAsync(request.Id);
            if (entity == null)
                return new ServiceResponse<TEntity>
                {
                    Status = ServiceResponseStatus.NotFound
                };

            _mapper.Map(request, entity);

            await _repository.UpdateAsync(entity);

            return new ServiceResponse<TEntity>
            {
                Status = ServiceResponseStatus.Ok,
                Payload = entity
            };
        }
    }
}
