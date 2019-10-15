using DataInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ExamManager : IExamManager
    {
        public AnswerAlternative AddAnswerOption(Question question, string text, bool isCorrect)
        {
            using var context = new SchoolContext();
            var answerAlternative = new AnswerAlternative();
            answerAlternative.AnswerText = text;
            answerAlternative.IsCorrect = isCorrect;
            answerAlternative.QuestionID = question.QuestionID;
            context.AnswerAlternatives.Add(answerAlternative);
            context.SaveChanges();
            return answerAlternative;
        }

        public Exam AddExam(string courseName, decimal fractionOfGrade)
        {
            var courseManager = new CourseManager();
            using var context = new SchoolContext();
            var exam = new Exam();
            exam.CourseID = courseManager.GetCourse(courseName).CourseID;
            exam.FractionOfGrade = fractionOfGrade;
            context.Exams.Add(exam);
            context.SaveChanges();
            return exam;
        }

        public Question AddQuestion(Exam exam, string text)
        {
            var context = new SchoolContext();
            var question = new Question();
            question.ExamID = exam.ExamID;
            question.QuestionText = text;
            context.Questions.Add(question);
            context.SaveChanges();
            return question;
        }
    }
}
