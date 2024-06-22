using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using NetSchool.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NetSchool;

public partial class MainWindow : Window
{
    int CurMonth = DateTime.Now.Month;
    int CurYear = DateTime.Now.Year;
    int SelectedSubject = 0;
    int SelectedClass = 0;
    public MainWindow()
    {
        InitializeComponent();
        SelectSubject.ItemsSource = SchoolStuff.Subjects_List.ToList();
        SelectClass.ItemsSource = SchoolStuff.Class_List.ToList();
        int SelectedSubject = 1;
        int SelectedClass = 0;
        SelectSubject.SelectedIndex = SelectedSubject;
        SelectClass.SelectedIndex = SelectedClass;
        FillGrid(CurMonth, CurYear);
    }
    public void FillGrid(int month, int year)
    {
        MainDataGrid.Columns.Clear();
        MonthName.Text = new DateTime(year, month, 1).ToString("MMMM/yyyy");
        List<List<string>> Temp = new List<List<string>>();
        List<string> header = new List<string>();
        header.Add("Имя");
        int numDays = DateTime.DaysInMonth(year, month);
        for (int i = 1; i < numDays + 1; i++)
        {
            header.Add($"{i}");
        }
        int n = 0;
        foreach (var student in SchoolStuff.ShownStudents)
        {
            Temp.Add(new List<string>());
            Temp[n] = Enumerable.Repeat("", numDays + 1).ToList();
            Temp[n][0] = student.name;
            foreach (var grade in student.grades.Where(d => d.time.Value.Month == month && d.time.Value.Year == year))
            {
                Temp[n][header.IndexOf(grade.time.Value.Day.ToString())] = grade.grade.ToString();
            }
            n++;
        }
        MainDataGrid.ItemsSource = Temp;
        for (int j = 0; j < numDays + 1; j++)
        {
            MainDataGrid.Columns.Add(new DataGridTextColumn() { Binding = new Binding($"[{j}]"), IsReadOnly = j == 0, Header = header[j] });
        }
    }

    private void Next(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (CurMonth < 12)
        {
            CurMonth++;
        }
        else
        {
            CurMonth = 1;
            CurYear++;
        }
        FillGrid(CurMonth, CurYear);
    }
    private void Previous(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (CurMonth > 1)
        {
            CurMonth--;
        }
        else
        {
            CurMonth = 12;
            CurYear--;
        }
        FillGrid(CurMonth, CurYear);
    }

    private void Subject_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        SelectedSubject = SelectSubject.SelectedIndex;
        SchoolStuff.Fill(SelectedClass, SelectedSubject);
        FillGrid(CurMonth, CurYear);
    }
    private void Class_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        SelectedClass = SelectClass.SelectedIndex;
        SchoolStuff.Fill(SelectedClass, SelectedSubject);
        FillGrid(CurMonth, CurYear);
    }

    private void DataGrid_BeginningEdit(object? sender, Avalonia.Controls.DataGridBeginningEditEventArgs e)
    {

    }

    private void DataGrid_CellEditEnded(object? sender, Avalonia.Controls.DataGridCellEditEndedEventArgs e)
    {
        var r = ((List<string>)MainDataGrid.SelectedItem);
        int a = MainDataGrid.SelectedIndex;
        int b = MainDataGrid.CurrentColumn.DisplayIndex;
        MonthName.Text = $"AAAAAAAAA не трогай {a}, {b} а предмет {r}";

        SchoolStuff.AddGrade(SchoolStuff.students.FirstOrDefault(s => s.Name == r[0] &&
        s.Class == SchoolStuff.Class_List[SelectedClass] &&
        s.Subjects.Where(sub => sub.Name == SchoolStuff.Subjects_List[SelectedSubject]).Count() != 0).FindMyId, SelectedSubject, Int32.Parse(r[b]), new DateTime(CurYear, CurMonth, b));

    }
}