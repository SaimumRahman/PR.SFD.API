
using JM.Middleware.Http;
using JM.Application.Interfaces.I_Common;
using JM.Infrastructure.Base;
using System;
using System.Threading.Tasks;



namespace JM.Application.Common.Generic
{
    public interface IUnitOfWorkJM: IDisposable 
    {
        public IBaseDapperRepository dapperRepo { get; }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;



        public ICommonLibRepository Common { get; }

        void BeginTransaction();

        void Commit();

        void Rollback();
        
    }
}
