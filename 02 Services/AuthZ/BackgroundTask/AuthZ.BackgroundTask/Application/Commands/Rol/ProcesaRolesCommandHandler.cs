using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthZ.Domain.AggregatesModel;
using AuthZ.BackgroundTask.Application.ViewModels;
using AuthZ.Domain.AggregatesModel.RolAggregate;

namespace AuthZ.BackgroundTask.Application.Commands
{
    public class ProcesaRolesCommandHandler : IRequestHandler<ProcesaRolesCommand, GenericResponseViewModel>
    {
        private readonly IMongoDBGenericRepository _genericRepository;
        private readonly IMediator _mediator;

        public ProcesaRolesCommandHandler(IMediator mediator, IMongoDBGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
            _mediator = mediator;
        }

        public async Task<GenericResponseViewModel> Handle(ProcesaRolesCommand message, CancellationToken cancellationToken)
        {
            var rsp = new GenericResponseViewModel();

            message.Roles.ForEach(async x =>
            {
                var data = await _genericRepository.GetOneAsync<Rol>(j => j.IdRol == x.IdRol);

                if(data != null)
                {
                    data.IdSistema = x.IdSistema;
                    data.Nombre = x.Nombre;

                    await _genericRepository.UpdateOneAsync(data);
                }
                else
                {
                    var entidad = new Rol
                    {
                        IdRol = x.IdRol,
                        IdSistema = x.IdSistema,
                        Nombre = x.Nombre
                    };

                    await _genericRepository.AddOneAsync(entidad);
                }

            });

            return rsp;
        }
    }
}
