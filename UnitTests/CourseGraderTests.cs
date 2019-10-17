using DataInterface;
using EFCT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class CourseGraderTests
    {
        [TestMethod]
        public void GradeOneStudentOneTest0Percent()
        {
            var gradeManager = new Mock<IGradeManager>();
            var courseManager = new Mock<ICourseManager>();
            var testResultManager = new Mock<ITestResultManager>();

            courseManager.Setup(cm =>
                cm.GetStudentsInCourse(It.IsAny<string>()))
                .Returns(new List<Student> { new Student()});

            testResultManager.Setup(trm =>
                trm.GetAllTestResultsForOneStudentAndOneCourse(
                    It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<DataInterface.TestResult>
                {
                    new DataInterface.TestResult
                    {
                        ExamAnswer = new ExamAnswer
                        {
                            Exam = new Exam()
                        }
                    }
                });
            gradeManager.Setup(gm =>
                gm.GetGradeLimits(It.IsAny<string>()))
                .Returns(new List<GradeLimit>
                {
                    new GradeLimit
                    {
                        Limit = 0,
                        GradeLetter = "F"
                    }
                });

            var courseGrader = new CourseGrader(gradeManager.Object, courseManager.Object, testResultManager.Object);
            courseGrader.GradeCourse("C#");
            gradeManager.Verify(
                gm => gm.SetGrade(
                    It.Is<string>(s => s == "C#"), 
                    It.Is<string>(g => g == "F"), 
                    It.IsAny<string>()),
                    Times.Once()
                );
        }
    }
}
