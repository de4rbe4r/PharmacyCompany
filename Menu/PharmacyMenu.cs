using PharmacyCompany.Models;
using PharmacyCompany.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany.Menu
{
    class PharmacyMenu
    {
        public static void Show()
        {
            bool isExit = false;
            char key;
            while (!isExit)
            {
                List<Pharmacy> pharmacies = DatabaseServices.GetPharmacies();

                Console.Clear();
                Console.WriteLine("    Список Аптек");
                Console.WriteLine(" -----------------------------------------------------------------------");
                Console.WriteLine("| Id\t| Название аптеки\t| Адрес \t| Номер телефона\t|");
                Console.WriteLine(" -----------------------------------------------------------------------");

                foreach (Pharmacy p in pharmacies)
                {
                    Console.WriteLine($"| " + p.Id + "\t| " + p.Title + "\t\t|" + p.Address + "\t| " + p.PhoneNumber + "\t|");
                }
                Console.WriteLine(" -----------------------------------------------------------------------");
                Console.WriteLine("\n1. Создать аптеку");
                Console.WriteLine("2. Удалить аптеку");
                Console.WriteLine("\n0. Выход в главное меню");

                key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (key)
                {
                    case '1':
                        Pharmacy pharmacy = AddPharmacy();
                        DatabaseServices.InsertPharmacy(pharmacy.Title, pharmacy.Address, pharmacy.PhoneNumber);
                        break;
                    case '2':
                        DatabaseServices.DeletePharmacy(Int32.Parse(DeletePharmacy()));
                        break;
                    case '0':
                        isExit = true;
                        break;
                }
            }
        }
        private static Pharmacy AddPharmacy()
        {
            Pharmacy pharmacy = new Pharmacy();
            Console.Clear();
            Console.Write("Введите название: ");
            pharmacy.Title = Console.ReadLine();
            Console.Write("Введите адрес: ");
            pharmacy.Address = Console.ReadLine();
            Console.Write("Введите телефон: ");
            pharmacy.PhoneNumber = Console.ReadLine();
            return pharmacy;
        }

        private static string DeletePharmacy()
        {
            Console.Write("Введите Id удаляемой аптеки: ");
            return Console.ReadLine();
        }
    }
}
