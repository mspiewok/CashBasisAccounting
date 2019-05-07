using CashBasisAccounting.Models;
using System;

namespace CashBasisAccounting.Data
{
    public static class TestData
    {
        public static void Seed(CashBasisAccountingContext context)
        {
            //context.UserAccounts.AddRange
            //        (
            //        //    new UserAccount
                    //    {
                    //        Username = "mspiewok@outlook.com",
                    //        CreatedOn = DateTime.Now,
                    //        LastLoginOn = null,
                    //        Password = null,
                    //        VerifiedFlag = "N"
                    //    }
                    //);
            context.Clients.AddRange
                    (
                        new Client
                        {
                            Id = 1,
                            Firstname = "Hardy",
                            Surname = "Work",
                            CompanyName = "Softwareentwicklung",
                            StreetAdress = "Neue Strasse 100",
                            City = "Neustadt",
                            PostalCode = "12345",
                            Email = "mspiewok@outlook.com",
                            PhoneNumber = null,
                            MobilNumber = "+49 178 7743965",
                            TaxNumber = "12/34567/89",
                            TaxOffice = "FA Neustadt"
                        }
                    );
            context.TaxTypes.AddRange
                (
                    new TaxType
                    {
                        Id = 1,
                        Name = "Free",
                        Value = 0.0
                    },
                    new TaxType
                    {
                        Id = 2,
                        Name = "Reduced",
                        Value = 7.0
                    },
                    new TaxType
                    {
                        Id = 3,
                        Name = "Full",
                        Value = 19.0
                    }
                );
            context.AccountSystemTypes.AddRange
                (
                    new AccountSystemType
                    {
                        Id = 1,
                        Name = "Income"
                    },
                    new AccountSystemType
                    {
                        Id = 2,
                        Name = "Expense"
                    },
                    new AccountSystemType
                    {
                        Id = 3,
                        Name = "Other"
                    }
                );
            context.AccountSystems.AddRange
                (
                    new AccountSystem
                    {
                        Id = 1,
                        AccountSystemTypeId = 1,
                        Description = "Einnahmen"
                    },
                    new AccountSystem
                    {
                        Id = 2,
                        AccountSystemTypeId = 2,
                        Description = "Ausgaben"
                    },
                    new AccountSystem
                    {
                        Id = 3,
                        AccountSystemTypeId = 3,
                        Description = "Sonstiges, z.B. Anlagenkäufe"
                    }
                    );

            var numberOfPostings = 10;

            var nextPostingId = 0;
            var documentNumber = "";
            for (int i = 0; i < numberOfPostings; i++)
            {
                nextPostingId++;
                documentNumber = nextPostingId.ToString().PadLeft(4, '0');
                // Income
                context.Postings.AddRange
                                    (
                                    new Posting
                                    {
                                        Id = nextPostingId,
                                        ClientId = 1,
                                        DocumentNumber = documentNumber,
                                        Description = $"Rechnung No. {documentNumber}",
                                        PostingDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i + 1),
                                        NetAmount = (i + 1) * 1000,
                                        AccountSystemId = 1,
                                        TaxTypeId = 1
                                    }
                                    );
                // expenses
                nextPostingId++;
                documentNumber = nextPostingId.ToString().PadLeft(4, '0');
                context.Postings.AddRange
                                    (
                                    new Posting
                                    {
                                        Id = nextPostingId,
                                        ClientId = 1,
                                        DocumentNumber = documentNumber,
                                        Description = $"Rechnung No. {documentNumber}",
                                        PostingDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i + 1),
                                        NetAmount = (i + 1) * 100,
                                        AccountSystemId = 2,
                                        TaxTypeId = 1
                                    }
                                    );
                nextPostingId++;
                documentNumber = nextPostingId.ToString().PadLeft(4, '0');
                context.Postings.AddRange
                                    (
                                    new Posting
                                    {
                                        Id = nextPostingId,
                                        ClientId = 1,
                                        DocumentNumber = documentNumber,
                                        Description = $"Rechnung No. {documentNumber}",
                                        PostingDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i + 1),
                                        NetAmount = (i + 1) * 100,
                                        AccountSystemId = 3,
                                        TaxTypeId = 1
                                    }
                                    );

            }
            context.SaveChanges();
        }
    }
}
