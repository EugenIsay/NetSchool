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
        public static string[] Subjects_List = { "История", "Математика", "Информатика", "Русский язык", "Химия", "География", "Обществознаним" };
        public static List<string> Class_List = new List<string>();
        public static List<Student> students = new List<Student>();

        public static List<Response> ShownStudents = new List<Response>();

        public static void AddStudent(string name, string cur_class)
        {
            students.Add(new Student() { Name = name, Class = cur_class });
            if (!Class_List.Contains(cur_class))
                Class_List.Add(cur_class);
        }
        public static void AddToSubject(int student_id, int subject_name, string Teacher)
        {
            students[student_id]._subjects.Add( new Subject { Name = Subjects_List[subject_name], Teacher = Teacher });
        }
        public static void AddGrade(int student_id, int subject_id, int grade, DateTime? time)
        {
            students[student_id]._subjects.FirstOrDefault(sb => sb.Name == Subjects_List[subject_id])._subjectGrades.Add( new Grade() { grade = grade, time = time });
        }
        public static void Fill(int Class, int Subject)
        {
            List<Student> tmp = students.Where(s => s.Class == Class_List[Class]).ToList();
            List<Response> answer = new List<Response>();
            foreach (Student student in tmp)
            {
                if (student.Subjects.Where(s => s.Name == Subjects_List[Subject]).Count() != 0)
                {
                    answer.Add(new Response { name = student.Name, grades = student.Subjects.FirstOrDefault(s => s.Name == Subjects_List[Subject]).SubjectGrades });
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
