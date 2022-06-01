namespace InsuranceRecordsWeb.Models
{
    public class HoldersInsurancesViewModel
    {
        public List<InsuranceModel>? Insurances { get; set; }
        public List<PolicyHolderModel>? PolicyHolders { get; set; }
        public List<InsuranceEventModel>? InsuranceEvents { get; set; }
    }
}
