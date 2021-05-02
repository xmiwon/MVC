using Microsoft.AspNetCore.Identity;
using SchoolPortal3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal3.Models
{
    public class SchoolClassViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public string ClassName { get; set; }

        public IEnumerable<ApplicationUser> Students { get; set; }


        [Display(Name = "Teacher")]
        public ApplicationUser ProgramManager { get; set; }


        //public string Role { get; set; }



    }
}
