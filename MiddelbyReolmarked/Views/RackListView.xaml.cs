using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.Configuration;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.DbRepos;
using MiddelbyReolmarked.ViewModels;

namespace MiddelbyReolmarked.Views
{
    /// <summary>
    /// Interaction logic for RackListView.xaml
    /// </summary>
    public partial class RackListView : UserControl
    {
        public RackListView()
        {
            InitializeComponent();
        }


        public void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is not ListView listView)
                return;
            if (listView.SelectedItem is not Rack rack)
                return;

            var viewModel = (RackListViewModel)DataContext;
            viewModel.SelectRack(rack);
        }
    }
}
