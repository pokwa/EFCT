using DataInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCT
{
    public class CourseGrader
    {
        // För att vi ska kunna mocka våra managers skapar vi variabler
        // för dessa, så att vi kan sätta dem en gång, istället för 
        // att i koden göra t.ex. "gradeManager = new GradeManager();"
        IGradeManager gradeManager;
        ICourseManager courseManager;
        ITestResultManager testResultManager;

        // Här skickar vi in våra managers när klassen skapas. De ligger
        // i konstruktorn dels för att vi bara ska behöva skicka in dem
        // en gång, och dels för att detta garanterar att man inte kan 
        // glömma att sätta dessa.
        public CourseGrader(IGradeManager gradeManager, ICourseManager courseManager, ITestResultManager testResultManager)
        {
            // Här har vi ett fält och en parameter som har samma namn
            // för att sätta fältet refererar vi därför till this. för att
            // skilja dem åt
            this.gradeManager = gradeManager;
            this.courseManager = courseManager;
            this.testResultManager = testResultManager;
        }

        public void GradeCourse(string courseName)
        {
            // Vi ska sätta betyg på alla elever i en kurs, så vi hämtar
            // alla elever i kursen och loopar igenom dem
            var students = courseManager.GetStudentsInCourse(courseName);
            foreach(var student in students)
            {
                // Sen kollar vi på alla prov som studenten har skrivit
                // i den kursen
                var examGrades = testResultManager.GetAllTestResultsForOneStudentAndOneCourse(courseName, student.Name);
                // Vi ska summera resultatet från alla proven, så vi börjar
                // med en summa på noll, sen loopar vi igenom alla proven
                decimal sum = 0;
                foreach(var examGrade in examGrades)
                {
                    // För varje prov lägger vi till elevens resultat
                    // multiplicerat med det provets andel av totalen
                    sum += examGrade.Score * examGrade.ExamAnswer.Exam.FractionOfGrade;
                }
                // Sen hämtar vi betygsgränserna
                // Vi vill ha det minsta resultatet som krävs för att
                // få ett visst betyg i GradeLimit.Limit, och motsvarande
                // betyg i GradeLetter. Vi tar därför och väljer alla betyg
                // som är mindre än eller lika med elevens resultat, och väljer
                // det högsta genom att sortera dem i fallande ordning och
                // ta det första
                var gradeLimits = gradeManager.GetGradeLimits(courseName);
                string gradeLetter = gradeLimits.Where(
                    gl => gl.Limit <= sum)
                    .OrderByDescending(gl => gl.Limit)
                    .First().GradeLetter;
                // Till slut sätter vi elevens betyg
                gradeManager.SetGrade(courseName, gradeLetter, student.Name);
            }
        }
    }
}
