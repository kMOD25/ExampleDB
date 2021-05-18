using Simplified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TabItem.ViewModelInterfaces;

namespace TabItem
{
    public class PageNavigator : BaseInpc
    {
        #region Поля для хранения значений свойств
        private Type _typeCurrentPage;
        private ICommand _navigatorCommand;
        private ITestVM _testVM;
        private string _titleNavigatorButton;
        #endregion

        /// <summary>ViewModel Окна.</summary>
        public ITestVM TestVM { get => _testVM; set => Set(ref _testVM, value); }

        /// <summary>Тип текущей страницы навигатора.</summary>
        public Type TypeCurrentPage { get => _typeCurrentPage; set => Set(ref _typeCurrentPage, value); }

        /// <summary>Название кнопки навигации на текущей странице.</summary>
        public string TitleNavigatorButton { get => _titleNavigatorButton; set => Set(ref _titleNavigatorButton, value); }

        // Словарь данных. Содержит для теущего типа: следующий тип,
        // текст кнопки навигации, команду VM выполняемую перед навигацией.
        private readonly Dictionary<Type, (Type type, string title, Func<ICommand> command)> datas;

        /// <summary>Создаёт экземпляр навигатора.</summary>
        public PageNavigator()
        {
            datas = new Dictionary<Type, (Type type, string title, Func<ICommand> command)>()
            {
                {typeof(ILevelsVM), (typeof(IQuestionsVM), "Начать тест", () => TestVM?.GetQuestionsCommand)},
                {typeof(IQuestionsVM), (typeof(IResultVM), "Получить результат", () => TestVM?.GetResultCommand)},
                {typeof(IResultVM), (typeof(ILevelsVM), "Начать тест заново", () => TestVM?.StartSelectLevelCommand)},
            };
            TypeCurrentPage = datas.Keys.First();
        }

        /// <summary>Команда кнопки навигации.</summary>
        public ICommand NavigatorCommand => _navigatorCommand ??= new RelayCommand(NavigatorExecute, NavigatorCanExecute);

        /// <summary>Метод состояния команды.</summary>
        /// <returns><see langword="true"/> если выполнение команды допустимо.</returns>
        private bool NavigatorCanExecute()
        {
            if (!datas.TryGetValue(TypeCurrentPage, out (Type type, string title, Func<ICommand> command) data))
                throw new Exception("Недопустимый тип.");

            var command = data.command();

            if (command == null)
                return false;

            return command.CanExecute(null);
        }

        /// <summary>Метод исполнения команды.</summary>
        private void NavigatorExecute()
        {

            if (!datas.TryGetValue(TypeCurrentPage, out (Type type, string title, Func<ICommand> command) data))
                throw new Exception("Недопустимый тип.");

            var command = data.command();

            if (command == null)
                return;

            if (command.TryExecute())
                TypeCurrentPage = datas[TypeCurrentPage].type;

        }

        // Метод вызываемый при изменеии свойств методм Set.
        // Используется для задания зависимостей свойств.
        protected override void OnPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnPropertyChanged(propertyName, oldValue, newValue);

            // Задание зависимости свойства TitleNavigatorButton от свойства TypeCurrentPage
            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(TypeCurrentPage))
                TitleNavigatorButton = datas[TypeCurrentPage].title;
        }
    }
}
