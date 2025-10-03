using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Configuration;
using MiddelbyReolmarked.Repositories.DbRepos;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.ViewModels;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            // Repositories:
            var customerRepository = new DbCustomerRepository(connectionString);
            var rackRepository = new DbRackRepository(connectionString);
            var rentalAgreementRepository = new DbRentalAgreementRepository(connectionString);

            // ViewModels:
            var customerViewModelFactory = new CustomerViewModelFactory(customerRepository);
            var customerListViewModel = new CustomerListViewModel(customerRepository);
            var rackListViewModel = new RackListViewModel(rackRepository);
            var availableRackListViewModel = new RackListViewModel(rackRepository);
            var rentalAgreementViewModel = new RentalAgreementViewModel(rentalAgreementRepository, rackRepository, customerRepository);
            var mainWindowModel = new MainViewModel(
                rentalAgreementViewModel, 
                rackListViewModel, 
                availableRackListViewModel, 
                customerListViewModel, 
                customerViewModelFactory
            );

            //RackInitializer.CreateDefaultRacks(rackRepository);

            var mainWindow = new MainWindow(mainWindowModel);
            mainWindow.Show();

        }
    }

}
