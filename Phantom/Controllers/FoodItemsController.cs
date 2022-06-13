using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phantom.Data;
using Phantom.Data.Models;

namespace Phantom.Controllers
{
    public class FoodItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FoodItems.Include(f => f.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FoodItems == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItems
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,ImageUrl,CategoryId")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                foodItem.Id = Guid.NewGuid();
                _context.Add(foodItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", foodItem.CategoryId);
            return View(foodItem);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FoodItems == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", foodItem.CategoryId);
            return View(foodItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Price,ImageUrl,CategoryId")] FoodItem foodItem)
        {
            if (id != foodItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodItemExists(foodItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", foodItem.CategoryId);
            return View(foodItem);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FoodItems == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItems
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FoodItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FoodItems'  is null.");
            }
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem != null)
            {
                _context.FoodItems.Remove(foodItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodItemExists(Guid id)
        {
          return (_context.FoodItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
