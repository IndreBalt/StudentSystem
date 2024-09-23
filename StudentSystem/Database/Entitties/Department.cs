using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Database.Entitties
{
    public class Department
    {
        public string DepartmentCode {  get; set; }
        public string DepartmentName { get; set; }
        public IEnumerable<Student>? Students {  get; set; } = new List<Student>();
        public IEnumerable<Lecture>? Lectures { get; set; } = new List<Lecture>();
    }
}
