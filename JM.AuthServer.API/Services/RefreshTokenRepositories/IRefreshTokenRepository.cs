using JM.AuthServer.API.Models;
using JM.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JM.AuthServer.API.Services.RefreshTokenRepositories
{
    public interface IRefreshTokenRepository: IBaseDapperRepository
    {
        //Task<RefreshToken> GetByToken(string token);
        Task<RefreshToken> GetByToken(string token);

        //Task Create(RefreshToken refreshToken);
        //Task Create(RefreshToken refreshToken);
        Task <int> CreaterRefreshToken(RefreshToken refreshToken);
        Task<int> DeleteTokenByUserId(int UserId);
        //Task Delete(Guid id);

        //Task DeleteAll(Guid userId);
        //Task<List<Role>> GetRolesByUserId(int userId);

    }
}
