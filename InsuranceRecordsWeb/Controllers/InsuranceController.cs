using InsuranceRecordsWeb.Areas.Identity.Data;
using InsuranceRecordsWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceRecordsWeb.Controllers
{
    public class InsuranceController : Controller
    {
        private readonly ApplicationDbContext _db;     

        public InsuranceController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult InsuranceList()
        {
            //List<Insurance> objInsuredList = _db.Insurance.ToList();
            var objInsuranceList = _db.Insurance.ToList();
            return View(objInsuranceList);
        }
        //GET
        public IActionResult Create(int? userId)
        {
            if (userId == null || userId == 0)
            {
                return NotFound();
            }          
            var dateTime = DateTime.Now.Date;
            var insurance = new Insurance();
            int holderId = (int)userId;
            insurance.InsuranceValidFrom = dateTime;
            insurance.InsuranceValidUntil = dateTime;                       
            insurance.InsuranceHolderId = holderId;
            return View(insurance);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Insurance obj)
        {       
            if (ModelState.IsValid)
            {              
                _db.Insurance.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("PolicyHolderDetail", "PolicyHolderDetail", new { id = obj.InsuranceHolderId });
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var insuranceFromDb = _db.Insurance.Find(id);

            if (insuranceFromDb == null)
            {
                return NotFound(insuranceFromDb);
            }
            return View(insuranceFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Insurance obj)
        {
         
            if (ModelState.IsValid)
            {
                _db.Insurance.Update(obj);
                _db.SaveChanges();
                TempData["success"] = " updated succesfully";
                return RedirectToAction("PolicyHolderDetail", "PolicyHolderDetail", new { id = obj.InsuranceHolderId });
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var insuranceFromDb = _db.Insurance.Find(id);

            if (insuranceFromDb == null)
            {
                return NotFound(insuranceFromDb);
            }
            return View(insuranceFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Insurance.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Insurance.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("PolicyHolderDetail", "PolicyHolderDetail", new { id = obj.InsuranceHolderId });

        }


    }
}
