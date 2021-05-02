using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal3.Models
{
    public class SchoolClass
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required(ErrorMessage = "Choose a Teacher")]
        public string TeacherId { get; set; }
    }
}
