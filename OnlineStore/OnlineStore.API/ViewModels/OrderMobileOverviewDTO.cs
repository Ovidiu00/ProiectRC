using System;
using System.Collections.Generic;

namespace OnlineStore.API.ViewModels
{
    public class OrderMobileOverviewDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<ProductQuantity> Products { get; set; }
    }

    public class ProductQuantity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
