using CashBasisAccounting.Models;
using System.Threading.Tasks;

namespace CashBasisAccounting.Data
{
    public interface IAuthRepository
    {
        Task<UserAccount> Register(UserAccount userAccount, string password);
        Task<UserAccount> Login(string username, string password);
        Task<bool> UserAccountExists(string username);
    }
}
