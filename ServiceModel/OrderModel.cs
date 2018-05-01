using System;
using System.Collections.Generic;

namespace ServiceModel
{
    public class OrderModel
    {

        public int? OrderId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }
            
        public string Email { get; set; }
        
        public DateTime? Created { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int? ItemId { get; set; }
        
        public decimal OrderPrice { get; set; }
        
    }
}
