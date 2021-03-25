using System;
using System.Net.Http;

namespace ClientShop
{
    class Program
    {
        static void Main(string[] args)
        {



            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("http://localhost:51369/").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
        
        }
    }
}
