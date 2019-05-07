using CashBasisAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace CashBasisAccounting.Data
{
    public class CashBasisAccountingContext : DbContext
    {
        public CashBasisAccountingContext(DbContextOptions<CashBasisAccountingContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Posting> Postings { get; set; }
        public DbSet<AccountSystem> AccountSystems { get; set; }
        public DbSet<AccountSystemType> AccountSystemTypes { get; set; }
        public DbSet<TaxType> TaxTypes { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
