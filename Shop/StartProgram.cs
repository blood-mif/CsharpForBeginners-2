using System;
using System.Collections.Generic;
using System.Text;
using Shop.Interafaces;
using System.Threading;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using System.Net;
using ShopModels;
using System.Linq;

namespace Shop
{
    public class StartProgram
    {
        public ConsoleKeyInfo keyInfo;
        public List<ShowcaseModel> productStores;
        public uint IdForShowWindow = 3;
        String StatusFunc = "";
        public void Start()
        {
            //Дефолтное добавление витрин в магазин

            productStores = new List<ShowcaseModel>
            {
                new ShowcaseModel("Игрушки для взрослых", 20,0),
                new ShowcaseModel("Латексные изделия", 50,1),
                new ShowcaseModel("Костюмы для ролевых игр",35,2)
            };

            bool isContinue = true;
            while (isContinue)
            {
                Console.WriteLine("Выберите необходимую операцию\n" +
                    "\t[1] ----> Создать новую витрину с названием и объемом\n" +
                    "\t[2] ----> Отредактировать витрину\n" +
                    "\t[3] ----> Удалить витрину\n" +
                    "\t[4] ----> Операции с товаром\n" +
                    "\t[ESCAPE] ----> Выход\n");

                keyInfo = Console.ReadKey(true);
                Console.Clear();
                switch (keyInfo.KeyChar)
                {
                    case '1': //Создать новую ветрину 
                        ShowcaseModel productStore = CreateProductStore();
                        productStores.Add(productStore);
                        IdForShowWindow++;
                        break;
                    case '2':
                        EditProductStore(productStores);
                        break;
                    case '3':
                        RemoveProductStore(productStores);
                        break;
                    case '4':
                        ActionWithProductStore(productStores);
                        break;
                    case (char)ConsoleKey.Escape:
                        isContinue = false;
                        break;
                }
                Console.Clear();
            }
        }

        public void StartServer()
        {
            var httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:51369/");
            httpListener.Start();

            ShowcaseModel store = new ShowcaseModel();

            productStores = new List<ShowcaseModel>
            {
                new ShowcaseModel("Игрушки для взрослых", 20,0),
                new ShowcaseModel("Латексные изделия", 50,1),
                new ShowcaseModel("Костюмы для ролевых игр",35,2)
            };

            //var issueList = new IssueList(1000);

            while (true)
            {
                var requestContext = httpListener.GetContext();
                requestContext.Response.StatusCode = 200; //OK

                //      requestContext.Response.Close();

                var request = requestContext.Request;
                var responseValue = "";

                switch (request.HttpMethod)
                {
                    case "GET":

                            if (request.Url.PathAndQuery == "/PrintShowcase")
                        {
                            requestContext.Response.StatusCode = 200; //OK

                            var _productStores = new ListShowcaseModel()
                            {
                                Items = productStores.Select(dat =>
                                new ShowcaseItemRequestModel
                                {
                                    Id = (int)dat.Id,
                                    Name = dat.Name,
                                    CreationTime = dat.CreationTime,
                                    RemovalTime = dat.RemovalTime,
                                    Size = dat.Size
                                }).ToArray()
                            };

                            responseValue = JsonConvert.SerializeObject(_productStores, Formatting.Indented);
                        }
                        break;
                    case "POST":

                            requestContext.Response.StatusCode = 200; //OK
                            var InpStream = requestContext.Request.InputStream;
                            var bytemas = new byte[requestContext.Request.ContentLength64];
                            InpStream.ReadAsync(bytemas, 0, (int)requestContext.Request.ContentLength64);
                            var content = Encoding.UTF8.GetString(bytemas);
                            var showcaseItemValue = JsonConvert.DeserializeObject<ShowcaseItemRequestModel>(content);

                        if (request.Url.PathAndQuery == "/AddShowcase")
                        {
                            productStores.Add(CreateProductStore(showcaseItemValue.Name, showcaseItemValue.Size));
                        }
                        if (request.Url.PathAndQuery == "/DeleteShowcase")
                        {
                           
                            productStores.RemoveAt((int)showcaseItemValue.Id);
                        }
                        if (request.Url.PathAndQuery == "/EdditShowcase")
                        {
                            responseValue = EditProductStore((uint)showcaseItemValue.Id, showcaseItemValue.Size);
                            StatusFunc = responseValue;
                            

                        }

                        break;


                    case "PUT":
                        requestContext.Response.StatusCode = 200; //OK
                        responseValue = "EDIT_ISSUE";
                        break;

                    default:
                        requestContext.Response.StatusCode = 500; //OK
                        responseValue = "Что то пошло не так";
                        break;
                }

                var stream = requestContext.Response.OutputStream;
                var bytes = Encoding.UTF8.GetBytes(responseValue);
                stream.Write(bytes, 0, bytes.Length);
                requestContext.Response.Close();

            }

            httpListener.Stop();
            httpListener.Close();
        }

        //Создание витрины
        public ShowcaseModel CreateProductStore()
        {
            Console.Write("Введите имя витрины --> ");
            string name = Console.ReadLine();
            Console.Write("Введите размер витрины --> ");
            int size = int.Parse(Console.ReadLine());
            return new ShowcaseModel(name, size, IdForShowWindow);
        }

