using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CQRSSqlServer.Domain.AggregatesModel.LocalAggregate;
using Core.SeedworkCommands;

namespace CQRSSqlServer.Infrastructure.Repositories
{
    public class LocalRepository: ILocalRepository
    {
        private readonly InstitucionalContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public LocalRepository(InstitucionalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Local Add(Local filial)
        {
            return _context.Set<Local>().Add(filial).Entity;
        }

        public async Task<Local> FindByIdAsync(int id)
        {
            var filial = await _context.Set<Local>().FindAsync(id);
            return filial;
        }

        public Local Update(Local local)
        {
            return _context.Locales.Update(local).Entity;
        }

        public Local DeleteLogic(Local local)
        {
            return _context.Set<Local>().Update(local).Entity;
        }

        public List<Local> GetListByIdFilial(int idFilial)
        {
            return _context.Locales.Where(x => x._IdFilial.Equals(idFilial) && !x._EsEliminado).ToList();
        }

        public List<Local> GetListaLocalesByIdFilialyVigencia(int idFilial, int idTblEstado)
        {
            return _context.Locales.Where(x => x._IdFilial.Equals(idFilial) && x._IdTblEstadoVigencia.Equals(idTblEstado) && !x._EsEliminado).ToList();
        }
    }
}
