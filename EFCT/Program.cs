using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace EFCT
{
    /*
    Vi har kurser i klassen Course och studenter i klassen Student
    Vi vill ha en begränsning så att studenter måste tillhöra en kurs
    Klasserna Course och Student definierar hur en rad i tabellerna 
    Courses och Students ska se ut. Dessa definieras i klassen schoolContext
    nedan
    */
    public class Course 
    {
        [Key] // Detta markerar att nästa property under detta (dvs. CourseID) är tabellens primärnyckel
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Detta gäller också
        // nästa property (CourseID), och motsvarar IDENTITY(1,1), dvs att detta fält räknas upp
        // automatiskt och inte ska sättas
        public int CourseID { get; set; } // Vår primärnyckel. 
        public string Name { get; set; } // En kolumn i tabellen med studenter som 
        // innehåller studentens namn. Lägg märke till att vi har tomma getters och setters
        // EntityFramework kräver detta på många ställen

        //Denna collection är en del av vår foreign key mellan Course och Student
        //dvs den begränsningen vi ville ha för att alla studenter måste ha en kurs
        //detta fält skapar också en relation mellan studenter och kurser, så att
        //vi kan fråga efter alla studenter som går en viss kurs. Se Main nexan
        public ICollection<Student> Students { get; set; }
    }
    public class Student
    {
        // Nyckel, på samma sätt som i Course
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        // Ett fält för studentens namn
        public string Name { get; set; }

        public int Age { get; set; }

        // Dessa två fält är för vår Foreign Key
        // Fältet CourseID bör vi aldrig sätta!
        public int CourseID { get; set; }
        // ... det sätts istället genom att vi tilldelar ett värde till denna 
        // variabel. Då kommer CourseID att uppdateras automatiskt. På så sätt
        // kopplar vi ihop studenter och kurser
        public Course Course { get; set; }
    }

    // En kontext motsvarar ungefär en databas
    // Lägg märke till att vi ärver från DbContext genom att skriva : 
    // arvet betyder att SchoolContext har alla metoder och egenskaper som DbContext har
    // och vi kan ändra på beteendet hos dem genom polymorfism, se override nedan
    public class SchoolContext : DbContext
    {
        // Detta är "addressen" till vår databas, så vi vet var vi ska läsa och skriva data
        private const string connectionString = "Server=localhost;Database=EFCore;Trusted_Connection=True;";

        // Denna metod är markerad med "override". Detta betyder att när andra metoder i 
        // basklassen (DbContext) anropar OnConfiguration så är det denna metod som kommer
        // att anropas, och inte den som finns i basklassen. Precis som när vi 
        // implementerade interface, så måste signaturen på metoden (return type, namn, och 
        // parameterlista) överensstämma exakt med vad som står i basklassen. Motsvarande
        // metod i basklassen måste också vara märkt "virtual"
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // i det här fallet anropar basklassen vår klass här för att få veta 
            // vilken databas den ska prata med
            optionsBuilder.UseSqlServer(connectionString);
        }
        // Ett DbSet motsvarar ungefär en tabell. DbSet är en Generic Type, dvs vi skriver
        // en typ till innanför <> efter DbSet. Denna typ är den som motsvarar en rad i tabellen
        // Detta kommer att bli en tabell som heter Students
        public DbSet<Student> Students { get; set; }
        // Här är en tabell till
        public DbSet<Course> Courses { get; set; }
    }
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
                Console.ReadLine();

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
