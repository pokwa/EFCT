using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class TestResultAnswers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestResultAnswersID { get; set; }

        public int TestResultID { get; set; }
        public TestResult TestResult { get; set; }

        public int AnswerAlternativeID { get; set; }
        public AnswerAlternative AnswerAlternative { get; set; }
   
        public bool IsCorrect { get; set; }
    }
}
