using Avalonia.Controls;
using Avalonia.Interactivity;

namespace up;

public partial class MainMenuWindow : Window
{
    public MainMenuWindow()
    {
        InitializeComponent();
    }

    private void CustomerOrders_Click(object sender, RoutedEventArgs e)
    {
        var customerOrdersWindow = new CustomerOrdersWindow();
        customerOrdersWindow.Show();
        this.Close();
    }
}