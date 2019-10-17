using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class GradeLimit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeLimitID { get; set; }

        public decimal Limit { get; set; }
        public string GradeLetter { get; set; }

        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
