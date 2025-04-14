using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using upp.Entities;

namespace up
{
    public partial class AddCustomerOrderWindow : Window
    {
        public CustomerOrder? Result { get; private set; }

        public AddCustomerOrderWindow()
        {
            InitializeComponent();
            SetupInitialValues();
        }

        private void SetupInitialValues()
        {
            // Установка значений по умолчанию
            StatusComboBox.ItemsSource = new string[] { "New", "Processing", "Cancelled" };
            StatusComboBox.SelectedIndex = 0;
            OrderDatePicker.SelectedDate = DateTimeOffset.Now;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                Result = new CustomerOrder
                {
                    CustomerName = CustomerNameTextBox.Text,
                    ContactPhone = ContactPhoneTextBox.Text,
                    ContactEmail = ContactEmailTextBox.Text,
                    Status = StatusComboBox.SelectedItem as string ?? "Новый",
                    OrderDate = DateOnly.FromDateTime(OrderDatePicker.SelectedDate!.Value.DateTime),
                    DeliveryDate = DeliveryDatePicker.SelectedDate.HasValue 
                        ? DateOnly.FromDateTime(DeliveryDatePicker.SelectedDate.Value.DateTime)
                        : null
                };

                Close(true);
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = $"Ошибка при создании заказа: {ex.Message}";
            }
        }

        private bool ValidateInput()
        {
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(CustomerNameTextBox.Text))
            {
                ErrorTextBlock.Text = "Введите имя клиента";
                return false;
            }

            if (string.IsNullOrWhiteSpace(ContactPhoneTextBox.Text))
            {
                ErrorTextBlock.Text = "Введите телефон клиента";
                return false;
            }

            if (!OrderDatePicker.SelectedDate.HasValue)
            {
                ErrorTextBlock.Text = "Выберите дату заказа";
                return false;
            }

            ErrorTextBlock.Text = "";
            return true;
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            Close(false);
        }
    }
}