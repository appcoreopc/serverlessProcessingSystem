using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using MessagingService;


using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace FormLambdaEndpoint
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest evnt, ILambdaContext context)
        {
            var body = evnt.Body;
            //var bodyCount = evnt.Body.Count();
            
            context.Logger.Log(evnt.Resource);
            context.Logger.Log($"v2 input. Suspected name is {body}");
            
            var writer = new SNSWriter();
            var result = writer.WriteMessage(evnt.Body);

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = result.HttpStatusCode.ToString()
            };
        }
    }
}
