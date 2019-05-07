namespace CashBasisAccounting.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }

        public string StreetAdress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }
        public string MobilNumber { get; set; }
        public string Email { get; set; }

        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }

        public string UserAccountId { get; set; }
    }
}
