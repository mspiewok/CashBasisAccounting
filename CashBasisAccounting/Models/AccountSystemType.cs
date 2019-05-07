namespace CashBasisAccounting.Models
{
    public class AccountSystemType
    {
        public int Id { get; set; }

        // Check for values Income, Expense, Other 
        public string Name { get; set; }
    }
}