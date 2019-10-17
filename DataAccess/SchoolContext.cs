using DataInterface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace DataAccess
{
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            var studentCourseFk = modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetForeignKeys().Where(
                    fk => fk.DeclaringEntityType.ClrType.Name == "ExamQuestionAnswer" &&
                    (
                    fk.DependentToPrincipal.ClrType.Name == "ExamAnswer" ||
                    fk.DependentToPrincipal.ClrType.Name == "Question"
                    )));
            foreach(var fk in studentCourseFk)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            var testResultAnswersTestResult = modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetForeignKeys().Where(
                    fk => fk.DeclaringEntityType.ClrType.Name == "TestResultAnswer" &&
                    fk.DependentToPrincipal.ClrType.Name == "TestResult")).First();
            testResultAnswersTestResult.DeleteBehavior = DeleteBehavior.Restrict;
        }
        // Ett DbSet motsvarar ungefär en tabell. DbSet är en Generic Type, dvs vi skriver
        // en typ till innanför <> efter DbSet. Denna typ är den som motsvarar en rad i tabellen
        // Detta kommer att bli en tabell som heter Students
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        // Här är en tabell till
        public DbSet<Course> Courses { get; set; }
        public DbSet<AnswerAlternative> AnswerAlternatives { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamAnswer> ExamAnswers { get; set; }
        public DbSet<ExamQuestionAnswer> ExamQuestionAnswers { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<TestResultAnswer> TestResultAnswers { get; set; }

        public DbSet<GradeLimit> GradeLimits { get; set; }
    }
}
