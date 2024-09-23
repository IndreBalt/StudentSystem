using Microsoft.EntityFrameworkCore;
using StudentSystem.Database.Entitties;
using StudentSystem.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Database.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private StudentSystemDbContext _context;
        public DepartmentRepository(StudentSystemDbContext context)
        {
            _context = context;
        }

        public Department Create(Department department) // sukuriamas departamentas DB
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return department;
        }
        public Department Update(Department department) //Atnaujinami departamento duomnys DB
        {
            _context.Departments.Update(department);
            return department;
        }
        public List<Department> GetAll()
        {
            return _context.Departments.ToList(); //Gaunamas departamentu sarasas is DB
        }
        public List<Department> GetAllWithStudents()
        {
            return _context.Departments.Include(s => s.Students).ToList();//Gaunamas departamentu sarasas su studentais is DB
        }
        public List<Department> GetAllWithLectures()
        {
            return _context.Departments.Include(s => s.Lectures).ToList();//Gaunamas departamentu sarasas su paskaitom is DB
        }


    }
}
