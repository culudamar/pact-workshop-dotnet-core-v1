using System;
using Xunit;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Consumer;
using System.Collections.Generic;
using Newtonsoft.Json;

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

        [Fact]
        public void MBBninTumFaturalariDonuyor()
        {
            var mbb = "123";

            var faturalar = new List<Fatura>()
            {
                new Fatura{ mbb = 123, kurum = "ISKI", sonOdemeTarihi = "1.1.2022", faturaNo = "ngut12" },
                new Fatura{ mbb = 123, kurum = "Ayedas", sonOdemeTarihi = "1.2.2022", faturaNo = "te53bd" },
            };

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
            var servistenGelenFaturalar = JsonConvert.DeserializeObject<List<Fatura>>(resultBody);

            // Assert
            Assert.Equal(faturalar, servistenGelenFaturalar);
        }

        [Fact]
        public void MBBninSpesifikFaturasiDonuyor()
        {
            var mbb = "123";
            var kurum = "ISKI";
            var iskiFaturalari = new List<Fatura>()
            {
                new Fatura { mbb = 123, kurum = "ISKI", sonOdemeTarihi = "1.1.2022", faturaNo = "ngut12" },
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
                                    Body = iskiFaturalari
                                });

            // Act
            var result = ConsumerApiClient.FaturaOku(mbb, kurum, _mockProviderServiceBaseUri).GetAwaiter().GetResult();
            var resultBody = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var servistenGelenFaturalar = JsonConvert.DeserializeObject<List<Fatura>>(resultBody);

            // Assert
            Assert.Equal(iskiFaturalari, servistenGelenFaturalar);
        }
        public class Fatura
        {
            public int mbb { get; set; }
            public string kurum { get; set; }
            public string sonOdemeTarihi { get; set; }
            public string faturaNo { get; set; }

            // override object.Equals
            public override bool Equals(object obj)
            {
                //       
                // See the full list of guidelines at
                //   http://go.microsoft.com/fwlink/?LinkID=85237  
                // and also the guidance for operator== at
                //   http://go.microsoft.com/fwlink/?LinkId=85238
                //

                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                var f = obj as Fatura;
                return mbb.Equals(f.mbb) && faturaNo.Equals(f.faturaNo) && kurum.Equals(f.kurum) && sonOdemeTarihi.Equals(f.sonOdemeTarihi);
            }

            // override object.GetHashCode
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
    }
}
