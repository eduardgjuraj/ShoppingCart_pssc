using System;
using System.Collections.Generic;
namespace ShoppingCart.Data.Models
{
    public class CartItemDto
    {
        public Guid Id { get; set; }
        public Guid ShoppingCartId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
    }
}
