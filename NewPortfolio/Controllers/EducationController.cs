using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewPortfolio.Data;
using NewPortfolio.Entities;
using NewPortfolio.Models;

namespace NewPortfolio.Controllers
{
        [Authorize]
    public class EducationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EducationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Education = await _context.Educations.ToListAsync();
            return View(Education);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EducationViewModel model)
        {
            if (ModelState.IsValid)
                return View(model);

            var education = new Education();

            education.Subject = model.Subject;
            education.PassingYear = model.PassingYear;
            education.InstituteName = model.InstituteName;

            _context.Educations.Add(education);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Educations.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Education model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Educations.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.Educations.FindAsync(id);

            if (model == null) return NotFound();

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Educations.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _context.Educations.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
