using InsuranceRecordsWeb.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceRecordsWeb.Controllers
{
    public class InsuranceDetailController : Controller
    {
        private readonly ApplicationDbContext _db;
        public InsuranceDetailController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> InsuranceDetail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var insuranceFromDb = await _db.Insurance.FindAsync(id);


            if (insuranceFromDb == null)
            {
                return NotFound(insuranceFromDb);
            }

            return View(insuranceFromDb);
        }
    }
}
