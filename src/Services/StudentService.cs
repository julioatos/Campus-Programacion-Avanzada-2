using EFAndLinqPractice_SchoolAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EFAndLinqPractice_SchoolAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly SchoolContext _dbContext;

        public StudentService(SchoolContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Student Create(Student student)
        {

            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            return student;
        }

        public Student GetById(int id)
        {
            return _dbContext.Students.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _dbContext.Students.ToList();
        }

        public IEnumerable<Student> GetStudentsByCourseId(int courseId)
        {
            var students = from student in _dbContext.Students
                           where student.Courses.Any(course => course.Id == courseId)
                           select student;
            return students.ToList();

        }

        public void DeleteById(int id)
        {
            var student = GetById(id);
            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();
        }
    }
}