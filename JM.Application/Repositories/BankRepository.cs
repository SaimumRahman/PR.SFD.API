using JM.Application.Common.Generic;
using JM.Application.Interfaces;
using JM.Domain.Entities;
using JM.Infrastructure.Base;
using JM.Infrastructure.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.Application.Repositories
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        public readonly ILogger<BankRepository> _logger;

        public BankRepository(ILogger<BankRepository> logger, IConnectionFactory connectionFactory, IDbContextCore dbContext) : base(logger, connectionFactory, dbContext.Instance)
        {
            _logger = logger;

        }

        public Task<Bank> GetALlBank()
        {
            throw new NotImplementedException();
        }
    }
}
