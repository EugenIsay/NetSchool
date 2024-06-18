using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using System.Linq;

namespace NetSchool;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainDataGrid.ItemsSource = SchoolStuff.ShownStudents;
        var a = SchoolStuff.students;
        foreach (var time in SchoolStuff.GetDates())
        {
            var column = new DataGridTextColumn();
            foreach (var item in SchoolStuff.ShownStudents)
            {
                column.Header = item.grades[0].time.ToString();
            }
            MainDataGrid.Columns.Add(column);
        }

    }
}