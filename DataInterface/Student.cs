using System.Collections.Generic;
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

        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<ExamAnswer> ExamAnswers { get; set; }
    }
}
