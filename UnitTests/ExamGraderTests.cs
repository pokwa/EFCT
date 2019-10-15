using DataInterface;
using EFCT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class ExamGraderTests
    {
        private int AnswerAlternativeId;

        [TestMethod]
        public void TestExamGraderCorrectAnswer()
        {
            ExamAnswer answer = CreateExameAnswer(1, 0);
            var testResultManager = GradeExam(answer);
            testResultManager.Verify(c => 
                c.SetTotalScore(It.IsAny<DataInterface.TestResult>(), It.Is<decimal>(m => m == 1)), 
                Times.Once());
        }

        [TestMethod]
        public void TestExamGraderOneCorrectOneIncorrectAnswer()
        {
            ExamAnswer answer = CreateExameAnswer(1, 1);
            var testResultManager = GradeExam(answer);
            testResultManager.Verify(c => 
                c.SetTotalScore(It.IsAny<DataInterface.TestResult>(),  It.Is<decimal>(m => m == 0.5m)), 
                Times.Once());
        }

        private static Mock<ITestResultManager> GradeExam(ExamAnswer answer)
        {
            var examManagerMock = CreateExamManagerMock(answer);
            var testResultManager = CreateTestResultManagerMock();

            var examGrader = new ExamGrader(testResultManager.Object, examManagerMock.Object);
            examGrader.GradeExam("A", "B", 0);
            return testResultManager;
        }

        private static Mock<ITestResultManager> CreateTestResultManagerMock()
        {
            var testResultManager = new Mock<ITestResultManager>();
            testResultManager.Setup(m => m.AddTestResult(It.IsAny<ExamAnswer>()));
            testResultManager.Setup(m => m.SetTotalScore(It.IsAny<DataInterface.TestResult>(), It.IsAny<decimal>()));
            return testResultManager;
        }

        private static Mock<IExamAnswerManager> CreateExamManagerMock(ExamAnswer answer)
        {
            var examManagerMock = new Mock<IExamAnswerManager>();
            examManagerMock.Setup(m =>
                m.GetAnswer(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(answer);
            return examManagerMock;
        }

        private ExamAnswer CreateExameAnswer(int correct, int incorrect)
        {
            var answer = new ExamAnswer();
            answer.Exam = new Exam();
            answer.Exam.Questions = new List<Question>();
            answer.ExamQuestionAnswers = new List<ExamQuestionAnswer>();
            for (int i = 0; i < correct + incorrect; i++)
                CreateOneQuestionAnswerPair(correct, answer, i);
            return answer;
        }

        private void CreateOneQuestionAnswerPair(int numberOfCorrectAnswer, ExamAnswer answer, int currentAnswerIndex)
        {
            int studentAnswerId = AnswerAlternativeId;
            List<AnswerAlternative> answerAlternatives = CreateAnswerAlternatives(numberOfCorrectAnswer, currentAnswerIndex);

            answer.ExamQuestionAnswers.Add(
                new ExamQuestionAnswer { QuestionID = currentAnswerIndex, AnswerAlternativeID = studentAnswerId });
            answer.Exam.Questions.Add(
                new Question { AnswerAlternatives = answerAlternatives, QuestionID = currentAnswerIndex });
        }

        private List<AnswerAlternative> CreateAnswerAlternatives(int correct, int i)
        {
            var answerAlternatives = new List<AnswerAlternative>();
            CreateOneAnswerAlternative(i < correct, answerAlternatives);
            if (i >= correct)
                CreateOneAnswerAlternative(true, answerAlternatives);
            return answerAlternatives;
        }

        private void CreateOneAnswerAlternative(bool isCorrect, List<AnswerAlternative> answerAlternatives)
        {
            var answerAlternative = new AnswerAlternative();
            answerAlternative.IsCorrect = isCorrect;
            answerAlternative.AnswerAlternativeID = AnswerAlternativeId++;
            answerAlternatives.Add(answerAlternative);
        }
    }
}
