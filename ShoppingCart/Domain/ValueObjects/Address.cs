namespace ShoppingCart.Domain.ValueObjects
{
    public record Address(string Street, string City, string PostalCode, string Country)
    {
        public override string ToString()
        {
            return $"{Street}, {City}, {PostalCode}, {Country}";
        }
    }
}
