using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class ExamQuestionAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamQuestionAnswerID { get; set; }

        public int ExamAnswerID { get; set; }
        public ExamAnswer ExamAnswer { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }
        public int AnswerAlternativeID { get; set; }
        public AnswerAlternative AnswerAlternative { get; set; }
    }
}
