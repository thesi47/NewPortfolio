using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewPortfolio.Data;
using NewPortfolio.Entities;
using NewPortfolio.Models;
using System.Diagnostics.Contracts;

namespace NewPortfolio.Controllers
{
    public class PersonalInfoController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PersonalInfoController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var personalInfo = await _context.PersonalInfos.ToListAsync();
            return View(personalInfo);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PersonalInfoViewModel model)
        {
            var personalInfo = new PersonalInfo();
            if(ModelState.IsValid)
            {
                personalInfo.Name = model.Name;
                personalInfo.Address = model.Address;
                personalInfo.Email = model.Email;
                personalInfo.Phone = model.Phone;
            }
            _context.PersonalInfos.Add(personalInfo);
            await _context.SaveChangesAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.PersonalInfos.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PersonalInfo model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return NotFound();
                }
                _context.PersonalInfos.Update(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.PersonalInfos.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.PersonalInfos.FindAsync(id);
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return NotFound();
                }
                _context.PersonalInfos.Remove(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }
    }
}
