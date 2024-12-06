using FleXFrame.AcademicSuite.Models;
using Microsoft.EntityFrameworkCore;

namespace FleXFrame.AcademicSuite
{
    public class AcademicSuiteDataContext : DbContext
    {
        public AcademicSuiteDataContext(DbContextOptions<AcademicSuiteDataContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<CourseBatch> CourseBatches { get; set; }
        public DbSet<StudentEnrollment> StudentEnrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define Schema for tables
            modelBuilder.Entity<Student>().ToTable("Students", "AcademicSuite");
            modelBuilder.Entity<Course>().ToTable("Courses", "AcademicSuite");
            modelBuilder.Entity<Batch>().ToTable("Batches", "AcademicSuite");
            modelBuilder.Entity<CourseBatch>().ToTable("CourseBatches", "AcademicSuite");
            modelBuilder.Entity<StudentEnrollment>().ToTable("StudentEnrollments", "AcademicSuite");

            // Define Primery Keys for the tables
            modelBuilder.Entity<Student>().HasKey(s => s.StudentID);
            modelBuilder.Entity<Course>().HasKey(c => c.CourseID);
            modelBuilder.Entity<Batch>().HasKey(b => b.BatchID);
            modelBuilder.Entity<CourseBatch>().HasKey(cb => cb.CourseBatchID);
            modelBuilder.Entity<StudentEnrollment>().HasKey(se => se.EnrollmentID);

            // Relationships
            modelBuilder.Entity<StudentEnrollment>()
                .HasOne(se => se.Student)
                .WithMany(s => s.StudentEnrollments)
                .HasForeignKey(se => se.StudentID)
                .OnDelete(DeleteBehavior.Cascade); // or DeleteBehavior.Cascade if you want cascading delete

            modelBuilder.Entity<StudentEnrollment>()
                .HasOne(se => se.Course)
                .WithMany(c => c.StudentEnrollments)
                .HasForeignKey(se => se.CourseID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentEnrollment>()
                .HasOne(se => se.Batch)
                .WithMany()
                .HasForeignKey(se => se.BatchID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseBatch>()
                .HasOne(cb => cb.Course)
                .WithMany()
                .HasForeignKey(cb => cb.CourseID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseBatch>()
                .HasOne(cb => cb.Batch)
                .WithMany()
                .HasForeignKey(cb => cb.BatchID)
                .OnDelete(DeleteBehavior.Restrict);

            // Enum to string conversion (for readability in database)
            modelBuilder.Entity<Course>()
                .Property(c => c.CourseType)
                .HasConversion<string>();

            modelBuilder.Entity<Course>()
                .Property(c => c.CourseStatus)
                .HasConversion<string>();

            modelBuilder.Entity<Batch>()
                .Property(b => b.BatchStatus)
                .HasConversion<string>();

            modelBuilder.Entity<Student>()
                .Property(s => s.Gender)
                .HasConversion<string>();

            // Default values for audit fields
            modelBuilder.Entity<Student>()
                .Property(s => s.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Course>()
                .Property(c => c.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Batch>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<StudentEnrollment>()
                .Property(se => se.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<CourseBatch>()
                .Property(cb => cb.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
        }
    }

}
