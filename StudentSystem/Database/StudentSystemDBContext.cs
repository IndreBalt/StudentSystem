using Microsoft.EntityFrameworkCore;
using StudentSystem.Database.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Database
{
    public class StudentSystemDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Localhost;Database=SchoolSystemDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
              .Property(d => d.DepartmentCode)
              .ValueGeneratedNever();

            modelBuilder.Entity<Department>()
                .HasKey(k => k.DepartmentCode);

            modelBuilder.Entity<Lecture>()
                .HasKey(c => c.LectureName);

            modelBuilder.Entity<Student>()
              .Property(s => s.StudentNumber)
              .ValueGeneratedNever();

            modelBuilder.Entity<Student>()
                .HasKey(c => c.StudentNumber);
           

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentCode);

            modelBuilder.Entity<Student>()
                .HasData(CsvHelperService.GetStudentsFromCsv());

            modelBuilder.Entity<Department>()
                .HasMany(s => s.Students)
                .WithOne(c => c.Department)
                .HasForeignKey(j => j.DepartmentCode);

            modelBuilder.Entity<Department>()
                .HasData(CsvHelperService.GetDepartmentsFromCsv());

            modelBuilder.Entity<Lecture>()
                .HasData(CsvHelperService.GetLecturesFromCsv());

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Lectures)
                .WithMany(c => c.Students)
                .UsingEntity<StudentLecture>(
                 l => l.HasOne<Lecture>().WithMany().HasForeignKey(s => s.LectureName),
                 s => s.HasOne<Student>().WithMany().HasForeignKey(s => s.StudentNumber));

            modelBuilder.Entity<StudentLecture>()
                .HasData(CsvHelperService.GetStudentLecturesFromCsv());

            modelBuilder.Entity<Department>()
                .HasMany(l => l.Lectures)
                .WithMany(d => d.Departments)
                .UsingEntity<DepartmentLectures>(
                 l => l.HasOne<Lecture>().WithMany().HasForeignKey(s => s.LectureName),
                 d => d.HasOne<Department>().WithMany().HasForeignKey(s => s.DepartmentCode));

            modelBuilder.Entity<DepartmentLectures>()
                .HasData(CsvHelperService.GetDepartmentLecturesFromCsv());

           

        }
    }
}
