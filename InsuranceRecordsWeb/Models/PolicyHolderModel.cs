using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InsuranceRecordsWeb.Models
{
    public class PolicyHolder
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole je povinné")]
        [DisplayName("Jméno")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pole je povinné")]
        [DisplayName("Příjmení")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Pole je povinné")]
        [DisplayName("E-mail")]
        public string EMail { get; set; }
        [Required(ErrorMessage = "Pole je povinné")]
        [DisplayName("Telefonní číslo")]
        public string TelephoneNumber { get; set; }
        [Required(ErrorMessage = "Pole je povinné")]
        [DisplayName("Ulice")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "Pole je povinné")]
        [DisplayName("Číslo popisné")]
        public string BuildingNumber { get; set; }
        [Required(ErrorMessage = "Pole je povinné")]
        [DisplayName("Město")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "Pole je povinné")]
        [DisplayName("PSČ")]
        public string ZIPCode { get; set; }


          

    }
}
