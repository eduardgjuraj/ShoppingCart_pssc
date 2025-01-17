using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Data;
using ShoppingCart.Data.Models;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartDbContext _context;

        public ShoppingCartController(ShoppingCartDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddShoppingCart([FromBody] ShoppingCartDto shoppingCart)
        {
            if (shoppingCart == null)
            {
                return BadRequest("ShoppingCart data is required.");
            }

            shoppingCart.Id = Guid.NewGuid(); 
            shoppingCart.CreatedAt = DateTime.UtcNow; 

            await _context.ShoppingCarts.AddAsync(shoppingCart);
            await _context.SaveChangesAsync();

            return Ok(shoppingCart);
        }
    }
}
