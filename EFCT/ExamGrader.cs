using DataAccess;
using DataInterface;
using System.Linq;

namespace EFCT
{
    public class ExamGrader
    {
        private ITestResultManager testResultManager;
        private IExamAnswerManager examAnswerManager;
        public ExamGrader(ITestResultManager testResultManager, IExamAnswerManager examAnswerManager)
        {
            this.testResultManager = testResultManager;
            this.examAnswerManager = examAnswerManager;
        }
        public void GradeExam(string studentName, string courseName)
        {
            var examAnswer = examAnswerManager.GetAnswer(studentName, courseName);
            var testResult = testResultManager.AddTestResult(examAnswer);
            int correct = 0;
            int total = 0;
            foreach(var question in examAnswer.Exam.Questions)
            {
                var studentAnswer = examAnswer.ExamQuestionAnswers.FirstOrDefault(eqa => 
                    eqa.QuestionID == question.QuestionID);
                var isCorrect = studentAnswer.AnswerAlternativeID == 
                    question.AnswerAlternatives.FirstOrDefault(a => a.IsCorrect)?.AnswerAlternativeID;
                testResultManager.AddAnswer(testResult, studentAnswer.AnswerAlternative, isCorrect);
                if (isCorrect)
                    correct++;
                total++;
            }
            testResultManager.SetTotalScore(testResult, (decimal)correct / total);
        }
    }
}
