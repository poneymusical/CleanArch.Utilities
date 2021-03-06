﻿using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Entities;
using CleanArch.Utilities.GenericCrud.Repository;

namespace CleanArch.Utilities.GenericCrud.Services.Delete
{
    public class DeleteHandler<TRequest, TEntity, TId> : IServiceRequestHandler<TRequest>
        where TRequest : IDeleteRequest<TEntity, TId>
        where TEntity : class, IIdentifiable<TId>
    {
        private readonly IGenericRepository<TEntity, TId> _repository;

        public DeleteHandler(IGenericRepository<TEntity, TId> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindAsync(request.Id);
            if (entity == null)
                return ServiceResponseFactory.NotFound();

            await _repository.DeleteAsync(entity);

            return ServiceResponseFactory.Ok();
        }
    }
}