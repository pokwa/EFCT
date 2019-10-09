using DataAccess;
using DataInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCT
{
    public class ExamGrader
    {
        public void GradeExam(ExamAnswer examAnswer)
        {
            ITestResultManager testResultManager = new TestResultManager();
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
            testResultManager.SetTotalScore(testResult, (decimal)correct / (decimal)total);
        }
    }
}
