using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSchool.Models
{
    public class Teacher
    {
        public string Name
        { get; set; }
        public string Description 
        {
            get { return string.Join(" ", Subjects); }
        }
        public List<string> Subjects
        { get; set; }
        public int FindMyId
        {
            get => SchoolStuff.teachers.IndexOf(SchoolStuff.teachers.FirstOrDefault(t => t.Name == Name&& t.Subjects == Subjects));
        }
        public void Delete()
        {
            UserManager.Users.Remove(UserManager.Users.FirstOrDefault(t => t.Name == Name && t.Role == "teacher"));
            SchoolStuff.teachers.RemoveAt(FindMyId);
        }
    }
}
