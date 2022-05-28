using InsuranceRecordsWeb.Areas.Identity.Data;
using InsuranceRecordsWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceRecordsWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;           
            _db = db;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var users = await _userManager.Users.ToListAsync();
        //    return View(users);
        //}


        public async Task<IActionResult> Index(string? id)
        {
            if (id == "")
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            var userViewModel = new UserViewModel();

            var thisModel = new UserViewModel();
            thisModel.UserId = user.Id;
            thisModel.Name = user.Name;
            thisModel.LastName = user.LastName;
            thisModel.StreetName = user.StreetName;
            thisModel.BuildingNumber = user.BuildingNumber;
            thisModel.CityName = user.CityName;
            thisModel.ZipCode = user.ZipCode;
            thisModel.Email = user.Email;
            thisModel.TelephoneNumber = user.TelephoneNumber;
            thisModel.PolicyHolders = await GetPolicyHolders(id);
            userViewModel = thisModel;

            return View(userViewModel);

        }
        private async Task<List<PolicyHolder>> GetPolicyHolders(string? id)
        {
            //var insurancesFromDb = _db.Insurance.Find();
            //return insurancesFromDb;
            var policyHolders = from i in _db.Insured
                                    where i.UserId == id
                                    select i;
            return policyHolders.ToList();
            //return new List<Insurance>(_db.Insurance.ToList());
        }
    }
}
