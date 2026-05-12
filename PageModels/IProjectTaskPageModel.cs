using CommunityToolkit.Mvvm.Input;
using KeyGenerator.Models;

namespace KeyGenerator.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}