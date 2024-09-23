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
    public class LecturesService : ILecturesService
    {
        private ILectureRepository _lectureRepository;
        public LecturesService(ILectureRepository lectureRepository)
        {

            _lectureRepository = lectureRepository;
        }

        public Lecture CreateLecture(string lectureName, TimeOnly lectureStartTime, TimeOnly lectureEndTime, Department department)
        {
            Lecture lecture = new Lecture()
            {
                LectureName = lectureName,
                LectureStartTime = lectureStartTime,
                LectureEndTime = lectureEndTime,
                
            };
            List <Department> lectureDepartments = new List<Department>();
            lectureDepartments = lecture.Departments.ToList();
            lectureDepartments.Add(department);
            lecture.Departments = lectureDepartments;
            _lectureRepository.Create(lecture);
            return lecture;
        }
        public Lecture AddLectureDepartment(Lecture lecture, Department department)
        {
            List<Department> departmentList = new List<Department>();
            departmentList = lecture.Departments.ToList();
            departmentList.Add(department);
            lecture.Departments = departmentList;
            _lectureRepository.Update(lecture);
            return lecture;
        }
        public List<Lecture> PrintAllLectures()
        {
            List<Lecture> lectures = _lectureRepository.GetAll().ToList();
            foreach (var lecture in lectures)
            {
                Console.WriteLine($"Pavadinimas: {lecture.LectureName}, Laikas: {lecture.LectureStartTime} - {lecture.LectureEndTime}");
            }
            return lectures;
        }
        public Lecture GetLectureByName(string lectureName)
        {
            List<Lecture> lectures = _lectureRepository.GetAll().ToList();
            Lecture lecture = new Lecture();
            lecture = lectures.Find(d => d.LectureName == lectureName);
            return lecture;
        }
        public bool IfLectureExist(string lectureName) //Tikrina ar paduota i parametrus paskaita jau yra DB
        {
            List<Lecture> existingLectures = _lectureRepository.GetAll().ToList();
            foreach (Lecture existingLecture in existingLectures)
            {
                if (existingLecture.LectureName == lectureName)
                {
                    return true;
                }
            }
            return false;
        }
      
    }
}
