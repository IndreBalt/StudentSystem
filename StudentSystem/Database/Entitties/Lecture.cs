using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Database.Entitties
{
    public class Lecture
    {
        public string LectureName { get; set; }
        public TimeOnly LectureStartTime { get; set; }
        public TimeOnly LectureEndTime { get; set; }
        public IEnumerable<Student>? Students { get; set; }
        public IEnumerable<Department>? Departments { get; set; } = new List<Department>();
    }
}
