using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using DataInterface;
using DataAccess;

namespace EFCT
{
    class Program
    {
        static void Main(string[] args)
        {
            IStudentManager studentManager = new StudentManager();
            studentManager.AddStudent("Adam", 15);
            studentManager.AddStudent("Bertil", 17);
            studentManager.AddStudent("Ceasar", 21);
            studentManager.AddStudent("David", 65);

            ITeacherManager teacherManager = new TeacherManager();
            teacherManager.AddTeacher("Kalle");

            ICourseManager courseManager = new CourseManager();
            courseManager.AddCourse("C#", "Kalle");
            courseManager.AddCourse("Datalagring med .Net", "Kalle");

            IExamManager examManager = new ExamManager();
            var exam1 = examManager.AddExam("C#", 0.4m);
            var exam2 = examManager.AddExam("C#", 0.6m);

            var question1 = examManager.AddQuestion(exam1, "Vad är ett interface?");
            var option1 = examManager.AddAnswerOption(question1, "En grej i C#", true);
            var option2 = examManager.AddAnswerOption(question1, "En sorts fisk", false);

            var question2 = examManager.AddQuestion(exam1, "Vad är en klass?");
            var option3 = examManager.AddAnswerOption(question2, "En annan grej i C#", true);
            var option4 = examManager.AddAnswerOption(question2, "En sorts insekt", false);

            var question3 = examManager.AddQuestion(exam2, "Vad är Entity Framework?");
            var option5 = examManager.AddAnswerOption(question3, "En ORM i C#", true);
            var option6 = examManager.AddAnswerOption(question3, "En sorts däggdjur", false);


            IExamAnswerManager examAnswerManager = new ExamAnswerManager();
            var examAnswer1 = examAnswerManager.AddExamAnswer("Adam", exam1);
            examAnswerManager.AddExamQuestionAnswer(examAnswer1, question1, option1);
            examAnswerManager.AddExamQuestionAnswer(examAnswer1, question2, option4);

            var examAnswer2 = examAnswerManager.AddExamAnswer("Adam", exam2);
            examAnswerManager.AddExamQuestionAnswer(examAnswer2, question2, option5);

            var examAnswer = examAnswerManager.GetAnswer("Adam", exam1);

            var examGrader = new ExamGrader();
            examGrader.GradeExam(examAnswer);
            Console.WriteLine("Klart.");
            Console.ReadLine();
            
        }
    }
}
