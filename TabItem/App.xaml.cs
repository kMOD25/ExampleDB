using Entitys.Model;
using System.Windows;

namespace TabItem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            PageNavigator pageNavigator = (PageNavigator)Resources["pageNavigator"];

            pageNavigator.TestVM = new TestViewModel(new TestModel());
        }
    }
}
