using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InsuranceRecordsWeb.Models
{
    public class Insurance
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int InsuranceHolderId { get; set; }
        [Required]
        [DisplayName("Pojištění")]
        public string InsuranceType { get; set; }
        [Required]
        [DisplayName("Částka")]
        public string InsuranceAmount { get; set; }
        [Required]
        [DisplayName("Předmět pojištění")]
        public string InsuranceSubject { get; set; }
        [Required]
        [DisplayName("Platnost od")]
        public DateTime InsuranceValidFrom { get; set; }
        [Required]
        [DisplayName("Platnost do")]
        public DateTime InsuranceValidUntil { get; set; }


        
    }
}
