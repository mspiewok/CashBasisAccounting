namespace CashBasisAccounting.Models
{
    public class TaxType
    {
        public int Id { get; set; }
        // check for values Free, Reduced, Full
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
