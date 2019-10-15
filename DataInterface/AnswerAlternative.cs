using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class AnswerAlternative
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerAlternativeID { get; set; }

        public int QuestionID { get; set; }
        public Question Question { get; set; }

        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
