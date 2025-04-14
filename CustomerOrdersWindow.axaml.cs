using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using upp.Entities;

namespace up
{
    public partial class CustomerOrdersWindow : Window
    {
        private readonly PostgresContext _context = new PostgresContext();

        public CustomerOrdersWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            new MainMenuWindow().Show();
            this.Close();
        }

        private void LoadData()
        {
            try
            {
                var orders = _context.CustomerOrders
                    .Include(o => o.OrderItems)
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();
                
                dataGridOrders.ItemsSource = orders;
                StatusTextBlock.Text = $"Загружено заказов: {orders.Count}";
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Ошибка загрузки данных: {ex.Message}";
            }
        }

        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            var searchText = SearchTextBox.Text?.ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadData();
                return;
            }

            try
            {
                var filtered = _context.CustomerOrders
                    .Include(o => o.OrderItems)
                    .Where(o => 
                        o.CustomerName.ToLower().Contains(searchText) ||
                        o.Status.ToLower().Contains(searchText) ||
                        (o.ContactEmail != null && o.ContactEmail.ToLower().Contains(searchText)) ||
                        o.ContactPhone.ToLower().Contains(searchText))
                    .ToList();

                dataGridOrders.ItemsSource = filtered;
                StatusTextBlock.Text = $"Найдено заказов: {filtered.Count}";
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Ошибка поиска: {ex.Message}";
            }
        }


        private async void OnAddRecordClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new AddCustomerOrderWindow();
                var result = await dialog.ShowDialog<bool>(this);

                if (result && dialog.Result != null)
                {
                    _context.CustomerOrders.Add(dialog.Result);
                    int affected = _context.SaveChanges();
                    
                    if (affected > 0)
                    {
                        LoadData();
                        StatusTextBlock.Text = "Заказ успешно добавлен!";
                    }
                    else
                    {
                        StatusTextBlock.Text = "Не удалось добавить заказ";
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                StatusTextBlock.Text = $"Ошибка БД: {dbEx.InnerException?.Message ?? dbEx.Message}";
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void OnDeleteRecordClick(object sender, RoutedEventArgs e)
        {
            if (dataGridOrders.SelectedItem is not CustomerOrder selected)
            {
                StatusTextBlock.Text = "Выберите заказ для удаления";
                return;
            }

            try
            {
                _context.CustomerOrders.Remove(selected);
                _context.SaveChanges();
                LoadData();
                StatusTextBlock.Text = "Заказ успешно удален";
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Ошибка удаления: {ex.Message}";
            }
        }

        private void OnRefreshDataClick(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}