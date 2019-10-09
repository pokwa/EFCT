using DataInterface;
using System.Linq;

namespace DataAccess
{
    public class TeacherManager : ITeacherManager
    {
        public void AddTeacher(string teacherName)
        {
            using var schoolContext = new SchoolContext();
            var teacher = new Teacher();
            teacher.Name = teacherName;
            schoolContext.Teachers.Add(teacher);
            schoolContext.SaveChanges();
        }

        public Teacher GetTeacher(string teacherName)
        {
            using var schoolContext = new SchoolContext();
            return (from t in schoolContext.Teachers
                    where t.Name == teacherName
                    select t).FirstOrDefault();
        }
    }
}
