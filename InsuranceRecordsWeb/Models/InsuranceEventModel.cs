using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InsuranceRecordsWeb.Models
{
    public class InsuranceEventModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné")]
        public int InsuranceId { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné")]
        public int PolicyHolderId { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné")]
        [DataType(DataType.Date)]
        [DisplayName("Datum vzniku škodní události")]
        public DateTime InsuranceEventTime { get; set; }

        public InsuranceModel Insurance { get; set; }

        public PolicyHolderModel PolicyHolder { get; set; }
    }
}
