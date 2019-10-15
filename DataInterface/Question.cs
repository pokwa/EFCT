using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }

        public int ExamID { get; set; }
        public Exam Exam { get; set; }

        public string QuestionText { get; set; }

        public ICollection<AnswerAlternative> AnswerAlternatives { get; set; }
    }
}
