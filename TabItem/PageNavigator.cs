using Simplified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabItem.ViewModelInterfaces;

namespace TabItem
{
    public class PageNavigator : BaseInpc
    {
        private readonly Dictionary<Type, (Type type, string title)> titles = new Dictionary<Type, (Type type, string title)>()
        {
            {typeof(ILevelsVM), (typeof(IQuestionsVM), "Начать тест")},
            {typeof(IQuestionsVM), (typeof(IResultVM), "Получить результат")},
            {typeof(IResultVM), (typeof(ILevelsVM), "Начать тест заново")},
        };
        private Type _typeCurrentPage;
        private ICommand _navigatorCommand;
        private ITestVM _testVM;
        private string _titleNavigatorButton;

        public ITestVM TestVM { get => _testVM; set => Set(ref _testVM, value); }

        public Type TypeCurrentPage { get => _typeCurrentPage; set => Set(ref _typeCurrentPage, value); }

        public string TitleNavigatorButton { get => _titleNavigatorButton; set => Set(ref _titleNavigatorButton, value); }

        public ICommand NavigatorCommand => _navigatorCommand
            ?? (_navigatorCommand = new RelayCommand(NavigatorExecute, NavigatorCanExecute));

        private bool NavigatorCanExecute()
        {
            if (TestVM == null)
                return false;

            if (TypeCurrentPage == typeof(ILevelsVM))
                return TestVM.GetQuestionsCommand.CanExecute(null);
            if (TypeCurrentPage == typeof(IQuestionsVM))
                return TestVM.GetResultCommand.CanExecute(null);
            if (TypeCurrentPage == typeof(IResultVM))
                return TestVM.StartSelectLevelCommand.CanExecute(null);

            throw new Exception("Недопустимый тип.");
        }

        private void NavigatorExecute()
        {
            if (TestVM == null)
                return;

            if (TypeCurrentPage == typeof(ILevelsVM))
            {
                if (TestVM.GetQuestionsCommand.TryExecute(null))
                    TypeCurrentPage = typeof(IQuestionsVM);
            }

            else if (TypeCurrentPage == typeof(IQuestionsVM))
            {
                if (TestVM.GetResultCommand.TryExecute(null))
                    TypeCurrentPage = typeof(IResultVM);
            }

            else if (TypeCurrentPage == typeof(IResultVM))
            {
                if (TestVM.StartSelectLevelCommand.TryExecute(null))
                    TypeCurrentPage = typeof(ILevelsVM);
            }

            throw new Exception("Недопустимый тип.");
        }

        protected override void OnPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnPropertyChanged(propertyName, oldValue, newValue);

            if (propertyName == nameof(TypeCurrentPage))
                TitleNavigatorButton = titles[TypeCurrentPage].title;
        }
    }
}
