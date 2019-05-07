using System.Threading.Tasks;

namespace CashBasisAccounting.Helpers
{
    public interface IMessageService
    {
        Task Send(string email, string subject, string message);
    }
}
