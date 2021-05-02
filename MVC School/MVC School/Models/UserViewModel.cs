using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal3.Models
{
    public class UserViewModel : IdentityUser
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(4, ErrorMessage = "A minimum of 4 characters is required")]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MinLength(4, ErrorMessage = "A minimum of 4 characters is required")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Display(Name = "Role")]
        [Required]
        public string Role { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";
    }
}

