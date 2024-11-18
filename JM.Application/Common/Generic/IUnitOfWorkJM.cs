
using JM.Middleware.Http;
using JM.Application.Interfaces.I_Common;
using JM.Infrastructure.Base;
using System;
using System.Threading.Tasks;
using JM.Application.Interfaces.I_Config;



namespace JM.Application.Common.Generic
{
    public interface IUnitOfWorkJM: IDisposable 
    {
        public IBaseDapperRepository dapperRepo { get; }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;



        public ICommonLibRepository Common { get; }

        #region Configurations

        public IPropertySetting PropertySetting { get; }
        #endregion

        void BeginTransaction();

        void Commit();

        void Rollback();
        
    }
}
