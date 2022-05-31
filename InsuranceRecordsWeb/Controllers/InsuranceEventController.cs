using InsuranceRecordsWeb.Areas.Identity.Data;
using InsuranceRecordsWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceRecordsWeb.Controllers
{
    [Authorize]
    public class InsuranceEventController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public InsuranceEventController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }      

        //GET
        public IActionResult Create(int? id)
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
            int holderId = insuranceFromDb.InsuranceHolderId;
            var insuredFromDb = _db.Insured.Find(holderId);
            if (insuredFromDb == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            //prevents from creating any InsuranceEvent under any user
            if (insuredFromDb.UserId != _userManager.GetUserId(User))
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }

            var dateTime = DateTime.Now.Date;
            var insuranceEvent = new InsuranceEventModel();
            insuranceEvent.InsuranceEventTime = dateTime;            
            insuranceEvent.InsuranceId = insuranceFromDb.Id;
            insuranceEvent.PolicyHolderId = insuredFromDb.Id;
            insuranceEvent.PolicyHolder = insuredFromDb;
            insuranceEvent.Insurance = insuranceFromDb; 

            return View(insuranceEvent);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InsuranceEventModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Event.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Pojistná událost uložena.";
                return RedirectToAction("PolicyHolderDetail", "PolicyHolderDetail", new { id = obj.PolicyHolderId });
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
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
            //prevents from accessing any InsuranceEvent by any user
            int holderId = insuranceEventFromDb.PolicyHolderId;
            var insuredFromDb = _db.Insured.Find(holderId);
            if (insuredFromDb == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            if (insuredFromDb.UserId != _userManager.GetUserId(User))
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }

            return View(insuranceEventFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InsuranceEventModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Event.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Pojistná událost aktualizována.";
                return RedirectToAction("PolicyHolderDetail", "PolicyHolderDetail", new { id = obj.PolicyHolderId });
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
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
            //prevents from accessing any InsuranceEvent by any user
            int holderId = insuranceEventFromDb.PolicyHolderId;
            var insuredFromDb = _db.Insured.Find(holderId);
            if (insuredFromDb == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            if (insuredFromDb.UserId != _userManager.GetUserId(User))
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }

            return View(insuranceEventFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Event.Find(id);
            if (obj == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            _db.Event.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Pojistná událost odstraněna.";
            return RedirectToAction("PolicyHolderDetail", "PolicyHolderDetail", new { id = obj.PolicyHolderId });
        }
    }
}
