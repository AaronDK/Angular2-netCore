using System;
using TourOfHeroesService.Data.Models;
using TourOfHeroesService.Infrastructure.Common;
using TourOfHeroesService.Infrastructure.Repository;

namespace TourOfHeroesService.Services.Common
{
    public class ManagerBase : DisposableBase, IManagerBase
    {
        public ManagerBase()
            : this(null)
        { }

        public ManagerBase(IUnitOfWork uow)
        {
            _uow = uow;
            AutoDisposeUow = _uow == null;
        }

        private IUnitOfWork _uow;
        public IUnitOfWork Uow
        {
            get { return _uow ?? (_uow = CreateUow()); }
            set { _uow = value; }
        }

        protected bool AutoDisposeUow { get; set; }

        protected virtual IUnitOfWork CreateUow()
        {
            return new UnitOfWork(MySqlDataBaseConfig.CreateContext());
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            if (_uow != null && AutoDisposeUow)
            {
                ((IDisposable)_uow).Dispose();
            }
            _uow = null;
        }
    }
}