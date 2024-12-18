using System;
using System.Collections.Generic;
namespace ShoppingCart.Data.Models
{
    public class CheckedOutShoppingCartDto
    {
        public Guid Id { get; set; } 
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CheckedOutDate { get; set; }
        public string ShippingAddress { get; set; }
    }
}