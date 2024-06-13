using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Linq;

namespace NetSchool;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainDataGrid.ItemsSource = SchoolStuff.ShownStudents;
    }
}