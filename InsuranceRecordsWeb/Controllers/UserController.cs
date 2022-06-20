using InsuranceRecordsWeb.Areas.Identity.Data;
using InsuranceRecordsWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wkhtmltopdf.NetCore;

namespace InsuranceRecordsWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IGeneratePdf _generatePdf;

        public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IGeneratePdf generatePdf)
        {
            _userManager = userManager;           
            _db = db;
            _generatePdf = generatePdf; 
        }
        //Listing Policy Holders/Insured linked to user
        public async Task<IActionResult> Index(string? id, int pg=1)
        {
            if (id == "")
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            UserViewModel userViewModel = new UserViewModel();        

            UserViewModel thisModel = new UserViewModel();
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

            //prevents from accessing a UserViewModel by any user, redirecting to "error" page
            if (thisModel.PolicyHolders.Count > 0)
            { 
                if (thisModel.PolicyHolders.First().UserId != _userManager.GetUserId(User))
                {
                    return RedirectToAction("NotFoundCustom", "Home");
                }
            }

            //pagination
            const int pageSize = 5;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = thisModel.PolicyHolders.Count();
            var pager = new PagerModel(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            thisModel.PolicyHolders = thisModel.PolicyHolders.Skip(recSkip).Take(pager.PageSize).ToList();
            userViewModel = thisModel;       
            
            this.ViewBag.Pager = pager;

            return View(userViewModel);
        }
        private async Task<List<PolicyHolderModel>> GetPolicyHolders(string? id)
        {
            var policyHolders = from i in _db.Insured
                                where i.UserId == id
                                select i;
            return policyHolders.ToList();
        }



        //Generating PDF Report      
        public async Task<IActionResult> Report()
        {
            string id = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            //getting insured from db based on logged in user id
            var insuredFromDb = from i in _db.Insured
                                where i.UserId == user.Id
                                select i;

            //prevents from accessing a HoldersInsurancesViewModel by any user, redirecting to "error" page
            if (insuredFromDb.Count() > 0)
            {
                if (insuredFromDb.First().UserId != _userManager.GetUserId(User))
                {
                    return RedirectToAction("NotFoundCustom", "Home");
                }
            }
            //creating list of IDs of PolicyHolders/Insured
            List<int> Ids = new List<int>();
            foreach (var insured in insuredFromDb)
            {
                int holderId = insured.Id;
                Ids.Add(holderId);
            }
            //getting insurances from db based on list of insured IDs
            var insurancesFromDb = _db.Insurance.Where(insurance => Ids.Contains(insurance.InsuranceHolderId))
                     .Select(a => a).ToList();
            //creating list of IDs of Insurances
            List<int> InsuranceIds = new List<int>();
            foreach (var insurance in insurancesFromDb)
            {
                int insuranceId = insurance.Id;
                InsuranceIds.Add(insuranceId);
            }
            //getting insurance events from db based on list of insurances IDs
            var insuranceEventsFromDb = _db.Event.Where(ev => InsuranceIds.Contains(ev.InsuranceId))
                     .Select(a => a).ToList();

            UserReportModel userReportModel = new UserReportModel();

            UserReportModel thisModel = new UserReportModel();
            thisModel.ApplicationUser = user;
            thisModel.PolicyHolders = insuredFromDb.ToList();
            thisModel.Insurances = insurancesFromDb.ToList();
            thisModel.InsuranceEvents = insuranceEventsFromDb.ToList();         
            userReportModel = thisModel;

            return await _generatePdf.GetPdf("Views/User/Report.cshtml", userReportModel);
        }
    }
}
