using InsuranceRecordsWeb.Areas.Identity.Data;
using InsuranceRecordsWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceRecordsWeb.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;           
        }

        public async Task<IActionResult> IndexRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public async Task<IActionResult> IndexUserRole()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Name = user.Name; 
                thisViewModel.LastName = user.LastName; 
                thisViewModel.Email = user.Email;
                thisViewModel.TelephoneNumber = user.TelephoneNumber;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public IActionResult CreateRole()
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RolesModel role)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role.RoleName);
            if (!roleExist)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role.RoleName.Trim()));
            }
            TempData["success"] = "Role úspěšně vytvořena";
            return View();
        }

        //GET
        public async Task<IActionResult> DeleteRole(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound(role);
            }
            return View(role);
        }
        //POST
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRolePOST(string? id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            await _roleManager.DeleteAsync(role);
            TempData["success"] = "Role úspěšně odstraněna";
            return RedirectToAction("IndexRole");

        }


    }
}
