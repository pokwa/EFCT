using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface IExamAnswerManager
    {
        ExamAnswer AddExamAnswer(string studentName, Exam exam);
        void AddExamQuestionAnswer(ExamAnswer examAnswer1, Question question1, AnswerAlternative option1);
        ExamAnswer GetAnswer(string studentName, string courseName);
    }
}
