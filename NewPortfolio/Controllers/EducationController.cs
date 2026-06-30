using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewPortfolio.Data;
using NewPortfolio.Entities;
using NewPortfolio.Models;
using System.Diagnostics.Contracts;

namespace NewPortfolio.Controllers
{
    public class EducationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EducationController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var education = await _context.Educations.ToListAsync();
            return View(education);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EducationViewModel model)
        {
            var education = new Education();
            if (ModelState.IsValid)
            {
                education.Subject = model.Subject;
                education.PassingYear = model.PassingYear;
                education.instituitionName = model.instituitionName;
 
            }
            _context.Educations.Add(education);
            await _context.SaveChangesAsync();
            return View(model);
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
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return NotFound();
                }
                _context.Educations.Update(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }
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
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return NotFound();
                }
                _context.Educations.Remove(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }
    }
}
