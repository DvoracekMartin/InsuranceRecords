using InsuranceRecordsWeb.Areas.Identity.Data;
using InsuranceRecordsWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wkhtmltopdf.NetCore;

namespace InsuranceRecordsWeb.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IGeneratePdf _generatePdf;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext db, IGeneratePdf generatePdf)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _generatePdf = generatePdf;
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


        //PolicyHolder/Edit/id
        //GET
        public IActionResult EditPolicyHolder(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            var insuredFromDb = _db.Insured.Find(id);

            if (insuredFromDb == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            //Admin can do that, no point in preventing it
            //prevents from accessing any PolicyHolder by any user, redirecting to "error" page

            //if (insuredFromDb.UserId != _userManager.GetUserId(User))
            //{
            //    return RedirectToAction("NotFoundCustom", "Home");
            //}

            return View(insuredFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPolicyHolder(PolicyHolderModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Insured.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Pojištěnec aktualizován.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //Insurance/Edit/id
        //GET
        public IActionResult EditInsurance(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            var insuranceFromDb = _db.Insurance.Find(id);

            if (insuranceFromDb == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }

            //Admin can do that, no point in preventing it
            //prevents from accessing any Insurance by any user, redirecting to "error" page

            //int insuranceId = insuranceFromDb.InsuranceHolderId;
            //var insuredFromDb = _db.Insured.Find(insuranceId);
            //if (insuredFromDb == null)
            //{
            //    return RedirectToAction("NotFoundCustom", "Home");
            //}
            //if (insuredFromDb.UserId != _userManager.GetUserId(User))
            //{
            //    return RedirectToAction("NotFoundCustom", "Home");
            //}

            return View(insuranceFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditInsurance(InsuranceModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Insurance.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Pojištění aktualizováno.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //InsuranceEvent/Edit/id
        //GET
        public IActionResult EditInsuranceEvent(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            var insuranceEventFromDb = _db.Event.Find(id);

            if (insuranceEventFromDb == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            //Admin can do that, no point in preventing it
            //prevents from accessing any InsuranceEvent by any user, redirecting to "error" page

            //int holderId = insuranceEventFromDb.PolicyHolderId;
            //var insuredFromDb = _db.Insured.Find(holderId);
            //if (insuredFromDb == null)
            //{
            //    return RedirectToAction("NotFoundCustom", "Home");
            //}
            //if (insuredFromDb.UserId != _userManager.GetUserId(User))
            //{
            //    return RedirectToAction("NotFoundCustom", "Home");
            //}

            return View(insuranceEventFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditInsuranceEvent(InsuranceEventModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Event.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Pojistná událost aktualizována.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Generating PDF Report      
        public async Task<IActionResult> AdminReport()
        {
            var users = from user in _db.Users
                        orderby user.LastName, user.Name
                        select user;
            var objInsuredList = _db.Insured.ToList();
            var objInsuranceList = _db.Insurance.ToList();
            var objInsuranceEventList = _db.Event.ToList();
            var adminUserModel = new AdminUserModel();

            var thisModel = new AdminUserModel();
            thisModel.Users = users.ToList();
            thisModel.PolicyHolders = objInsuredList.ToList();
            thisModel.Insurances = objInsuranceList.ToList();
            thisModel.InsuranceEvents = objInsuranceEventList.ToList();
            adminUserModel = thisModel;

            return await _generatePdf.GetPdf("Views/Admin/AdminReport.cshtml", adminUserModel);
        }
       

        public async Task<IActionResult> IndexRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        //Getting users and their roles
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

        //GET
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            ViewBag.UserName = user.UserName;
            var manageUserRolesViewModel = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                manageUserRolesViewModel.Add(userRolesViewModel);
            }
            return View(manageUserRolesViewModel);
        }

        //Adding/removing roles to users
        //POST
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("IndexUserRole");
        }
    }
}
