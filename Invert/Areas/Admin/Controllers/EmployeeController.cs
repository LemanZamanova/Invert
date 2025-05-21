using Invert.DAL;
using Invert.Models;
using Invert.Utilities;
using Invert.Utilities.Extensions;
using Invert.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invert.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin,Moderator")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<GetEmployeeVM> employeeVMs = await _context.Employees.Select(e => new GetEmployeeVM
            {
                Id = e.Id,
                Name = e.Name,
                Surname = e.Surname,
                Image = e.Image,
                PositionName = e.Position.Name,

            }
            ).ToListAsync();

            return View(employeeVMs);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateEmployeeVM employeeVM = new CreateEmployeeVM
            {
                Position = await _context.Position.ToListAsync(),
            };


            return View(employeeVM);


        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM employeeVM)
        {
            employeeVM.Position = await _context.Position.ToListAsync();
            if (!ModelState.IsValid) return View(employeeVM);


            if (!employeeVM.Photo.ValidateType("image/"))
            {

                ModelState.AddModelError(nameof(employeeVM.Photo), "File type is incorrect");
                return View(employeeVM);
            }

            if (!employeeVM.Photo.ValidateSize(FileSize.MB, 1))
            {
                ModelState.AddModelError(nameof(employeeVM.Photo), "File size is incorrect");
                return View(employeeVM);
            }

            Employee employee = new Employee
            {
                Name = employeeVM.Name,
                Surname = employeeVM.Surname,
                Decription = employeeVM.Decription,
                XUrl = employeeVM.XUrl,
                InstagramUrl = employeeVM.InstagramUrl,
                FacebookUrl = employeeVM.FacebookUrl,
                LinkedinUrl = employeeVM.LinkedinUrl,
                PositionId = employeeVM.PositionId.Value,
                Image = await employeeVM.Photo.CreateFile(_env.WebRootPath, "assets", "img", "person")
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null && id <= 0) return BadRequest();

            Employee? employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null) return NotFound();
            _context.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is not null && id <= 0) return BadRequest();
            Employee? employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();

            UpdateEmployeeVm updateEmployeeVm = new UpdateEmployeeVm

            {
                Name = employee.Name,
                Surname = employee.Surname,
                Decription = employee.Decription,
                XUrl = employee.XUrl,
                InstagramUrl = employee.InstagramUrl,
                FacebookUrl = employee.FacebookUrl,
                LinkedinUrl = employee.LinkedinUrl,
                Image = employee.Image,
                Position = await _context.Position.ToListAsync(),
            };
            return View(updateEmployeeVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CreateEmployeeVM employeeVM)
        {
            employeeVM.Position = await _context.Position.ToListAsync();
            if (!ModelState.IsValid) return View(employeeVM);
            Employee? existed = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employeeVM.Photo is not null)
            {

                if (!employeeVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(employeeVM.Photo), "File type is incorrect");
                    return View(employeeVM);
                }
                if (!employeeVM.Photo.ValidateSize(FileSize.MB, 1))
                {
                    ModelState.AddModelError(nameof(employeeVM.Photo), "File size is incorrect");
                    return View(employeeVM);
                }


                if (employeeVM.Photo is null)
                {
                    existed.Image.DeleteFile(_env.WebRootPath, "assets", "img", "person");
                }


                existed.Image = await employeeVM.Photo.CreateFile(_env.WebRootPath, "assets", "img", "person");

            }

            existed.Name = employeeVM.Name;
            existed.Surname = employeeVM.Surname;
            existed.Decription = employeeVM.Decription;
            existed.XUrl = employeeVM.XUrl;
            existed.InstagramUrl = employeeVM.InstagramUrl;
            existed.FacebookUrl = employeeVM.FacebookUrl;
            existed.LinkedinUrl = employeeVM.LinkedinUrl;
            existed.PositionId = employeeVM.PositionId.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}