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
           

           /* var insuredFromDb = from insured in _db.Insured
                                   join insurance in _db.Insurance on insured.Id equals insurance.InsuranceHolderId
                                   select insured;
            var insuranceFromDb = from insured in _db.Insured
                                join insurance in _db.Insurance on insured.Id equals insurance.InsuranceHolderId
                                select insurance;*/

            if (insuredFromDb == null)
            {
                return NotFound(insuredFromDb);
            }
            //return View(insuredFromDb);
            //dynamic myDynamicmodel = new System.Dynamic.ExpandoObject();
            //ViewBag.PolicyHolders = insuredFromDb;
           // ViewBag.Insurances = insuranceFromDb;
            return View(insuredFromDb);
        }
        
        /*public List<PolicyHolder> GetPolicyHolders()
        {
            List<PolicyHolder> InsuredList = _db.Insured.ToList();
            return InsuredList;
        }
        public List<Insurance> GetInsurances()
        {
            List<Insurance> InsuranceList = _db.Insurance.ToList();
            return InsuranceList;

        }*/

        /*public ActionResult DynamicDemo()
        {
            dynamic myDynamicmodel = new System.Dynamic.ExpandoObject();
            myDynamicmodel.PolicyHolders = GetPolicyHolders();
            myDynamicmodel.Insurances = GetInsurances();
            return View(myDynamicmodel);
        }*/

    }
}
