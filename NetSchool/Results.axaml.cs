using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSchool;

public partial class Results : Window
{
    public Results()
    {
        InitializeComponent();
    }
    public Results(int cur_class, int subject, DateTime date)
    {
        InitializeComponent();

        SchoolStuff.Fill(cur_class, subject);
        if (date.Month < 6)
        {
            FillGrid(date.Month, date.Year);
        }
        else
        {
            FillGrid(date.Month, date.Year + 1);
        }
    }
    public void FillGrid(int month, int year)
    {
        MainDataGrid.Columns.Clear();
        List<List<string>> Temp = new List<List<string>>();
        List<string> header = new List<string>();
        header.Add("Имя");
        int numDays = DateTime.DaysInMonth(year, month);
        for (int i = 1; i < 5; i++)
        {
            header.Add($"{i} четверть");
        }
        header.Add("Итог");
        int n = 0;
        foreach (var student in SchoolStuff.ShownStudents)
        {
            Temp.Add(new List<string>());
            Temp[n] = Enumerable.Repeat("", 6).ToList();
            Temp[n][0] = student.name;
            try
            {
                Temp[n][1] = student.grades.Where(d => (d.time.Value.Month == 9 || d.time.Value.Month == 10) && d.time.Value.Year == year - 1).Select(g => g.grade).AsQueryable().Average().ToString();
            }
            catch { }
            try
            {
                Temp[n][2] = student.grades.Where(d => (d.time.Value.Month == 11 || d.time.Value.Month == 12) && d.time.Value.Year == year - 1).Select(g => g.grade).AsQueryable().Average().ToString();
            }
            catch { }
            try
            {
                Temp[n][3] = student.grades.Where(d => (d.time.Value.Month == 1 || d.time.Value.Month == 2 || d.time.Value.Month == 3) && d.time.Value.Year == year).Select(g => g.grade).AsQueryable().Average().ToString();
            }
            catch { }
            try
            {
                Temp[n][4] = student.grades.Where(d => (d.time.Value.Month == 4 || d.time.Value.Month == 5) && d.time.Value.Year == year).Select(g => g.grade).AsQueryable().Average().ToString();
            }
            catch { }
            try
            {
                Temp[n][5] = student.grades.Where(d => ((Enumerable.Range(9, 12).Contains(d.time.Value.Month)) && d.time.Value.Year == year - 1) || ((Enumerable.Range(1, 5).Contains(d.time.Value.Month)) && d.time.Value.Year == year)).Select(g => g.grade).AsQueryable().Average().ToString();
            }
            catch { }
            n++;
        }

        MainDataGrid.ItemsSource = Temp;
        MainDataGrid.Columns.Add(new DataGridTextColumn() { Binding = new Binding($"[{0}]"), IsReadOnly = true, Header = header[0] });
        for (int j = 1; j < 6; j++)
        {
            MainDataGrid.Columns.Add(new DataGridTextColumn() { Binding = new Binding($"[{j}]"), IsReadOnly = false, Header = header[j] });
        }
    }
}