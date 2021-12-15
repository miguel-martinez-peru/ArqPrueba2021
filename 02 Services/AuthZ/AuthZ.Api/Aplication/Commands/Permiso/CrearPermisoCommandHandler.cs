using AuthZ.Api.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthZ.Domain.AggregatesModel.PermisoAggregate;
using AuthZ.Domain.AggregatesModel;

namespace AuthZ.Api.Application.Commands
{
    public class CrearPermisoCommandHandler : IRequestHandler<CrearPermisoCommand, GenericResponseViewModel>
    {
        private readonly IMongoDBGenericRepository _genericRepository;
        private readonly IMediator _mediator;

        public CrearPermisoCommandHandler(IMediator mediator, IMongoDBGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
            _mediator = mediator;
        }

        public async Task<GenericResponseViewModel> Handle(CrearPermisoCommand message, CancellationToken cancellationToken)
        {
            var rsp = new GenericResponseViewModel();

            var data = new Permiso
            {
                IdPermiso = message.IdPermiso,
                IdSistema = message.IdSistema,
                Codigo = message.Codigo,
                Nombre = message.Nombre,
                Descripcion = message.Descripcion,
                EsActivo = true
            };

            await _genericRepository.AddOneAsync(data);

            return rsp;
        }
    }
}
