using Microsoft.EntityFrameworkCore;
using StudentSystem.Database.Entitties;
using StudentSystem.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Database.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private StudentSystemDbContext _context;
        public StudentRepository(StudentSystemDbContext context)
        {
            _context = context;
        }
        public Student Create(Student student) // sukuriamas studentas DB
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }
        public Student Update(Student student) //  atnaujinami studento duomednys DB
        {
            _context.Students.Update(student);
            _context.SaveChanges();
            return student;
        }
        public IEnumerable<Student> GetAll() // gaunamas studentu sarasas is DB
        {
            return _context.Students.ToList();
        }
        public IEnumerable<Student> GetAllWithDepartment() // gaunamas studentu sarasas is DB
        {
            return _context.Students.Include(d => d.Department).ToList();
        }
        public IEnumerable<Student> GetAllWithLectures()
        {
            return _context.Students.Include(l => l.Lectures).ToList();
        }

    }
}
