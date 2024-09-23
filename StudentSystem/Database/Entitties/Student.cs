using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Database.Entitties
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StudentNumber { get; set; }
        public string Email {  get; set; }
        public IEnumerable<Lecture>? Lectures  { get; set; } = new List<Lecture>();
        public string? DepartmentCode { get; set; }
        public Department? Department { get; set; }
        
    }
}
