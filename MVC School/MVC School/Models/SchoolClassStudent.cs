using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal3.Models
{
    public class SchoolClassStudent
    {
        [Key]
        public string StudentId { get; set; }
        public Guid SchoolClassId { get; set; }

        public virtual SchoolClass SchoolClass { get; set; }
    }
}
