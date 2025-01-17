using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.ValueObjects;
using ShoppingCart.Domain.Workflows;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
                if (request == null)
                {
                    return BadRequest("Shipping address is required.");
                }

                // Create the Address object from the request body
                var shippingAddress = new Address(
                    request.Street,
                    request.City,
                    request.PostalCode,
                    request.Country
                );

                // Call the OrderPlacedWorkflow
                var checkedOutCart = _orderWorkflow.PlaceOrder(cartId, shippingAddress);

                // Return success response with the checked-out cart details
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

    public class PlaceOrderRequest
    {
        [Required, MaxLength(255)]
        public string Street { get; set; }

        [Required, MaxLength(100)]
        public string City { get; set; }

        [Required, MaxLength(20)]
        public string PostalCode { get; set; }

        [Required, MaxLength(100)]
        public string Country { get; set; }
    }
}
