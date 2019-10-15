using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class TestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestResultID { get; set; }

        public decimal Score { get; set; }

        public int ExamAnswerID { get; set; }
        public ExamAnswer ExamAnswer {get; set;}

        public ICollection<TestResultAnswer> TestResultAnswers { get; set; }
    }
}
