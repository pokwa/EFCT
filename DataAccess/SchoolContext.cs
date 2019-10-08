using DataInterface;
using Microsoft.EntityFrameworkCore;

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
        // Ett DbSet motsvarar ungefär en tabell. DbSet är en Generic Type, dvs vi skriver
        // en typ till innanför <> efter DbSet. Denna typ är den som motsvarar en rad i tabellen
        // Detta kommer att bli en tabell som heter Students
        public DbSet<Student> Students { get; set; }
        // Här är en tabell till
        public DbSet<Course> Courses { get; set; }
    }
}
