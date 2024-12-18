using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data.Models;

namespace ShoppingCart.Data.Repositories
{
    public class CartItemRepository
    {
        private readonly ShoppingCartDbContext _context;

        public CartItemRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItemDto>> GetAllAsync()
        {
            return await _context.CartItems.ToListAsync();
        }

        public async Task<IEnumerable<CartItemDto>> GetByShoppingCartIdAsync(Guid shoppingCartId)
        {
            return await _context.CartItems
                .Where(ci => ci.ShoppingCartId == shoppingCartId)
                .ToListAsync();
        }

        public async Task<CartItemDto> GetByIdAsync(Guid id)
        {
            return await _context.CartItems.FindAsync(id);
        }

        public async Task AddAsync(CartItemDto cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CartItemDto cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var cartItem = await GetByIdAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
