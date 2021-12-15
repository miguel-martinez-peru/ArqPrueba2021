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
    public class EditarPermisoCommandHandler : IRequestHandler<EditarPermisoCommand, GenericResponseViewModel>
    {
        private readonly IMongoDBGenericRepository _genericRepository;
        private readonly IMediator _mediator;

        public EditarPermisoCommandHandler(IMediator mediator, IMongoDBGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
            _mediator = mediator;
        }

        public async Task<GenericResponseViewModel> Handle(EditarPermisoCommand message, CancellationToken cancellationToken)
        {
            var rsp = new GenericResponseViewModel();

            var data = await _genericRepository.GetOneAsync<Permiso>(x => x.IdPermiso == message.IdPermiso);

            data.Codigo = message.Codigo;
            data.Nombre = message.Nombre;
            data.Descripcion = message.Descripcion;
            data.EsActivo = true;

            await _genericRepository.UpdateOneAsync(data);

            return rsp;
        }
    }
}
