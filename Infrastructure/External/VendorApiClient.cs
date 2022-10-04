namespace DomainArchitecture.Infrastructure.External
{
    public class VendorApiClient {
        private HttpClient _client;

        public int NotifyPurchase(int productId) {
            var quantityAvailable = _client.PostAsync("https://api.url.com/purchases?productId=" + productId, new StringContent("")).Result.Content.ReadFromJsonAsync<int>().Result;
            return quantityAvailable;
        }
    }
}
