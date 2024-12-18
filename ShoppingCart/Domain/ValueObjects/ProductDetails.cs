namespace ShoppingCart.Domain.ValueObjects
{
    public record ProductDetails(string Name, string Description, Money Price)
    {
        public override string ToString()
        {
            return $"{Name} - {Description} ({Price})";
        }
    }
}
