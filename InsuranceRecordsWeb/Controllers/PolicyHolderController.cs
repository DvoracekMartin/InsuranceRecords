using InsuranceRecordsWeb.Areas.Identity.Data;
using InsuranceRecordsWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceRecordsWeb.Controllers
{
    public class PolicyHolderController : Controller
    {
        private readonly ApplicationDbContext _db;
        

        public PolicyHolderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var objInsuredList = _db.Insured.ToList();
            //List<PolicyHolder> objInsuredList = _db.Insured.ToList();
            return View(objInsuredList);
        }
        //GET
        public IActionResult Create()
        {     
            return View();
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
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
            
        }

        
    }
}
