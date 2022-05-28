using InsuranceRecordsWeb.Areas.Identity.Data;
using InsuranceRecordsWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceRecordsWeb.Controllers
{
    [Authorize]
    public class InsuranceController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public InsuranceController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
       
        //GET
        public IActionResult Create(int? userId)
        {
            if (userId == null || userId == 0)
            {
                return NotFound();
            }

            var insuredFromDb = _db.Insured.Find(userId);

            if (insuredFromDb == null)
            {
                return NotFound(insuredFromDb);
            }
            //prevents from creating any Insurance under any user
            if (insuredFromDb.UserId != _userManager.GetUserId(User))
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
            //prevents from accessing any Insurance by any user
            int insuranceId = insuranceFromDb.InsuranceHolderId;
            var insuredFromDb = _db.Insured.Find(insuranceId);
            if (insuredFromDb == null)
            {
                return NotFound(insuranceFromDb);
            }
            if (insuredFromDb.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
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
            //prevents from accessing any Insurance by any user
            int insuranceId = insuranceFromDb.InsuranceHolderId;
            var insuredFromDb = _db.Insured.Find(insuranceId);
            if (insuredFromDb == null)
            {
                return NotFound(insuranceFromDb);
            }
            if (insuredFromDb.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
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
