using StudentSystem.Database.Entitties;

namespace StudentSystem.Services.Interfaces
{
    public interface IStudentsService
    {
        Student AddStudentDepartmentAndDepartmentLectures(Student student, Department department);
        Student AddStudentLectures(Student student, List<Lecture> lectures);
        Student CreateStudent(string firstName, string lastName, int studentNumber, string email, string departmentCode, List<Lecture> lectures);
        Student GetStudentByNumber(int studentNumber);
        bool IfStudentExist(int studentNumber);
        List<Student> PrintAllStudents();
        List<Student> PrintAllStudentsByDepartment(Department department);
        List<Student> PrintAllStudentsWithDepartment();
        List<Lecture> PrintStudentLectures(int studentNumber);
    }
}