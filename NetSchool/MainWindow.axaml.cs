using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSchool;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        List<List<string>> Temp = new List<List<string>>();
        var time = SchoolStuff.GetDates().OrderBy(t => t);
        Temp.Add(new List<string>());
        Temp[0].Add("Name");
        foreach (var t in time)
        {
            Temp[0].Add(t.Value.ToShortDateString());
        }
        Temp.Add(new List<string>());
        int i = 1;
        foreach (var student in SchoolStuff.ShownStudents)
        {
            Temp.Add(new List<string>());
            Temp[i].Add(student.name);
            for (int j = 0; j < time.Count(); j++)
            {
                Temp[i].Add("");
            }
            foreach (var grade in student.grades)
            {
                var c = Array.IndexOf(time.ToArray(), grade.time.Value.Date) + 1;
                var aaa = grade.time.Value.ToShortDateString();
                if (c != 0)
                {
                    Temp[i][c] = (grade.grade.ToString());
                }
            }
            i++;
        }

        MainDataGrid.ItemsSource = Temp;
        for (int j = 0; j < Temp[0].Count(); j++)
        {
            MainDataGrid.Columns.Add(new DataGridTextColumn() { Binding = new Binding($"[{j}]") });
        }
        var a = SchoolStuff.students;

    }
}