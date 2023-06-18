using PharmacyCompany.Models;
using PharmacyCompany.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PharmacyCompany.Menu
{
    public class ProductMenu
    {
        public static void Show()
        {
            bool isExit = false;
            char key;
            while (!isExit)
            {
                List<Product> products = DatabaseServices.GetProducts();

                Console.Clear();
                Console.WriteLine("    Список товарных наименований");
                Console.WriteLine(" --------------------------------");
                Console.WriteLine("| Id\t| Наименование товара\t|");
                Console.WriteLine(" --------------------------------");
                foreach (Product p in products)
                {
                    Console.WriteLine($"| " + p.Id + "\t| " + p.Title + "\t\t|");
                }
                Console.WriteLine("--------------------------------");
                Console.WriteLine("\n1. Создать товар");
                Console.WriteLine("2. Удалить товар");
                Console.WriteLine("\n0. Выход в главное меню");

                key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (key)
                {
                    case '1':
                        DatabaseServices.InsertProduct(AddProduct());
                        break;
                    case '2':
                        DatabaseServices.DeleteProduct(Int32.Parse(DeleteProduct()));
                        break;
                    case '0':
                        isExit = true;
                        break;
                }
            }
        }
        private static string AddProduct()
        {
            Console.Clear();
            Console.Write("Введите название товарного наименования: ");
            return Console.ReadLine();
        }

        private static string DeleteProduct()
        {
            Console.Write("Введите Id удаляемого товарного наименования: ");
            return Console.ReadLine();
        }
    }
}
