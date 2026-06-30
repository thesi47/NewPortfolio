using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewPortfolio.Data;
using NewPortfolio.Entities;
using NewPortfolio.Models;
using System.Diagnostics.Contracts;

namespace NewPortfolio.Controllers
{
    public class SkillController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SkillController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Skill = await _context.Skills.ToListAsync();
            return View(Skill);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SkillViewModel model)
        {
            var skill = new Skill();
            if (ModelState.IsValid)
            {
                skill.Name = model.Name;
                skill.Level = model.Level;
                skill.Duration = model.Duration;
            }
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Skills.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Skill model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return NotFound();
                }
                _context.Skills.Update(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.Skills.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Skills.FindAsync(id);
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return NotFound();
                }
                _context.Skills.Remove(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }
    }
}
