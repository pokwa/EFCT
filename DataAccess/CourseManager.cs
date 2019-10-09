using DataInterface;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class CourseManager : ICourseManager
    {
        public void AddCourse(string name, string teacherName)
        {
            var teacherManager = new TeacherManager();

            using(var schoolContext  = new SchoolContext())
            {
                var course = new Course();
                course.Name = name;
                course.TeacherID = teacherManager.GetTeacher(teacherName).TeacherID;
                schoolContext.Courses.Add(course);
                schoolContext.SaveChanges();
            }
        }

        public Course GetCourse(string courseName)
        {
            using var context = new SchoolContext();
            return (from c in context.Courses 
                    where c.Name == courseName 
                    select c).FirstOrDefault();
        }
    }
}
