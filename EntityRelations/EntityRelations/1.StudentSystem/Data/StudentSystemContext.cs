using System;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data 
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(Config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnConfiguringStudent(modelBuilder);

            OnConfiguringCourse(modelBuilder);

            OnConfiguringResource(modelBuilder);

            OnConfiguringHomework(modelBuilder);

            OnConfiguringStudentCourse(modelBuilder);
        }

        private void OnConfiguringStudentCourse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.StudentId,
                    e.CourseId
                });

                entity
                    .HasOne(e => e.Student)
                    .WithMany(s => s.CourseEnrollments)
                    .HasForeignKey(e => e.StudentId);

                entity
                    .HasOne(e => e.Course)
                    .WithMany(s => s.StudentsEnrolled)
                    .HasForeignKey(e => e.CourseId);
            });
        }

        private void OnConfiguringHomework(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Homework>(entity => 
            {
                entity
                .HasKey(e => e.HomeworkId);

                entity
                    .Property(e => e.Content);

                entity
                    .HasOne(s => s.Student)
                    .WithMany(s => s.HomeworkSubmissions);

                entity
                    .HasOne(s => s.Course)
                    .WithMany(c => c.HomeworkSubmissions);
            });
        }

        private void OnConfiguringResource(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>(entity =>
            {
                entity
                    .HasKey(e => e.ResourceId);

                entity
                    .Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode()
                    .IsRequired();

                entity
                    .Property(e => e.Url)
                    .IsRequired();

                entity
                   .HasOne(e => e.Course)
                   .WithMany(c => c.Resources);
            });
        }

        private static void OnConfiguringCourse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity
                    .HasKey(e => e.CourseId);

                entity
                    .Property(e => e.Name)
                    .HasMaxLength(80)
                    .IsUnicode()
                    .IsRequired();

                entity
                   .Property(e => e.Description)
                   .IsUnicode();
            });
        }

        private static void OnConfiguringStudent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity
                    .HasKey(e => e.StudentId);

                entity
                    .Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode()
                    .IsRequired();

                entity
                    .Property(e => e.PhoneNumber)
                    .HasColumnName("CHAR(10)");

                entity
                   .Property(e => e.PhoneNumber)
                   .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
