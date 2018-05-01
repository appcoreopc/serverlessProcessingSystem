using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using System;
using AWSDataStore;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace OrderToStoreLambdaEndpoint
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(SNSEvent evnt, ILambdaContext context)
        {
            var order = new ClientOrderService(context.Logger);
            
            foreach (var item in evnt?.Records)
            {
                context.Logger.Log($"details {item.Sns.Timestamp}");
                context.Logger.Log($"sns content {item.Sns.Message}");

                var result = order.CreateOrder(item.Sns.Message);
                Console.WriteLine($"Status of order v2 : {result.Status}");
                
                Console.WriteLine(item.Sns.Message);
            }
            return string.Empty;
        }
    }
}
