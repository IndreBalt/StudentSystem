using StudentSystem.Database.Entitties;
using StudentSystem.Database.Repositories.Interfaces;
using StudentSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    public class DepartmentsServise : IDepartmentsServise
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentsServise(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Department CreateDepartment(string departmentCode, string departmentName)
        {
            Department department = new Department()
            {
                DepartmentName = departmentName,
                DepartmentCode = departmentCode,
            };
            _departmentRepository.Create(department);
            return department;
        }
        public Department AddDepartmentStudents(Department department, List<Student> students)
        {
            department.Students.ToList().AddRange(students);
            _departmentRepository.Update(department);
            return department;
        }
        public void PrintAllDepartments()
        {
            List<Department> departments = _departmentRepository.GetAll().ToList();

            departments.ForEach(department =>
            {
                Console.WriteLine($"Kodas: {department.DepartmentCode}, Pavadinimas: {department.DepartmentName}");
            });
        }
        public Department GetDepartmentByCode(string code)
        {
            List<Department> departments = _departmentRepository.GetAllWithLectures().ToList();
            Department department = new Department();
            department = departments.Find(d => d.DepartmentCode == code);
            return department;
        }
        public List<Lecture> PrintDepartmentsLectures(Department department)
        {
            List<Department> departments = _departmentRepository.GetAllWithLectures();
            Department depart = departments.Find(d => d.DepartmentCode == department.DepartmentCode);
            List<Lecture> lectures = new List<Lecture>();
            lectures = depart.Lectures.ToList();
            foreach (Lecture l in lectures)
            {
                Console.WriteLine($"pamoka {l.LectureName}, {l.LectureStartTime} - {l.LectureEndTime}");
            }
            return lectures;
        }        
        public bool IfDepartmentExist(string departmentCode) //Tikrina ar paduotas i parametrus studentas jau yra DB
        {
            List<Department> existingDepartments = _departmentRepository.GetAll().ToList();
            foreach (Department department in existingDepartments)
            {
                if (department.DepartmentCode == departmentCode)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
