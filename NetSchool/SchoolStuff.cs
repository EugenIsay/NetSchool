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
        public static List<DateTime?> GetDates()
        {
            List<DateTime?> dates = new List<DateTime?>();
            foreach (var s in ShownStudents)
            {
                foreach (var t in s.grades)
                {
                    dates.Add(t.time.Value.Date.Date);
                }
            }
            dates = dates.Distinct().ToList();
            return dates;

        }

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
            List<Student> tmp = students.Where(s => s.Class == Class).ToList();
            List<Response> answer = new List<Response>();
            foreach (Student student in tmp)
            {
                if (student.Subjects.Where(s => s.Name == Subject).Count() != 0)
                {
                    answer.Add(new Response { name = student.Name, grades = student.Subjects.FirstOrDefault(s => s.Name == Subject).SubjectGrades });
                }
            }
            ShownStudents = answer;
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
