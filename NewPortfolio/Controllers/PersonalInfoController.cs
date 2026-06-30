using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewPortfolio.Data;
using NewPortfolio.Entities;
using NewPortfolio.Models;

namespace NewPortfolio.Controllers
{
    [Authorize]
    public class PersonalInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonalInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.PersonalInfos.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonalInfoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            PersonalInfo personalInfo = new PersonalInfo
            {
                Name = model.Name,
                Phone = model.Phone,
                Address = model.Address,
                Email = model.Email
            };

            _context.PersonalInfos.Add(personalInfo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var personalInfo = await _context.PersonalInfos.FindAsync(id);

            if (personalInfo == null)
                return NotFound();

            return View(personalInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonalInfo model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.PersonalInfos.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var personalInfo = await _context.PersonalInfos.FindAsync(id);

            if (personalInfo == null)
                return NotFound();

            return View(personalInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PersonalInfo model)
        {
            var personalInfo = await _context.PersonalInfos.FindAsync(model.Id);

            if (personalInfo != null)
            {
                _context.PersonalInfos.Remove(personalInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}