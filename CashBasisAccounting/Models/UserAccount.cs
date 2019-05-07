using Microsoft.AspNetCore.Identity;
using System;

namespace CashBasisAccounting.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastLoginOn { get; set; }
        public string VerifiedFlag { get; set; }
    }
}
