using System;
using Xunit;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Consumer;
using System.Collections.Generic;

namespace tests
{
    public class ConsumerPactTests : IClassFixture<ConsumerPactClassFixture>
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public ConsumerPactTests(ConsumerPactClassFixture fixture)
        {
            _mockProviderService = fixture.MockProviderService;
            _mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
            _mockProviderServiceBaseUri = fixture.MockProviderServiceBaseUri;
        }

        readonly List<object> faturalar = new List<object>()
        {
            new { mbb = 123, kurum = "ISKI", sonOdemeTarihi = "1.1.2022", faturaNo = "ngut12" },
            new { mbb = 123, kurum = "Ayedas", sonOdemeTarihi = "1.2.2022", faturaNo = "te53bd" },
        };

        [Fact]
        public void MBBninTumFaturalariDonuyor()
        {
            var mbb = "123";

            // Arrange
            _mockProviderService.Given("There is data")
                                .UponReceiving("GET MBB'nin tum faturalari")
                                .With(new ProviderServiceRequest
                                {
                                    Method = HttpVerb.Get,
                                    Path = $"/api/fatura"
                                    ,
                                    Query = $"mbb={mbb}"
                                })
                                .WillRespondWith(new ProviderServiceResponse
                                {
                                    Status = 200,
                                    Headers = new Dictionary<string, object>
                                    {
                                        { "Content-Type", "application/json; charset=utf-8" }
                                    },
                                    Body = faturalar
                                });

            // Act
            var result = ConsumerApiClient.FaturaOku(mbb, _mockProviderServiceBaseUri).GetAwaiter().GetResult();
            var resultBody = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            // TODO: Assert
            // Assert.Contains(expectedDateParsed, resultBody);
            Console.WriteLine("Mock servis tum faturalar: " + resultBody);
        }

        [Fact]
        public void MBBninSpesifikFaturasiDonuyor()
        {
            var mbb = "123";
            var kurum = "ISKI";
            var iskiFaturasi = new List<object>()
            {
                new { mbb = 123, kurum = "ISKI", sonOdemeTarihi = "1.1.2022", faturaNo = "ngut12" },
            };

            // Arrange
            _mockProviderService.Given("There is data")
                                .UponReceiving("GET MBB'nin spesifik faturasi")
                                .With(new ProviderServiceRequest
                                {
                                    Method = HttpVerb.Get,
                                    Path = $"/api/fatura"
                                    ,
                                    Query = $"mbb={mbb}&kurum={kurum}"
                                })
                                .WillRespondWith(new ProviderServiceResponse
                                {
                                    Status = 200,
                                    Headers = new Dictionary<string, object>
                                    {
                                        { "Content-Type", "application/json; charset=utf-8" }
                                    },
                                    Body = iskiFaturasi
                                });

            // Act
            var result = ConsumerApiClient.FaturaOku(mbb, kurum, _mockProviderServiceBaseUri).GetAwaiter().GetResult();
            var resultBody = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            // TODO Assert
            // Assert.Contains(expectedDateParsed, resultBody);
            Console.WriteLine("Mock servis spesifik fatura: " + resultBody);
        }
    }
}
