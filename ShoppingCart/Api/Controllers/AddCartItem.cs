using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Data.Models;
using ShoppingCart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController : ControllerBase
    {
        private readonly ShoppingCartDbContext _context;

        public CartItemController(ShoppingCartDbContext context)
        {
            _context = context;
        }

        [HttpPost("{shoppingCartId}")]
        public async Task<IActionResult> AddCartItem(Guid shoppingCartId, [FromBody] CartItemRequest request)
        {
            var shoppingCartExists = await _context.ShoppingCarts.AnyAsync(sc => sc.Id == shoppingCartId);

            if (!shoppingCartExists)
            {
                return NotFound(new { Message = $"ShoppingCart with ID {shoppingCartId} not found." });
            }

            var cartItem = new CartItemDto
            {
                Id = Guid.NewGuid(),
                ShoppingCartId = shoppingCartId,
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                PricePerUnit = request.PricePerUnit,
                Quantity = request.Quantity
            };

            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "CartItem added successfully.", CartItem = cartItem });
        }
    }

}
