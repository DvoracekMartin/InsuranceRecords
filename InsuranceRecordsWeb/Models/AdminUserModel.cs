using InsuranceRecordsWeb.Areas.Identity.Data;

namespace InsuranceRecordsWeb.Models
{
    public class AdminUserModel
    {
        public List<ApplicationUser>? Users { get; set; }
        public List<InsuranceModel>? Insurances { get; set; }
        public List<PolicyHolderModel>? PolicyHolders { get; set; }
        public List<InsuranceEventModel>? InsuranceEvents { get; set; }
    }
}
