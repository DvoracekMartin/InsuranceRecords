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
            var objInsuredList = _db.Insurance.ToList();
            return View(objInsuredList);
        }
        ////GET
        //public IActionResult Create()
        //{     
        //    return View();
        //}
        ////POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Insurance obj)
        //{
        //    /*if (obj.Name == obj.DisplayOrder.ToString())
        //    {
        //        ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the name.");
        //    }*/
        //    if (ModelState.IsValid)
        //    {
        //        _db.Insurance.Add(obj);
        //        _db.SaveChanges();
        //        TempData["success"] = "Category created succesfully";
        //        return RedirectToAction("InsuranceList");
        //    }
        //    return View(obj);           
        //}

        ////GET
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();  
        //    }
        //    var insuredFromDb = _db.Insured.Find(id);

        //    if (insuredFromDb == null)
        //    {
        //        return NotFound(insuredFromDb);
        //    }           
        //    return View(insuredFromDb);
        //}
        ////POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(PolicyHolder obj)
        //{
        //    /*if (obj.Name == obj.DisplayOrder.ToString())
        //    {
        //        ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the name.");
        //    }*/


        //    if (ModelState.IsValid)
        //    {
        //        _db.Insured.Update(obj);
        //        _db.SaveChanges();
        //        TempData["success"] = " updated succesfully";                              
        //        return RedirectToAction("PolicyHolderList");
        //    }
        //    return View(obj);
        //}
        
        ////GET
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var insuredFromDb = _db.Insured.Find(id);

        //    if (insuredFromDb == null)
        //    {
        //        return NotFound(insuredFromDb);
        //    }
        //    return View(insuredFromDb);
        //}
        ////POST
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeletePOST(int? id)
        //{
        //    var obj = _db.Insured.Find(id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Insured.Remove(obj);
        //    _db.SaveChanges();
        //    TempData["success"] = "Category deleted succesfully";
        //    return RedirectToAction("PolicyHolderList");
            
        //}

        
    }
}
