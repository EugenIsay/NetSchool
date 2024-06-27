using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using NetSchool.Models;
using System.Collections.Generic;
using System.Linq;

namespace NetSchool;

public partial class SecondWindow : Window
{
    public SecondWindow()
    {
        InitializeComponent();

        StudList.ItemsSource = SchoolStuff.students.ToList();
        TeachList.ItemsSource = SchoolStuff.students.ToList();
    }
    private async void Add_User(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        await new AddUsers().ShowDialog(this);
        StudList.ItemsSource = SchoolStuff.students.ToList();
        TeachList.ItemsSource = SchoolStuff.students.ToList();
    }
    private async void Back_Main(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }
}