using StudentSystem.Database;
using StudentSystem.Database.Entitties;
using StudentSystem.Database.Repositories;
using StudentSystem.Database.Repositories.Interfaces;
using StudentSystem.Presentation;
using StudentSystem.Services;
using StudentSystem.Services.Interfaces;


namespace StudentSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StudentSystemDbContext context = new StudentSystemDbContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();


            IDepartmentRepository departmentRepository = new DepartmentRepository(context);
            ILectureRepository lectureRepository = new LectureRepository(context);
            IStudentRepository studentRepository = new StudentRepository(context);

            IDepartmentsServise departmentsServise = new DepartmentsServise(departmentRepository);
            ILecturesService lecturesService = new LecturesService(lectureRepository);
            IStudentsService studentService = new StudentsService(studentRepository);

            
            UserUI ui = new UserUI(departmentsServise, studentService, lecturesService);
            ui.Run();

        }
    }
}
