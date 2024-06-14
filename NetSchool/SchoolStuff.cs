using System;
using System.Collections.Generic;
using System.Linq;
using NetSchool.Models;
using System.Text;
using System.Threading.Tasks;

namespace NetSchool
{
    public static class SchoolStuff
    {
        public static string[] Subjects_List = { "История", "Математика", "Информатика" };
        public static List<Student> students = new List<Student>();
        public static List<Response> ShownStudents = new List<Response>();

        public static void AddStudent(string name, string cur_class)
        {
            students.Add(new Student() { Name = name, Class = cur_class });
        }
        public static void AddToSubject(int student_id, int subject_name, string Teacher)
        {
            students[student_id]._subjects.Add( new Subject { Name = Subjects_List[subject_name], Teacher = Teacher });
        }
        public static void AddGrade(int student_id, int subject_name, int grade, DateTime? time)
        {
            students[student_id]._subjects.FirstOrDefault(sb => sb.Name == Subjects_List[subject_name])._subjectGrades.Add( new Grade() { grade = grade, time = time });
        }
        public static void Fill(string Class, string Subject)
        {
            ShownStudents = students.Where(s => s.Class == Class).Select(s => new Response() { name = s.Name }).ToList();
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
