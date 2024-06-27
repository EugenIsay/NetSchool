using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media.Imaging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetSchool;

public partial class LoginWindow : Window
{
    Random rnd = new Random();
    public LoginWindow()
    {
        InitializeComponent();
        if (UserManager.Users.Count == 0)
        {
            UserManager.AddUser("admin", "admin", "admin");
        }
    }
    public void Comfirm(object? sender, RoutedEventArgs e)
    {
        if (UserManager.CheckUser(Name.Text, Password.Text))
        {
            new MainWindow().Show();
            this.Close();
        }

    }
}