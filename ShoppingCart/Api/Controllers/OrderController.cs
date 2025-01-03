using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.ValueObjects;
using ShoppingCart.Domain.Workflows;
using System;

namespace ShoppingCart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderPlacedWorkflow _orderWorkflow;

        public OrderController(OrderPlacedWorkflow orderWorkflow)
        {
            _orderWorkflow = orderWorkflow;
        }

        [HttpPost("{cartId}/place-order")]
        public IActionResult PlaceOrder(Guid cartId, [FromBody] PlaceOrderRequest request)
        {
            try
            {
                // Create Address from the request
                var shippingAddress = new Address(
                    request.Street,
                    request.City,
                    request.PostalCode,
                    request.Country
                );

                // Call the workflow
                var checkedOutCart = _orderWorkflow.PlaceOrder(cartId, shippingAddress);

                return Ok(new
                {
                    Message = "Order placed successfully.",
                    OrderId = checkedOutCart.Id,
                    TotalAmount = checkedOutCart.TotalAmount.ToString(),
                    CheckedOutDate = checkedOutCart.CheckedOutDate,
                    ShippingAddress = checkedOutCart.ShippingAddress.ToString()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    // DTO for the API request
    public class PlaceOrderRequest
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
