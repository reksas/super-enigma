using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace up;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

   private void LoginButton_Click(object sender, RoutedEventArgs e)
{
    var roleComboBox = this.FindControl<ComboBox>("RoleComboBox");
    var selectedItem = roleComboBox.SelectedItem as ComboBoxItem;
    var role = selectedItem?.Content?.ToString();
    
    var login = this.FindControl<TextBox>("LoginTextBox").Text;
    var password = this.FindControl<TextBox>("PasswordTextBox").Text;

    if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
    {
        Console.WriteLine("Заполните все поля");
        return;
    }

    if (role == "Администратор")
    {
        if (login == "admin" && password == "123")
        {
            var mainMenu = new MainMenuWindow();
            mainMenu.Show();
            this.Close();
        }
        else
        {
            Console.WriteLine("Неверный логин или пароль администратора");
        }
    }
    else if (role == "Сотрудник")
    {
        if (login == "employee" && password == "456")
        {
            var mainMenu = new MainMenuWindow();
            mainMenu.Show();
            this.Close();
        }
        else
        {
            Console.WriteLine("Неверный логин или пароль сотрудника");
        }
    }
}
}