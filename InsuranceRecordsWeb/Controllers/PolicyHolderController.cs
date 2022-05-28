using InsuranceRecordsWeb.Areas.Identity.Data;
using InsuranceRecordsWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceRecordsWeb.Controllers
{
    [Authorize]
    public class PolicyHolderController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public PolicyHolderController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        
        //GET
       /* public IActionResult Create()
        {     
            return View();
        }*/
        public IActionResult Create(string? userId)
        {
            if (userId == "")
            {
                return NotFound();
            }
            //prevents from creating a PolicyHolder under another user
            if (userId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            var policyHolder = new PolicyHolder();
            string userHolderId = (string)userId;
            
            policyHolder.UserId = userHolderId;
            return View(policyHolder);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PolicyHolder obj)
        {
            /*if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the name.");
            }*/
            if (ModelState.IsValid)
            {
                _db.Insured.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("Index", "User", new { id = obj.UserId });
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
            var insuredFromDb = _db.Insured.Find(id);        

            if (insuredFromDb == null)
            {
                return NotFound(insuredFromDb);
            }
            //prevents from accessing any PolicyHolder by any user
            if (insuredFromDb.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }
            return View(insuredFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PolicyHolder obj)
        {
            /*if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the name.");
            }*/


            if (ModelState.IsValid)
            {
                _db.Insured.Update(obj);
                _db.SaveChanges();
                TempData["success"] = " updated succesfully";                              
                return RedirectToAction("PolicyHolderDetail", "PolicyHolderDetail", new { id = obj.Id });
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
            var insuredFromDb = _db.Insured.Find(id);       

            if (insuredFromDb == null)
            {
                return NotFound(insuredFromDb);
            }
            //prevents from accessing any PolicyHolder by any user
            if (insuredFromDb.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }
            return View(insuredFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Insured.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Insured.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index", "User", new { id = obj.UserId });
            
        }

        
    }
}
