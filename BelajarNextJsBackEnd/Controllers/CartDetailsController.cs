using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BelajarNextJsBackEnd.Entities;
using static OpenIddict.Abstractions.OpenIddictConstants;
using BelajarNextJsBackEnd.Models;
using Microsoft.AspNetCore.Authorization;

namespace BelajarNextJsBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CartDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDetail>>> GetCartDetails()
        {
          if (_context.CartDetails == null)
          {
              return NotFound();
          }
            return await _context.CartDetails.ToListAsync();
        }

        // GET: api/CartDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CartGridModel>>> GetCartDetail(string id)
        {
            if (_context.CartDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CartDetails'  is null.");
            }

            var userId = User.FindFirst(Claims.Subject)?.Value;

            if (userId == null)
            {
                userId = "01GXZBZDT9CZRHQF2QCCFBH40N";
            }

            var cart = await _context.Carts
                .Where(Q => Q.RestaurantId == id && Q.UserId == userId)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                return new List<CartGridModel>();
            }

            var cartItems = await _context.CartDetails
                .Where(Q => Q.CartId == cart.Id)
                .ToListAsync();

            var cartList = new List<CartGridModel>();

            foreach(CartDetail item in cartItems)
            {
                cartList.Add(new CartGridModel
                {
                    Name = item.FoodItem.Name,
                    Price = item.FoodItem.Price,
                    Qty = item.Qty
                });
            }

            return cartList;
        }

        // POST: api/CartDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<IActionResult> PostCartDetail(string id, CartDetail cartDetail)
        {
            if (id != cartDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(cartDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CartDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name = "AddItem")]
        public async Task<ActionResult<bool>> PostCartDetail(AddToCartModel model)
        {
            if (_context.CartDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CartDetails'  is null.");
            }

            var userId = User.FindFirst(Claims.Subject)?.Value;

            if (userId == null)
            {
                userId = "01GXZBZDT9CZRHQF2QCCFBH40N";
            }

            var cart = await _context.Carts
                .Where(Q => Q.RestaurantId == model.RestaurantId && Q.UserId == userId)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Ulid.NewUlid().ToString(),
                    UserId = userId,
                    RestaurantId = model.RestaurantId,
                    CreatedAt = DateTimeOffset.UtcNow
                };

                _context.Carts.Add(cart);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }

            var existing = await _context.CartDetails
                .Where(Q => Q.FoodItemId == model.FoodItemId && Q.CartId == cart.Id)
                .FirstOrDefaultAsync();

            if (existing != null)
            {
                existing.Qty += model.Qty;
            }
            else
            {
                _context.CartDetails.Add(new CartDetail
                {
                    CartId = cart.Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    FoodItemId = model.FoodItemId,
                    Qty = model.Qty,
                    Id = Ulid.NewUlid().ToString()
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE: api/CartDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartDetail(string id)
        {
            if (_context.CartDetails == null)
            {
                return NotFound();
            }
            var cartDetail = await _context.CartDetails.FindAsync(id);
            if (cartDetail == null)
            {
                return NotFound();
            }

            _context.CartDetails.Remove(cartDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartDetailExists(string id)
        {
            return (_context.CartDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
