using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Xml;

namespace GS.Models
{
    public class DACSDbContext : IdentityDbContext<ApplicationUser>
    {
       
            

            public DACSDbContext(DbContextOptions<DACSDbContext> options)
                : base(options)
            {
            }




            public virtual DbSet<Course> Courses { get; set; }
            public virtual DbSet<Subject> Subjects { get; set; }
            public virtual DbSet<TimeCourse> TimeCourses { get; set; }
            public virtual DbSet<HomeWork> HomeWork { get; set; }

    }
    
}
