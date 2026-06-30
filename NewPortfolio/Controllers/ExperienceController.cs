using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewPortfolio.Data;
using NewPortfolio.Entities;
using NewPortfolio.Models;
using System.Diagnostics.Contracts;

namespace NewPortfolio.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExperienceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var experience = await _context.Experiences.ToListAsync();
            return View(experience);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExperienceViewModel model)
        {
            var experience = new Experience();
            if (ModelState.IsValid)
            {
                experience.JobPosition = model.JobPosition;
                experience.YearOfExperience = model.YearOfExperience;
                experience.JoiningDate = model.JoiningDate;
                experience.Role = model.Role;
            }
            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();
            return View(model);
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
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return NotFound();
                }
                _context.Experiences.Update(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }
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
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return NotFound();
                }
                _context.Experiences.Remove(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }
    }
}
