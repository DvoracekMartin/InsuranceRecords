using System.ComponentModel;

namespace InsuranceRecordsWeb.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        [DisplayName("Jméno")]
        public string Name { get; set; }
        [DisplayName("Příjmení")]
        public string LastName { get; set; }
        [DisplayName("Ulice")]
        public string StreetName { get; set; }
        [DisplayName("Číslo popisné")]
        public string BuildingNumber { get; set; }
        [DisplayName("Město")]
        public string CityName { get; set; }
        [DisplayName("PSČ")]
        public string ZipCode { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [DisplayName("Telefonní číslo")]
        public string TelephoneNumber { get; set; }

        public List<PolicyHolderInsuranceModel> PolicyHolders { get; set; }
    }
}
