using System.Collections.Generic;
using System.Windows.Input;

namespace TabItem.ViewModelInterfaces
{
    public interface ILevelsVM : ISelectedLevel
    {
        IEnumerable<ILevelVM> Levels { get; }

        ICommand GetQuestionsCommand { get; }
    }
}
