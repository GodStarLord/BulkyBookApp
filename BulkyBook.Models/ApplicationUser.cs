using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Postalcode { get; set; }

        // Navigation Property
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]       // "F_Key_Name" this should be same as the above property
        [ValidateNever]
        public Company Company { get; set; }    // F_Key to the Model
    }
}
