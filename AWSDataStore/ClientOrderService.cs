using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using ServiceModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWSDataStore
{
    public class ClientOrderService
    {
        private AmazonDynamoDBClient _client;
        private const string OrderTableName = "clientOrders";
        private const string OrderIdKey = "orderId";
        private const string ValueIdKey = "value";
        private const string OrderDetail = "orderDetail";
        private const string KeySeparator = "_";

        private const string AttributeTypeS = "S";
        private ILambdaLogger _logger;
        
        public ClientOrderService(ILambdaLogger logger)
        {
            _logger = logger;
            var config = new AmazonDynamoDBConfig();
            config.RegionEndpoint = RegionEndpoint.APSoutheast2;
            
            //config.ServiceURL = "http://localhost:8000";

            _client = new AmazonDynamoDBClient(config);

        }

        public async ValueTask<DescribeTableResponse> DescribeTable(string tableName)
        {
            var response = await _client.DescribeTableAsync(tableName);
            return response;
        }

        public async ValueTask<CreateTableResponse> CreateTable(string tableName)
        {

            var response = await _client.CreateTableAsync(new CreateTableRequest()
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                  new AttributeDefinition
                    {
                      AttributeName = OrderIdKey,
                      AttributeType = AttributeTypeS
                    }
                },
                KeySchema = new List<KeySchemaElement>()
                {
                new KeySchemaElement
                {
                  AttributeName = OrderIdKey,
                  KeyType = "HASH"  //Partition key
                }
              },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                }
            });

            return response;
        }

        public Table GetTable(string tableName)
        {
            Table targetTable = Table.LoadTable(_client, tableName);
            return targetTable;
        }
        

        public async Task<Document> CreateOrder(string orderStr)
        {
            try
            {
                var config = new AmazonDynamoDBConfig();
                config.RegionEndpoint = RegionEndpoint.APSoutheast2;
                _client = new AmazonDynamoDBClient(config);

                _logger.Log("Orderv7" + orderStr);

                var orderModel = JsonConvert.DeserializeObject<OrderModel>(orderStr);

                if (orderModel != null)
                {
                    //// Construct the key //
                    var key = orderModel.OrderId + KeySeparator + orderModel.Email;

                    _logger.Log("Key " + key);

                    Document document = new Document();
                    document[OrderIdKey] = key;
                    document[ValueIdKey] = orderStr;

                    var table = Table.LoadTable(_client, OrderTableName);
                    var doc = table.PutItemAsync(document);
                    return doc.Result;
                }
                
            }
            catch (Exception ex)
            {
                _logger.Log("Eror: " + ex.Message);
                _logger.Log("Stack Trace: " + ex.StackTrace);
                throw;
            }

            return null;
        }


        public async Task<GetItemResponse> GetKey(string order)
        {

            var orderModel = JsonConvert.DeserializeObject<OrderModel>(order);
            var store = new DynamoDataStore(_client);

            //// Construct the key //
            var key = orderModel.OrderId + "_" + orderModel.Email;

            try
            {
                var request = new GetItemRequest
                {
                    TableName = OrderTableName,
                    Key = new Dictionary<string, AttributeValue>() { { OrderIdKey, new AttributeValue { S = key } } },
                };

                return await _client.GetItemAsync(request);
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }

    }
}
