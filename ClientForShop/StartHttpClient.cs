using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using ShopModels;

namespace ClientForShop
{
    internal class StartHttpClient 
    {
        private IHttpClient _httpClient;
        internal StartHttpClient(IHttpClient shopHttpClientFunc)
        {
            _httpClient = shopHttpClientFunc;
        }

        internal void Start()
        {
            
           // Console.OutputEncoding = Encoding.UTF8;
            bool isContinue = true;

            while (isContinue)
            {
                Console.WriteLine("Выберите необходимую операцию\n" +
                    "\t[1] ----> Вывести список витрин\n" +
                    "\t[2] ----> Создать новую витрину с названием и объемом\n" +
                    "\t[3] ----> Отредактировать витрину\n" +
                    "\t[4] ----> Удалить витрину\n" +
                    "\t[5] ----> Операции с товаром\n" +
                    "\t[ESCAPE] ----> Выход\n");

                string operation = Console.ReadLine();

                switch (operation.ToLower())
                {
                    case Operations.SHOW_SHOWCASE:
                        PrintShowcaseList();
                        break;
                    case Operations.CREATE_NEW_SHOWCASE:
                        CreateNewShowcase();
                        break;

                    case Operations.EDIT_SHOWCASE:
                        EditShowcase();
                        break;

                    case Operations.DELITE_SHOWCASE:
                        DeliteShowcase();
                        break;

                    case Operations.OPERATIONS_WITH_PRODUCT:

                        Console.WriteLine("Аналогично, что и с витринами");
                        //OpenMenuOpetationsWithProduct();
                        break;

                    case Operations.EXIT:
                        isContinue = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды нет! " + operation);
                        break;
                }

                if (isContinue)
                {
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private void PrintShowcaseList()
        {
            
            var showcaseList = _httpClient.GetShowcaseList();

            foreach (var item in showcaseList.Items)
            {
                Console.WriteLine($"№{item.Id}: {item.Name} - {item.Size} - {item.CreationTime} - {item.RemovalTime}");
            }
        }


        private void CreateNewShowcase()
        {
            Console.WriteLine("Введите имя витрины");
            var userAnswer = Console.ReadLine();
            var nameShowcaseFromClient = userAnswer;

            Console.WriteLine("Введите вместимость витрины");
            userAnswer = Console.ReadLine();
            var sizeShowcaseFromClient = int.Parse(userAnswer);

            _httpClient.CreateNewShowcase(nameShowcaseFromClient, sizeShowcaseFromClient);
        }

        private void EditShowcase()
        {
            PrintShowcaseList();
            Console.WriteLine("Введите id витрины");
            var userAnswer = Console.ReadLine();
            var idShowcase = int.Parse(userAnswer);

            Console.WriteLine("Введите вместимость витрины");
            userAnswer = Console.ReadLine();
            var sizeShowcaseFromClient = int.Parse(userAnswer);

            //_httpClient.EdditShowcase(idShowcase, sizeShowcaseFromClient);


            Console.WriteLine(_httpClient.EdditShowcase(idShowcase, sizeShowcaseFromClient));
        }

        private void DeliteShowcase()
        {
            Console.WriteLine("Введите id витрины");
            var userAnswer = Console.ReadLine();
            var idShowcase = int.Parse(userAnswer);

            _httpClient.DeleteShowcase(idShowcase);
        }

        private void OpenMenuOpetationsWithProduct()
        {
            throw new NotImplementedException();
        }

        private  void SetAsDoneIssue()
        {
            var httpClient = new HttpClient();
            var response = httpClient.PatchAsync("http://localhost:51369/FirstMenu", null).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
        }

    }
}
