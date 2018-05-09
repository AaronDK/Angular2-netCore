using System;
using TourOfHeroesService.Infrastructure.Repository;

namespace TourOfHeroesService.Services.Common
{
    public interface IManagerBase : IDisposable
    {
        IUnitOfWork Uow { get; }
    }
}