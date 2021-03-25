using System;
using System.Net.Http;
using System.Text;

namespace ClientForShop
{
    class Program
    {
        static void Main(string[] args)
        {

            var httpClintFunc = new HttpClient();
            var baseUrl = new Uri("http://localhost:51369");
            var shopHttpClientFunc = new ShopHttpClientFunc(httpClintFunc,baseUrl);

            var start = new StartHttpClient(shopHttpClientFunc);
            start.Start();
           
        }
    }
}
