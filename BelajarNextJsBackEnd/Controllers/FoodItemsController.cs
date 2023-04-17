using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BelajarNextJsBackEnd.Entities;
using BelajarNextJsBackEnd.Models;
using static BelajarNextJsBackEnd.Models.FoodItemModel;

namespace BelajarNextJsBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FoodItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FoodItems
        [HttpGet]
        public async Task<ActionResult<List<FoodItemDetailModel>>> GetFoodItems()
        {
            if (_context.FoodItems == null)
            {
                return NotFound();
            }
            return await _context.FoodItems.AsNoTracking().Select(Q => new FoodItemDetailModel
            {
                Id = Q.Id,
                Name = Q.Name,
                Price = Q.Price,
                RestaurantId = Q.RestaurantId,
                RestaurantName = Q.Restaurant.Name
            }).ToListAsync();
        }

        // GET: api/FoodItems/5
        [HttpGet("{id}", Name = "GetFoodItemDetail")]
        public async Task<ActionResult<List<FoodItemDetailModel>>> GetFoodItem(string id)
        {
            if (_context.FoodItems == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItems.AsNoTracking().Where(Q => Q.RestaurantId == id).Select(Q => new FoodItemDetailModel
            {
                Id = Q.Id,
                Name = Q.Name,
                Price = Q.Price,
                RestaurantId = Q.RestaurantId,
                RestaurantName = Q.Restaurant.Name
            }).ToListAsync();

            if (foodItem == null)
            {
                return NotFound();
            }

            return foodItem;
        }

        // PUT: api/FoodItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}", Name = "UpdateFoodItem")]
        public async Task<IActionResult> Post(string id, FoodItemModel foodItem)
        {
            var update = await _context.FoodItems.Where(Q => Q.Id == id).FirstOrDefaultAsync();
            if (update == null)
            {
                return NotFound();
            }

            update.Name = foodItem.Name;
            update.Price = foodItem.Price;
            update.RestaurantId = foodItem.RestaurantId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(id))
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

        // POST: api/FoodItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name = "CreateFoodItem")]
        public async Task<ActionResult<FoodItem>> Post(FoodItemModel foodItem)
        {
            if (_context.FoodItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FoodItems'  is null.");
            }

            // harus ada validasinya...

            var insert = new FoodItem
            {
                Id = Ulid.NewUlid().ToString(),
                Name = foodItem.Name,
                Price = foodItem.Price,
                RestaurantId = foodItem.RestaurantId,
                CreatedAt = DateTimeOffset.UtcNow,
            };

            _context.FoodItems.Add(insert);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FoodItemExists(insert.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return insert;
        }

        // DELETE: api/FoodItems/5
        [HttpDelete("{id}", Name = "DeleteFoodItem")]
        public async Task<IActionResult> DeleteFoodItem(string id)
        {
            if (_context.FoodItems == null)
            {
                return NotFound();
            }
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodItemExists(string id)
        {
            return (_context.FoodItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
