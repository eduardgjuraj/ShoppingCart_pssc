using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data.Models;

namespace ShoppingCart.Data.Repositories
{
    public class ShoppingCartRepository
    {
        private readonly ShoppingCartDbContext _context;

        public ShoppingCartRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCartDto>> GetAllAsync()
        {
            return await _context.ShoppingCarts.ToListAsync();
        }

        public async Task<ShoppingCartDto> GetByIdAsync(Guid id)
        {
            return await _context.ShoppingCarts.FindAsync(id);
        }

        public async Task AddAsync(ShoppingCartDto shoppingCart)
        {
            await _context.ShoppingCarts.AddAsync(shoppingCart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShoppingCartDto shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var shoppingCart = await GetByIdAsync(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCarts.Remove(shoppingCart);
                await _context.SaveChangesAsync();
            }
        }
    }
}
