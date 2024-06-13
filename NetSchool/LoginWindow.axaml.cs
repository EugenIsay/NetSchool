using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSchool;

public partial class LoginWindow : Window
{
    public string[] stdnt = { "aaa", "bbb", "ccc" };
    public LoginWindow()
    {
        InitializeComponent();
        if (SchoolStuff.students.Count() == 0)
        {
            foreach (string s in stdnt)
            {
                SchoolStuff.students.Add(new Student() { Name = s, Class = "A", Subjects = new List<Subject>() { new Subject("hstr", new Grade(4, DateTime.Now)) } });
            }
            SchoolStuff.Fill("A", "hstr");
        }
    }
    public void Comfirm(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }
}