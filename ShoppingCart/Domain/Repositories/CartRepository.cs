using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace ShoppingCart.Domain.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly Dictionary<Guid, ActiveShoppingCart> _activeCarts = new Dictionary<Guid, ActiveShoppingCart>();

        public ActiveShoppingCart GetActiveCartById(Guid cartId)
        {
            return _activeCarts.TryGetValue(cartId, out var cart) ? cart : null;
        }

        public void UpdateCartToCheckedOut(CheckedOutShoppingCart checkedOutCart)
        {
            if (_activeCarts.ContainsKey(checkedOutCart.Id))
                _activeCarts.Remove(checkedOutCart.Id);

            // Log to simulate database persistence
            Console.WriteLine($"Cart {checkedOutCart.Id} has been checked out.");
        }

        // For testing purposes: Add an active cart
        public void AddActiveCart(ActiveShoppingCart cart)
        {
            _activeCarts[cart.Id] = cart;
        }
    }
}
