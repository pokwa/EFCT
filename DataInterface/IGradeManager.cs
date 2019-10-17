using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface IGradeManager
    {
        void SetGrade(string courseName, string grade, string studentName);
        List<GradeLimit> GetGradeLimits(string courseName);
    }
}
