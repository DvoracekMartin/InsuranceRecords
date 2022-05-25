﻿using System.ComponentModel.DataAnnotations;

namespace InsuranceRecordsWeb.Models
{
    public class RolesModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole je povinné")]
        [Display(Name="Role")]
        public string RoleName { get; set; }
    }
}
