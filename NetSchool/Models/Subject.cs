using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSchool.Models
{
    public class Subject
    {
        public string Name
        { get; set; }
        public List<Grade> SubjectGrades
        { get => _subjectGrades; }
        public List<Grade> _subjectGrades = new List<Grade>();
    }
}
