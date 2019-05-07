using System;

namespace CashBasisAccounting.Models
{
    public class Posting
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime PostingDate { get; set; }
        public string Description { get; set; }
        public double NetAmount { get; set; }
        public int AccountSystemId { get; set; }
        public int TaxTypeId { get; set; }
    }
}
