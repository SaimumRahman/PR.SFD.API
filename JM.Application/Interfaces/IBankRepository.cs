using JM.Domain.Entities;
using JM.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.Application.Interfaces
{
    public interface IBankRepository : IBaseDapperRepository, IGenericRepository<Bank>
    {
        Task<Bank> GetALlBank();
    }
}
