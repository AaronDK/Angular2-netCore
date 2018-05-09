using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace TourOfHeroesService.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        DbContext Context { get; set; }
        TransactionScope CreateTransaction(IsolationLevel isolationLevel, int timeoutInSeconds);
        TransactionScope CreateTransaction(System.Transactions.IsolationLevel isolationLevel, int timeoutInSeconds, TransactionScopeAsyncFlowOption asyncFlowOption);
        int Commit();
        Task<int> CommitAsync();

        IRepository<T> Repository<T>() where T : class;
    }
}