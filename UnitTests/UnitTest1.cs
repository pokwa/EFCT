using DataInterface;
using EFCT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestExamGrader()
        {
            var answer = new ExamAnswer();

            answer.Exam = new Exam();
            answer.Exam.Questions = new List<Question>();
            var answerAlternatives = new List<AnswerAlternative>();
            answerAlternatives.Add(new AnswerAlternative());
            answer.Exam.Questions.Add(new Question { AnswerAlternatives = answerAlternatives });
                   var examManagerMock = new Mock<IExamAnswerManager>();
            examManagerMock.Setup(m =>
                m.GetAnswer(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(answer);
            var testResultManager = new Mock<ITestResultManager>();
            testResultManager.Setup(m => m.AddTestResult(It.IsAny<ExamAnswer>()));
            var examGrader = new ExamGrader(testResultManager.Object,
                examManagerMock.Object);
            examGrader.GradeExam("A", "B");
        }
    }
}
