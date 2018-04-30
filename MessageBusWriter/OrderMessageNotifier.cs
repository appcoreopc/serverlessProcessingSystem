using System.Net;

namespace MessagingService
{
    class OrderMessageNotifier
    {
        public bool SendNotification(string message)
        {
            var status = HttpUtil.Send(message);

            switch (status)
            {
                case HttpStatusCode.OK:
                    return true;
                default:
                    return false;
            }
        }
    }
}
