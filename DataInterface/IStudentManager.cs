using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface IStudentManager
    {
        public void AddStudent(string studentName, int age);
        public Student GetStudentByName(string studentName);
    }
}
