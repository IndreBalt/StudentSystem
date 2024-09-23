using StudentSystem.Database.Entitties;

namespace StudentSystem.Database.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Student Create(Student student);
        IEnumerable<Student> GetAll();
        IEnumerable<Student> GetAllWithDepartment();
        IEnumerable<Student> GetAllWithLectures();
        Student Update(Student student);
    }
}