
using JM.AuthServer.API.Models;
using JM.Infrastructure.Base;
using JM.Infrastructure.Common;
using JM.Infrastructure.Grid;
using JM.Infrastructure.Paging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace JM.AuthServer.API.Repository.Auths
{
    public class AuthRepository : BaseRepository<User>, IAuthRepository
    {
        public readonly ILogger<AuthRepository> _logger;
        public AuthRepository(ILogger<AuthRepository> logger, IConnectionFactory connectionFactory) : base(logger, connectionFactory)
        {
            _logger = logger;
        }

        public async Task<int> CreateUser(User users,string hasPass)
         {
            try
            {
                string sql = $@"INSERT INTO core_users (Email, UserName, IsFirstLogin, IsActive, PhoneNumber, FullName, DesignationId, DepartmentId, CreateBy, CreateDate, IsExpired, PASSWORDHASH)
                     VALUES
                 (:Email, :UserName, :IsFirstLogin, :IsActive, :PhoneNumber, :FullName, :DesignationId, :DepartmentId, :CreateBy, SYSTIMESTAMP, :IsExpired, '{hasPass}')";

                var j =await base.ExecuteAsync(sql, users);
             
                return j;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async  Task<User> IsUserExists(string username, string email, string phonenumber)
        {
            var query = "Select UserId,Email,phonenumber from core_users Where username= :username or phonenumber = :phonenumber or email = :email";
            var t= await base.QueryFirstOrDefaultAsync<User>(query, new { username = username, phonenumber= phonenumber, email= email });
            return t;

        }

        public async Task<User> LoginUser(string username)
        {
            var query =$@"Select UserId,Email, UserName, LockoutEnd, IsFirstLogin, IsActive, FullName, DesignationId,phonenumber,
                          DepartmentId, CreateBy, UpdateBy, LandingPage,ThemeId,LastLoginDate,CreateDate,IsExpired, PASSWORDHASH from core_users 
                           Where username= :username or phonenumber = :username or email = :username";
            var t = await base.QueryFirstOrDefaultAsync<User>(query, new { username = username });
            return t;
        }
        public async Task<User> LoginUserById(int userId)
        {
            var query =$@"Select UserId,Email, UserName, LockoutEnd, IsFirstLogin, IsActive, FullName, DesignationId,phonenumber,
                          DepartmentId, CreateBy, UpdateBy, LandingPage,ThemeId,LastLoginDate,CreateDate,IsExpired, PASSWORDHASH from core_users 
                           Where UserId= :userId ";
            var t = await base.QueryFirstOrDefaultAsync<User>(query, new { UserId = userId });
            return t;
        }
    }
}
