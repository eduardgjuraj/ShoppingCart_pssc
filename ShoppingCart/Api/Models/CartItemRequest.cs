using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Models
{
    public class CartItemRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
    }

}
