using DataInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class TeacherManager : ITeacherManager
    {
        public void AddTeacher(string teacherName)
        {
            using var context = new SchoolContext();
            var teacher = new Teacher();
            teacher.Name = teacherName;
            context.Teachers.Add(teacher);
            context.SaveChanges();
        }
    }
}
