using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Dados
{
    public class UnitOfWork
    {
        private readonly LocadoraContext _locadoraContext;
        private Transacao _transacao = null;

        public UnitOfWork(LocadoraContext locadoraContext)
        {
            _locadoraContext = locadoraContext;
        }

        public Transacao IniciarTransacao()
        {
            if (_transacao != null)
                _transacao = new Transacao(_locadoraContext);

            return _transacao;
        }
    }

    public class Transacao : IDisposable
    {
        private readonly LocadoraContext _locadoraContext;
        public Transacao(LocadoraContext locadoraContext)
        {
            _locadoraContext = locadoraContext;
            _locadoraContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _locadoraContext.SaveChanges();
            if (_locadoraContext.Database.CurrentTransaction != null)
                _locadoraContext.Database.CommitTransaction();
        }

        public void Dispose()
        {
            Rollback();
        }

        private void Rollback()
        {
            if (_locadoraContext.Database.CurrentTransaction != null)
                _locadoraContext.Database.RollbackTransaction();
        }
    }
}
