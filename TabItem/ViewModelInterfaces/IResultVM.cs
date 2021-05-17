using System.Windows.Input;

namespace TabItem.ViewModelInterfaces
{
    public interface IResultVM : ISelectedLevel
    {
       int TotalCount { get; }
        int RightCount { get; }
       ICommand StartSelectLevelCommand { get; }
    }

}
