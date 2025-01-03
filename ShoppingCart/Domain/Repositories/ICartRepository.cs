using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Repositories
{
    public interface ICartRepository
    {
        ActiveShoppingCart GetActiveCartById(Guid cartId);
        void UpdateCartToCheckedOut(CheckedOutShoppingCart checkedOutCart);
    }
}
