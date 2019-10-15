﻿using DataInterface;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class CourseManager : ICourseManager
    {
        public void AddCourse(string courseName)
        {
            using(var schoolContext = new SchoolContext())
            {
                var course = new Course();
                course.Name = courseName;
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
    }
}
