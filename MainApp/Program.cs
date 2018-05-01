using AWSDataStore;
using Newtonsoft.Json;
using ServiceModel;
using System;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var order = new ClientOrderService();

            ////var tableName = "clientOrders";
            ////var response = order.CreateTable(tableName);
            ////Console.WriteLine(response.Result.HttpStatusCode);
            ////var result = order.DescribeTable(tableName);

            //var model = new OrderModel()
            //{
            //    OrderId = 99999,
            //    Name = "Test",
            //    Email = "kepung@gmail.com"
            //};

            //var strOrder = JsonConvert.SerializeObject(model);
            ////Console.WriteLine(result.Result.HttpStatusCode);
            ////var createOrderRes = order.CreateOrder(strOrder);
            ////Console.WriteLine(createOrderRes.Status);

            //Console.WriteLine("Getting order");
            //var res = order.GetKey(strOrder);
            //var r = res.Result.Item["OrderId"];
            //var s = res.Result.Item["Value"];

            //Console.WriteLine(r.S);

            //Console.WriteLine(s.S);
            //Console.WriteLine(res.Status);
            
            var x = "{\"OrderId\": 1221, \"Email\":\"momok@gmail.com\",  \"Name\":\"Jeremy Woo\"  }";

            var y = JsonConvert.DeserializeObject<OrderModel>(x);
            
        }
    }
}
