using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthZ.Api.Application.Commands;
using AuthZ.Api.Application.ViewModels;
using AuthZ.Domain.AggregatesModel;
using AuthZ.Domain.AggregatesModel.RolPermisoAggregate;

namespace AuthZ.Api.Application.Commands
{
    public class EliminarRolPermisoCommandHandler : IRequestHandler<EliminarRolPermisoCommand, GenericResponseViewModel>
    {
        private readonly IMongoDBGenericRepository _genericRepository;
        private readonly IMediator _mediator;

        public EliminarRolPermisoCommandHandler(IMongoDBGenericRepository genericRepository, IMediator mediator)
        {
            _genericRepository = genericRepository;
            _mediator = mediator;
        }

        public async Task<GenericResponseViewModel> Handle(EliminarRolPermisoCommand request, CancellationToken cancellationToken)
        {
            var rsp = new GenericResponseViewModel();

            var data = await _genericRepository.GetOneAsync<RolPermiso>(x => x.IdPermiso == request.IdPermiso && x.IdRol == request.IdRol);

            await _genericRepository.DeleteOneAsync(data);

            return rsp;
        }
    }
}
