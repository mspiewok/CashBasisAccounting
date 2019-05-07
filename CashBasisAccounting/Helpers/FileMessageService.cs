using System.IO;
using System.Threading.Tasks;

namespace CashBasisAccounting.Helpers
{
    public class FileMessageService : IMessageService
    {
        public Task Send(string email, string subject, string message)
        {
            var emailMessage = $"To: {email}\nSubject: {subject}\nMessage: {message}";
            File.AppendAllText("emails.txt", emailMessage);
            return Task.FromResult(0);
        }
    }
}
