using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Consumer
{
    public static class ConsumerApiClient
    {
        static public async Task<HttpResponseMessage> FaturaOku(string mbb, string baseUri)
        {
            using var client = new HttpClient { BaseAddress = new Uri(baseUri) };
            var response = await client.GetAsync($"/api/fatura?mbb={mbb}");
            return response;
        }
        static public async Task<HttpResponseMessage> FaturaOku(string mbb, string kurum, string baseUri)
        {
            using var client = new HttpClient { BaseAddress = new Uri(baseUri) };
            var response = await client.GetAsync($"/api/fatura?mbb={mbb}&kurum={kurum}");
            return response;
        }
        static public async Task<HttpResponseMessage> ValidateDateTimeUsingProviderApi(string dateTimeToValidate, string baseUri)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(baseUri)})
            {
                try
                {
                    var response = await client.GetAsync($"/api/provider?validDateTime={dateTimeToValidate}");
                    return response;
                }
                catch (System.Exception ex)
                {
                    throw new Exception("There was a problem connecting to Provider API.", ex);
                }
            }
        }
    }
}