using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Configuration;
using MiddelbyReolmarked.Repositories.DbRepos;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.Utils;
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

            var currentViewService = new CurrentViewService();

            // Repositories:
            var customerRepository = new DbCustomerRepository(connectionString);
            var rackRepository = new DbRackRepository(connectionString);
            var rentalAgreementRepository = new DbRentalAgreementRepository(connectionString);
            var rentalAgreementRackRepository = new DbRentalAgreementRackRepository(connectionString);

            // ViewModels:
            var viewModelFactory = new ViewModelFactory(customerRepository, rackRepository, rentalAgreementRepository);
            var customerListViewModel = new CustomerListViewModel(customerRepository, viewModelFactory, currentViewService);
            var rackListViewModel = new RackListViewModel(rackRepository, viewModelFactory, currentViewService);
            var availableRackListViewModel = new RackListViewModel(rackRepository, viewModelFactory, currentViewService);
            var rentalAgreementListViewModel = new RentalAgreementListViewModel(rentalAgreementRepository, rackRepository, customerRepository, rentalAgreementRackRepository, viewModelFactory, currentViewService);
            var mainWindowModel = new MainViewModel(
                rentalAgreementListViewModel,
                rackListViewModel,
                availableRackListViewModel,
                customerListViewModel,
                viewModelFactory,
                currentViewService
            );

            //RackInitializer.CreateDefaultRacks(rackRepository);

            var mainWindow = new MainWindow(mainWindowModel);
            mainWindow.Show();

        }
    }

}
