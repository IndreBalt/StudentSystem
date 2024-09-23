using StudentSystem.Database.Entitties;

namespace StudentSystem.Database.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Department Create(Department department);
        List<Department> GetAll();
        List<Department> GetAllWithLectures();
        List<Department> GetAllWithStudents();
        Department Update(Department department);
    }
}