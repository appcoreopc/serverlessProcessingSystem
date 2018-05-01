using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System.Threading.Tasks;

namespace AWSDataStore
{
    public class DynamoDataStore
    {
        private AmazonDynamoDBClient _client;

        public DynamoDataStore(AmazonDynamoDBClient client)
        {   
            _client = client;
        }
        
        public Table GetTable(string tableName)
        {
            Table targetTable = Table.LoadTable(_client, tableName);
            return targetTable;
        }

        public async ValueTask<bool> Create(string tableName, Document doc)
        {
            var targetTable = GetTable(tableName);
            var result = await targetTable.PutItemAsync(doc);
            return true;
        }

        public async ValueTask<bool> Update(string tableName, Document doc)
        {
            var targetTable = GetTable(tableName);
            await targetTable.UpdateItemAsync(doc);
            return true;
        }

        public async ValueTask<bool> Delete(string tableName, Document doc)
        {
            var targetTable = GetTable(tableName);
            targetTable.DeleteItemAsync(doc);
            return true;
        }
    }
}
