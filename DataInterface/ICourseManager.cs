using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface ICourseManager
    {
        public void AddCourse(string courseName, string teacherName);

        public List<Student> GetStudentsInCourse(string courseName);
    }
}
