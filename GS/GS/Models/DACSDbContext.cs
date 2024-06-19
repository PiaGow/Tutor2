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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            

            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<Course> Courses { get; set; }
            public virtual DbSet<Subject> Subjects { get; set; }
            public virtual DbSet<TimeCourse> TimeCourses { get; set; }
            public virtual DbSet<HomeWork> HomeWork { get; set; }
            public virtual DbSet<Assess> Assesses { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Ratting> Ratings { get; set; }
        public virtual DbSet<TransactionHistory> TransactionHistory { get; set; }
        public virtual DbSet<RequiredScore> RequiredScores { get; set; }
        public virtual DbSet<Servicer> Servicers { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<CoursesStudent> CoursesStudents { get;set; }
        public virtual DbSet<HomeWorkStudent> HomeWorkStudents { get; set;}
    }
    
}
