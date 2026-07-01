using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewPortfolio.Entities;
using NewPortfolio.Models;

namespace NewPortfolio.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;

            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if(ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(role);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var existingRole = await _roleManager.FindByIdAsync(role.Id);
                if (existingRole == null)
                {
                    return NotFound();
                }
                existingRole.Name = role.Name;
                var result = await _roleManager.UpdateAsync(existingRole);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach(var error in  result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var personalInfo = await _roleManager.FindByIdAsync(id);

            if (personalInfo == null)
                return NotFound();

            return View(personalInfo);
        }

        [HttpPost, ActionName("Delete")]
       public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if(role == null)
            { return RedirectToAction("Index"); }
            var result = await _roleManager.DeleteAsync(role);
            if(result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(role);
        }
        public async Task<IActionResult> AssignRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            var model = new AssignRoleViewModel
            {
                UserId = user.Id,
                UserName = user.FullName,
                Roles = _roleManager.Roles.Select(r => r.Name!).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Assignrole(AssignRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if(user == null)
            {
                return NotFound();
            }
            var result = await _userManager.AddToRoleAsync(user, model.SelectedRole);

            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
                model.Roles = _roleManager.Roles.Select(r => r.Name!).ToList();

            return View(model);
        }
    }
}
