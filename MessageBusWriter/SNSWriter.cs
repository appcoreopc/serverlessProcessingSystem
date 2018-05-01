using Newtonsoft.Json;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace MessagingService
{
    
    public class SNSWriter : IOrderMessageWriter
    {
        const string OrderArn = "arn:aws:sns:ap-southeast-2:998081349423:customerOrder"; 

        public PublishResponse WriteMessage<T>(T targetype)
        {
            try
            {
                var message = JsonConvert.SerializeObject(targetype);

                if (message != null)
                {

                    var snsClient = new AmazonSimpleNotificationServiceClient();

                    var response = snsClient.PublishAsync(new PublishRequest()
                    {
                        TopicArn = OrderArn,
                        Message = message
                    });
                    
                    return response.Result;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return null;
        }
    }
    
    interface IOrderMessageWriter
    {
        PublishResponse WriteMessage<T>(T message);
    }


}
