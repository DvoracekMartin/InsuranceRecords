using InsuranceRecordsWeb.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceRecordsWeb.Controllers
{
    [Authorize]
    public class InsuranceDetailController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public InsuranceDetailController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
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
    }
}
