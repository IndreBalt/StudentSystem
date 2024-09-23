using StudentSystem.Database.Entitties;
using StudentSystem.Database.Repositories;
using StudentSystem.Database.Repositories.Interfaces;
using StudentSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    public class StudentsService : IStudentsService
    {
        private IStudentRepository _studentRepository;
        public StudentsService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public Student CreateStudent(string firstName, string lastName, int studentNumber, string email, string departmentCode, List<Lecture> lectures)
        {
            Student student = new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                StudentNumber = studentNumber,
                Email = email,
                DepartmentCode = departmentCode
                
            };
            student.Lectures = lectures;

            _studentRepository.Create(student);
            return student;
        }
        public Student AddStudentDepartmentAndDepartmentLectures(Student student, Department department) //pakeiciamas departamentas ir pakeiciamos paskaitos
        {
            student.DepartmentCode = department.DepartmentCode;
            student.Lectures = department.Lectures;
            _studentRepository.Update(student);
            return student;
        }
        public Student AddStudentLectures(Student student, List<Lecture> lectures)
        {
            List<Lecture> studentLectures = new List<Lecture>();
            studentLectures = student.Lectures.ToList();
            studentLectures.AddRange(lectures);
            student.Lectures = studentLectures;
            _studentRepository.Update(student);
            return student;
        }
        public bool IfStudentExist(int studentNumber) //Tikrina ar paduotas i parametrus studentas jau yra DB
        {
            List<Student> existingStudents = _studentRepository.GetAll().ToList();
            foreach (Student existingStudent in existingStudents)
            {
                if (existingStudent.StudentNumber == studentNumber)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Student> PrintAllStudentsWithDepartment()
        {
            List<Student> allStudents = _studentRepository.GetAllWithDepartment().ToList();
            foreach (Student student in allStudents)
            {
                Console.WriteLine($"Vardas: {student.FirstName}, Pavarde: {student.LastName}, " +
                    $"Kodas: {student.StudentNumber}, Departamentas {student.Department.DepartmentName}");
            }
            return allStudents;
        }
        public List<Student> PrintAllStudents()
        {
            List<Student> allStudents = _studentRepository.GetAll().ToList();
            foreach (Student student in allStudents)
            {
                Console.WriteLine($"Vardas: {student.FirstName}, Pavarde: {student.LastName}");
            }
            return allStudents;
        }
        public List<Student> PrintAllStudentsByDepartment(Department department)
        {
            List<Student> allStudents = _studentRepository.GetAll().Where(d => d.DepartmentCode == department.DepartmentCode).ToList();
            foreach (Student student in allStudents)
            {
                Console.WriteLine($"Vardas: {student.FirstName}, Pavarde: {student.LastName}, Kodas: {student.StudentNumber}");
            }
            return allStudents;
        }
        public Student GetStudentByNumber(int studentNumber)
        {
            List<Student> students = _studentRepository.GetAll().ToList();
            Student student = new Student();
            student = students.Find(d => d.StudentNumber == studentNumber);
            return student;
        }
        public List<Lecture> PrintStudentLectures(int studentNumber)
        {
            List<Student> students = _studentRepository.GetAllWithLectures().ToList();
            List<Lecture> studentLectures = new List<Lecture>();
            Student student = students.Find(s => s.StudentNumber == studentNumber);
            studentLectures = student.Lectures.ToList();
            foreach (Lecture l in studentLectures)
            {
                Console.WriteLine($"{l.LectureName}, {l.LectureStartTime} - {l.LectureEndTime}");
            }
            return studentLectures;
        }

    }
}
