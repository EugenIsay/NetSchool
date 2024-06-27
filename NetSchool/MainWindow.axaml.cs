using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using NetSchool.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NetSchool;

public partial class MainWindow : Window
{
    int CurMonth = 5;
    int CurYear = 2024;
    int Last = 0;
    int SelectedSubject = 0;
    int SelectedClass = 0;
    List<string> Subjects = new List<string>();
    List<List<string>> Table;
    public MainWindow()
    {
        InitializeComponent();
        Name.Text = UserManager.curentUser.Name;
        if (UserManager.curentUser.Role == "teacher")
        {
            Subjects = SchoolStuff.teachers.FirstOrDefault(t => t.Name == UserManager.curentUser.Name).Subjects;
        }
        else
        {
            Subjects = SchoolStuff.Subjects_List.ToList();
        }
        SelectSubject.ItemsSource = Subjects.ToList();
        SelectClass.ItemsSource = SchoolStuff.Class_List.ToList();
        int SelectedSubject = 0;
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
        header.Add("���");
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
        Table = new List<List<string>>(Temp);
        MainDataGrid.ItemsSource = Temp;
        MainDataGrid.Columns.Add(new DataGridTextColumn() { Binding = new Binding($"[{0}]"), IsReadOnly = true, Header = header[0] });
        for (int j = 1; j < numDays + 1; j++)
        {
            if ((int)(new DateTime(year, month, j).DayOfWeek) != 0)
            {
                MainDataGrid.Columns.Add(new DataGridTextColumn() { Binding = new Binding($"[{j}]"), IsReadOnly = false, Header = header[j] });
            }

        }
    }

    private void Next(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (CurMonth < 12 && CurMonth != 5)
        {
            CurMonth++;
        }
        else if (CurMonth == 5)
        {
            CurMonth = 9;
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
        if (CurMonth > 1 && CurMonth != 9)
        {
            CurMonth--;
        }
        else if (CurMonth == 9)
        {
            CurMonth = 5;
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
        SelectedSubject = Array.IndexOf( SchoolStuff.Subjects_List, SelectSubject.SelectedItem );
        if (SelectedClass != -1 && SelectedSubject != -1)
        {
            SchoolStuff.Fill(SelectedClass, SelectedSubject);
            FillGrid(CurMonth, CurYear);
        }
    }
    private void Class_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        SelectedClass = SelectClass.SelectedIndex;
        if (SelectedClass != -1 && SelectedSubject != -1)
        {
            SchoolStuff.Fill(SelectedClass, SelectedSubject);
            FillGrid(CurMonth, CurYear);
        }
    }
    private void DataGrid_CellEditEnded(object? sender, Avalonia.Controls.DataGridCellEditEndedEventArgs e)
    {
        var r = (List<string>)MainDataGrid.SelectedItem;
        int a = MainDataGrid.SelectedIndex;
        int b = Int32.Parse(MainDataGrid.CurrentColumn.Header.ToString());
        if (Last != 0)
        {
            var f = SchoolStuff.students.FirstOrDefault(s => s.Name == r[0] && s.Class == SchoolStuff.Class_List[SelectedClass])
                .Subjects.FirstOrDefault(s => s.Name == SchoolStuff.Subjects_List[SelectedSubject]).SubjectGrades;
            Grade grade = f.FirstOrDefault(g => g.grade == Last && g.time == new DateTime(CurYear, CurMonth, b));
            int ind = f.IndexOf(grade);
            SchoolStuff.students.FirstOrDefault(s => s.Name == r[0] && s.Class == SchoolStuff.Class_List[SelectedClass])
                .Subjects.FirstOrDefault(s => s.Name == SchoolStuff.Subjects_List[SelectedSubject]).SubjectGrades.RemoveAt(ind);
        }
        try
        {
            if (Enumerable.Range(1, 5).Contains(Int32.Parse(r[b])))
            {
                SchoolStuff.AddGrade(SchoolStuff.students.FirstOrDefault(s => s.Name == r[0] &&
                s.Class == SchoolStuff.Class_List[SelectedClass] &&
                s.Subjects.Where(sub => sub.Name == SchoolStuff.Subjects_List[SelectedSubject]).Count() != 0).FindMyId, SelectedSubject, Int32.Parse(r[b]), new DateTime(CurYear, CurMonth, b));

            }

        }
        catch (Exception ex)
        {
        }
        Last = 0;
        SchoolStuff.Fill(SelectedClass, SelectedSubject);
        FillGrid(CurMonth, CurYear);
    }

    private async void To_Add_Window(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new SecondWindow().Show();
        this.Close();
    }

    private void Button_Exit(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        UserManager.curentUser = null;
        new LoginWindow().Show();
        this.Close();
    }

    private void DataGrid_BeginningEdit(object? sender, Avalonia.Controls.DataGridBeginningEditEventArgs e)
    {
        var r = (List<string>)MainDataGrid.SelectedItem;
        int a = MainDataGrid.SelectedIndex;
        int b = Int32.Parse(MainDataGrid.CurrentColumn.Header.ToString());
        if (Table[a][b] != "")
        {
            Last = Int32.Parse(Table[a][b]);
        }
    }
    private async void To_Result(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        await new Results(SelectedClass, SelectedSubject, new DateTime(CurYear, CurMonth, 1)).ShowDialog(this);
    }
}