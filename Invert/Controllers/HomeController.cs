
using Invert.DAL;
using Invert.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invert.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
           _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {

            Employees= await _context.Employees.Include(e=>e.Position).ToListAsync(),



            };
            return View(homeVM);
        }
    }
}
