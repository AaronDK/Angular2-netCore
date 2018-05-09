using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace TourOfHeroesService.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Constructor

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        #endregion

        #region IUnitOfWork

        public DbContext Context { get; set; }

        public TransactionScope CreateTransaction(System.Transactions.IsolationLevel isolationLevel, int timeoutInSeconds)
        {
            return CreateTransaction(isolationLevel, timeoutInSeconds, TransactionScopeAsyncFlowOption.Suppress);
        }

        public TransactionScope CreateTransaction(System.Transactions.IsolationLevel isolationLevel, int timeoutInSeconds, TransactionScopeAsyncFlowOption asyncFlowOption)
        {
            var option = TransactionScopeOption.Required;
            var options = new TransactionOptions()
            {
                IsolationLevel = isolationLevel,
                Timeout = new TimeSpan(0, 0, timeoutInSeconds)
            };
            return new TransactionScope(option, options, asyncFlowOption);
        }

        public int Commit()
        {
            return Context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            return new GenericRepository<T>(this.Context);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                }
            }
            // free native resources if there are any.
        }

        #endregion
    }
}