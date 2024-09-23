using StudentSystem.Database.Entitties;

namespace StudentSystem.Services.Interfaces
{
    public interface IDepartmentsServise
    {
        Department AddDepartmentStudents(Department department, List<Student> students);
        Department CreateDepartment(string departmentCode, string departmentName);
        Department GetDepartmentByCode(string code);
        bool IfDepartmentExist(string departmentCode);
        void PrintAllDepartments();
        List<Lecture> PrintDepartmentsLectures(Department department);
    }
}