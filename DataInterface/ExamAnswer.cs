using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class ExamAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamAnswerID { get; set; }

        public int ExamID { get; set; }
        public Exam Exam { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }

        public ICollection<ExamQuestionAnswer> ExamQuestionAnswers { get; set; }
    }
}
