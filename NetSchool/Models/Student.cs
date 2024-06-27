using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSchool.Models
{
    public class Student
    {
        public string Name
        { get; set; }
        public string Class
        { get; set; }
        public string str
        {
            get {
                string Description = "";
                foreach (Subject sub in _subjects)
                {
                    Description += $" {sub.Name}";
                    List<int> g = sub._subjectGrades.Select(s => s.grade).ToList();
                    if (g.Count() != 0)
                        Description += $" {g.AsQueryable().Average()} \n";
                }
                return Description; 
            }
        }
        public List<Subject> Subjects
        { get => _subjects; set => _subjects = value; }
        public List<Subject> _subjects = new List<Subject>();
        public int FindMyId { get => SchoolStuff.students.FindIndex(s => s.Name == Name && s.Class == Class && s.Subjects == _subjects); }
        public void Delete()
        {
            SchoolStuff.students.RemoveAt(FindMyId);
        }
    }
}
