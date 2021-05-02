using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolPortal3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPortal3.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options )
            : base(options)
        {
        }

        public virtual DbSet<SchoolClassStudent> SchoolClassStudents { get; set; }
        public virtual DbSet<SchoolClass> SchoolClasses { get; set; }
    }
}
