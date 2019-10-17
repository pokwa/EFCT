using DataInterface;
using EFCT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class CourseGraderTests
    {
        [TestMethod]
        public void GradeOneStudentOneTest0Percent()
        {
            // Detta test ska testa att sätta betyg på
            // en student, som har gått en kurs, med ett
            // prov, där studenten har noll rätt

            // Här skapar vi upp våra mock-objekt
            // vi behöver dessa till konstruktorn av vår
            // CourseGrader. Vi mockar dessa då vi vill
            // kunna skicka in det testdata vi behöver för 
            // just det här testet, och för att vi ska slippa
            // prata med databasen, som är långsam
            var gradeManager = new Mock<IGradeManager>();
            var courseManager = new Mock<ICourseManager>();
            var testResultManager = new Mock<ITestResultManager>();

            // CourseGrader kommer att anropa GetStudentsInCourse,
            // som tar en sträng som är namnet på kursen som 
            // parameter, och returnerar en lista av studenter i
            // kursen.
            // It.IsAny<string>() betyder att det spelar ingen
            // roll vilken sträng som CourseGrader kommer att 
            // skicka in, och .Returns betyder att den metoden
            // då komer att returnera argumentet till .Returns,
            // i det här fallet en ny lista som innehåller en 
            // student som vi inte har satt någon information på.
            courseManager.Setup(cm =>
                cm.GetStudentsInCourse(It.IsAny<string>()))
                .Returns(new List<Student> { new Student()});

            // Detta gör samma sak som ovan, men med lite mer 
            // data. Vi vill ha tillbaka en lista med TestResult:s
            // I listan lägger vi till en TestResult, och på den
            // sätter vi en ny ExamAnswer, och på den en ny Exam.
            // Detta behöver vi för att det i CourseGrader står:
            // sum += examGrade.Score * examGrade.ExamAnswer.Exam.FractionOfGrade;
            // Detta innebär att vi läser ExamAnswer från TestResult 
            // och Exam från ExamAnswer, så om dessa inte är satta
            // kommer de att vara null när vi kör testet, och
            // vi kommer att få ett fel.
            testResultManager.Setup(trm =>
                trm.GetAllTestResultsForOneStudentAndOneCourse(
                    It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<DataInterface.TestResult>
                {
                    new DataInterface.TestResult
                    {
                        ExamAnswer = new ExamAnswer
                        {
                            Exam = new Exam()
                        }
                    }
                });

            // Så ger vi GetGradeLimits på vårt mockobjekt
            // lite data att skicka tillbaka när den anropas
            gradeManager.Setup(gm =>
                gm.GetGradeLimits(It.IsAny<string>()))
                .Returns(new List<GradeLimit>
                {
                    new GradeLimit
                    {
                        Limit = 0,
                        GradeLetter = "F"
                    }
                });

            // Nu skapar vi en CourseGrader, som är det objektet vi ska
            // testa. Vi skickar in .Object från våra mockobjekt, som 
            // är mockar som implementerar respektive interface. Det är
            // på detta sätt vi "styr bort" CourseGrader från databasen
            // och får den att använda våra mockobjekt istället
            var courseGrader = new CourseGrader(gradeManager.Object, courseManager.Object, testResultManager.Object);
            // Sen anropar vi metoden som vi vill testa
            courseGrader.GradeCourse("C#");
            // .Verify på ett mockobjekt motsvarar en Assert.
            // Här vill vi kontrollera att SetGrade har anropats en 
            // gång, med den första parametern satt till "C#", den
            // andra till "F", och den tredje bryr vi oss inte om 
            // vad den var satt till.
            gradeManager.Verify(
                gm => gm.SetGrade(
                    It.Is<string>(s => s == "C#"), 
                    It.Is<string>(g => g == "F"), 
                    It.IsAny<string>()),
                    Times.Once()
                );
        }
    }
}
