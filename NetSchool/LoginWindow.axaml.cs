using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media.Imaging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetSchool;

public partial class LoginWindow : Window
{
    public string[] stdnt = { "aaa", "bbb", "ccc" };
    Random rnd = new Random();
    public LoginWindow()
    {
        InitializeComponent();
        UserManager.AddUser("admin", "admin", "admin");
        foreach (var t in stdnt)
        {
            SchoolStuff.AddStudent(t, "a");
        }
        foreach (var s in SchoolStuff.students)
        {
            SchoolStuff.AddToSubject(s.FindMyId, rnd.Next(0, 2));
            foreach (var g in s._subjects)
            {
                SchoolStuff.AddGrade(s.FindMyId, Array.IndexOf(SchoolStuff.Subjects_List, g.Name), rnd.Next(2, 5), DateTime.UtcNow);
            }
        }
        SchoolStuff.AddStudent("Me", "a");
        SchoolStuff.AddToSubject(SchoolStuff.students.FirstOrDefault(s => s.Name == "Me").FindMyId, 1);
        SchoolStuff.AddGrade(SchoolStuff.students.FirstOrDefault(s => s.Name == "Me").FindMyId, 1, 5, DateTime.Parse("Jan 1, 2009"));
        SchoolStuff.AddGrade(SchoolStuff.students.FirstOrDefault(s => s.Name == "Me").FindMyId, 1, 5, DateTime.Parse("Jan 1, 2022"));
        
    }
    public void Comfirm(object? sender, RoutedEventArgs e)
    {
        if (UserManager.CheckUser(Name.Text, Password.Text))
        {
            new MainWindow().Show();
            this.Close();
        }

    }
}