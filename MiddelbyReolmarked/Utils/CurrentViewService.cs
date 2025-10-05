using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.Utils;

public class CurrentViewService
{
    public Action<BaseViewModel> OnViewChanged { get; set; } = (_) => { };
}
