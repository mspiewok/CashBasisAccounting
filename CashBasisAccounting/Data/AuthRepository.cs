using System;
using System.Threading.Tasks;
using CashBasisAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace CashBasisAccounting.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CashBasisAccountingContext context;

        public AuthRepository(CashBasisAccountingContext context) {
            this.context = context;
        }

        public async Task<UserAccount> Login(string username, string password)
        {
            var userAccount = await this.context.UserAccounts.FirstOrDefaultAsync(x => x.Username == username);

            if (userAccount == null)
                return null;
            
            if (!VerifyPasswordHash(password, userAccount.PasswordHash, userAccount.PasswordSalt))
                return null;

            return userAccount;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }

        public async Task<UserAccount> Register(UserAccount userAccount, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            userAccount.PasswordHash = passwordHash;
            userAccount.PasswordSalt = passwordSalt;

            await this.context.UserAccounts.AddAsync(userAccount);
            await this.context.SaveChangesAsync();

            return userAccount;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserAccountExists(string username)
        {
            if (await this.context.UserAccounts.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }
    }
}
