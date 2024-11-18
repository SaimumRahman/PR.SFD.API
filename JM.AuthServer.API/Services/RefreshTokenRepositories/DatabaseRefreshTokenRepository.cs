using JM.AuthServer.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JM.AuthServer.API.Services.RefreshTokenRepositories
{
    public class DatabaseRefreshTokenRepository //:IRefreshTokenRepository
    {
        private readonly AuthenticationDbContext _context;

        public DatabaseRefreshTokenRepository(AuthenticationDbContext context)
        {
            _context = context;
        }

        //public async Task Create(RefreshToken refreshToken)
        //{
        //    _context.RefreshTokens.Add(refreshToken);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task Delete(Guid id)
        //{
        //    RefreshToken refreshToken = await _context.RefreshTokens.FindAsync(id);
        //    if (refreshToken != null)
        //    {
        //        _context.RefreshTokens.Remove(refreshToken);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task DeleteAll(Guid userId)
        //{
        //    IEnumerable<RefreshToken> refreshTokens = await _context.RefreshTokens
        //        .Where(t => t.UserId == userId)
        //        .ToListAsync();

        //    _context.RefreshTokens.RemoveRange(refreshTokens);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<RefreshToken> GetByToken(string token)
        //{
        //    return await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        //}
        //public async  Task<List<Role>> GetRolesByUserId(int userId)
        //{
        //    string query = $@"SELECT ReferenceId AS RoleId,role.RoleName ,GroupId 
        //                        FROM core_GroupPermission  gm
        //                        inner JOIN core_Role role ON role.RoleId=gm.ReferenceId
        //                        WHERE PermissionTableName='Role'
        //                          AND GroupId IN (SELECT groupId FROM core_GroupMember WHERE userid={userId})";
        //    var roles=   _context.Roles.FromSqlRaw<Role>(query);
        //    return await roles.ToListAsync();
        //}
    }


}
