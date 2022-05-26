using InsuranceRecordsWeb.Areas.Identity.Data;
using InsuranceRecordsWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceRecordsWeb.Controllers
{
    public class PolicyHolderDetailController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PolicyHolderDetailController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task <IActionResult> Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var insuredFromDb = await _db.Insured.FindAsync(id);
           

            if (insuredFromDb == null)
            {
                return NotFound(insuredFromDb);
            }
            
            return View(insuredFromDb);
        }

        public async Task<IActionResult> PolicyHolderDetail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var insuredFromDb = await _db.Insured.FindAsync(id);
            var policyHolderInsuranceModel = new PolicyHolderInsuranceModel();

            var thisModel = new PolicyHolderInsuranceModel();
            thisModel.PolicyHolderId = insuredFromDb.Id;    
            thisModel.Name = insuredFromDb.Name;
            thisModel.LastName = insuredFromDb.LastName;
            thisModel.EMail = insuredFromDb.EMail;
            thisModel.TelephoneNumber = insuredFromDb.TelephoneNumber;  
            thisModel.StreetName = insuredFromDb.StreetName;
            thisModel.BuildingNumber = insuredFromDb.BuildingNumber;
            thisModel.CityName = insuredFromDb.CityName;
            thisModel.ZIPCode = insuredFromDb.ZIPCode;
            //thisModel.Insurances = (IEnumerable<Insurance>)await GetInsurances(insuredFromDb);
            policyHolderInsuranceModel = thisModel;

            return View(policyHolderInsuranceModel);

        }

        /*private async Task<List<Insurance>> GetInsurances(PolicyHolder user)
        {
            return new List<Insurance>(await _userManager.GetRolesAsync(user));
        }*/







    }
}
