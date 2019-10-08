using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataInterface
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
}
