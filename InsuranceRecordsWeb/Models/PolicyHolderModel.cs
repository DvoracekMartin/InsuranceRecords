using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InsuranceRecordsWeb.Models
{
    public class PolicyHolder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Jméno")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Příjmení")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("E-mail")]
        public string EMail { get; set; }
        [Required]
        [DisplayName("Telefonní číslo")]
        public string TelephoneNumber { get; set; }
        [Required]
        [DisplayName("Ulice")]
        public string StreetName { get; set; }
        [Required]
        [DisplayName("Číslo popisné")]
        public string BuildingNumber { get; set; }
        [Required]
        [DisplayName("Město")]
        public string CityName { get; set; }
        [Required]
        [DisplayName("PSČ")]
        public string ZIPCode { get; set; }


        //public List<Insurance>? Insurances { get; set; }    

    }
}
