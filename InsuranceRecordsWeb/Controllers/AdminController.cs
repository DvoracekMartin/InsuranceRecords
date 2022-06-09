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
        private readonly ApplicationDbContext _db;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }


        //Listing Users, their Insured and their linked Insurances and Events
        public async Task<IActionResult> Index()
        {
            //getting all Users from db
            //var users = await _userManager.Users.ToListAsync();
            var users = from user in _db.Users
                        orderby user.LastName
                        select user;

            //if (users == null)
            //{
            //    return RedirectToAction("NotFoundCustom", "Home");
            //}

            //getting insured from db
            var insuredFromDb = _db.Insured.ToList();     
            //getting insurances from db
            var insurancesFromDb = _db.Insurance.ToList();
            //getting events from db
            var eventsFromDb = _db.Event.ToList();

            var adminUserModel = new AdminUserModel();

            var thisModel = new AdminUserModel();
            thisModel.Users = users.ToList();
            thisModel.PolicyHolders = insuredFromDb.ToList();
            thisModel.Insurances = insurancesFromDb.ToList();
            thisModel.InsuranceEvents = eventsFromDb.ToList();         
            adminUserModel = thisModel;

            return View(adminUserModel);
        }

        public IActionResult InsuredList()
        {
            var objInsuredList = _db.Insured.ToList();
            return View(objInsuredList);
        }

        public IActionResult InsuranceList()
        {
            var objInsuranceList = _db.Insurance.ToList();
            return View(objInsuranceList);
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
                return RedirectToAction("NotFoundCustom", "Home");
            }
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
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
                return RedirectToAction("NotFoundCustom", "Home");
            }
            await _roleManager.DeleteAsync(role);
            TempData["success"] = "Role úspěšně odstraněna";
            return RedirectToAction("IndexRole");

        }


    }
}
