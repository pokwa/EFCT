using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamID { get; set; }

        public int CourseID { get; set; }
        public Course Course { get; set; }

        public decimal FractionOfGrade { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<ExamAnswer> ExamAnswers { get; set; }
    }
}
