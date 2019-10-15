using DataInterface;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ExamAnswerManager : IExamAnswerManager
    {
        public ExamAnswer AddExamAnswer(string studentName, Exam exam)
        {
            var context = new SchoolContext();
            var examAnswer = new ExamAnswer();
            examAnswer.StudentID = (new StudentManager()).GetStudentByName(studentName).StudentID;
            examAnswer.ExamID = exam.ExamID;
            context.ExamAnswers.Add(examAnswer);
            context.SaveChanges();
            return examAnswer;
        }

        public void AddExamQuestionAnswer(ExamAnswer examAnswer, Question question, AnswerAlternative option)
        {
            var context = new SchoolContext();
            var examQuestionAnswer = new ExamQuestionAnswer();
            examQuestionAnswer.AnswerAlternativeID = option.AnswerAlternativeID;
            examQuestionAnswer.ExamAnswerID = examAnswer.ExamAnswerID;
            examQuestionAnswer.QuestionID = question.QuestionID;
            context.ExamQuestionAnswers.Add(examQuestionAnswer);
            context.SaveChanges();
        }

        public ExamAnswer GetAnswer(string studentName, string courseName)
        { 
            var context = new SchoolContext();

            var exam = 
            (from e in context.Exams
             join c in context.Courses
             on e.CourseID equals c.CourseID
             where c.Name == courseName
             select e).First();

            var studentID = (new StudentManager()).GetStudentByName(studentName).StudentID;
            var examAnswer =
                context.ExamAnswers.Where(ea =>
                    ea.ExamID == exam.ExamID &&
                    ea.StudentID == studentID).
                    Include(ea => ea.Exam)
                        .ThenInclude(e => e.Questions)
                    .Include(ea => ea.ExamQuestionAnswers)
                        .ThenInclude(qa => qa.AnswerAlternative)
                    .Include(ea => ea.Exam)
                        .ThenInclude(ex => ex.Questions)
                            .ThenInclude(q => q.AnswerAlternatives);
              
            return examAnswer.FirstOrDefault();
        }
    }
}
