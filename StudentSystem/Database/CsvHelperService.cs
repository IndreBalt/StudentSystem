using StudentSystem.Database.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Database
{
    public static class CsvHelperService
    {
        public static List<Department> GetDepartmentsFromCsv()
        {
            List<Department> departments = new List<Department>();
            string filePath = "C:\\Users\\Indre\\source\\repos\\StudentSystem\\StudentSystem\\Database\\InitialData\\departments.csv";
            List<string> stringList =  File.ReadAllLines(filePath).Skip(1).ToList();//sudeda i string sarasa, praleidziant pirma eilute (nes tai stulpeliu antrastes)
            foreach (string stringDepartment in stringList)
            {
                List<string> values = stringDepartment.Split(',').ToList();
                Department department = new Department()
                {
                    DepartmentCode = values[0],
                    DepartmentName = values[1]
                };
                departments.Add(department);
            }    

            return departments;
        }
        public static List<DepartmentLectures> GetDepartmentLecturesFromCsv()
        {
            List<DepartmentLectures> departmentLectures = new List<DepartmentLectures>();
            string filePath = "C:\\Users\\Indre\\source\\repos\\StudentSystem\\StudentSystem\\Database\\InitialData\\department_lectures.csv";
            List<string> stringList = File.ReadAllLines(filePath).Skip(1).ToList();//sudeda i string sarasa, praleidziant pirma eilute (nes tai stulpeliu antrastes)
            foreach (string stringDepartmentLecture in stringList)
            {
                if (!string.IsNullOrWhiteSpace(stringDepartmentLecture))
                {
                    List<string> values = stringDepartmentLecture.Split(',').ToList();
                    DepartmentLectures departmenLecture = new DepartmentLectures()
                    {
                        DepartmentCode = values[0],
                        LectureName = values[1]
                    };
                    departmentLectures.Add(departmenLecture);
                }
              
            }

            return departmentLectures;
        }
        public static List<Lecture> GetLecturesFromCsv() 
        { 
            List<Lecture> lectures = new List<Lecture>();
            string filePath = "C:\\Users\\Indre\\source\\repos\\StudentSystem\\StudentSystem\\Database\\InitialData\\lectures.csv";
            List<string> stringList = File.ReadAllLines(filePath).Skip(1).ToList();//sudeda i string sarasa, praleidziant pirma eilute (nes tai stulpeliu antrastes)
            foreach (string stringLecture in stringList)
            {                
                List<string> values = stringLecture.Split(",").ToList();
                List<string> time = values[1].Split("-").ToList();
                Lecture lecture = new Lecture()
                {
                    LectureName = values[0],
                    LectureStartTime = TimeOnly.Parse(time[0]),
                    LectureEndTime = TimeOnly.Parse(time[1]),
                };
                lectures.Add(lecture);
            }
            return lectures;        
        }
        public static List<StudentLecture> GetStudentLecturesFromCsv()
        {
            List<StudentLecture> studentsLectures = new List<StudentLecture>();
            string filePath = "C:\\Users\\Indre\\source\\repos\\StudentSystem\\StudentSystem\\Database\\InitialData\\student_lectures.csv";
            List<string> stringList = File.ReadAllLines(filePath).Skip(1).ToList();//sudeda i string sarasa, praleidziant pirma eilute (nes tai stulpeliu antrastes)
            foreach (string stringStudentLecture in stringList)
            {
                if (!string.IsNullOrWhiteSpace(stringStudentLecture))
                {
                    List<string> values = stringStudentLecture.Split(',').ToList();
                    StudentLecture studentLecture = new StudentLecture()
                    {
                        StudentNumber = int.Parse(values[0]),
                        LectureName = values[1],
                    };
                    studentsLectures.Add(studentLecture);
                }
                    
            }  
            return studentsLectures;
        }
        public static List<Student> GetStudentsFromCsv()
        {
            List<Student> students = new List<Student>();
            string filePath = "C:\\Users\\Indre\\source\\repos\\StudentSystem\\StudentSystem\\Database\\InitialData\\students.csv";
            List<string> stringList = File.ReadAllLines(filePath).Skip(1).ToList();//sudeda i string sarasa, praleidziant pirma eilute (nes tai stulpeliu antrastes)
            foreach(string stringStudent in stringList)
            {
                List<string> values = stringStudent.Split(",").ToList();
                Student student = new Student()
                {
                    FirstName = values[0],
                    LastName = values[1],
                    StudentNumber = int.Parse(values[2]),
                    Email = values[3],
                    DepartmentCode = values[4],
                };
                students.Add(student);
            }
            return students;
        
        
        }
    }
}
