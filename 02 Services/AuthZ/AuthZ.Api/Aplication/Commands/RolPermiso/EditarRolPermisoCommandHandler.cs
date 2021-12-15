
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthZ.Api.Application.ViewModels;
using AuthZ.Api.Application.Commands;
using AuthZ.Domain.AggregatesModel;
using AuthZ.Domain.AggregatesModel.RolPermisoAggregate;

namespace AuthZ.Api.Application.Commands
{
    public class EditarRolPermisoCommandHandler : IRequestHandler<EditarRolPermisoCommand, GenericResponseViewModel>
    {
        private readonly IMongoDBGenericRepository _genericRepository;
        private readonly IMediator _mediator;

        public EditarRolPermisoCommandHandler(IMediator mediator, IMongoDBGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
            _mediator = mediator;
        }

        public async Task<GenericResponseViewModel> Handle(EditarRolPermisoCommand message, CancellationToken cancellationToken)
        {
            var rsp = new GenericResponseViewModel();

            var data = await _genericRepository.GetOneAsync<RolPermiso>(x => x.IdPermiso == message.IdPermiso && x.IdRol == message.IdRol);

            data.EsEliminado = message.Eliminado;

            await _genericRepository.UpdateOneAsync(data);

            return rsp;
        }
    }
}
