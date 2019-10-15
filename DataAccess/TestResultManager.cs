using DataInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class TestResultManager : ITestResultManager
    {
        public void AddAnswer(TestResult testResult, AnswerAlternative answerAlternative, bool isCorrect)
        {
            var context = new SchoolContext();
            var testResultAnswer = new TestResultAnswer();
            testResultAnswer.AnswerAlternativeID = answerAlternative.AnswerAlternativeID;
            testResultAnswer.IsCorrect = answerAlternative.IsCorrect;
            testResultAnswer.TestResultID = testResult.TestResultID;
            context.TestResultAnswers.Add(testResultAnswer);
            context.SaveChanges();
        }

        public TestResult AddTestResult(ExamAnswer examAnswer)
        {
            var context = new SchoolContext();
            var testResult = new TestResult();
            testResult.ExamAnswerID = examAnswer.ExamAnswerID;
            context.TestResults.Add(testResult);
            context.SaveChanges();
            return testResult;
        }

        public void SetTotalScore(TestResult testResult, decimal score)
        {
            var context = new SchoolContext();
            var result = (from r in context.TestResults
                          where r.TestResultID == testResult.TestResultID
                          select r).FirstOrDefault();
            result.Score = score;
            context.SaveChanges();
        }
    }
}
