using PharmacyCompany.Models;
using PharmacyCompany.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany.Menu
{
    class BatchMenu
    {
        public static void Show()
        {
            bool isExit = false;
            char key;
            while (!isExit)
            {
                List<Batch> batches = DatabaseServices.GetBatches();

                Console.Clear();
                Console.WriteLine("\t\t\tСписок Партий");
                Console.WriteLine(" -------------------------------------------------------------------------------------------------------");
                Console.WriteLine("| Id\t| Название товара \t| Id товара \t| Название склада \t| Id склада \t| Количество\t|");
                Console.WriteLine(" -------------------------------------------------------------------------------------------------------");

                foreach (Batch b in batches)
                {
                    Console.WriteLine($"| " + b.Id + "\t| " + b.Product.Title + "\t\t| " + b.ProductId + "\t\t| " + b.Storage.Title + "\t\t| " + b.StorageId + "\t\t| " + b.Quantity + "\t\t|");
                }
                Console.WriteLine(" -------------------------------------------------------------------------------------------------------");
                Console.WriteLine("\n1. Создать партию");
                Console.WriteLine("2. Удалить партию");
                Console.WriteLine("\n0. Выход в главное меню");

                key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (key)
                {
                    case '1':
                        Batch batch = AddBatch();
                        DatabaseServices.InsertBatch(batch.ProductId, batch.StorageId, batch.Quantity);
                        break;
                    case '2':
                        DatabaseServices.DeleteBatch(Int32.Parse(DeleteBatch()));
                        break;
                    case '0':
                        isExit = true;
                        break;
                }
            }
        }
        private static Batch AddBatch()
        {
            Batch batch = new Batch();
            Console.Write("Введите Id товара, для которого добавляется партия: ");
            batch.ProductId = Int32.Parse(Console.ReadLine());
            Console.Write("Введите Id склада, для которого добавляется партия: ");
            batch.StorageId = Int32.Parse(Console.ReadLine());
            Console.Write("Введите количесто товара: ");
            batch.Quantity = Int32.Parse(Console.ReadLine());
            return batch;
        }

        private static string DeleteBatch()
        {
            Console.Write("Введите Id удаляемой партии: ");
            return Console.ReadLine();
        }
    }
}
