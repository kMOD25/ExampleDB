using System.Collections.Generic;
using System.Windows.Input;

namespace TabItem.ViewModelInterfaces
{
    public interface IQuestionsVM : ISelectedLevel
    {
        IEnumerable<IQuestionVM> Questions { get; }
        ICommand GetResultCommand { get; }
    }

}
