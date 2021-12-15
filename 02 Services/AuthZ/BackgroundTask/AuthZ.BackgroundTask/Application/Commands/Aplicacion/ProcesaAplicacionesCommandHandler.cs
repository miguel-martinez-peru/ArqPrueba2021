using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthZ.Domain.AggregatesModel;
using AuthZ.BackgroundTask.Application.ViewModels;
using AuthZ.Domain.AggregatesModel.AplicacionAggregate;

namespace AuthZ.BackgroundTask.Application.Commands
{
    public class ProcesaAplicacionesCommandHandler : IRequestHandler<ProcesaAplicacionesCommand, GenericResponseViewModel>
    {
        private readonly IMongoDBGenericRepository _genericRepository;
        private readonly IMediator _mediator;

        public ProcesaAplicacionesCommandHandler(IMediator mediator, IMongoDBGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
            _mediator = mediator;
        }

        public async Task<GenericResponseViewModel> Handle(ProcesaAplicacionesCommand message, CancellationToken cancellationToken)
        {
            var rsp = new GenericResponseViewModel();

            message.Aplicaciones.ForEach(async x =>
            {
                var data = await _genericRepository.GetOneAsync<Aplicacion>(j => j.IdSistema == x.IdSistema);

                if (data != null)
                {
                    data.Codigo = x.Codigo;
                    data.Nombre = x.Nombre;
                    data.Abreviatura = x.Abreviatura;

                    await _genericRepository.UpdateOneAsync(data);
                }
                else
                {
                    var entidad = new Aplicacion
                    {
                        IdSistema = x.IdSistema,
                        Codigo = x.Codigo,
                        Nombre = x.Nombre,
                        Abreviatura = x.Abreviatura
                    };
                    await _genericRepository.AddOneAsync(entidad);
                }

            });

            return rsp;
        }
    }
}
