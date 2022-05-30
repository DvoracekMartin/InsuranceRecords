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
        public PolicyHolder PolicyHolder { get; set; }

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
                return RedirectToAction("NotFoundCustom", "Home");
            }
            //prevents from creating a PolicyHolder under another user
            if (userId != _userManager.GetUserId(User))
            {
                return RedirectToAction("NotFoundCustom", "Home");
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
                obj.Name = Uppercase(obj.Name.Trim().ToLower());
                obj.LastName = Uppercase(obj.LastName.Trim().ToLower());
                obj.EMail = obj.EMail.Trim();
                obj.TelephoneNumber = obj.TelephoneNumber.Trim();
                obj.StreetName = Uppercase(obj.StreetName.Trim());
                obj.BuildingNumber = obj.BuildingNumber.Trim();
                obj.CityName = Uppercase(obj.CityName.Trim());
                obj.ZIPCode = obj.ZIPCode.Trim();

                _db.Insured.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Pojištěnec uložen.";
                return RedirectToAction("Index", "User", new { id = obj.UserId });
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
            var insuredFromDb = _db.Insured.Find(id);        

            if (insuredFromDb == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            //prevents from accessing any PolicyHolder by any user
            if (insuredFromDb.UserId != _userManager.GetUserId(User))
            {
                return RedirectToAction("NotFoundCustom", "Home");
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
                TempData["success"] = "Pojištěnec aktualizován.";
                return RedirectToAction("PolicyHolderDetail", "PolicyHolderDetail", new { id = obj.Id });
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
            var insuredFromDb = _db.Insured.Find(id);       

            if (insuredFromDb == null)
            {
                return RedirectToAction("NotFoundCustom", "Home");
            }
            //prevents from accessing any PolicyHolder by any user
            if (insuredFromDb.UserId != _userManager.GetUserId(User))
            {
                return RedirectToAction("NotFoundCustom", "Home");
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
                return RedirectToAction("NotFoundCustom", "Home");
            }
            _db.Insured.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Pojištěnec odstraněn.";
            return RedirectToAction("Index", "User", new { id = obj.UserId });

        }

        //Returns string from the parameter with the uppercased first letter 
        public string Uppercase(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

    }
}
