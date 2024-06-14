using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSchool;

public partial class LoginWindow : Window
{
    public string[] stdnt = { "aaa", "bbb", "ccc" };
    Random rnd = new Random();
    public LoginWindow()
    {
        InitializeComponent();
        foreach (var t in stdnt)
        {
            SchoolStuff.AddStudent(t, "a");
        }
        foreach (var s in SchoolStuff.students)
        {
            SchoolStuff.AddToSubject(s.FindMyId, rnd.Next(0, 2), "Me");
            foreach (var g in s._subjects)
            {
                SchoolStuff.AddGrade(s.FindMyId, Array.IndexOf(SchoolStuff.Subjects_List, g.Name), rnd.Next(2, 5), null);
            }
        }
        SchoolStuff.Fill("a", "Математика");
    }
    public void Comfirm(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }
}