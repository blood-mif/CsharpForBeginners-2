using System;
using System.Collections.Generic;

namespace ShopModels
{
    public class ShowcaseModel 
    {
    
        public uint Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime RemovalTime { get; set; }
        public ShowcaseModel() { }
        public ShowcaseModel(string name, int size, uint id)
        {
            Id = id;
            Name = name;
            Size = size;
            CreationTime = DateTime.Now;
        }

        public List<Product> Products = new List<Product>();

        public void AddProduct(ShowcaseModel showCase)
        {
            int counterId = 0;
            string name;
            int prodSize;
            decimal price;

            do
            {
                Console.WriteLine("Укажите имя товара:");
                name = Console.ReadLine();

                //Проверка на дурака
                if (CheckProductName(name) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("\t\tОШИБКА!!!\n" +
                        "1. Введенное название витрины должно быть не меньше 4 и не больше 24 символов\n" +
                        "2. Запрещенные символы 1 2 3 4 5 6 7 8 9 0 / * - +\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    break;
                }
            } while (true);

            Console.Clear();

            do
            {
                Console.WriteLine("Укажите размер товара:");
                string size = Console.ReadLine();

                if (CheckCorrectInput(size) == true)
                {
                    int sizeProduct = Convert.ToInt32(size);

                    if (showCase.Size > sizeProduct)
                    {
                        showCase.Size -= sizeProduct;
                        prodSize = sizeProduct;
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Недостаточно место для этого товара!\n" +
                            $"Текущий размер ветрины равен {showCase.Size}\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Размер введена не корректно!\n" +
                        $"Убедитесь, что в указанном размере присуствуют только цифры\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

            } while (true);

            Console.Clear();

            do
            {
                Console.WriteLine("Укажите цену товара:");
                string priceProduct = Console.ReadLine(); ;

                //Проверка введенной цены 
                if (CheckCorrectInput(priceProduct) == true)
                {
                    price = Convert.ToDecimal(priceProduct);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Цена введена не корректно!\n" +
                        $"Убедитесь, что в указанной цене присуствуют только цифры\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

            } while (true);

            Product product = new Product(counterId, name, prodSize, price);
            Products.Add(product);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Товар: {product.Name}, успешо добавлен на прилавок [{showCase.Name}]");
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        public void RemoveProduct(ShowcaseModel showCase)
        {
            ShowProducts(showCase);
            Console.WriteLine("\nВыберите Id товара для удаления");
            int number = int.Parse(Console.ReadLine());

            Product product = new Product();
            for (int i = 0; i < Products.Count; i++)
            {
                if (number == Products[i].Id)
                {
                    product = Products[i];
                }
            }
            Products.Remove(product);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Продукс: {product.Name} успешно удален из витрины {showCase.Name}");
            Console.ForegroundColor = ConsoleColor.Gray;

            showCase.Size += product.OccupiedSize;
        }

        public void ShowProducts(ShowcaseModel showCase)
        {
            int counter = 1;
            Console.WriteLine($"Список ассортимента витрины [{showCase.Name}]\n" +
                $"{new string('-', 30)}");
            if (showCase.Products.Count != 0)
            {
                foreach (Product prod in Products)
                {
                    Console.WriteLine($"{counter}.[ID {prod.Id}|NAME - {prod.Name}|Size - {prod.OccupiedSize}| Price - {prod.Price:C}]");
                    counter++;
                }
            }
            else
            {
                Console.WriteLine("Данная в данный момент витрина пуста и нуждаеться в наполнении...");
            }
            Console.WriteLine("Для продолжения тисни что-нибудь");
            Console.ReadKey();

        }

        public bool CheckProductName(string productName)
        {
            char[] forbiddeSymbols = new char[] { '1','2','3','4','5','6','7','8','9','0','/','*','-',
            '+'};
            if (productName.Length < 4 || productName.Length > 24)
            {
                return false;
            }

            foreach (char symb in forbiddeSymbols)
            {
                foreach (char symbProd in productName)
                {
                    if (symbProd == symb)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool CheckCorrectInput(string price)
        {
            string list = @"qwertyuiop[]asdfghjkl;'\zxcvbnm,./=-)(*&^%$#@!йцукенгшщзхъфывапролджэ\ячсмитьбю";
            string priceString = price.ToString();
            for (int i = 0; i < priceString.Length; i++)
            {
                if (list.Contains(priceString[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
