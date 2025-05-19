using Invert.DAL;
using Invert.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace Invert.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _context.Position.Include(e => e.Employees).AsNoTracking().ToListAsync();


            return View(positions);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Position position)
        {
            if (!ModelState.IsValid)
            {
                return View(position);
            }
            bool result = await _context.Position.AnyAsync(p => p.Name == position.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(Position.Name), $"{position.Name}  named already exists");
                return View();
            }
            await _context.Position.AddAsync(position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null && id <= 0) return BadRequest();
            Position? position = await _context.Position.FirstOrDefaultAsync(p => p.Id == id);
            if (position is null) return NotFound();



            return View(position);

        }


        [HttpPost]
        public async Task<IActionResult> Update(int? id, Position position)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool result = await _context.Position.AnyAsync(p => p.Name == position.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(Position.Name), $"{position.Name}  named already exists");
                return View();

            }
            Position? existed = await _context.Position.FirstOrDefaultAsync(e => e.Id == position.Id);
            existed.Name = position.Name;


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null && id <= 0) { return BadRequest(); }
            Position? existed = await _context.Position.FirstOrDefaultAsync(e => e.Id == id);
            if (existed is null) return NotFound();


            _context.Position.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}