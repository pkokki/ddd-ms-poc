using Accounts.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileGateway.Models
{
    public class TransferResponse
    {
        public string CreditProductCode { get; set; }
        public IEnumerable<CreditProductOwner> CreditProductOwners { get; set; }
        public string ProcessDate { get; set; }
        public string SubmittedTimeStamp { get; set; }
        public string TransactionAUN { get; set; }
        public string TransactionUN { get; set; }
    }

    public class CreditProductOwner
    {
        public string CompanyName { get; set; }
        public string CompanyTitle { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
        public string CustomerNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string FatherName { get; set; }
        public string IdentityCode { get; set; }
        public int IdentityType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }

    public class Contact
    {
        public int ContactType { get; set; }
        public string Description { get; set; }
    }

    public static class TransferResponseExtensions
    {
        public static TransferResponse ToResponse(this TransferCommandResponse result)
        {
            return new TransferResponse()
            {
                CreditProductCode = null,
                CreditProductOwners = null,
                ProcessDate = null,
                SubmittedTimeStamp = null,
                TransactionAUN = null,
                TransactionUN = null
            };
        }
    }
}
