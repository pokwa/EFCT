using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface IExamManager
    {
        public Exam AddExam(string courseName, decimal fractionOfGrade);
        Question AddQuestion(Exam exam1, string v);
        AnswerAlternative AddAnswerOption(Question question1, string v1, bool v2);
    }
}
