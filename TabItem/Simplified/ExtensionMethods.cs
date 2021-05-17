using System.Windows.Input;
namespace Simplified
{
    /// <summary>Класс с методами расширения.</summary>
    public static class ExtensionMethods
    {
        /// <summary>Выполняет команду, если её выполнение разрешено.</summary>
        /// <param name="command">Команда.</param>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns><see langword="true"/> если команда была выполнена.</returns>
        public static bool TryExecute(this ICommand command, object parameter)
        {
            if (!command.CanExecute(parameter))
                return false;
            command.Execute(parameter);
            return true;
        }


        /// <summary>Выполняет команду с праметром <see langword="null"/>, если её выполнение разрешено.</summary>
        /// <param name="command">Команда.</param>
        /// <returns><see langword="true"/> если команда была выполнена.</returns>
        public static bool TryExecute(this ICommand command)
                  => command.TryExecute(null);

    }
}
