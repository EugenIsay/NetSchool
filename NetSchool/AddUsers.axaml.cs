using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace NetSchool;

public partial class AddUsers : Window
{
    public AddUsers()
    {
        InitializeComponent();
        List<GetSubg> Lst = SchoolStuff.Subjects_List.Select(s => new GetSubg { Name = s, Add = false }).ToList();
        List<GetSubg> Lst2 = new List<GetSubg>(Lst);
        StudSubj.ItemsSource = Lst.ToList();
        TecherSubjects.ItemsSource = Lst2.ToList();
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
        if (StudName.Text != string.Empty && classNum.SelectedIndex != -1 && classLetter.SelectedIndex != -1 && StudSubj.Items.Cast<GetSubg>().ToList().Select(s => s.Add).Contains(true))
        {
            SchoolStuff.AddStudent(StudName.Text, $"{classNum.SelectedItem}{classLetter.SelectedItem}");
            foreach (GetSubg subj in StudSubj.Items)
            {
                if (subj.Add)
                {
                    SchoolStuff.AddToSubject(SchoolStuff.students.FirstOrDefault(s => s.Name == StudName.Text && s.Class == $"{classNum.SelectedItem}{classLetter.SelectedItem}").FindMyId, Array.IndexOf(SchoolStuff.Subjects_List.ToArray(), subj.Name));
                }
            }
            Good();
        } 
        else
        {
            Error();
        }
    }
    private void AddTeach(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (TeachName.Text != string.Empty && TeachPass.Text != string.Empty && TecherSubjects.Items.Cast<GetSubg>().ToList().Select(s => s.Add).Contains(true))
        {
            List<string> sub = new List<string>();
            foreach (GetSubg subj in TecherSubjects.Items)
            {
                if (subj.Add)
                {
                    sub.Add(subj.Name);
                }
            }
            SchoolStuff.AddTeacher(TeachName.Text, TeachPass.Text, sub);
            Good();
        }
        else
        {
            Error();
        }

    }
    public async void Error()
    {
        Massage.IsVisible = true;
        Massage.Text = "Что-то пошло не так";
        await Task.Delay(500);
        Massage.IsVisible = false;
    }
    public async void Good()
    {
        Massage.IsVisible = true;
        Massage.Foreground = Brushes.Black;
        Massage.Text = "Успешно добавлен";
        await Task.Delay(500);
        Massage.IsVisible = false;
        Massage.Foreground = Brushes.Red;
    }
}
public class GetSubg
{
    public string Name { get; set; }
    public bool Add { get; set; }
}