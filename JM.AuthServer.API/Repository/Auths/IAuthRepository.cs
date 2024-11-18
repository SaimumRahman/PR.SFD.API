using JM.AuthServer.API.Models;
using JM.Infrastructure.Base;
using System.Threading.Tasks;

namespace JM.AuthServer.API.Repository.Auths
{
    public interface IAuthRepository : IBaseDapperRepository, IGenericRepository<User>
    {
        Task<User> IsUserExists(string username, string email, string phonenumber);
        Task<int> CreateUser(User users,string hasPass);
        Task<User> LoginUser(string username);
        Task<User> LoginUserById(int userId);
    }
}
