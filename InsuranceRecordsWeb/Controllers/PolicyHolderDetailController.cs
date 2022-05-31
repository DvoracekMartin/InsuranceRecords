using InsuranceRecordsWeb.Areas.Identity.Data;
using InsuranceRecordsWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceRecordsWeb.Controllers
{
    [Authorize]
    public class PolicyHolderDetailController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public PolicyHolderDetailController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
        }
    
        public async Task<IActionResult> PolicyHolderDetail(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            
            var insuredFromDb = await _db.Insured.FindAsync(id);
            if (insuredFromDb == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }

            //prevents from accessing a PolicyHolder by any user
            if (insuredFromDb.UserId != _userManager.GetUserId(User))
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }

            var policyHolderInsuranceModel = new PolicyHolderDetailModel();

            var thisModel = new PolicyHolderDetailModel();
            thisModel.PolicyHolderId = insuredFromDb.Id;    
            thisModel.Name = insuredFromDb.Name;
            thisModel.LastName = insuredFromDb.LastName;
            thisModel.EMail = insuredFromDb.EMail;
            thisModel.TelephoneNumber = insuredFromDb.TelephoneNumber;  
            thisModel.StreetName = insuredFromDb.StreetName;
            thisModel.BuildingNumber = insuredFromDb.BuildingNumber;
            thisModel.CityName = insuredFromDb.CityName;
            thisModel.ZIPCode = insuredFromDb.ZIPCode;
            thisModel.Insurances = await GetInsurances(id);
            policyHolderInsuranceModel = thisModel;

            return View(policyHolderInsuranceModel);

        }

        private async Task<List<InsuranceModel>> GetInsurances(int? id)
        {         
            var insuranecOfHolder = from i in _db.Insurance
                                    where i.InsuranceHolderId == id
                                    select i;
            return insuranecOfHolder.ToList();
        }







    }
}
