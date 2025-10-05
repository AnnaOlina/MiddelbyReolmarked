using System.Collections.ObjectModel;
using System.Linq;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.Utils;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels;

public class RackListViewModel : BaseViewModel
{
    private readonly IRackRepository _rackRepository;
    private readonly ViewModelFactory _viewModelFactory;
    private readonly CurrentViewService _currentViewService;
    private ObservableCollection<Rack> _racks;

    public ObservableCollection<Rack> Racks
    {
        get { return _racks; }
        set
        {
            if (_racks != value)
            {
                _racks = value;
                OnPropertyChanged(nameof(Racks));
            }
        }
    }

    public RackListViewModel(
        IRackRepository rackRepository,
        ViewModelFactory viewModelFactory,
        CurrentViewService currentViewService
        )
    {
        _rackRepository = rackRepository;
        _viewModelFactory = viewModelFactory;
        _currentViewService = currentViewService;
        _racks = new ObservableCollection<Rack>();
        LoadRacks();
    }

    // Hent alle reoler fra repository
    public void LoadRacks()
    {
        Racks.Clear();
        var racksFromRepo = _rackRepository.GetAllRacks();
        foreach (var rack in racksFromRepo)
        {
            Racks.Add(rack);
        }
    }

    // Filtrer ledige reoler
    public ObservableCollection<Rack> GetAvailableRacks()
    {
        var availableIds = _rackRepository.ListAvailableRackIds();
        var availableRacks = new ObservableCollection<Rack>();

        foreach (var rack in Racks)
        {
            if (availableIds.Contains(rack.RackId))
            {
                availableRacks.Add(rack);
            }
        }
        return availableRacks;
    }

    public void SelectRack(Rack rack)
    {
        var rackViewModel = _viewModelFactory.CreateRackViewModel(rack);
        _currentViewService.OnViewChanged?.Invoke(rackViewModel);
    }
}