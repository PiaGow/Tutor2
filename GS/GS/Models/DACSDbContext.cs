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
            if (modelBuilder == null)
                throw new ArgumentNullException("modelBuilder");

            // for the other conventions, we do a metadata model loop
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                entityType.SetTableName(entityType.DisplayName());

                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

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
    }
    
}
