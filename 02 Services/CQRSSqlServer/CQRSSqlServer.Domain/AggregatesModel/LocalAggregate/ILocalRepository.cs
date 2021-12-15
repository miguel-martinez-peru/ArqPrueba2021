using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Core.SeedworkCommands;

namespace CQRSSqlServer.Domain.AggregatesModel.LocalAggregate
{
    public interface ILocalRepository: IRepository<Local>
    {
        Local Add(Local local);
        Local Update(Local local);
        Task<Local> FindByIdAsync(int id);
        Local DeleteLogic(Local entidad);
        List<Local> GetListByIdFilial(int idFilial);
        List<Local> GetListaLocalesByIdFilialyVigencia(int idFilial, int idTblEstadoVigencia);
    }
}
