using Amazon.Lambda.SNSEvents;
using Amazon.Lambda.Core;
using System;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace NotificationLambdaEndpoint
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
            context.Logger.Log("We have a message");
            

            foreach (var item in evnt?.Records)
            {
                Console.WriteLine("C# console logger");
                context.Logger.Log(item.Sns.Message);
                Console.WriteLine(item.Sns.Message);
            }

            context.Logger.Log("End message");
            return string.Empty;
        }
    }
}

