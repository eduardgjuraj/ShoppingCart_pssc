using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data.Models;

namespace ShoppingCart.Data.Repositories
{
    public class CheckedOutShoppingCartRepository
    {
        private readonly ShoppingCartDbContext _context;

        public CheckedOutShoppingCartRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CheckedOutShoppingCartDto>> GetAllAsync()
        {
            return await _context.CheckedOutShoppingCarts.ToListAsync();
        }

        public async Task<CheckedOutShoppingCartDto> GetByIdAsync(Guid id)
        {
            return await _context.CheckedOutShoppingCarts.FindAsync(id);
        }

        public async Task AddAsync(CheckedOutShoppingCartDto checkedOutCart)
        {
            await _context.CheckedOutShoppingCarts.AddAsync(checkedOutCart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var checkedOutCart = await GetByIdAsync(id);
            if (checkedOutCart != null)
            {
                _context.CheckedOutShoppingCarts.Remove(checkedOutCart);
                await _context.SaveChangesAsync();
            }
        }
    }
}
