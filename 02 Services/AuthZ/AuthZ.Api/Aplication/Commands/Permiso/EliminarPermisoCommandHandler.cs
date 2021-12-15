using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthZ.Api.Application.ViewModels;
using AuthZ.Domain.AggregatesModel.PermisoAggregate;
using AuthZ.Domain.AggregatesModel;

namespace AuthZ.Api.Application.Commands
{
    public class EliminarPermisoCommandHandler : IRequestHandler<EliminarPermisoCommand, GenericResponseViewModel>
    {
        private readonly IMongoDBGenericRepository _genericRepository;
        private readonly IMediator _mediator;

        public EliminarPermisoCommandHandler(IMongoDBGenericRepository genericRepository, IMediator mediator)
        {
            _genericRepository = genericRepository;
            _mediator = mediator;
        }

        public async Task<GenericResponseViewModel> Handle(EliminarPermisoCommand request, CancellationToken cancellationToken)
        {
            var rsp = new GenericResponseViewModel();

            var data = await _genericRepository.GetOneAsync<Permiso>(x => x.IdPermiso == request.IdPermiso);
            
            await _genericRepository.DeleteOneAsync(data);

            return rsp;
        }
    }
}
