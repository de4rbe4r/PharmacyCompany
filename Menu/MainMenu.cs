using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PharmacyCompany
{
    public static class MainMenu
    {
        public static void Show()
        {
            Console.WriteLine("\t\tГлавное меню");
            Console.WriteLine("\n\t1. Товарные наименования");
            Console.WriteLine("\t2. Аптеки");
            Console.WriteLine("\t3. Склады");
            Console.WriteLine("\t4. Партии");
            Console.WriteLine("\n\t0. Выход");
        }
    }
}
