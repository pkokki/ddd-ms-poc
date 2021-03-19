using Microsoft.AspNetCore.Mvc.Testing;
using MobileGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MobileGateway.IntegrationTests.Tests
{
    public class TransferTests : TestFixture
    {

        public TransferTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task PostValidTransferRequest_OK()
        {
            // Arrange
            var client = CreateClient();
            var url = "retail/rest-api/Transfers";
            var headers = new Dictionary<string, string>()
            {
                { "request-id", "1" },
                { "device-identifier", "2" },
                { "services-version", "6" },
            };
            var request = new TransferRequest()
            {
                DebitProductCurrency = 1,
                DebitProductCode = "GR1G0B4A1C5A1C5A0C3B0A0C7H5",
                DebitProductType = 4,
                DebitProductID = 50982,
                CreditProductCode = "GR2G0B4A1C5A1C5A0C3B0A0B9B6",
                CreditProductType = 4,
                CreditProductID = 215,
                AmountCurrency = 1,
                TransferType = 0,
                Amount = 12,
                DebitReason = "καταθέτη",
                CreditReason = "δικαιούχο ",
                TransferDetails = new TransferDetailsItem[]
                {
                    new TransferDetailsItem() { Name = 14, DataType = 0, Value ="DXXo ΣXXXXXXXXXς" },
                    new TransferDetailsItem() { Name = 121, DataType = 0, Value ="Μήνυμα Συνδρομητής Demo Μεταφορά μέσω my Alpha Web" },
                    new TransferDetailsItem() { Name = 126, DataType = 0, Value ="δικαιούχο " },
                },
                ActivityDateTime = DateTime.Now,
                UniqueIdentifier = "5c39bb48-3922-4b6f-a294-dcc66f327a83",
                OTP = "1a781b7642aa3713a57988f18b190a2ffa2d7b1f9ababd9e8e0fe755657b3c03dd47bb84c6fda5e783459f2466db59f4ba13454732cc47a9de9b5ed9fa5964dc0015bb5efd9aaf2974e7dcbc344f6aa7f74288d158743a1c2e987843b919bbbf9b3fba59891bd757e5edbd1ddde5875209f3abfb344ec70d5845ed6aca1401c71399fc1f387848feeeeced2c645a2fadd236cba6c2177b737a95ce70ef2b5245bb9005e7527a47eb7746e2f866713264bc46ceca97577ef58eab39c04392a3c8f3074ac44c11b8aa43b67b43e8f218900ca834cde414fbf0880dea74849dd84b4ca15f4096f67b62466957ce08eb44010a3aa2b298325b3b66fe9cb6046f29 AD2ED77C3E0108C16C76EA5FECD2C266257322EA69A3AF9C252371D394CED41DEBA2F251B126B7562A0A54884E1FEBBD9743E84970A505C6BF59AFF9CED28BBCE53362A92C2CFB4C418925FDBB6502D4395284BA5167712A0BF593090CBB9C110AB4DCADB117E66052C18A32EAD2A03C1A4026EA62BDD7B266FF933740C7EE8CFD6A69CBD8902D3371E588BAF13F2AC39D2F9884323C53D148B7AC85E0D4832E6887229E43718D84388A48744ADE633FF1741CE0CE41C006EE3E1BCF70CF3DA3C2BFC1D071190AAF8B486EEF2FBE1F9931CC77672A9C4A2EF163A01057D12B6CB183C3063DD560728B329B7DE3A11F74775BC9F2B8A90E3E33B3A73CD15EA28B 010001"
            };

            // Act
            var response = await client.PostJsonAsync<TransferResponse>(url, request, HttpStatusCode.Created, headers);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ProcessDate);
            Assert.NotNull(response.SubmittedTimeStamp);
            Assert.NotNull(response.TransactionAUN);
            Assert.NotNull(response.TransactionUN);
        }

    }
}
