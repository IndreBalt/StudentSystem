using StudentSystem.Database.Entitties;

namespace StudentSystem.Services.Interfaces
{
    public interface ILecturesService
    {
        Lecture AddLectureDepartment(Lecture lecture, Department department);
        Lecture CreateLecture(string lectureName, TimeOnly lectureStartTime, TimeOnly lectureEndTime, Department department);
        Lecture GetLectureByName(string lectureName);
        bool IfLectureExist(string lectureName);
        List<Lecture> PrintAllLectures();
    }
}