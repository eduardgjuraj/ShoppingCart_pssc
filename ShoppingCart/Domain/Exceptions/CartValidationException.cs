namespace ShoppingCart.Domain.Exceptions
{
    public class CartValidationException : ShoppingCartException
    {
        public CartValidationException(string message) : base(message) { }
    }
}
