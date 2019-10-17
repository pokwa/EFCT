using DataInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCT
{
    public class CourseGrader
    {
        IGradeManager gradeManager;
        ICourseManager courseManager;
        ITestResultManager testResultManager;

        public CourseGrader(IGradeManager gradeManager, ICourseManager courseManager, ITestResultManager testResultManager)
        {
            this.gradeManager = gradeManager;
            this.courseManager = courseManager;
            this.testResultManager = testResultManager;
        }

        public void GradeCourse(string courseName)
        {
            var students = courseManager.GetStudentsInCourse(courseName);
            foreach(var student in students)
            {
                var examGrades = testResultManager.GetAllTestResultsForOneStudentAndOneCourse(courseName, student.Name);
                decimal sum = 0;
                foreach(var examGrade in examGrades)
                {
                    sum += examGrade.Score * examGrade.ExamAnswer.Exam.FractionOfGrade;
                }
                var gradeLimits = gradeManager.GetGradeLimits(courseName);
                string gradeLetter = gradeLimits.Where(
                    gl => gl.Limit <= sum)
                    .OrderByDescending(gl => gl.Limit)
                    .First().GradeLetter;
                gradeManager.SetGrade(courseName, gradeLetter, student.Name);
            }
        }
    }
}
