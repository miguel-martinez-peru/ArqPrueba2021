using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthZ.Domain.AggregatesModel;
using AuthZ.Api.Application.Commands;
using AuthZ.Api.Application.ViewModels;
using AuthZ.Domain.AggregatesModel.RolPermisoAggregate;

namespace AuthZ.Api.Application.Commands
{
    public class CrearRolPermisoCommandHandler : IRequestHandler<CrearRolPermisoCommand, GenericResponseViewModel>
    {
        private readonly IMongoDBGenericRepository _genericRepository;
        private readonly IMediator _mediator;

        public CrearRolPermisoCommandHandler(IMediator mediator, IMongoDBGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
            _mediator = mediator;
        }

        public async Task<GenericResponseViewModel> Handle(CrearRolPermisoCommand message, CancellationToken cancellationToken)
        {
            var rsp = new GenericResponseViewModel();

            var data = new RolPermiso
            {
                IdPermiso = message.IdPermiso,
                IdRol = message.IdRol,
                EsActivo = true
            };

            await _genericRepository.AddOneAsync(data);

            return rsp;
        }
    }
}
