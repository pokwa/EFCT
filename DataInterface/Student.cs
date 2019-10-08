using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataInterface
{
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
}
