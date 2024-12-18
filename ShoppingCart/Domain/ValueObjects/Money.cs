namespace ShoppingCart.Domain.ValueObjects
{
    public record Money(decimal Amount, string Currency)
    {
        public Money Add(Money other)
        {
            if (other.Currency != Currency)
                throw new InvalidOperationException("Cannot add amounts with different currencies.");

            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            if (other.Currency != Currency)
                throw new InvalidOperationException("Cannot subtract amounts with different currencies.");

            return new Money(Amount - other.Amount, Currency);
        }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}
