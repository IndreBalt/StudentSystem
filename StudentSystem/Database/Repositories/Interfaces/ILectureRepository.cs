using StudentSystem.Database.Entitties;

namespace StudentSystem.Database.Repositories.Interfaces
{
    public interface ILectureRepository
    {
        Lecture Create(Lecture lecture);
        IEnumerable<Lecture> GetAll();
        IEnumerable<Lecture> GetAllWithDepartment();
        Lecture Update(Lecture lecture);
    }
}