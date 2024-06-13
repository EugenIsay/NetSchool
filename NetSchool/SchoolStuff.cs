using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSchool
{
    public static class SchoolStuff
    {
        public static string[] sbjct = { "hstr", "math" };
        public static List<Student> students = new List<Student>();
        public static List<Response> ShownStudents = new List<Response>();

        public static void Fill(string Class, string Subject)
        {
            ShownStudents = students.Where(s => s.Class == Class)
                .Select(s => new Response { name = s.Name,
                grades = s.Subjects.FirstOrDefault(g => g.Name == Subject).SubjectGrades
                }).ToList();
        }
    }

    public class Teacher
    {
        public string Name 
        { get; set; }
    }
    public class Student
    {
        public string Name 
        { get; set; }
        public string Class 
        { get; set; }
        public List<Subject> Subjects
        { get => _subjects; set => _subjects = value; }
        List<Subject> _subjects = new List<Subject>();
        public Student(string name, Subject Subject) { this.Name = name; _subjects.Add(Subject); }
        public Student() { }
    }
    public class Subject
    {
        public string Name 
        { get; set; }
        public string Teacher 
        { get; set; }

        public List<Grade> SubjectGrades
        {
            get => _subjectGrades;
        }
        public List<Grade> _subjectGrades = new List<Grade>();
        public Subject(string str, Grade grade) { this.Name = str; _subjectGrades.Add(grade);  }
        public void AddGrades(int Grade, DateTime Time)
        {
            _subjectGrades.Add(new Grade(Grade, Time));
        }
    }

    public class Grade
    {
        public int grade
        { get; set; }
        public DateTime time
        { get; set; }
        public Grade()
        { }
        public Grade(int Grade, DateTime Time)
        {
            this.grade = Grade;
            this.time = Time;
        }

    }
    public class Response
    {
        public string name
        { get; set; }
        public List<Grade> grades 
        { get; set; }
    }
}
