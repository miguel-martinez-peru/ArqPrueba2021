using Core;
using CQRSSqlServer.Domain.AggregatesModel.LocalAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institucional.Api.Application.Commands
{
    public class CrearLocalCommandHandler : IRequestHandler<CrearLocalCommand, GenericResult>
    {
        private readonly ILocalRepository _localRepository;
        private readonly IMediator _mediator;

        public CrearLocalCommandHandler(IMediator mediator, ILocalRepository localRepository)
        {
            _localRepository = localRepository;
            _mediator = mediator;
        }

        public async Task<GenericResult> Handle(CrearLocalCommand message, CancellationToken cancellationToken)
        {
            var rsp = new GenericResult();

            if (!rsp.HasErrors)
            {
                Local local = new Local(
                    message.IdLocal,
                    message.IdFilial,
                    message.Codigo,
                    message.AforoLocal,
                    message.Comentarios,
                    message.IdTblEstadoVigencia,
                    message.EsEliminado,
                    message.Guid

                    , message.UsuarioCreacion
                    , message.FechaCreacion
                    , message.IpCreacion
                    );

                _localRepository.Add(local);
                await _localRepository.UnitOfWork.SaveEntitiesAsync();
            }

            return rsp;
        }
    }
}
