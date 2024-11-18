
using JM.AuthServer.API.Models;
using JM.Infrastructure.Base;
using JM.Infrastructure.Common;
using JM.Infrastructure.Grid;
using JM.Infrastructure.Paging;
using JM.AuthServer.API.Services.RefreshTokenRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace JM.AuthServer.API.Repository.Auths
{
    public class RefreshTokenRpository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public readonly ILogger<RefreshTokenRpository> _logger;
        public RefreshTokenRpository(ILogger<RefreshTokenRpository> logger, IConnectionFactory connectionFactory) : base(logger, connectionFactory)
        {
            _logger = logger;
        }

        public async Task<int> CreaterRefreshToken(RefreshToken refreshToken)
        {
            try
            {
                string sql = $@"insert into core_refreshtoken (UserId,Token) values (:UserId,:Token)";

                var j = await base.ExecuteAsync(sql, refreshToken);

                return j;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RefreshToken> GetByToken(string Token)
        {
            var query = "Select UserId,Token from core_refreshtoken Where Token= :Token";
            var t = await base.QueryFirstOrDefaultAsync<RefreshToken>(query, new { Token = Token });
            return t;
        } 
        public async Task<int> DeleteTokenByUserId(int UserId)
        {
            var query = "delete from core_refreshtoken Where UserId= :UserId";
            var t = await base.ExecuteAsync(query, new { UserId = UserId });
            return t;
        }
    }
}
