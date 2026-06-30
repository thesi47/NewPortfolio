using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewPortfolio.Data;
using NewPortfolio.Entities;
using NewPortfolio.Models;

namespace NewPortfolio.Controllers
{
    [Authorize]
    public class ExperienceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExperienceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Experience = await _context.Experiences.ToListAsync();
            return View(Experience);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExperienceViewModel model)
        {
            if (ModelState.IsValid)
                return View(model);

            var experience = new Experience();

            experience.JobPosition = model.JobPosition;
            experience.Role = model.Role;
            experience.YearOfExperience = model.YearOfExperience;

            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Experiences.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Experience model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Experiences.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.Experiences.FindAsync(id);

            if (model == null) return NotFound();

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Experiences.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _context.Experiences.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
