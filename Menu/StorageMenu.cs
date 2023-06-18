using PharmacyCompany.Models;
using PharmacyCompany.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany.Menu
{
    class StorageMenu
    {
        public static void Show()
        {
            bool isExit = false;
            char key;
            while (!isExit)
            {
                List<Storage> storages = DatabaseServices.GetStorages();

                Console.Clear();
                Console.WriteLine("\t\t\tСписок Складов");
                Console.WriteLine(" ------------------------------------------------------------------------");
                Console.WriteLine("| Id\t| Название склада\t| Название аптеки \t| Id аптеки \t|");
                Console.WriteLine(" ------------------------------------------------------------------------");

                foreach (Storage s in storages)
                {
                    Console.WriteLine($"| " + s.Id + "\t| " + s.Title + "\t\t|" + s.Pharmacy.Title + "\t\t|" + s.PharmacyId + "\t\t|");
                }
                Console.WriteLine(" ------------------------------------------------------------------------");
                Console.WriteLine("\n1. Создать склад");
                Console.WriteLine("2. Удалить склад");
                Console.WriteLine("\n0. Выход в главное меню");

                key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (key)
                {
                    case '1':
                        Storage storage = AddStorage();
                        DatabaseServices.InsertStorage(storage.Title, storage.PharmacyId);
                        break;
                    case '2':
                        DatabaseServices.DeleteStorage(Int32.Parse(DeleteStorage()));
                        break;
                    case '0':
                        isExit = true;
                        break;
                }
            }
        }
        private static Storage AddStorage()
        {
            Storage storage = new Storage();
            Console.Write("Введите название: ");
            storage.Title = Console.ReadLine();
            Console.Write("Введите Id аптеки, для которой добавляется склад: ");
            storage.PharmacyId = Int32.Parse(Console.ReadLine());
            return storage;
        }

        private static string DeleteStorage()
        {
            Console.Write("Введите Id удаляемого склада: ");
            return Console.ReadLine();
        }
    }
}
