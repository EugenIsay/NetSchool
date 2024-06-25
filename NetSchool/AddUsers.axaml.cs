using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;

namespace NetSchool;

public partial class AddUsers : Window
{
    public AddUsers()
    {
        InitializeComponent();
    }
    private void AddStud(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SchoolStuff.AddStudent(StudName.Text, StudClass.Text);
        foreach (var r in StudSubj.Items)
        {
            string a = r.ToString();
            //SchoolStuff.AddToSubject(SchoolStuff.students.FirstOrDefault().FindMyId, a);
        }
    }
    private void AddTeach(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }
}