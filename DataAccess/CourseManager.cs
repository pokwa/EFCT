using DataInterface;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class CourseManager : ICourseManager
    {
        public void AddCourse(string courseName, string teacherName)
        {
            using(var schoolContext = new SchoolContext())
            {
                var teacher = (from t in schoolContext.Teachers
                               where t.Name == teacherName
                               select t).First();

                var course = new Course();
                course.Name = courseName;
                course.TeacherID = teacher.TeacherID;
                schoolContext.Courses.Add(course);
                schoolContext.SaveChanges();
            }
        }

        public Course GetCourse(string courseName)
        {
            using (var schoolContext = new SchoolContext())
            {
                return (from c in schoolContext.Courses
                        where c.Name == courseName
                        select c).First();
            }
        }

        public List<Student> GetStudentsInCourse(string courseName)
        {
            using var context = new SchoolContext();
            return (from s in context.Students
                    join sc in context.StudentCourses
                    on s.StudentID equals sc.StudentID
                    join c in context.Courses
                    on sc.CourseID equals c.CourseID
                    where c.Name == courseName
                    select s
                ).ToList();
        }
    }
}