        //Перегрузка метода для http
        public ShowcaseModel CreateProductStore(string name, int size)
        {
            return new ShowcaseModel(name, size, IdForShowWindow);
        }

        //Вывод списка витрин в консоль
        public void ShowProductStores(List<ShowcaseModel> productStores)
        {
            int counter = 1;
            Console.WriteLine("Список доступных витрин: ");
            foreach (ShowcaseModel showWindow in productStores)
            {
                Console.WriteLine($"{counter}.[ID {showWindow.Id}|NAME - {showWindow.Name}|Size - {showWindow.Size}| CreateTime - {showWindow.CreationTime}]");
                counter++;
            }
        }

        //Редактирование витрины
        public void EditProductStore(List<ShowcaseModel> productStores)
        {
            ShowProductStores(productStores);
            Console.WriteLine("\nВыберите Id витрины для редактирования");
            int number = int.Parse(Console.ReadLine());

            ShowcaseModel store = new ShowcaseModel();

            for (int i = 0; i < productStores.Count; i++)
            {
                if (number == productStores[i].Id)
                {
                    store = productStores[i];
                    break;
                }
            }

            do
            {
                Console.WriteLine("Укажите новый размер для данной витрины");
                number = int.Parse(Console.ReadLine());

                if (store.Products != null || store.Size < number)
                {
                    store.Size = number;
                    Console.WriteLine($"На витрине - {store.Name}, установлен новый размер {store.Size}");
                    break;
                }
                else
                {
                    Console.WriteLine($"Ошибка..\n" +
                        $"Указанный размер {number} меньше нынешнего размера {store.Size} на {store.Size - number}");
                }
            } while (true);
            // Thread.Sleep(3000);
        }

        //Перегрузка метода для http
        public string EditProductStore(uint id, int size)
        {
            //ShowProductStores(productStores);
            //Console.WriteLine("\nВыберите Id витрины для редактирования");
            //int number = int.Parse(Console.ReadLine());

            ShowcaseModel store = new ShowcaseModel();

            for (int i = 0; i < productStores.Count; i++)
            {
                if (id == productStores[i].Id)
                {
                    store = productStores[i];
                    break;
                }
            }

                var requestValue = "";
            do
            {
                int number = size;
                if (store.Products != null || store.Size < number)
                {
                    store.Size = number;
                    requestValue = $"На витрине - { store.Name}, установлен новый размер { store.Size}";

                    break;
                }
                else
                {
                    requestValue =$"Ошибка..\n" +
                        $"Указанный размер {number} меньше нынешнего размера {store.Size} на {store.Size - number}";
                }
            } while (true);
            return requestValue;
            // Thread.Sleep(3000);
        }


        //Удаление витрины
        public void RemoveProductStore(List<ShowcaseModel> productStores)
        {
            ShowProductStores(productStores);
            Console.WriteLine("\nВыберите Id витрины для удаления");
            int number = int.Parse(Console.ReadLine());

            ShowcaseModel store = new ShowcaseModel();
            for (int i = 0; i < productStores.Count; i++)
            {
                if (number == productStores[i].Id)
                {
                    store = productStores[i];
                }
            }

            if (store != null)
            {
                productStores.Remove(store);
                Console.WriteLine($"Витрина - {store.Name}, успешно удалена!");
            }
            else
            {
                Console.WriteLine($"Ошибка..\n" +
                        $"Витрина [{store.Name}] не очищена и имеет в себе товары");

            }
            //Thread.Sleep(3000);
        }

        //Действия с витриной
        public void ActionWithProductStore(List<ShowcaseModel> productStores)
        {
            ShowProductStores(productStores);
            Console.WriteLine("\nВыберите Id витрины для для добавления товаров");
            int number = int.Parse(Console.ReadLine());

            ShowcaseModel store = new ShowcaseModel();
            for (int i = 0; i < productStores.Count; i++)
            {
                if (number == productStores[i].Id)
                {
                    store = productStores[i];
                }
            }
            Console.Clear();
            var responseValue = "";
            bool isContinue = true;
            while (isContinue)
            {
                Console.WriteLine($"[ID {store.Id}|NAME - {store.Name}|Size - {store.Size}| CreateTime - {store.CreationTime}]");

                Console.WriteLine("Выберите необходимую операцию\n" +
                    "\t[1] ----> Добавить товар на прилавок\n" +
                    "\t[2] ----> Удалить товар с прилавка\n" +
                    "\t[3] ----> Посмотреть содержимое прилавка\n" +
                    "\t[4] ----> Вернуться на главную\n");

                keyInfo = Console.ReadKey(true);
                Console.Clear();
                switch (keyInfo.KeyChar)
                {
                    case '1': //
                        store.AddProduct(store);
                        break;
                    case '2':
                        store.RemoveProduct(store);
                        break;
                    case '3':
                        var _productStores = productStores;
                        responseValue = JsonConvert.SerializeObject(_productStores, Formatting.Indented);
                        break;
                    case '4':
                        isContinue = false;
                        break;
                }
                Console.Clear();
            }
        }
    }
}
