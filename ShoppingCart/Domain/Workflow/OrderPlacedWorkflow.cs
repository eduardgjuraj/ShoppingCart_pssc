﻿using System;
using System.Linq;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Operations;
using ShoppingCart.Domain.Repositories;
using ShoppingCart.Domain.ValueObjects;

namespace ShoppingCart.Domain.Workflows
{
    public class OrderPlacedWorkflow
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderPublisher _orderPublisher;

        public OrderPlacedWorkflow(ICartRepository cartRepository, IOrderPublisher orderPublisher)
        {
            _cartRepository = cartRepository;
            _orderPublisher = orderPublisher;
        }

        public CheckedOutShoppingCart PlaceOrder(Guid cartId, Address shippingAddress)
        {
            // Retrieve the active shopping cart
            var activeCart = _cartRepository.GetActiveCartById(cartId);

            if (activeCart == null)
                throw new InvalidOperationException($"Shopping cart with ID {cartId} does not exist or is not active.");

            // Validate the shopping cart
            var validatedCart = ValidateShoppingCart(activeCart);

            // Check out the shopping cart
            var checkedOutCart = CheckoutShoppingCart(validatedCart, shippingAddress);

            // Publish the OrderPlacedEvent
            PublishOrderPlacedEvent(checkedOutCart);

            // Persist the changes
            _cartRepository.UpdateCartToCheckedOut(checkedOutCart);

            return checkedOutCart;
        }

        private ValidatedShoppingCart ValidateShoppingCart(ActiveShoppingCart activeCart)
        {
            if (!activeCart.Items.Any())
                throw new InvalidOperationException("Cannot place an order with an empty cart.");

            var totalAmount = activeCart.Items
                .Select(item => item.TotalPrice.Amount)
                .Sum();

            return new ValidatedShoppingCart(
                activeCart.Id,
                activeCart.UserId,
                activeCart.Items,
                new Money(totalAmount, activeCart.Items.First().Details.Price.Currency)
            );
        }

        private CheckedOutShoppingCart CheckoutShoppingCart(ValidatedShoppingCart validatedCart, Address shippingAddress)
        {
            return new CheckedOutShoppingCart(
                validatedCart.Id,
                validatedCart.UserId,
                validatedCart.Items,
                validatedCart.TotalAmount,
                DateTime.UtcNow,
                shippingAddress
            );
        }

        private void PublishOrderPlacedEvent(CheckedOutShoppingCart checkedOutCart)
        {
            var orderPlacedEvent = new OrderPlacedEvent(
                checkedOutCart.Id,
                checkedOutCart.UserId,
                checkedOutCart.Items.Select(item => new OrderPlacedEvent.OrderItem(
                    item.ProductId,
                    item.Quantity,
                    item.TotalPrice
                )).ToList(),
                checkedOutCart.TotalAmount,
                checkedOutCart.CheckedOutDate,
                checkedOutCart.ShippingAddress
            );

            _orderPublisher.Publish(orderPlacedEvent);
        }
    }
}
