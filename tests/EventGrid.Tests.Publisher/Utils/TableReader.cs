using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace EventGrid.Tests.Publisher.Utils
{
    public class TableReader<T>
        where T : class, ITableEntity, new()
    {
        private readonly CloudStorageAccount _storageAccount;
        private const string TableName = "Events";
        private const string RowKey = "TestMessage"; // This is hard coded in the function that writes to the table

        public TableReader(string connectionString)
        {
            _storageAccount = CreateStorageAccountFromConnectionString(connectionString);
        }

        public async Task<T> ReadDataAsync(string id)
        {
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            CloudTable table = tableClient.GetTableReference(TableName);
            var operation = TableOperation.Retrieve<T>(id, RowKey);
            var result = await table.ExecuteAsync(operation);
            return result.Result as T;
        }

        private CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }
    }
}