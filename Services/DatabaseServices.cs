using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PharmacyCompany.Models;

namespace PharmacyCompany.Services
{
    public class DatabaseServices
    {
        static SqlConnection connectionToMaster = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        static SqlConnection connectionToDb = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Pharmacy;Integrated Security=True;");
        private DatabaseServices() { }
        private static void ExecuteQuery(string query, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                Thread.Sleep(3000);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public static List<Product> GetProducts(int id = 0)
        {
            string query;
            if (id == 0) query = "SELECT * FROM dbo.Products";
            else query = "SELECT * FROM dbo.Products WHERE Id = " + id;
            List<Product> products = new List<Product>();

            SqlCommand command = new SqlCommand(query, connectionToDb);
            try
            {
                connectionToDb.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product tempProduct = new Product();
                    tempProduct.Id = reader.GetInt32(0);
                    tempProduct.Title = reader.GetString(1);
                    products.Add(tempProduct);
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Ошибка :" + ex.Message);
                Thread.Sleep(3000);
            }
            finally
            {
                if (connectionToDb.State == System.Data.ConnectionState.Open)
                {
                    connectionToDb.Close();
                }
            }
            return products;
        }
        public static List<Pharmacy> GetPharmacies(int id = 0)
        {
            string query;
            if (id == 0) query = "SELECT * FROM dbo.Pharmacies";
            else query = "SELECT * FROM dbo.Pharmacies WHERE Id = " + id;
            List<Pharmacy> pharmacies = new List<Pharmacy>();

            SqlCommand command = new SqlCommand(query, connectionToDb);
            try
            {
                connectionToDb.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pharmacy tempPharmacy = new Pharmacy();
                    tempPharmacy.Id = reader.GetInt32(0);
                    tempPharmacy.Title = reader.GetString(1);
                    tempPharmacy.Address = reader.GetString(2);
                    tempPharmacy.PhoneNumber = reader.GetString(3);
                    pharmacies.Add(tempPharmacy);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка :" + ex.Message);
                Thread.Sleep(3000);
            }
            finally
            {
                if (connectionToDb.State == System.Data.ConnectionState.Open)
                {
                    connectionToDb.Close();
                }
            }
            return pharmacies;
        }
        public static List<Storage> GetStorages(int id = 0, int pharmacyId = 0)
        {
            string query;
            if (id != 0) query = "SELECT * FROM dbo.Storages WHERE id = " + id;
            else if (pharmacyId != 0) query = "SELECT * FROM dbo.Storages WHERE PharmacyId = " + pharmacyId;
            else query = "SELECT * FROM dbo.Storages";
            List<Storage> storages = new List<Storage>();

            SqlCommand command = new SqlCommand(query, connectionToDb);
            try
            {
                connectionToDb.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Storage tempStorage = new Storage();
                    tempStorage.Id = reader.GetInt32(0);
                    tempStorage.Title = reader.GetString(1);
                    tempStorage.PharmacyId = reader.GetInt32(2);
                    storages.Add(tempStorage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка :" + ex.Message);
                Thread.Sleep(3000);
            }
            finally
            {
                if (connectionToDb.State == System.Data.ConnectionState.Open)
                {
                    connectionToDb.Close();
                }
            }

            foreach (Storage s in storages)
            {
                s.Pharmacy = GetPharmacies(s.PharmacyId)[0];
            }

            return storages;
        }
        public static List<Batch> GetBatches(int id = 0)
        {
            string query;
            if (id == 0) query = "SELECT * FROM dbo.Batches";
            else query = "SELECT * FROM dbo.Batches WHERE id = " + id;
            List<Batch> batches = new List<Batch>();

            SqlCommand command = new SqlCommand(query, connectionToDb);
            try
            {
                connectionToDb.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Batch tempBatch = new Batch();
                    tempBatch.Id = reader.GetInt32(0);
                    tempBatch.ProductId = reader.GetInt32(1);
                    tempBatch.StorageId = reader.GetInt32(2);
                    tempBatch.Quantity = reader.GetInt32(3);
                    batches.Add(tempBatch);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка :" + ex.Message);
                Thread.Sleep(3000);
            }
            finally
            {
                if (connectionToDb.State == System.Data.ConnectionState.Open)
                {
                    connectionToDb.Close();
                }
            }

            foreach (Batch b in batches)
            {
                b.Storage = GetStorages(b.StorageId)[0];
                b.Product = GetProducts(b.ProductId)[0];
            }

            return batches;
        }
        public static void CreateDatabase()
        {
            ExecuteQuery("CREATE DATABASE Pharmacy", connectionToMaster);
        }
        public static void CreateTables()
        {
            string query = "CREATE TABLE dbo.Products (Id INT PRIMARY KEY IDENTITY, Title NVARCHAR(100) );" +
                           "CREATE TABLE dbo.Pharmacies (Id INT PRIMARY KEY IDENTITY, Title NVARCHAR(100), Address NVARCHAR(100), PhoneNumber NVARCHAR(100) );" +
                           "CREATE TABLE dbo.Storages (Id INT PRIMARY KEY IDENTITY, Title NVARCHAR(100), PharmacyId INT FOREIGN KEY REFERENCES dbo.Pharmacies (id) );" +
                           "CREATE TABLE dbo.Batches (Id INT PRIMARY KEY IDENTITY, ProductId INT FOREIGN KEY REFERENCES dbo.Products (id), StorageId INT FOREIGN KEY REFERENCES dbo.Storages (id), Quantity INT );"; ;
            ExecuteQuery(query, connectionToDb);
        }
        public static void InsertDefaulValues()
        {
            string queryIsnertProducts = "INSERT INTO dbo.Products (Title) VALUES " +
                                      "(N'Товар 1'), (N'Товар 2'), (N'Товар 3'), (N'Товар 4'), (N'Товар 5')";
            string queryIsnertPharmacies = "INSERT INTO dbo.Pharmacies (Title, Address, PhoneNumber) VALUES " +
                                       "(N'Аптека 1', N'Город 1', N'8-846-777-55-44'), " +
                                       "(N'Аптека 2', N'Город 2', N'8-846-777-55-43'), " +
                                       "(N'Аптека 3', N'Город 3', N'8-846-777-55-42'), " +
                                       "(N'Аптека 4', N'Город 4', N'8-846-777-55-42'), " +
                                       "(N'Аптека 5', N'Город 5', N'8-846-777-55-41'); ";
            string queryIsnertStorages = "INSERT INTO dbo.Storages (Title, PharmacyId) VALUES " +
                                      "(N'Склад 1', 1), " +
                                      "(N'Склад 2', 2), " +
                                      "(N'Склад 3', 3), " +
                                      "(N'Склад 4', 4), " +
                                      "(N'Склад 5', 5), " +
                                      "(N'Склад 6', 1), " +
                                      "(N'Склад 7', 2), " +
                                      "(N'Склад 8', 3), " +
                                      "(N'Склад 9', 4), " +
                                      "(N'Склад 10', 5) ; ";
            string queryIsnertBatches = "INSERT INTO dbo.Batches (ProductId, StorageId, Quantity) VALUES " +
                                         "(1, 1, 10), (1, 2, 20), (1, 3, 30), (1, 4, 40), (1, 5, 50), " +
                                         "(2, 1, 10), (2, 2, 20), (2, 3, 30), (2, 4, 40), (2, 5, 50), " +
                                         "(3, 1, 10), (3, 2, 20), (3, 3, 30), (3, 4, 40), (3, 5, 50), " +
                                         "(4, 1, 10), (4, 2, 20), (4, 3, 30), (4, 4, 40), (4, 5, 50), " +
                                         "(5, 1, 10), (5, 2, 20), (5, 3, 30), (5, 4, 40), (5, 5, 50) ;";
            string query = queryIsnertProducts + queryIsnertPharmacies + queryIsnertStorages + queryIsnertBatches;
            ExecuteQuery(query, connectionToDb);
        }
        public static void InsertProduct(string title)
        {
            string query = "INSERT INTO dbo.Products (Title) VALUES (N'" + title + "');";
            ExecuteQuery(query, connectionToDb);
        }
        public static void InsertPharmacy(string title, string address, string phoneNumber)
        {
            string query = "INSERT INTO dbo.Pharmacies (Title, Address, PhoneNumber) VALUES (N'" + title + "', N'" + address +"', N'" + phoneNumber + "');";
            ExecuteQuery(query, connectionToDb);
        }
        public static void InsertStorage(string title, int pharmacyId)
        {
            string query = "INSERT INTO dbo.Storages (Title, PharmacyId) VALUES (N'" + title + "', " + pharmacyId + ");";
            ExecuteQuery(query, connectionToDb);
        }
        public static void InsertBatch(int productId, int storageId, int quantity)
        {
            string query = "INSERT INTO dbo.Batches (ProductId, StorageId, Quantity) VALUES (" + productId + ", " + storageId + ", " + quantity + ");";
            ExecuteQuery(query, connectionToDb);
        }
        public static void DeleteBatch(int id)
        {
            string query = "DELETE FROM dbo.Batches WHERE Id = " + id + ";";
            ExecuteQuery(query, connectionToDb);
        }
        public static void DeleteBatchByProductId(int id)
        {
            string query = "DELETE FROM dbo.Batches WHERE ProductId = " + id + ";";
            ExecuteQuery(query, connectionToDb);
        }
        public static void DeleteBatchByStorageId(int id)
        {
            string query = "DELETE FROM dbo.Batches WHERE StorageId = " + id + ";";
            ExecuteQuery(query, connectionToDb);
        }
        public static void DeleteProduct(int id)
        {
            DeleteBatchByProductId(id);
            string query = "DELETE FROM dbo.Products WHERE Id = " + id + ";";
            ExecuteQuery(query, connectionToDb);
        }
        public static void DeleteStorage(int id)
        {
            DeleteBatchByStorageId(id);
            string query = "DELETE FROM dbo.Storages WHERE Id = " + id + ";";
            ExecuteQuery(query, connectionToDb);
        }
        public static void DeleteStorageByPharmacyId(int id)
        {
            string query = "DELETE FROM dbo.Storages WHERE PharmacyId = " + id + ";";
            ExecuteQuery(query, connectionToDb);
        }
        public static void DeletePharmacy(int id)
        {
            List<Storage> storages = GetStorages(0,id);
            foreach (Storage storage in storages) DeleteBatchByStorageId(storage.Id);
            DeleteStorageByPharmacyId(id);
            string query = "DELETE FROM dbo.Pharmacies WHERE Id = " + id + ";";
            ExecuteQuery(query, connectionToDb);
        }
    }
}
