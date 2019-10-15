using DataInterface;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class StudentManager : IStudentManager
    {
        public void AddStudent(string studentName, int age)
        {
            using(var schoolContext = new SchoolContext())
            {
                var student = new Student();
                student.Name = studentName;
                student.Age = age;
                schoolContext.Students.Add(student);
                schoolContext.SaveChanges();
            }
        }

        public Student GetStudentByName(string studentName)
        {
            using (var schoolContext = new SchoolContext())
            {
                return (from s in schoolContext.Students
                        where s.Name == studentName
                        select s).FirstOrDefault();
            }
        }
    }
}
