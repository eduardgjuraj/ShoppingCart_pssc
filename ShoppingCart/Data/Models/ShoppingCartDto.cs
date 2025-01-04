using System;
using System.Collections.Generic;
namespace ShoppingCart.Data.Models
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}