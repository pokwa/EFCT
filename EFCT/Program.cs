using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;

namespace EFCT
{
    class Program
    {
        static void Main(string[] args)
        {
            // schoolContext är vår anslutning till databasen
            // när vi har objekt som behöver göra extra arbete för att 
            // "avsluta" sig själva behöver vi ha dem i ett using-block
            // på det här sättet. Då kommer anslutningen till databasen
            // att stängas korrekt när variabeln går ur scope.
            using (var schoolContext = new SchoolContext())
            {
                // vi hittar på några kurser
                var course = new Course();
                course.Name = "Kurs"; // Kurserna har ett namn
                schoolContext.Courses.Add(course); //... och vi lägger till dem i kurslistan
                // Tabellen Courses i databasen kommer inte att uppdateras förrän vi gör
                // SaveChanges längre ner!

                var course2 = new Course();
                

                course2.Name = "Kurs 2";
                schoolContext.Courses.Add(course2);

                // Sen gör vi några studenter
                var student = new Student();
                student.Name = "Elev 1";
                student.Course = course; // Här kopplar vi ihop studenterna med kurserna
                // Eftersom vi har en foreign key måste vi sätta Course, annars kommer det 
                // inte att funka!
                schoolContext.Students.Add(student);

                var student2 = new Student();
                student2.Name = "Elev 2";
                student2.Course = course;
                schoolContext.Students.Add(student2);

                var student3 = new Student();
                student3.Name = "Elev 3";
                student3.Course = course2;
                schoolContext.Students.Add(student3);
                schoolContext.SaveChanges(); // Vi gör savechanges
                // en gång på slutet. Vi kan inte spara och sedan "spara om",
                // eftersom DbSet:en Students & Courses då kommer att ha fått
                // explicita primärnycklar, och de kan vi inte skriva till
                // databasen!

                // Detta, Linq-to-Sql, är ett sätt att hämta data via entity framework
                // Det ser ut som Sql, fast "baklänges". Detta motsvarar "SELECT" i SQL
                var studentsInCourseOne = (from s in schoolContext.Students
                                           join c in schoolContext.Courses
                                           on s.CourseID equals c.CourseID
                                           where c.Name == "Kurs"
                                           select s);

                // Sen loopar vi igenom listan vi läste ut ur databasen vi 
                // just läste ut, och skriver ut namnen
                foreach (var studentInClass in studentsInCourseOne)
                    Console.WriteLine(studentInClass.Name);

                
                // Nästa sätt att läsa ut data är detta, kallat Method Chaining
                // denna motsvarar också SELECT, och gör precis samma sak som
                // Linq-to-Sql-frågan ovan, det är bara två sätt att skriva samma sak!
                var students = schoolContext.Courses.
                    Include(c => c.Students).
                    Where(c => c.Name == "Kurs").
                    SelectMany(c => c.Students).
                    OrderBy(s => s.Age);


                Student youngestStudent = null;
                foreach (var c in students)
                {
                    if (youngestStudent == null || youngestStudent.Age > c.Age)
                        youngestStudent = c;
                    if (c.Name == "Elev 1") // Vill vi uppdatera ett fält vi har läst
                        c.Name = "ELEV!";   // kan vi ändra på det, förutsatt att det
                                            // kommer från databasen, sen gör vi SaveChanges
                                            // några rader längre ner
                    Console.WriteLine(c.Name);
                }
                schoolContext.SaveChanges();
                Console.ReadLine();
            }
        }
    }
}
