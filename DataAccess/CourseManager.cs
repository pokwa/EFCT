using DataInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class CourseManager
    {
        public void AddCourse(string name)
        {
            using(var schoolContext  = new SchoolContext())
            {
                var course = new Course();
                course.Name = name;
                schoolContext.Courses.Add(course);
                schoolContext.SaveChanges();
            }
        }
    }
}
