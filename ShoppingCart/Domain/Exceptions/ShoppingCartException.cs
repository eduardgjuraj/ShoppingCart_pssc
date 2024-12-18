namespace ShoppingCart.Domain.Exceptions
{
    public class ShoppingCartException : Exception
    {
        public ShoppingCartException(string message) : base(message) { }
    }
}
