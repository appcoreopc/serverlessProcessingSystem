using System.Net;

namespace MessagingService
{
    class VendorOrder
    {

        public bool SendOrder(string jsonOrder)
        {
            var status = HttpUtil.Send(jsonOrder);

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
