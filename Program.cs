using PharmacyCompany.Menu;
using PharmacyCompany.Services;
using System;

namespace PharmacyCompany
{
    class Program
    {
        static void Main(string[] args)
        {
            // Раскомментировать при первом запуске приложения
            //DatabaseServices.CreateDatabase();
            //DatabaseServices.CreateTables();
            //DatabaseServices.InsertDefaulValues();

            bool isExit = false;
            char key;
            while (!isExit)
            {
                Console.Clear();
                MainMenu.Show();
                key = Console.ReadKey().KeyChar;
                switch(key)
                {
                    case '1':
                        ProductMenu.Show();
                        break;
                    case '2':
                        PharmacyMenu.Show();
                        break;
                    case '3':
                        StorageMenu.Show();
                        break;
                    case '4':
                        BatchMenu.Show();
                        break;
                    case '0':
                        isExit = true;
                        break;
                }
            }
        }


    }
}
