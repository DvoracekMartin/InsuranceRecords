using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InsuranceRecordsWeb.Models_
{
    public class User
    {
        public int Id { get; set; }
        // user ID from AspNetUser table.
        public string? OwnerID { get; set; }
        [DisplayName("Jméno")]
        public string? Name { get; set; }
        [DisplayName("Příjmení")]
        public string? LastName { get; set; }
        [DisplayName("Ulice")]
        public string? StreetName { get; set; }
        [DisplayName("Číslo popisné")]
        public string? BuildingNumber { get; set; }
        [DisplayName("Město")]
        public string? CityName { get; set; }
        [DisplayName("PSČ")]
        public string? ZipCode { get; set; }
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        public string? Email { get; set; }
        [DisplayName("Telefonní číslo")]
        public string? TelephoneNumber { get; set; }

        public ContactStatus Status { get; set; }
    }
    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
