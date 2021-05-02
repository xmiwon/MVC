using SchoolPortal3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SchoolPortal3.Entities
{
    public partial class SchoolClass
    {
        public SchoolClass()
        {
            SchoolClassCourses = new HashSet<SchoolClassCourse>();
            SchoolClassStudents = new HashSet<SchoolClassStudent>();
        }

        public Guid Id { get; set; }
        public string ClassName { get; set; }

        [DisplayName("Program Manager")]
        public string ProgramManagerId { get; set; }
        public DateTime Created { get; set; }


       
        public virtual ApplicationUser ProgramManager { get; set; }
        public virtual ICollection<SchoolClassCourse> SchoolClassCourses { get; set; }
        public virtual ICollection<SchoolClassStudent> SchoolClassStudents { get; set; }
    }
}
