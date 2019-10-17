using DataInterface;
using Microsoft.EntityFrameworkCore;
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

        public List<TestResult> GetAllTestResultsForOneStudentAndOneCourse(string courseName, string studentName)
        {
            using var context = new SchoolContext();
            return (from tr in context.TestResults
                    join ea in context.ExamAnswers
                    on tr.ExamAnswerID equals ea.ExamAnswerID
                    join e in context.Exams
                    on ea.ExamID equals e.ExamID
                    join c in context.Courses
                    on e.CourseID equals c.CourseID
                    join s in context.Students
                    on ea.StudentID equals s.StudentID
                    where c.Name == courseName && s.Name == studentName
                    select tr)
                        .Include(tr => tr.ExamAnswer)
                            .ThenInclude(ea => ea.Exam)
                    .ToList();
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
