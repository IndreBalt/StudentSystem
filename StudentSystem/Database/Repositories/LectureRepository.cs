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
    public class LectureRepository : ILectureRepository
    {
        private StudentSystemDbContext _context;
        public LectureRepository(StudentSystemDbContext context)
        {
            _context = context;
        }

        public Lecture Create(Lecture lecture) // sukuriama paskaita DB
        {
            _context.Lectures.Add(lecture);
            _context.SaveChanges();
            return lecture;
        }
        public Lecture Update(Lecture lecture) //  atnaujinami paskaitos duomednys DB
        {
            _context.Lectures.Update(lecture);
            _context.SaveChanges();
            return lecture;
        }
        public IEnumerable<Lecture> GetAll() // gaunama paskaitu sarasas is DB
        {
            return _context.Lectures.ToList();
        }
        public IEnumerable<Lecture> GetAllWithDepartment() // gaunamas studentu sarasas is DB
        {
            return _context.Lectures.Include(d => d.Departments).ToList();
        }
        

        

    }
}
