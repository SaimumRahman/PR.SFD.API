using JM.Infrastructure.Base;
using JM.Infrastructure.Common;
using System;
using System.Collections;
using JM.Middleware.Http;
using JM.Application.Interfaces.I_Common;
using JM.Application.Interfaces.I_Config;

namespace JM.Application.Common.Generic
{
    public class UnitOfWorkJM : IUnitOfWorkJM
    {

        public UnitOfWorkJM(IBaseDapperRepository baseDapper, IUserIdentityService userIdentityService, ICommonLibRepository common, IPropertySetting propertySetting)
        {
            dapperRepo = baseDapper;
            UserIdentityService = userIdentityService;
            Common = common;
            PropertySetting= propertySetting;
        }

        public IUserIdentityService UserIdentityService { get; }

        public ICommonLibRepository Common { get; }
        public IConnectionFactory _conn { get; }
        public IBaseDapperRepository dapperRepo { get; }

        #region Configurations

        public IPropertySetting PropertySetting { get; }

        #endregion

        private Hashtable _repositories;
     

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IGenericRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(BaseRepository<>);
            var dType = repositoryType.MakeGenericType(typeof(TEntity));
            _repositories.Add(type, Activator.CreateInstance(dType, _conn));

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //_dbContext.Dispose();
            }
        }

        public void BeginTransaction()
        {
            dapperRepo.BeginTransaction();
        }

        public void Commit()
        {
            dapperRepo.Commit();
        }

        public void Rollback()
        {
            dapperRepo.Rollback();
        }
    }
}
