using System;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.DbRepos;
using MiddelbyReolmarked.ViewModels;

namespace MiddelbyReolmarked.Views
{
    /// <summary>
    /// Interaction logic for CustomerListView.xaml
    /// </summary>
    public partial class CustomerListView : UserControl
    {
        public CustomerListView()
        {
            InitializeComponent();
        }

        public void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(sender is not ListView listView)
                return;
            if (listView.SelectedItem is not Customer customer)
                return;

            var viewModel = (CustomerListViewModel)DataContext;
            viewModel.SelectCustomer(customer);
        }
    }
}