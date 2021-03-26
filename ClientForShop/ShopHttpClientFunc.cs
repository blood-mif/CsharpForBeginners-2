using Newtonsoft.Json;
using ShopModels;
using System;
using System.Net.Http;

namespace ClientForShop
{
    class ShopHttpClientFunc : IHttpClient
    {
        private readonly HttpClient _client;

        public ShopHttpClientFunc(HttpClient client, Uri baseUri)
        {
            _client = client;
            _client.BaseAddress = baseUri;
        }
        public void SelectMenuItem(int menuItemId, int quantity)
        {
            throw new NotImplementedException();
        }

        public ListShowcaseModel GetShowcaseList()
        {
            var response = _client.GetAsync("FirstMenu").Result;
            var content = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<ListShowcaseModel>(content);
        }

        public void CreateNewShowcase(string nameShowcase, int sizeShowcase)
        {
            var request = new ShowcaseItemRequestModel
            {
                Name = nameShowcase,
                Size = sizeShowcase
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json);
            var response = _client.PostAsync("FirstMenu", content).Result;
        }
    }

}
