using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSchool;

public partial class AddUsers : Window
{

    public AddUsers()
    {
        InitializeComponent();
        List<GetSubg> Lst = SchoolStuff.Subjects_List.Select(s => new GetSubg { Name = s, Add = false }).ToList();
        StudSubj.ItemsSource = Lst.ToList();

        List<char> Alphabet = new List<char>();
        for (int i = 1040; i < 1072; i++)
        {
            Alphabet.Add((char)i);
            if (i == 1045)
                Alphabet.Add((char)1025);
        }
        classLetter.ItemsSource = Alphabet;
        List<int> numbers = Enumerable.Range(1, 11).ToList();
        classNum.ItemsSource = numbers;
    }
    private void AddStud(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        //SchoolStuff.AddStudent(StudName.Text, StudClass.Text);
        foreach (GetSubg subj in StudSubj.Items)
        {
            SchoolStuff.AddToSubject(SchoolStuff.students.FirstOrDefault().FindMyId, Array.IndexOf(SchoolStuff.Subjects_List.ToArray(), subj.Name));
        }
    }
    private void AddTeach(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }
}
public class GetSubg
{
    public string Name { get; set; }
    public bool Add { get; set; }
}