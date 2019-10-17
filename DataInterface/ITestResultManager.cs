using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface ITestResultManager
    {
        TestResult AddTestResult(ExamAnswer examAnswer);
        void AddAnswer(TestResult testResult, AnswerAlternative answerAlternative, bool isCorrect);
        void SetTotalScore(TestResult testResult, decimal v);
        List<TestResult> GetAllTestResultsForOneStudentAndOneCourse(string courseName, string name);
    }
}
